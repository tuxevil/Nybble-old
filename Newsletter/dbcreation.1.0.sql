set xact_abort on
go

begin transaction
go

create table dbo.camCampaignHistory(
  CampaignHistoryID int           not null identity constraint PK_CampaignHistory primary key,
  CampaignID        int           not null,
  Subject           nvarchar(128),
  Body              ntext,
  SentDate          datetime      not null
)
go

create table dbo.camCampaignHistoryUsers(
  CampaignUserHistoryID int              not null identity constraint PK_CampaignHistoryUsers primary key,
  CampaignHistoryID     int              not null,
  UserID                uniqueidentifier not null
)
go

alter table dbo.camCampaignHistoryUsers add
  constraint FK_CampaignHistoryUsers_CampaignHistory foreign key(CampaignHistoryID) references dbo.camCampaignHistory(CampaignHistoryID)
go

create table dbo.camCampaigns(
  CampaignID        int           not null identity constraint PK_Campaigns primary key,
  [Name]            nvarchar(64)  not null,
  ApplicationName   nvarchar(256) not null,
  Type              int           not null,
  Status            int,
  StartDate         smalldatetime,
  EndDate           smalldatetime,
  Frequency         int           not null,
  Code              nvarchar(64)  not null,
  FixedNewsletterID int,
  DynamicCode       nvarchar(256)
)
go

create table dbo.camCampaignUsers(
  UserCampaignID int              not null identity constraint PK_MailCampaigns_1 primary key,
  CampaignID     int              not null,
  UserID         uniqueidentifier not null
)
go

alter table dbo.camCampaignUsers add
  constraint FK_UserCampaigns_Campaigns foreign key(CampaignID) references dbo.camCampaigns(CampaignID)
go

create table dbo.camExecute(
  ExecuteID     int      not null identity constraint PK_camExecute primary key,
  CampaignID    int      not null,
  RunDate       datetime not null,
  DateCreated   datetime not null,
  TestExecution bit      not null
)
go

create table dbo.camFrequencies(
  FrequencyID     int          not null identity constraint PK_CampaignFrequencies primary key,
  FrequencyCode   int          not null,
  ApplicationName nvarchar(64) not null
)
go

create table dbo.camNewsletters(
  NewsletterID   int           not null identity constraint PK_Newsletters primary key,
  NewsletterType int           not null,
  Subject        nvarchar(128) not null,
  Body           ntext         not null
)
go

create table dbo.camProcessExecution(
  ProcessExecutionID int           not null identity constraint PK_camProcessExecution primary key,
  CampaignID         int           not null,
  RunDate            smalldatetime not null,
  ApplicationName    nvarchar(50)  not null
)
go

alter table dbo.camProcessExecution add
  constraint FK_camProcessExecution_camCampaigns foreign key(CampaignID) references dbo.camCampaigns(CampaignID)
go

create index IX_camProcessExecution on dbo.camProcessExecution(ApplicationName,RunDate)
go

-- The script may need editing: Some other object must have been synchronized first!
go

alter table dbo.camCampaignUsers add
  constraint FK_UserCampaigns_aspnet_Users foreign key(UserID) references dbo.aspnet_Users(UserId)
go

alter table dbo.camCampaigns add
  constraint FK_Campaigns_Newsletters foreign key(FixedNewsletterID) references dbo.camNewsletters(NewsletterID)
go

alter table dbo.camCampaignHistory add
  constraint FK_CampaignHistory_Campaigns foreign key(CampaignID) references dbo.camCampaigns(CampaignID)
go

commit
go


