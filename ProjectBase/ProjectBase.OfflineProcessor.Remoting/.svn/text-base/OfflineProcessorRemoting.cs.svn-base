using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectBase.OfflineProcessor.Remoting
{
    public class DynamicProcessorRemoting : MarshalByRefObject
    {
        public bool AddTask(ProcessItem pi)
        {
            return DynamicProcessor.Instance.AddTask(pi);
        }

        public ProcessItem RetrieveTask(string identifier)
        {
            return DynamicProcessor.Instance.RetrieveTask(identifier);
        }

        public ProcessItemStatus RetrieveTaskStatus(string identifier)
        {
            return DynamicProcessor.Instance.RetrieveTaskStatus(identifier);
        }

        public void RemoveTask(string identifier)
        {
            DynamicProcessor.Instance.RemoveTask(identifier);
        }
    }
}
