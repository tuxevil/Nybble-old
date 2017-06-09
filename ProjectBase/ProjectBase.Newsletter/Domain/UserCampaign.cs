using System;
using ProjectBase.Data;

namespace ProjectBase.Newsletter
{
	/// <summary>
	/// Region object for NHibernate mapped table UserCampaigns.
	/// </summary>
	public class UserCampaign : DomainObject<int>
	{
		private Campaign _CampaignIDCampaign;
		private Guid _UserID;

		public UserCampaign()
		{
		}

		public UserCampaign(Int32 id)
		{
			base.id = id;
		}

		public virtual Guid UserID
		{
			get { return _UserID; }
			set { _UserID = value; }
		}

		public virtual Campaign Campaign
		{
			get { return _CampaignIDCampaign; }
			set { _CampaignIDCampaign = value; }
		}

		public override int GetHashCode()
		{
			return ID.GetHashCode();
		}
	}
}