using System;
using System.Collections.Generic;
using ProjectBase.Data;

namespace ProjectBase.Newsletter
{
	/// <summary>
	/// Region object for NHibernate mapped table Campaigns.
	/// </summary>
	public class ProcessExecution : DomainObject<int>
	{
		private Campaign campaign;
		private DateTime _RunDate;
		private String _ApplicationName;

		public ProcessExecution()
		{
		}

		public ProcessExecution( Int32 id )
		{
			base.id = id;
		}

		public virtual String ApplicationName
		{
			get { return _ApplicationName; }
			set { _ApplicationName = value; }
		}

		public virtual DateTime RunDate
		{
			get { return _RunDate; }
			set { _RunDate = value; }
		}

		public virtual Campaign Campaign
		{
			get { return campaign; }
			set { campaign = value; }
		}
	}
}