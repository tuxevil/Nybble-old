using System;
using System.Collections.Generic;
using ProjectBase.Data;
using NHibernate;
using NHibernate.Criterion;

namespace ProjectBase.Newsletter
{
	/// <summary>
	/// Provides DataAccess methods for the <see cref="UserCampaign">User Campaign</see> objects.
	/// </summary>
	class UserCampaignDao : AbstractNHibernateDao<UserCampaign , System.Int32>
    {
        public UserCampaignDao(string sessionFactoryConfigPath) : base(sessionFactoryConfigPath) { }

		public IList<UserCampaign> GetByUser( Guid user )
        {
            ICriteria crit = GetCriteria();
            crit.Add(Expression.Eq("UserID", user));
            return crit.List<UserCampaign>() as List<UserCampaign>;

        }

        public UserCampaign GetByUser(Guid user, int campaignID)
        {
            ICriteria crit = GetCriteria();
            crit.Add(Expression.Eq("UserID", user)).CreateCriteria("Campaign")
				.Add( Expression.Eq( "ID" , campaignID ) );

            return crit.UniqueResult() as UserCampaign;
        }

		public IList<UserCampaign> GetByCampaign( int campaignID )
		{
			ICriteria crit = GetCriteria( );
			crit.CreateCriteria( "Campaign" ).Add( Expression.Eq( "ID" , campaignID ) );
			return crit.List<UserCampaign>( );
		}
    }
}
