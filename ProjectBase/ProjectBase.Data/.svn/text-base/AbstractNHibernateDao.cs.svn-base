using System;
using System.Collections.Generic;
using System.Reflection;
using NHibernate;
using NHibernate.Criterion;
using ProjectBase.Utils;

namespace ProjectBase.Data
{
    public abstract class AbstractNHibernateDao<T, IdT> : IDao<T, IdT>
    {
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="sessionFactoryConfigPath">Fully qualified path of the session factory's config file</param>
		public AbstractNHibernateDao( string sessionFactoryConfigPath )
		{
            Check.Require(! string.IsNullOrEmpty(sessionFactoryConfigPath),
                "sessionFactoryConfigPath may not be null nor empty");

            SessionFactoryConfigPath = sessionFactoryConfigPath;
        }

        /// <summary>
        /// Loads an instance of type T from the DB based on its ID.
        /// </summary>
        public virtual T GetById(IdT id, bool shouldLock) {
            T entity;

            if (shouldLock) {
                entity = (T)NHibernateSession.Get(persitentType, id, LockMode.Upgrade);
            }
            else {
				entity = (T)NHibernateSession.Get( persitentType , id );
            }

            return entity;
        }

		/// <summary>
		/// Loads an instance of type T from the DB based on its ID.
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public T GetById( IdT id )
		{
			return GetById(id, false);
		}

    	/// <summary>
        /// Loads every instance of the requested type with no filtering.
        /// </summary>
        public virtual IList<T> GetAll() {
            return GetByCriteria();
        }

        /// <summary>
        /// Loads every instance of the requested type using the supplied <see cref="ICriterion" />.
        /// If no <see cref="ICriterion" /> is supplied, this behaves like <see cref="GetAll" />.
        /// </summary>
        public IList<T> GetByCriteria(params ICriterion[] criterion) {
            ICriteria criteria = NHibernateSession.CreateCriteria(persitentType);

            foreach (ICriterion criterium in criterion) {
                criteria.Add(criterium);
            }

            return criteria.List<T>();
        }

		/// <summary>
		/// Loads a unique instance of the requested type using the supplied <see cref="ICriterion" />.
		/// </summary>
		public T GetUniqueByCriteria( params ICriterion[] criterion )
		{
			ICriteria criteria = NHibernateSession.CreateCriteria( persitentType );

			foreach( ICriterion criterium in criterion )
			{
				criteria.Add( criterium );
			}

			return (T)criteria.UniqueResult();
		}

        public ICriteria GetCriteria() {
            return NHibernateSession.CreateCriteria(persitentType);
        }

        public ICriteria GetCriteria(Type currentType)
        {
            return NHibernateSession.CreateCriteria(currentType);
        }

        /// <summary>
        /// For entities that have assigned ID's, you must explicitly call Save to add a new one.
        /// See http://www.hibernate.org/hib_docs/reference/en/html/mapping.html#mapping-declaration-id-assigned.
        /// </summary>
        public virtual T Save(T entity)
        {
            NHibernateSession.Save(entity);
            return entity;
        }


        /// <summary>
        /// For entities with automatatically generated IDs, such as identity, SaveOrUpdate may 
        /// be called when saving a new entity.  SaveOrUpdate can also be called to _update_ any 
        /// entity, even if its ID is assigned.
        /// </summary>
        public virtual T SaveOrUpdate(T entity)
        {
            NHibernateSession.SaveOrUpdate(entity);
            return entity;
        }

        public virtual void Delete(T entity)
        {
            NHibernateSession.Delete(entity);
        }

        public virtual object SaveAny(object entity)
        {
            NHibernateSession.Save(entity);
            return entity;
        }

        public virtual object SaveOrUpdateAny(object entity)
        {
            NHibernateSession.SaveOrUpdate(entity);
            return entity;
        }

        public virtual void DeleteAny(object entity)
        {
            NHibernateSession.Delete(entity);
        }

        /// <summary>
        /// Commits changes regardless of whether there's an open transaction or not
        /// </summary>
        protected void CommitChanges() {
            if (NHibernateSessionManager.Instance.HasOpenTransactionOn(SessionFactoryConfigPath))
                NHibernateSessionManager.Instance.CommitTransactionOn(SessionFactoryConfigPath);
            else
                NHibernateSessionManager.Instance.GetSessionFrom(SessionFactoryConfigPath).Flush();
        }

        /// <summary>
        /// Commits changes regardless of whether there's an open transaction or not
        /// </summary>
        protected void BeginTransaction()
        {
            if (!NHibernateSessionManager.Instance.HasOpenTransactionOn(SessionFactoryConfigPath))
                NHibernateSessionManager.Instance.BeginTransactionOn(SessionFactoryConfigPath);
        }

        /// <summary>
        /// Commits changes regardless of whether there's an open transaction or not
        /// </summary>
        protected void RollbackChanges()
        {
            if (NHibernateSessionManager.Instance.HasOpenTransactionOn(SessionFactoryConfigPath))
                NHibernateSessionManager.Instance.RollbackTransactionOn(SessionFactoryConfigPath);
        }

        /// <summary>
        /// Exposes the ISession used within the DAO.
        /// </summary>
        protected ISession NHibernateSession {
            get {
                return NHibernateSessionManager.Instance.GetSessionFrom(SessionFactoryConfigPath);
            }
        }

        private readonly Type persitentType = typeof(T);
        protected readonly string SessionFactoryConfigPath;

        protected void SetupPaging(ICriteria crit, int pageSize, int pageNumber)
        {
            SetupPagingAndSorting(crit, pageNumber, pageNumber, null, false);
        }

        protected void SetupSorting(ICriteria crit, string sortField, bool sortAscending)
        {
            if (string.IsNullOrEmpty(sortField))
            {
                string[] sort = sortField.Split('.');
                ICriteria critSort = crit;
                if (!sortField.Contains("TimeStamp") && sort.Length > 1)
                {
                    critSort = crit.GetCriteriaByPath(sort[0]);
                    if (critSort == null)
                        critSort = crit.CreateCriteria(sort[0]);
                    sortField = sort[1];
                }

                critSort.AddOrder(new Order(sortField, sortAscending));
            }
        }

        protected void SetupPagingAndSorting(ICriteria crit, int pageSize, int pageNumber, string sortField, bool sortAscending)
        {
            crit.SetMaxResults(pageSize);
            if (pageNumber == 1)
                crit.SetFirstResult(0);
            else
                crit.SetFirstResult((pageNumber - 1) * pageSize);

            if (string.IsNullOrEmpty(sortField))
                SetupSorting(crit, sortField, sortAscending);
        }

    }
}
