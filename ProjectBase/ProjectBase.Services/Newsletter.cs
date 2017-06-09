using System;
using System.Configuration;
using System.Diagnostics;
using System.Net.Mail;
using System.ServiceProcess;
using System.Threading;
using ProjectBase.Newsletter;
using ProjectBase.Utils;

namespace ProjectBase.Services
{
	public class NewsletterService : ServiceBase
	{
		private Timer timer;

		protected override void OnStart(string[] args)
		{
#if DEBUG
			Debugger.Launch( );
			Debugger.Break( );
#endif

			timer = new Timer(
				OnElapsedTime,
				this,
				0,
				Convert.ToInt32(ConfigurationManager.AppSettings["TimeSlice"]));

            // Obtain the processor tu run
            INewsletterProcessor processor =
                (INewsletterProcessor)WindsorAccessor.Instance.Container[typeof(INewsletterProcessor)];

            if (!EventLog.SourceExists(processor.Name))
                EventLog.CreateEventSource(processor.Name, processor.Name);

            SendEmail(processor.Name, "Service started.");
		}

		private void OnElapsedTime(object sender)
		{
			// Obtain the processor tu run
			INewsletterProcessor processor =
				(INewsletterProcessor)WindsorAccessor.Instance.Container[typeof( INewsletterProcessor )];

			string errors;
			if( !processor.Execute( out errors ) )
			{
                SendEmail(processor.Name, errors);
                EventLog.WriteEntry(processor.Name, errors, EventLogEntryType.Error);
            }
		}

		protected override void OnStop()
		{
            // Obtain the processor tu run
            INewsletterProcessor processor =
                (INewsletterProcessor)WindsorAccessor.Instance.Container[typeof(INewsletterProcessor)];
            SendEmail(processor.Name, "Service stopped.");

			timer.Dispose();
			WindsorAccessor.Instance.Container.Dispose();
		}

        private void SendEmail(string processorName, string errors)
        {
            SmtpClient mailclient = new SmtpClient();

            MailMessage mm = new MailMessage();
            mm.To.Add(ConfigurationManager.AppSettings["SupportEmail"]);
            mm.Subject = processorName + " Message";
            mm.Body = errors;

            mailclient.Send(mm);
        }
	}
}