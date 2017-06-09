using System;
using System.Collections.Generic;
using ProjectBase.Data;
using NHibernate;
using NHibernate.Criterion;

namespace ProjectBase.Newsletter
{
	/// <summary>
	/// Provides DataAccess methods for the <see cref="Campaign">Campaign</see> objects.
	/// </summary>
	class CampaignDao : AbstractNHibernateDao<Campaign , System.Int32>
    {
        public CampaignDao(string sessionFactoryConfigPath) : base(sessionFactoryConfigPath) { }

		public IList<Campaign> GetPendings( string applicationName )
		{
			ICriteria crit = GetCriteria( );
			crit.AddOrder(new Order("StartDate", false))
				.Add(Expression.Eq("ApplicationName", applicationName));
			crit.CreateCriteria("CampaignExecutes").Add(Expression.Le("ExecuteDate", DateTime.Now));

			return crit.List<Campaign>( );
		}

		public IList<Campaign> GetPendingAutomatic( string applicationName , bool onlyFixed, object[] frequencies)
		{
			ICriteria crit = GetCriteria( );

			// Check Schedule Range
			Conjunction range = new Conjunction( );
			range.Add( Expression.Le( "StartDate" , DateTime.Today ) );
			range.Add( Expression.Ge( "EndDate" , DateTime.Today ) );

			// If EndDate is null, avoid range check
			Disjunction nullDate = new Disjunction( );
			nullDate.Add( new NullExpression( "EndDate" ) );
			nullDate.Add( range );

			// Apply filters
			crit.AddOrder( new Order( "StartDate" , false ) )
				.Add( Expression.Eq( "Status" , CampaignStatus.Enabled ) )
				.Add( Expression.Eq( "ApplicationName" , applicationName ) )
				.Add( nullDate );

			crit.Add(new InExpression("Frequency", frequencies));

			// If fixed only, check dynamic code is null
			if( onlyFixed )
				crit.Add( new NullExpression( "DynamicCode" ) );

			return crit.List<Campaign>( );
		}

		public IList<Campaign> GetActive( string applicationName, bool onlyFixed )
		{
			ICriteria crit = GetCriteria( );

			// Check Schedule Range
			Conjunction range = new Conjunction();
			range.Add( Expression.Le( "StartDate" , DateTime.Today ) );
			range.Add( Expression.Ge( "EndDate" , DateTime.Today ) );

			// If EndDate is null, avoid range check
			Disjunction nullDate = new Disjunction( );
			nullDate.Add( new NullExpression( "EndDate" ) );
			nullDate.Add( range );

			// Apply filters
			crit.AddOrder(new Order("StartDate", false))
				.Add(Expression.Eq("Status", CampaignStatus.Enabled))
				.Add(Expression.Eq("ApplicationName", applicationName))
				.Add( nullDate );

			// If fixed only, check dynamic code is null
			if( onlyFixed )
				crit.Add(new NullExpression("DynamicCode"));

			return crit.List<Campaign>( );
		}

		public IList<Campaign> GetActive( string applicationName )
		{
			return GetActive(applicationName, false);
		}

		public IList<Campaign> GetAll( string applicationName )
		{
			return GetAll(applicationName, false);
		}

		public IList<Campaign> GetAll( string applicationName, bool onlyFixed )
		{
			ICriteria crit = GetCriteria( );
			crit.AddOrder( new Order( "StartDate" , false ) )
				.Add( Expression.Eq( "ApplicationName" , applicationName ) );

			if( onlyFixed )
				crit.Add( new NullExpression( "DynamicCode" ) );

			return crit.List<Campaign>( ) as IList<Campaign>;
		}

		public Campaign GetByCode( string applicationName, string code )
		{
			ICriteria crit = GetCriteria( );
			crit.Add( Expression.Eq( "ApplicationName" , applicationName ) )
				.Add( Expression.Eq( "Code" , code ) );
			return crit.UniqueResult( ) as Campaign;
		}
    }
}
