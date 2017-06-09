using System;
using System.Collections.Generic;
using ProjectBase.Data;

namespace ProjectBase.Newsletter
{
	/// <summary>
	/// Region object for NHibernate mapped table Campaigns.
	/// </summary>
	public class Campaign : DomainObject<int>
	{
		private String _ApplicationName;
		private String _Code;

		private String _DynamicCode;
		private DateTime? _StartDate;
		private DateTime? _EndDate;
		private Newsletter _FixedNewsletterIDNewsletters;
		private MailFrequency _Frequency;
		private String _Name;
		private CampaignStatus _Status;
		private CampaignType _Type;
		private IList<UserCampaign> _UserCampaigns = new List<UserCampaign>();
		private IList<ProcessExecution> processExecutions;

		public Campaign()
		{
		}

		public Campaign(Int32 id)
		{
			base.id = id;
		}

		public virtual String Name
		{
			get { return _Name; }
			set { _Name = value; }
		}

		public virtual String ApplicationName
		{
			get { return _ApplicationName; }
			set { _ApplicationName = value; }
		}

		public virtual CampaignType Type
		{
			get { return _Type; }
			set { _Type = value; }
		}

		public virtual CampaignStatus Status
		{
			get { return _Status; }
			set { _Status = value; }
		}

		public virtual DateTime? StartDate
		{
			get { return _StartDate; }
			set { _StartDate = value; }
		}

		public virtual DateTime? EndDate
		{
			get { return _EndDate; }
			set { _EndDate = value; }
		}

		public virtual MailFrequency Frequency
		{
			get { return _Frequency; }
			set { _Frequency = value; }
		}

		public virtual String Code
		{
			get { return _Code; }
			set { _Code = value; }
		}

		public virtual Newsletter FixedNewsletter
		{
			get { return _FixedNewsletterIDNewsletters; }
			set { _FixedNewsletterIDNewsletters = value; }
		}

		public virtual String DynamicCode
		{
			get { return _DynamicCode; }
			set { _DynamicCode = value; }
		}

		public virtual IList<UserCampaign> UserCampaigns
		{
			get { return _UserCampaigns; }
			set { _UserCampaigns = value; }
		}

		public virtual IList<ProcessExecution> ProcessExecutions
		{
			get { return processExecutions; }
			set { processExecutions = value; }
		}

		public override int GetHashCode()
		{
			return ID.GetHashCode();
		}

		public override string ToString()
		{
			return "[" + ID + ":" + Code + "]";
		}
	}
}