namespace ProjectBase.Newsletter
{
	/// <summary>
	/// Mail frequency available
	/// </summary>
	public enum MailFrequency
	{
		[EnumDescription( "Diario" )]
		Daily = 1 ,
		[EnumDescription( "Semanal" )]
		Weekly = 2,
		[EnumDescription( "Mensual" )]
		Monthly = 3,
		[EnumDescription( "Por tiempo" )]
		TimeSpan = 4,
		[EnumDescription( "Manual" )]
		Manual = 5
	}

	/// <summary>
	/// Status of a campaign
	/// </summary>
	public enum CampaignStatus
	{
		[EnumDescription( "Activo" )]
		Enabled = 1,
		[EnumDescription( "Inactivo" )]
		Disabled = 0
	}

	/// <summary>
	/// Status of a campaign
	/// </summary>
	public enum CampaignType
	{
		[EnumDescription( "Subscriptos" )]
		Subscription = 1,
		[EnumDescription( "Todos los usuarios" )]
		AllUsers = 2
	}

	/// <summary>
	/// Status of a campaign
	/// </summary>
	public enum NewsletterType
	{
		[EnumDescription( "Unico" )]
		Default = 1
	}

}