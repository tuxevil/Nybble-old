using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectBase.OfflineProcessor
{
    public enum ProcessItemStatus
    {
        NotExist = 0,
        Queue = 1,
        Processing = 2,
        Failed = 3,
        Finished = 4
    }

    [Serializable]
    public class ProcessItem : IEquatable<ProcessItem>
    {
        public string Identifier;
        public string AssemblyName;
        public string ClassName;
        public string MethodName;
        public object[] MethodParameters;
        public object[] ConstructorParameters;
        public object Result;
        public string ErrorMessage;
        public ProcessItemStatus Status;

        public ProcessItem(string identifier) 
        {
            this.Identifier = identifier;
        }

        #region IEquatable<ProcessItem> Members

        public bool Equals(ProcessItem other)
        {
            return (Identifier == other.Identifier);
        }

        public override bool Equals(object obj)
        {
            return (this.Identifier == (obj as ProcessItem).Identifier);
        }

        #endregion
    }

}
