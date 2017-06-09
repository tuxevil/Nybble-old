using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Security;
using NHibernate;
using ProjectBase.Utils;

namespace ProjectBase.Data
{
    public class SessionInterceptor : EmptyInterceptor
    {
        private const string TIMESTAMP = "TimeStamp";

        private IUserContext currentUser;
        public IUserContext CurrentUser
        {
            get
            {
                if (currentUser == null)
                    currentUser = new UserContext();

                return currentUser;
            }
        }

        public SessionInterceptor()
        {
        }

        public SessionInterceptor(IUserContext context)
        {
            if (context != null)
                currentUser = context;
        }

        #region IInterceptor Members

        public override bool OnFlushDirty(object entity, object id, object[] currentState, object[] previousState, string[] propertyNames, NHibernate.Type.IType[] types)
        {
            if (entity is IAuditable)
            {
                TimeStamp ts = currentState[Array.IndexOf(propertyNames, TIMESTAMP)] as TimeStamp;
                if (ts != null)
                {
                    if (CurrentUser != null)
                        ts.ModifiedBy = CurrentUser.Identifier;
                    ts.ModifiedOn = DateTime.Now;
                }
                currentState[Array.IndexOf(propertyNames, TIMESTAMP)] = ts;

                return true;
            }

            return false;
        }

        private int FindProperty(string[] propertyNames, string name)
        {
            for (int i = 0; i < propertyNames.Length; i++)
                if (name == propertyNames[i])
                    return i;

            return -1;
        }

        public override bool OnSave(object entity, object id, object[] state, string[] propertyNames, NHibernate.Type.IType[] types)
        {
            if (entity is IAuditable)
            {
                TimeStamp ts = state[Array.IndexOf(propertyNames, TIMESTAMP)] as TimeStamp;
                if (ts != null)
                {
                    if (CurrentUser != null)
                    {
                        ts.ModifiedBy = CurrentUser.Identifier;
                        ts.CreatedBy = CurrentUser.Identifier;
                    }

                    ts.ModifiedOn = DateTime.Now;
                    ts.CreatedOn = DateTime.Now;
                }
                state[Array.IndexOf(propertyNames, TIMESTAMP)] = ts;

                return true;
            }

            return false;
        }

        #endregion
    }
}
