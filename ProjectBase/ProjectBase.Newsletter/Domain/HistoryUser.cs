using System;
using ProjectBase.Data;

namespace ProjectBase.Newsletter
{
	/// <summary>
	/// Region object for NHibernate mapped table UserCampaigns.
	/// </summary>
	public class HistoryUser : DomainObject<int>
	{
		private History _CampaignID;
		private Guid _UserID;

		public HistoryUser()
		{
		}

		public HistoryUser(Int32 id)
		{
			base.id = id;
		}

		public HistoryUser( History history, Guid userID )
		{
			this._CampaignID = history;
			this._UserID = userID;
		}

		public virtual Guid User
		{
			get { return _UserID; }
			set { _UserID = value; }
		}

		public virtual History History
		{
			get { return _CampaignID; }
			set { _CampaignID = value; }
		}

		public override int GetHashCode()
		{
			return ID.GetHashCode();
		}
	}
}