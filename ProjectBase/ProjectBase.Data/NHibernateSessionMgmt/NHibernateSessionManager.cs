using System;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Security;
using NHibernate;
using NHibernate.Cache;
using NHibernate.Cfg;
using ProjectBase.Utils;

namespace ProjectBase.Data
{
    /// <summary>
    /// Handles creation and management of sessions and transactions.  It is a singleton because 
    /// building the initial session factory is very expensive. Inspiration for this class came 
    /// from Chapter 8 of Hibernate in Action by Bauer and King.  Although it is a sealed singleton
    /// you can use TypeMock (http://www.typemock.com) for more flexible testing.
    /// </summary>
    public sealed class NHibernateSessionManager
    {
        #region Thread-safe, lazy Singleton

        /// <summary>
        /// This is a thread-safe, lazy singleton.  See http://www.yoda.arachsys.com/csharp/singleton.html
        /// for more details about its implementation.
        /// </summary>
        public static NHibernateSessionManager Instance {
            get {
                return Nested.NHibernateSessionManager;
            }
        }

        /// <summary>
        /// Private constructor to enforce singleton
        /// </summary>
        private NHibernateSessionManager() { }

        /// <summary>
        /// Assists with ensuring thread-safe, lazy singleton
        /// </summary>
        private class Nested
        {
            static Nested() { }
            internal static readonly NHibernateSessionManager NHibernateSessionManager =
                new NHibernateSessionManager();
        }

        #endregion

        /// <summary>
        /// This method attempts to find a session factory stored in <see cref="sessionFactories" />
        /// via its name; if it can't be found it creates a new one and adds it the hashtable.
        /// </summary>
        /// <param name="sessionFactoryConfigPath">Path location of the factory config</param>
        /// <remarks>If the path is not rooted, it will use the current library folder except for web projects where it will use the <see cref="HttpContext">HttpContext.Current</see> to determine project location.</remarks>
        private ISessionFactory GetSessionFactoryFor(string sessionFactoryConfigPath) {
            Check.Require(!string.IsNullOrEmpty(sessionFactoryConfigPath),
                "sessionFactoryConfigPath may not be null nor empty");

            //  Attempt to retrieve a stored SessionFactory from the hashtable.
            ISessionFactory sessionFactory = (ISessionFactory) sessionFactories[sessionFactoryConfigPath];

            //  Failed to find a matching SessionFactory so make a new one.
            if (sessionFactory == null) {
				string internalSessionFactoryConfigPath = sessionFactoryConfigPath;
				if (!Path.IsPathRooted(sessionFactoryConfigPath))
					if (IsInWebContext())
						internalSessionFactoryConfigPath = HttpContext.Current.Server.MapPath( sessionFactoryConfigPath );
					else
						internalSessionFactoryConfigPath = Path.Combine( Path.GetDirectoryName(Assembly.GetCallingAssembly( ).Location) , sessionFactoryConfigPath );

				Check.Require( File.Exists( internalSessionFactoryConfigPath ) ,
					"The config file at '" + internalSessionFactoryConfigPath + "' could not be found" );

                Configuration cfg = new Configuration();
				cfg.Configure( internalSessionFactoryConfigPath );

                //  Now that we have our Configuration object, create a new SessionFactory
                sessionFactory = cfg.BuildSessionFactory();

                if (sessionFactory == null) {
                    throw new InvalidOperationException("cfg.BuildSessionFactory() returned null.");
                }

				if( !sessionFactories.ContainsKey( sessionFactoryConfigPath ) )
					sessionFactories.Add(sessionFactoryConfigPath, sessionFactory);
				else
					sessionFactory = (ISessionFactory)sessionFactories[sessionFactoryConfigPath];
            }

            return sessionFactory;
        }

        /// <summary>
        /// Allows you to register an interceptor on a new session.  This may not be called if there is already
        /// an open session attached to the HttpContext.  If you have an interceptor to be used, modify
        /// the HttpModule to call this before calling BeginTransaction().
        /// </summary>
        public void RegisterInterceptorOn(string sessionFactoryConfigPath, IInterceptor interceptor) {
            ISession session = (ISession)ContextSessions[sessionFactoryConfigPath];

            if (session != null && session.IsOpen) {
                throw new CacheException("You cannot register an interceptor once a session has already been opened");
            }

            GetSessionFrom(sessionFactoryConfigPath, interceptor);
        }

        public IStatelessSession GetStatelessSessionFrom(string sessionFactoryConfigPath)
        {
            return GetStatelessSessionFrom(sessionFactoryConfigPath, null);
        }

        private IStatelessSession GetStatelessSessionFrom(string sessionFactoryConfigPath, IInterceptor interceptor)
        {
            IStatelessSession session = (IStatelessSession)ContextStatelessSessions[sessionFactoryConfigPath];

            if (session == null)
            {
                session = GetSessionFactoryFor(sessionFactoryConfigPath).OpenStatelessSession();
                ContextStatelessSessions[sessionFactoryConfigPath] = session;
            }

            Check.Ensure(session != null, "session was null");

            return session;
        }

        public ISession GetSessionFrom(string sessionFactoryConfigPath) {
            return GetSessionFrom(sessionFactoryConfigPath, null);
        }

        /// <summary>
        /// Gets a session with or without an interceptor.  This method is not called directly; instead,
        /// it gets invoked from other public methods.
        /// </summary>
        private ISession GetSessionFrom(string sessionFactoryConfigPath, IInterceptor interceptor) {
            ISession session = (ISession)ContextSessions[sessionFactoryConfigPath];

            if (session == null) {
                if (interceptor != null) {
                    session = GetSessionFactoryFor(sessionFactoryConfigPath).OpenSession(interceptor);
                }
                else {
                    session = GetSessionFactoryFor(sessionFactoryConfigPath).OpenSession();
                }

                ContextSessions[sessionFactoryConfigPath] = session;
            }

            Check.Ensure(session != null, "session was null");

            return session;
        }

        /// <summary>
        /// Flushes anything left in the session and closes the connection.
        /// </summary>
        public void CloseSessionOn(string sessionFactoryConfigPath) {
            ISession session = (ISession)ContextSessions[sessionFactoryConfigPath];

            if (session != null && session.IsOpen) {
                session.Flush();
                session.Close();
            }

            ContextSessions.Remove(sessionFactoryConfigPath);
        }

        public ITransaction BeginTransactionOn(string sessionFactoryConfigPath) {
            ITransaction transaction = (ITransaction)ContextTransactions[sessionFactoryConfigPath];

            if (transaction == null) {
                transaction = GetSessionFrom(sessionFactoryConfigPath).BeginTransaction();
                ContextTransactions.Add(sessionFactoryConfigPath, transaction);
            }

            return transaction;
        }

        public void CommitTransactionOn(string sessionFactoryConfigPath) {
            ITransaction transaction = (ITransaction)ContextTransactions[sessionFactoryConfigPath];

            try {
                if (HasOpenTransactionOn(sessionFactoryConfigPath)) {
                    transaction.Commit();
                    ContextTransactions.Remove(sessionFactoryConfigPath);
                }
            }
            catch (HibernateException) {
                RollbackTransactionOn(sessionFactoryConfigPath);
                throw;
            }
        }

        public bool HasOpenTransactionOn(string sessionFactoryConfigPath) {
            ITransaction transaction = (ITransaction)ContextTransactions[sessionFactoryConfigPath];

            return transaction != null && !transaction.WasCommitted && !transaction.WasRolledBack;
        }

        public void RollbackTransactionOn(string sessionFactoryConfigPath) {
            ITransaction transaction = (ITransaction)ContextTransactions[sessionFactoryConfigPath];

            try {
                if (HasOpenTransactionOn(sessionFactoryConfigPath)) {
                    transaction.Rollback();
                }

                ContextTransactions.Remove(sessionFactoryConfigPath);
            }
            finally {
                CloseSessionOn(sessionFactoryConfigPath);
            }
        }

        /// <summary>
        /// Since multiple databases may be in use, there may be one transaction per database 
        /// persisted at any one time.  The easiest way to store them is via a hashtable
        /// with the key being tied to session factory.  If within a web context, this uses
        /// <see cref="HttpContext" /> instead of the WinForms specific <see cref="CallContext" />.  
        /// Discussion concerning this found at http://forum.springframework.net/showthread.php?t=572
        /// </summary>
        private Hashtable ContextTransactions {
            get {
                if (IsInWebContext()) {
                    if (HttpContext.Current.Items[TRANSACTION_KEY] == null)
                        HttpContext.Current.Items[TRANSACTION_KEY] = new Hashtable();

                    return (Hashtable)HttpContext.Current.Items[TRANSACTION_KEY];
                }
                else {
                    if (CallContext.GetData(TRANSACTION_KEY) == null)
                        CallContext.SetData(TRANSACTION_KEY, new Hashtable());

                    return (Hashtable)CallContext.GetData(TRANSACTION_KEY);
                }
            }
        }

        /// <summary>
        /// Since multiple databases may be in use, there may be one session per database 
        /// persisted at any one time.  The easiest way to store them is via a hashtable
        /// with the key being tied to session factory.  If within a web context, this uses
        /// <see cref="HttpContext" /> instead of the WinForms specific <see cref="CallContext" />.  
        /// Discussion concerning this found at http://forum.springframework.net/showthread.php?t=572
        /// </summary>
        private Hashtable ContextSessions {
            get {
                if (IsInWebContext()) {
                    if (HttpContext.Current.Items[SESSION_KEY] == null)
                        HttpContext.Current.Items[SESSION_KEY] = new Hashtable();

                    return (Hashtable)HttpContext.Current.Items[SESSION_KEY];
                }
                else {
                    if (CallContext.GetData(SESSION_KEY) == null)
                        CallContext.SetData(SESSION_KEY, new Hashtable());

                    return (Hashtable)CallContext.GetData(SESSION_KEY);
                }
            }
        }

        private Hashtable ContextStatelessSessions
        {
            get
            {
                if (IsInWebContext())
                {
                    if (HttpContext.Current.Items[SESSIONSTATELESS_KEY] == null)
                        HttpContext.Current.Items[SESSIONSTATELESS_KEY] = new Hashtable();

                    return (Hashtable)HttpContext.Current.Items[SESSIONSTATELESS_KEY];
                }
                else
                {
                    if (CallContext.GetData(SESSIONSTATELESS_KEY) == null)
                        CallContext.SetData(SESSIONSTATELESS_KEY, new Hashtable());

                    return (Hashtable)CallContext.GetData(SESSIONSTATELESS_KEY);
                }
            }
        }

        private bool IsInWebContext() {
            return HttpContext.Current != null;
        }

        private Hashtable sessionFactories = new Hashtable();
        private const string TRANSACTION_KEY = "CONTEXT_TRANSACTIONS";
        private const string SESSION_KEY = "CONTEXT_SESSIONS";
        private const string SESSIONSTATELESS_KEY = "CONTEXT_SESSIONS_STATELESS";
        
    }
}
