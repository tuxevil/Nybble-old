using System;
using System.Data;
using System.Configuration;
using System.Reflection;
using System.Collections.Generic;
using System.Threading;
using System.Diagnostics;

namespace ProjectBase.OfflineProcessor
{
    /// <summary>
    /// Summary description for Processor
    /// </summary>

    public sealed class DynamicProcessor
    {
        private int MAX_PARALELL_PROCESSING 
        {
            get {
                if (ConfigurationManager.AppSettings["MAX_PARALELL_PROCESSING"] != null)
                    return Convert.ToInt32(ConfigurationManager.AppSettings["MAX_PARALELL_PROCESSING"]);
                else
                    return 1;
            } 
        }

        #region Thread-safe, lazy Singleton

        /// <summary>
        /// This is a thread-safe, lazy singleton.  See http://www.yoda.arachsys.com/csharp/singleton.html
        /// for more details about its implementation.
        /// </summary>
        public static DynamicProcessor Instance
        {
            get
            {
                return Nested.DynamicProcessor;
            }
        }

        /// <summary>
        /// Private constructor to enforce singleton
        /// </summary>
        private DynamicProcessor() 
        {
        }

        /// <summary>
        /// Assists with ensuring thread-safe, lazy singleton
        /// </summary>
        private class Nested
        {
            static Nested() { }
            internal static readonly DynamicProcessor DynamicProcessor =
                new DynamicProcessor();
        }

        #endregion

        private Queue<ProcessItem> queueItems = new Queue<ProcessItem>();
        private List<ProcessItem> workItems = new List<ProcessItem>();

        private int processingInParalell = 0;

        public bool CanProcessQueue 
        {
            get { return (processingInParalell < MAX_PARALELL_PROCESSING); }
        }

        public bool AddTask(ProcessItem pi)
        {
            if (!queueItems.Contains(pi) && !workItems.Contains(pi))
                queueItems.Enqueue(pi);
            else
                return false;

            return true;
        }

        public ProcessItemStatus RetrieveTaskStatus(string identifier)
        {
            ProcessItem pi = RetrieveTask(identifier);
            if (pi != null)
                return pi.Status;
            else if (queueItems.Contains(new ProcessItem(identifier)))
                return ProcessItemStatus.Queue;
            else
                return ProcessItemStatus.NotExist;
        }

        public ProcessItem RetrieveTask(string identifier)
        {
            ProcessItem pi = new ProcessItem(identifier);
            int index = workItems.IndexOf(pi);
            if (index >= 0)
            {
                pi = workItems[index];
                return pi;
            }
            else
                return null;
        }

        public void RemoveTask(string identifier)
        {
            ProcessItem pi = new ProcessItem(identifier);
            int index = workItems.IndexOf(pi);
            if (index >= 0 && workItems[index].Status != ProcessItemStatus.Processing)
                workItems.RemoveAt(index);
        }

        public void ProcessPending()
        {
            if (!CanProcessQueue)
                return;

            processingInParalell += 1;

            // Remove old items from the work queue and assure no more than 30 items
            // We are expecting the consumer to take care of what is happening
            // Anyway it good be a good idea to save them for later reference, maybe in a XML file
            if (workItems.Count > 30)
                workItems.RemoveRange(0, 19);

            while (queueItems.Count > 0)
            {
                ProcessItem pi = queueItems.Dequeue();
                pi.Status = ProcessItemStatus.Processing;
                workItems.Add(pi);

                // TODO: Process on another thread
                try
                {
                    pi.Result = ExecuteMethod(pi);
                    pi.Status = ProcessItemStatus.Finished;
                }
                catch (Exception ex)
                {
                    // TODO: Log error
                    pi.ErrorMessage = ex.ToString();
                    pi.Status = ProcessItemStatus.Failed;
                }
            }

            processingInParalell -= 1;
        }

        /// <summary>
        /// Executes a ProcessItem loading the corresponding assembly and method.
        /// </summary>
        /// <param name="processItem">Item to process</param>
        /// <returns>The result from the ProcessItem method</returns>
        private object ExecuteMethod(ProcessItem processItem)
        {
            Object dynamicObject;

            try
            {
                Assembly assembly = Assembly.Load(processItem.AssemblyName);
                dynamicObject = assembly.CreateInstance(processItem.ClassName, true, BindingFlags.CreateInstance, null, processItem.ConstructorParameters, null, null);
            }
            catch (Exception e)
            {
                throw new CouldNotLoadInstanceException("Instance could not be created", e);
            }

            if (dynamicObject == null)
                throw new CouldNotFindInstanceException("Instance could not be found in the assembly");

            try
            {
                return dynamicObject.GetType().InvokeMember(processItem.MethodName,
                                         BindingFlags.Default | BindingFlags.InvokeMethod,
                                         null,
                                         dynamicObject,
                                         processItem.MethodParameters);
            }
            catch (Exception e)
            {
                throw new CouldNotExecuteMethodException("Method could not be executed.", e);
            }
        }
    }

    public class CouldNotLoadInstanceException : Exception
    {
        public CouldNotLoadInstanceException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }

    public class CouldNotExecuteMethodException : Exception
    {
        public CouldNotExecuteMethodException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }

    public class CouldNotFindInstanceException : Exception
    {
        public CouldNotFindInstanceException(string message)
            : base(message)
        {
        }
    }

}