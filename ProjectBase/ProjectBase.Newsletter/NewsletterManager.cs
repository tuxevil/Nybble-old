using System;
using System.Collections.Generic;
using System.Web.Security;

namespace ProjectBase.Newsletter
{
	/// <summary>
	/// Manager class to maintain Newsletter campaigns on any application
	/// </summary>
	/// <remarks>ASP.NET Membership must be configured in the application to work correctly.</remarks>
	public sealed class NewsletterManager
	{
		private readonly string applicationName = Membership.ApplicationName.ToLower();
		private readonly string sessionFactoryConfigPath;

		#region Constructors

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="sessionFactoryConfigPath">Path to NHibernate configuration</param>
		public NewsletterManager(string sessionFactoryConfigPath)
		{
			this.sessionFactoryConfigPath = sessionFactoryConfigPath;
		}

		#endregion

		#region Campaign Methods

		public void ChangeCampaignStatus(string code, CampaignStatus campaignStatus)
		{
			ChangeCampaignStatus(GetCampaign(code), campaignStatus);
		}

		/// <summary>
		/// Get all campaigns
		/// </summary>
		/// <returns></returns>
		public IList<Campaign> GetCampaigns()
		{
			CampaignDao cd = new CampaignDao(sessionFactoryConfigPath);
			return cd.GetAll(applicationName);
		}

		/// <summary>
		/// Get all active or inactive campaigns
		/// </summary>
		/// <param name="active"></param>
		/// <returns></returns>
		public IList<Campaign> GetCampaigns(bool active)
		{
			CampaignDao cd = new CampaignDao(sessionFactoryConfigPath);
			if (!active)
				return cd.GetAll(applicationName);
			else
				return cd.GetActive(applicationName);
		}

		/// <summary>
		/// Get all active or inactive campaigns
		/// </summary>
		/// <param name="active"></param>
		/// <returns></returns>
		public IList<Campaign> GetFixedCampaigns(bool active)
		{
			CampaignDao cd = new CampaignDao(sessionFactoryConfigPath);
			if (!active)
				return cd.GetAll(applicationName, true);
			else
				return cd.GetActive(applicationName, true);
		}

		public Campaign GetCampaign(string code)
		{
			CampaignDao cd = new CampaignDao(sessionFactoryConfigPath);
			return cd.GetByCode(applicationName, code);
		}

		public void ChangeCampaignStatus(Campaign campaign, CampaignStatus campaignStatus)
		{
			campaign.Status = campaignStatus;

			CampaignDao cd = new CampaignDao(sessionFactoryConfigPath);
			cd.Save(campaign);
		}

		public Campaign CreateCampaign(string code, string name, MailFrequency frequency, Newsletter fixedNewsletter,
		                               string dynamicCode, DateTime? startDate, DateTime? endDate, CampaignStatus status,
		                               CampaignType type)
		{
			CampaignDao cd = new CampaignDao(sessionFactoryConfigPath);

			if (cd.GetByCode(applicationName, code) != null)
				throw new ApplicationException(
					string.Format("There is already a campaign with the code {{{0}}} on the application {{{1}}}", code, applicationName));

			Campaign c = new Campaign();
			c.ApplicationName = applicationName;
			c.Code = code;
			c.Name = name;
			c.StartDate = startDate;
			c.EndDate = endDate;
			c.DynamicCode = dynamicCode;
			c.Frequency = frequency;
			c.FixedNewsletter = fixedNewsletter;
			c.Status = status;
			c.Type = type;

			cd.Save(c);

			return c;
		}

		public Campaign UpdateCampaign(string code, string name, MailFrequency frequency, Newsletter fixedNewsletter,
		                               string dynamicCode, DateTime? startDate, DateTime? endDate, CampaignStatus status,
		                               CampaignType type)
		{
			CampaignDao cd = new CampaignDao(sessionFactoryConfigPath);
			Campaign c = cd.GetByCode(applicationName, code);

			if (c != null)
			{
				c.Name = name;
				c.StartDate = startDate;
				c.EndDate = endDate;
				c.DynamicCode = dynamicCode;
				c.Frequency = frequency;
				c.FixedNewsletter = fixedNewsletter;
				c.Status = status;
				c.Type = type;
				cd.Save(c);
			}

			return c;
		}

		#endregion

		#region Frequencies Methods

		public IList<Frequency> GetFrequencies()
		{
			FrequencyDao fd = new FrequencyDao(sessionFactoryConfigPath);
			return fd.GetByApplication(applicationName);
		}

		#endregion

		#region Newsletter Methods

		public IList<Newsletter> GetNewsleters()
		{
			NewsletterDao nd = new NewsletterDao(sessionFactoryConfigPath);
			return nd.GetAll();
		}

		public Newsletter GetNewsletter(int newsletterId)
		{
			NewsletterDao nd = new NewsletterDao(sessionFactoryConfigPath);
			return nd.GetById(newsletterId);
		}

		public Newsletter CreateNewsletter(NewsletterType type, string subject, string body)
		{
			NewsletterDao nd = new NewsletterDao(sessionFactoryConfigPath);
			Newsletter n = new Newsletter();
			n.Body = body;
			n.Subject = subject;
			n.NewsletterType = type;
			nd.Save(n);

			return n;
		}

		public Newsletter UpdateNewsletter(int newsletterId, NewsletterType type, string subject, string body)
		{
			NewsletterDao nd = new NewsletterDao(sessionFactoryConfigPath);
			Newsletter n = nd.GetById(newsletterId);
			if (n != null)
			{
				n.Body = body;
				n.Subject = subject;
				n.NewsletterType = type;
				nd.Save(n);
			}
			return n;
		}

		#endregion

		#region Execution Methods

		public void ExecuteTest( Campaign c )
		{
			Execute( c , DateTime.Now.AddMinutes(5) , true );
		}

		public void ExecuteTest(Campaign c, DateTime executeDate)
		{
			Execute(c, executeDate, true);
		}

		public void Execute( Campaign c )
		{
			Execute( c , DateTime.Now.AddMinutes( 5 ) , false );
		}

		public void Execute( Campaign c , DateTime executeDate )
		{
			Execute( c , executeDate , false );
		}

		private void Execute( Campaign c , DateTime executeDate , bool test )
		{
			Execution ce = new Execution( );
			ce.Campaign = c;
			ce.RunDate = executeDate;
			ce.DateCreated = DateTime.Now;
			ce.TestExecution = test;

			ExecutionDao cd = new ExecutionDao( sessionFactoryConfigPath );
			cd.Save( ce );
		}

		#endregion

		#region Subscription Methods

		public bool IsSubscribed(Campaign campaign)
		{
			return IsSubscribed(campaign, Membership.GetUser());
		}

		public bool IsSubscribed(Campaign campaign, Guid user)
		{
			return IsSubscribed(campaign, Membership.GetUser(user));
		}

		public void Subscribe(Campaign campaign)
		{
			SetUser(campaign, Membership.GetUser(), true, false);
		}

		public void Subscribe(Campaign campaign, Guid user)
		{
			SetUser(campaign, Membership.GetUser(user), true, false);
		}

		public void Unsubscribe(Campaign campaign)
		{
			SetUser(campaign, Membership.GetUser(), false, false);
		}

		public void Unsubscribe(Campaign campaign, Guid user)
		{
			SetUser(campaign, Membership.GetUser(user), false, false);
		}

		public IList<UserCampaign> GetSubscriptors()
		{
			UserCampaignDao ucd = new UserCampaignDao(sessionFactoryConfigPath);
			return ucd.GetAll();
		}

		public IList<UserCampaign> GetSubscriptors(Campaign c)
		{
			UserCampaignDao ucd = new UserCampaignDao(sessionFactoryConfigPath);
			return ucd.GetByCampaign(c.ID);
		}

		public IList<UserCampaign> GetSubscriptors(string code)
		{
			Campaign c = GetCampaign(code);
			if (c != null)
			{
				UserCampaignDao ucd = new UserCampaignDao(sessionFactoryConfigPath);
				return ucd.GetByCampaign(c.ID);
			}
			else
				return null;
		}

		private bool IsSubscribed(Campaign campaign, MembershipUser mu)
		{
			if (mu == null)
				return false;

			UserCampaignDao ucd = new UserCampaignDao(sessionFactoryConfigPath);
			UserCampaign uc = ucd.GetByUser((Guid) mu.ProviderUserKey, campaign.ID);

			return (uc != null);
		}

		private void SetUser(Campaign campaign, MembershipUser mu, bool add, bool verify)
		{
			if (mu == null)
				return;

			UserCampaignDao ucd = new UserCampaignDao(sessionFactoryConfigPath);
			UserCampaign uc = ucd.GetByUser((Guid) mu.ProviderUserKey, campaign.ID);

			if (add)
			{
				if (uc == null)
				{
					uc = new UserCampaign();
					uc.Campaign = campaign;
					uc.UserID = (Guid) mu.ProviderUserKey;
					ucd.Save(uc);
				}
			}
			else if (uc != null)
				ucd.Delete(uc);
		}

		#endregion
	}
}