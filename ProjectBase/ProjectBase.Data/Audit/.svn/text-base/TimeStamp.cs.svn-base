using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectBase.Data
{
    [Serializable]
    public class TimeStamp : ITimeStamp
    {
        private object createdBy;
        private DateTime createdOn;
        private object modifiedBy;
        private DateTime? modifiedOn;

        public virtual object CreatedBy
        {
            get { return createdBy; }
            set { createdBy = value; }
        }

        public virtual DateTime CreatedOn
        {
            get { return createdOn; }
            set { createdOn = value; }
        }

        public virtual object ModifiedBy
        {
            get { return modifiedBy; }
            set { modifiedBy = value; }
        }

        public virtual DateTime? ModifiedOn
        {
            get { return modifiedOn; }
            set { modifiedOn = value; }
        }
    }
}
