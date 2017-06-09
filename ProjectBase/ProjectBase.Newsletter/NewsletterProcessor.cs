using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Net.Mail;
using System.Reflection;
using System.Web.Security;

namespace ProjectBase.Newsletter
{
	/// <summary>
	/// Manager class to maintain Newsletter campaigns on any application
	/// </summary>
	/// <remarks>ASP.NET Membership must be configured in the application to work correctly.</remarks>
	public sealed class NewsletterProcessor : INewsletterProcessor
	{
		#region Private Properties & Constructors

		private readonly string applicationName = Membership.ApplicationName.ToLower();
		private DayOfWeek[] daysToExecute;
		private int monthDay = 1;
		private string sessionFactoryConfigPath;
		private string templatePath;
		private string[] timesToExecute;
		private DayOfWeek weekDay = DayOfWeek.Friday;

		///// <summary>
		///// Constructor
		///// </summary>
		///// <param name="sessionFactoryConfigPath">Path to NHibernate configuration</param>
		///// <param name="templatePath">Path to template files</param>
		///// <param name="timesToExecute">Hour to run the process per frequency</param>
		///// <param name="daysToExecute">Days to execute the process</param>
		//public NewsletterProcessor(string sessionFactoryConfigPath, string templatePath, TimeSpan[] timesToExecute,
		//                           DayOfWeek[] daysToExecute)
		//{
		//    this.sessionFactoryConfigPath = sessionFactoryConfigPath;
		//    this.templatePath = templatePath;
		//    this.timesToExecute = timesToExecute;
		//    this.daysToExecute = daysToExecute;
		//}

		///// <summary>
		///// Constructor
		///// </summary>
		///// <param name="sessionFactoryConfigPath">Path to NHibernate configuration</param>
		///// <param name="templatePath">Path to template files</param>
		///// <param name="timesToExecute">Hour to run the process per frequency</param>
		///// <param name="daysToExecute">Days to execute the process</param>
		///// <param name="weekDay"></param>
		//public NewsletterProcessor(string sessionFactoryConfigPath, string templatePath, TimeSpan[] timesToExecute,
		//                           DayOfWeek[] daysToExecute, DayOfWeek weekDay)
		//{
		//    this.sessionFactoryConfigPath = sessionFactoryConfigPath;
		//    this.templatePath = templatePath;
		//    this.timesToExecute = timesToExecute;
		//    this.daysToExecute = daysToExecute;
		//    this.weekDay = weekDay;
		//}


		///// <summary>
		///// Constructor
		///// </summary>
		///// <param name="sessionFactoryConfigPath">Path to NHibernate configuration</param>
		///// <param name="templatePath">Path to template files</param>
		///// <param name="timesToExecute">Hour to run the process per frequency</param>
		///// <param name="daysToExecute">Days to execute the process</param>
		//public NewsletterProcessor(string sessionFactoryConfigPath, string templatePath, TimeSpan[] timesToExecute,
		//                           DayOfWeek[] daysToExecute, DayOfWeek weekDay, int monthDay)
		//{
		//    this.sessionFactoryConfigPath = sessionFactoryConfigPath;
		//    this.templatePath = templatePath;
		//    this.timesToExecute = timesToExecute;
		//    this.daysToExecute = daysToExecute;
		//    this.weekDay = weekDay;
		//    this.monthDay = monthDay;
		//}

		#endregion

		#region INewsletterProcessor Members

		public bool Execute( out string errors )
		{
			return ExecuteAll(out errors, false);
		}

		public bool ExecuteTest( out string errors )
		{
			return ExecuteAll( out errors , true );
		}

		/// <summary>
		/// Process all the campaigns in the system and send the mails to the user subscribed.
		/// If dynamic campaigns are defined, the corresponding component must be located in the bin folder to be loaded dynamically.
		/// </summary>
		/// <returns>Any error occured during processing.</returns>
		/// <remarks>To be executed once a day.</remarks>
		public bool ExecuteAll( out string errors , bool testExecution )
		{
			errors = string.Empty;

			// Check if should be executed today
			bool execute = false;

			foreach (DayOfWeek day in DaysToExecute)
				if (day == DateTime.Today.DayOfWeek)
				{
					execute = true;
					break;
				}

			if (!execute)
				return true;

			ProcessExecutionDao ped = new ProcessExecutionDao(SessionFactoryConfigPath);
			CampaignDao cd = new CampaignDao(SessionFactoryConfigPath);

			ArrayList arr = new ArrayList(4);
			arr.Add( MailFrequency.TimeSpan );
			arr.Add( MailFrequency.Daily );
			if (DateTime.Today.DayOfWeek == WeekDay)
				arr.Add(MailFrequency.Weekly);
			if (DateTime.Today.Day == MonthDay)
				arr.Add(MailFrequency.Monthly);

			IList<Campaign> campaigns = cd.GetPendingAutomatic(applicationName, false, arr.ToArray());

			foreach (Campaign c in campaigns)
			{
				if( c.Frequency == MailFrequency.TimeSpan || ped.ExecutedToday( applicationName , c ) == null )
				{
					int position = ((int) c.Frequency) - 1;

					bool run = false;

					// Ejecuto el proceso si paso el tiempo desde la ultima vez
					if( c.Frequency == MailFrequency.TimeSpan )
					{
						ProcessExecution pe = ped.ExecutedLast(applicationName, c );
						if( pe != null ) 
						{
							TimeSpan diff = DateTime.Now - pe.RunDate;
							if( diff.Hours > TimeSpan.Parse( TimesToExecute[position] ).Hours || 
								(diff.Hours == TimeSpan.Parse( TimesToExecute[position] ).Hours && diff.Minutes >= TimeSpan.Parse( TimesToExecute[position] ).Minutes) )
								run = true;
						}
						else
							run = true;
					}
					// Ejecuto el proceso diario luego de la hora establecida
					else if( 
                        (
                        DateTime.Now.Hour == Convert.ToInt32( TimeSpan.Parse( TimesToExecute[position] ).Hours ) &&
					    DateTime.Now.Minute >= Convert.ToInt32(TimeSpan.Parse(TimesToExecute[position]).Minutes)
                        ) ||
                        DateTime.Now.Hour > Convert.ToInt32(TimeSpan.Parse(TimesToExecute[position]).Hours)
                        )
						run = true;

					if( run ) 
					{
						ExecuteCampaign( ref errors , c , testExecution );

						ProcessExecution pe = new ProcessExecution( );
						pe.ApplicationName = applicationName;
						pe.Campaign = c;
						pe.RunDate = DateTime.Now;
						ped.Save( pe );
					}

				}
			}

			// Ejecuto las campañas scheduleadas manualmente
			ExecutionDao ced = new ExecutionDao(SessionFactoryConfigPath);
			foreach (Execution ce in ced.GetPendings(applicationName))
			{
				// Ejecuto el proceso diario luego de la hora establecida
				if( (DateTime.Now - ce.RunDate).Ticks >= 0 )
				{
					ExecuteCampaign(ref errors, ce.Campaign, ce.TestExecution);
					ced.Delete(ce);
				}
			}

			return (errors == string.Empty);
		}

		public string Name
		{
			get { return applicationName; }
		}

		public DayOfWeek[] DaysToExecute
		{
			get { return daysToExecute; }
			set { daysToExecute = value; }
		}

		public int MonthDay
		{
			get { return monthDay; }
			set { monthDay = value; }
		}

		public string SessionFactoryConfigPath
		{
			get { return sessionFactoryConfigPath; }
			set { sessionFactoryConfigPath = value; }
		}

		public string TemplatePath
		{
			get { return templatePath; }
			set { templatePath = value; }
		}

		public string[] TimesToExecute
		{
			get { return timesToExecute; }
			set { timesToExecute = value; }
		}

		public DayOfWeek WeekDay
		{
			get { return weekDay; }
			set { weekDay = value; }
		}

		#endregion

		private void ExecuteCampaign(ref string errors, Campaign c, bool testExecution)
		{
			// Get Last Execution For This Campaign
			DateTime? lastRunDate = null;

			HistoryDao chd = new HistoryDao( SessionFactoryConfigPath );

			ProcessExecutionDao ped = new ProcessExecutionDao( SessionFactoryConfigPath );
			ProcessExecution pe = ped.ExecutedLast(applicationName, c);
			if( pe != null )
				lastRunDate = pe.RunDate;

			// Create common variables
			object dynamicObject = null;
			MailMessage mailMessage = null;

			if (c.FixedNewsletter != null)
			{
				mailMessage = new MailMessage();
				mailMessage.Subject = c.FixedNewsletter.Subject;
				mailMessage.Body = c.FixedNewsletter.Body;
			}
			else
			{
				if (c.DynamicCode.Contains(","))
				{
					string className = c.DynamicCode.Split(',')[0].Trim();
					string assemblyName = c.DynamicCode.Split(',')[1].Trim();

					try
					{
						Assembly assembly = Assembly.Load(assemblyName);
						dynamicObject = assembly.CreateInstance(className);
					}
					catch (Exception e)
					{
						errors += c + "\n" + e.Message + "\n";
					}

					if (dynamicObject is IDynamicNewsletter && TemplatePath != null)
					{
						IDynamicNewsletter no = (IDynamicNewsletter) dynamicObject;
						mailMessage = no.Get(TemplatePath, lastRunDate);
					}
				}
				else
					errors += c + "\n" + "Dynamic Code is invalid:" + c + ": " + c.DynamicCode + "\n";
			}

			IList<MembershipUser> users = GetMembers(c, testExecution);
			if (users.Count == 0)
				return;

			if ((mailMessage != null || (dynamicObject is IPersonalizedNewsletter && TemplatePath != null)))
			{
				// Save History
				History ch = new History();
				ch.Campaign = c;
				ch.SentDate = DateTime.Now;
				if (mailMessage != null)
				{
					ch.Body = mailMessage.Body;
					ch.Subject = mailMessage.Subject;
				}

				foreach (MembershipUser mu in users)
				{
					if (dynamicObject is IPersonalizedNewsletter)
					{
						IPersonalizedNewsletter pn = (IPersonalizedNewsletter) dynamicObject;
						mailMessage = pn.Get(mu, TemplatePath, lastRunDate);
					}

					if (mailMessage != null)
					{
						mailMessage.IsBodyHtml = true;
						mailMessage.To.Add(mu.Email);
						mailMessage.Body = mailMessage.Body.Replace("[SITEURL]", ConfigurationManager.AppSettings["SiteURL"]);

						// Add User to History
						ch.Users.Add(new HistoryUser(ch, (Guid) mu.ProviderUserKey));

						SmtpClient client = new SmtpClient();
						client.SendCompleted += SmtpClient_OnCompleted;
						client.SendAsync(mailMessage, null);

						mailMessage.To.Clear();
					}

					chd.Save(ch);
				}
			}
		}

		private void SmtpClient_OnCompleted(object sender, AsyncCompletedEventArgs e)
		{
			if (e.Error != null)
				EventLog.WriteEntry(Name,
				                    string.Format("Error {0} occurred when sending mail.", e.Error));
		}

		public IList<MembershipUser> GetMembers(Campaign c, bool test)
		{
			IList<MembershipUser> users = new List<MembershipUser>();

			if (!test)
			{
				if (c.Type == CampaignType.Subscription)
				{
					foreach (UserCampaign uc in c.UserCampaigns)
					{
						MembershipUser u = Membership.GetUser(uc.UserID);
						if (u != null)
							users.Add(u);
					}
				}
				else
				{
					foreach (MembershipUser mu in Membership.GetAllUsers())
						users.Add(mu);
				}
			}
			else
			{
				string[] userNames = Roles.GetUsersInRole("Newsletter Tester");
				foreach (string user in userNames)
					users.Add(Membership.GetUser(user));
			}

			return users;
		}
	}
}