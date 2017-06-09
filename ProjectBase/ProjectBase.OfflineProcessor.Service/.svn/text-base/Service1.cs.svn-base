using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;
using System.Runtime.Remoting.Channels.Ipc;
using System.Runtime.Remoting.Channels;
using ProjectBase.OfflineProcessor;
using ProjectBase.OfflineProcessor.Remoting;
using System.Threading;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels.Tcp;
using System.Configuration;

namespace WindowsService1
{
    public partial class Service1 : ServiceBase
    {
        private Timer timer;

        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
#if DEBUG
            Debugger.Launch();
            Debugger.Break();
#endif
            log4net.Config.XmlConfigurator.Configure();
    
            TcpChannel chan = new TcpChannel(Convert.ToInt32(ConfigurationManager.AppSettings["TCP_CHANNEL"]));
            ChannelServices.RegisterChannel(chan, false);

            //// Register a communication channel to use in the WebSite.
            //Dictionary<string, string> props = new Dictionary<string, string>();
            //props.Add("authorizedGroup", "Todos");
            //props.Add("portName", "OfflineProcessor");
            //IpcChannel channel;
            //channel = new IpcChannel(props, null, null);
            //ChannelServices.RegisterChannel(channel, false);

            RemotingConfiguration.RegisterWellKnownServiceType
                (typeof(DynamicProcessorRemoting), "RemoteProcessor", WellKnownObjectMode.Singleton);

            RemotingConfiguration.CustomErrorsEnabled(false);
            RemotingConfiguration.CustomErrorsMode = CustomErrorsModes.Off;

            timer = new Timer(
                OnElapsedTime,
                this,
                0,
                Convert.ToInt32(ConfigurationManager.AppSettings["TIMER_SECONDS"]) * 1000);

        }

        private void OnElapsedTime(object sender)
        {
            DynamicProcessor.Instance.ProcessPending();
        }

        protected override void OnStop()
        {
        }
    }
}
