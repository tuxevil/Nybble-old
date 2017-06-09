using System;
using System.Collections.Generic;
using ProjectBase.Data;

namespace ProjectBase.Newsletter
{
	/// <summary>
	/// Region object for NHibernate mapped table Campaigns.
	/// </summary>
	public class History : DomainObject<int>
	{
		private String _Body;
		private Campaign _Campaign;
		private DateTime _SentDate;
		private String _Subject;

		private IList<HistoryUser> _UserCampaigns = new List<HistoryUser>();

		public History()
		{
		}

		public History(Int32 id)
		{
			base.id = id;
		}

		public virtual DateTime SentDate
		{
			get { return _SentDate; }
			set { _SentDate = value; }
		}

		public virtual Campaign Campaign
		{
			get { return _Campaign; }
			set { _Campaign = value; }
		}

		public virtual String Subject
		{
			get { return _Subject; }
			set { _Subject = value; }
		}

		public virtual String Body
		{
			get { return _Body; }
			set { _Body = value; }
		}

		public virtual IList<HistoryUser> Users
		{
			get { return _UserCampaigns; }
			set { _UserCampaigns = value; }
		}

		public override int GetHashCode()
		{
			return ID.GetHashCode();
		}
	}
}