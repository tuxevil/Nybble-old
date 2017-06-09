using ProjectBase.Utils;

namespace ProjectBase.Newsletter
{
	/// <summary>
	/// Exposes access to NHibernate DAO classes.  Motivation for this DAO
	/// framework can be found at http://www.hibernate.org/328.html.
	/// </summary>
	class NewsletterDaoFactory
	{
		public NewsletterDaoFactory( string sessionFactoryConfigPath )
		{
			Check.Require( sessionFactoryConfigPath != null , "sessionFactoryConfigPath may not be null" );
			SessionFactoryConfigPath = sessionFactoryConfigPath;
		}

		private string sessionFactoryConfigPath;
		protected string SessionFactoryConfigPath
		{
			get
			{
				return sessionFactoryConfigPath;
			}
			set
			{
				sessionFactoryConfigPath = value;
			}
		}

		public CampaignDao GetCampaignDao( )
		{
			return new CampaignDao( sessionFactoryConfigPath );
		}

		public UserCampaignDao GetUserCampaignDao( )
		{
			return new UserCampaignDao( sessionFactoryConfigPath );
		}
	}
}
