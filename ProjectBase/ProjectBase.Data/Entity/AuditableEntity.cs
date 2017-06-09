using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectBase.Data
{
    /// <summary>
    /// Allows to define an auditable entity that automatically manages audit tracing on NHibernate.
    /// </summary>
    /// <typeparam name="IdT">Identifier Type</typeparam>
    [Serializable]
    public class AuditableEntity<IdT> : Entity<IdT>, IAuditable
    {
        #region IAuditable Members

        private ITimeStamp timeStamp;
        public virtual ITimeStamp TimeStamp
        {
            get
            {
                if (timeStamp == null)
                    timeStamp = new TimeStamp();

                return timeStamp;
            }
            set
            {
                timeStamp = value;
            }
        }

        #endregion
    }
}
