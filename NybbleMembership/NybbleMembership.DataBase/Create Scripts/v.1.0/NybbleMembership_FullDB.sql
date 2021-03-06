/****** Object:  Table [dbo].[mem_Permission]    Script Date: 05/25/2009 10:20:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[mem_Permission]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
BEGIN
CREATE TABLE [dbo].[mem_Permission](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](256) COLLATE Latin1_General_BIN NOT NULL,
	[Code] [nvarchar](256) COLLATE Latin1_General_BIN NOT NULL,
	[Description] [nvarchar](256) COLLATE Latin1_General_BIN NOT NULL,
	[Action] [int] NOT NULL,
	[ModifiedBy] [uniqueidentifier] NOT NULL,
	[ModifiedOn] [datetime] NOT NULL,
	[CreatedBy] [uniqueidentifier] NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
 CONSTRAINT [PK_mem_Permission] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)
)
END
GO
/****** Object:  Table [dbo].[mem_Site]    Script Date: 05/25/2009 10:20:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[mem_Site]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
BEGIN
CREATE TABLE [dbo].[mem_Site](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](256) COLLATE Latin1_General_BIN NOT NULL,
	[Code] [nvarchar](256) COLLATE Latin1_General_BIN NOT NULL,
	[AppName] [nvarchar](256) COLLATE Latin1_General_BIN NULL,
	[ModifiedBy] [uniqueidentifier] NOT NULL,
	[ModifiedOn] [datetime] NOT NULL,
	[CreatedBy] [uniqueidentifier] NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
 CONSTRAINT [PK_mem_Site] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)
)
END
GO
/****** Object:  Table [dbo].[aspnet_Applications]    Script Date: 05/25/2009 10:20:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[aspnet_Applications]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
BEGIN
CREATE TABLE [dbo].[aspnet_Applications](
	[ApplicationName] [nvarchar](256) COLLATE Latin1_General_BIN NOT NULL,
	[LoweredApplicationName] [nvarchar](256) COLLATE Latin1_General_BIN NOT NULL,
	[ApplicationId] [uniqueidentifier] NOT NULL,
	[Description] [nvarchar](256) COLLATE Latin1_General_BIN NULL,
 CONSTRAINT [PK__aspnet_Applicati__78B3EFCA] PRIMARY KEY NONCLUSTERED 
(
	[ApplicationId] ASC
),
 CONSTRAINT [UQ__aspnet_Applicati__79A81403] UNIQUE NONCLUSTERED 
(
	[LoweredApplicationName] ASC
),
 CONSTRAINT [UQ__aspnet_Applicati__7A9C383C] UNIQUE NONCLUSTERED 
(
	[ApplicationName] ASC
)
)
END
GO
IF NOT EXISTS (SELECT * FROM dbo.sysindexes WHERE id = OBJECT_ID(N'[dbo].[aspnet_Applications]') AND name = N'aspnet_Applications_Index')
CREATE CLUSTERED INDEX [aspnet_Applications_Index] ON [dbo].[aspnet_Applications] 
(
	[LoweredApplicationName] ASC
)
GO
/****** Object:  Table [dbo].[aspnet_Users]    Script Date: 05/25/2009 10:20:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[aspnet_Users]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
BEGIN
CREATE TABLE [dbo].[aspnet_Users](
	[ApplicationId] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[UserName] [nvarchar](256) COLLATE Latin1_General_BIN NOT NULL,
	[LoweredUserName] [nvarchar](256) COLLATE Latin1_General_BIN NOT NULL,
	[MobileAlias] [nvarchar](16) COLLATE Latin1_General_BIN NULL,
	[IsAnonymous] [bit] NOT NULL,
	[LastActivityDate] [datetime] NOT NULL,
 CONSTRAINT [PK__aspnet_Users__7D78A4E7] PRIMARY KEY NONCLUSTERED 
(
	[UserId] ASC
)
)
END
GO
IF NOT EXISTS (SELECT * FROM dbo.sysindexes WHERE id = OBJECT_ID(N'[dbo].[aspnet_Users]') AND name = N'aspnet_Users_Index')
CREATE UNIQUE CLUSTERED INDEX [aspnet_Users_Index] ON [dbo].[aspnet_Users] 
(
	[ApplicationId] ASC,
	[LoweredUserName] ASC
)
GO
IF NOT EXISTS (SELECT * FROM dbo.sysindexes WHERE id = OBJECT_ID(N'[dbo].[aspnet_Users]') AND name = N'aspnet_Users_Index2')
CREATE NONCLUSTERED INDEX [aspnet_Users_Index2] ON [dbo].[aspnet_Users] 
(
	[ApplicationId] ASC,
	[LastActivityDate] ASC
)
GO
/****** Object:  Table [dbo].[aspnet_Membership]    Script Date: 05/25/2009 10:20:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[aspnet_Membership]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
BEGIN
CREATE TABLE [dbo].[aspnet_Membership](
	[ApplicationId] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[Password] [nvarchar](128) COLLATE Latin1_General_BIN NOT NULL,
	[PasswordFormat] [int] NOT NULL,
	[PasswordSalt] [nvarchar](128) COLLATE Latin1_General_BIN NOT NULL,
	[MobilePIN] [nvarchar](16) COLLATE Latin1_General_BIN NULL,
	[Email] [nvarchar](256) COLLATE Latin1_General_BIN NULL,
	[LoweredEmail] [nvarchar](256) COLLATE Latin1_General_BIN NULL,
	[PasswordQuestion] [nvarchar](256) COLLATE Latin1_General_BIN NULL,
	[PasswordAnswer] [nvarchar](128) COLLATE Latin1_General_BIN NULL,
	[IsApproved] [bit] NOT NULL,
	[IsLockedOut] [bit] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[LastLoginDate] [datetime] NOT NULL,
	[LastPasswordChangedDate] [datetime] NOT NULL,
	[LastLockoutDate] [datetime] NOT NULL,
	[FailedPasswordAttemptCount] [int] NOT NULL,
	[FailedPasswordAttemptWindowStart] [datetime] NOT NULL,
	[FailedPasswordAnswerAttemptCount] [int] NOT NULL,
	[FailedPasswordAnswerAttemptWindowStart] [datetime] NOT NULL,
	[Comment] [ntext] COLLATE Latin1_General_BIN NULL,
 CONSTRAINT [PK__aspnet_Membershi__0DAF0CB0] PRIMARY KEY NONCLUSTERED 
(
	[UserId] ASC
)
)
END
GO
IF NOT EXISTS (SELECT * FROM dbo.sysindexes WHERE id = OBJECT_ID(N'[dbo].[aspnet_Membership]') AND name = N'aspnet_Membership_index')
CREATE CLUSTERED INDEX [aspnet_Membership_index] ON [dbo].[aspnet_Membership] 
(
	[ApplicationId] ASC,
	[LoweredEmail] ASC
)
GO
/****** Object:  Table [dbo].[mem_UsersBySite]    Script Date: 05/25/2009 10:20:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[mem_UsersBySite]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
BEGIN
CREATE TABLE [dbo].[mem_UsersBySite](
	[SiteId] [int] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_mem_UsersBySite] PRIMARY KEY CLUSTERED 
(
	[SiteId] ASC,
	[UserId] ASC
)
)
END
GO
/****** Object:  Table [dbo].[mem_UsersByPermissions]    Script Date: 05/25/2009 10:20:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[mem_UsersByPermissions]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
BEGIN
CREATE TABLE [dbo].[mem_UsersByPermissions](
	[PermissionId] [int] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_mem_UsersByPermissions_1] PRIMARY KEY CLUSTERED 
(
	[PermissionId] ASC,
	[UserId] ASC
),
 CONSTRAINT [IX_mem_UsersByPermissions] UNIQUE NONCLUSTERED 
(
	[PermissionId] ASC
)
)
END
GO
/****** Object:  Table [dbo].[mem_Rol]    Script Date: 05/25/2009 10:20:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[mem_Rol]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
BEGIN
CREATE TABLE [dbo].[mem_Rol](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](256) COLLATE Latin1_General_BIN NOT NULL,
	[Description] [nvarchar](256) COLLATE Latin1_General_BIN NULL,
	[IsAdministrator] [bit] NOT NULL,
	[SiteId] [int] NOT NULL,
	[ModifiedBy] [uniqueidentifier] NOT NULL,
	[ModifiedOn] [datetime] NOT NULL,
	[CreatedBy] [uniqueidentifier] NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
 CONSTRAINT [PK_mem_Rol] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)
)
END
GO
/****** Object:  Table [dbo].[mem_UsersByRoles]    Script Date: 05/25/2009 10:20:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[mem_UsersByRoles]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
BEGIN
CREATE TABLE [dbo].[mem_UsersByRoles](
	[RolId] [int] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_mem_UsersByRoles] PRIMARY KEY CLUSTERED 
(
	[RolId] ASC,
	[UserId] ASC
),
 CONSTRAINT [IX_mem_UsersByRoles] UNIQUE NONCLUSTERED 
(
	[RolId] ASC
)
)
END
GO
/****** Object:  Table [dbo].[mem_Function]    Script Date: 05/25/2009 10:20:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[mem_Function]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
BEGIN
CREATE TABLE [dbo].[mem_Function](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](256) COLLATE Latin1_General_BIN NOT NULL,
	[Description] [nvarchar](256) COLLATE Latin1_General_BIN NOT NULL,
	[Parent] [int] NULL,
	[SiteId] [int] NOT NULL,
	[ModifiedBy] [uniqueidentifier] NOT NULL,
	[ModifiedOn] [datetime] NOT NULL,
	[CreatedBy] [uniqueidentifier] NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
 CONSTRAINT [PK_mem_Function] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)
)
END
GO
/****** Object:  Table [dbo].[mem_PermissionByFunction]    Script Date: 05/25/2009 10:20:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[mem_PermissionByFunction]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
BEGIN
CREATE TABLE [dbo].[mem_PermissionByFunction](
	[FunctionId] [int] NOT NULL,
	[PermissionId] [int] NOT NULL,
 CONSTRAINT [PK_mem_PermissionByFunction] PRIMARY KEY CLUSTERED 
(
	[FunctionId] ASC,
	[PermissionId] ASC
),
 CONSTRAINT [IX_mem_PermissionByFunction] UNIQUE NONCLUSTERED 
(
	[FunctionId] ASC
)
)
END
GO
/****** Object:  Table [dbo].[mem_EntityPermission]    Script Date: 05/25/2009 10:20:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[mem_EntityPermission]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
BEGIN
CREATE TABLE [dbo].[mem_EntityPermission](
	[Id] [int] NOT NULL,
	[ClassName] [nvarchar](256) COLLATE Latin1_General_BIN NOT NULL,
	[Identifier] [nvarchar](256) COLLATE Latin1_General_BIN NOT NULL,
 CONSTRAINT [PK__mem_EntityPermis__656C112C] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)
)
END
GO
/****** Object:  Table [dbo].[mem_WebControlPermission]    Script Date: 05/25/2009 10:20:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[mem_WebControlPermission]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
BEGIN
CREATE TABLE [dbo].[mem_WebControlPermission](
	[Id] [int] NOT NULL,
	[RelativePath] [nvarchar](256) COLLATE Latin1_General_BIN NOT NULL,
	[ControlIdentifier] [nvarchar](256) COLLATE Latin1_General_BIN NOT NULL,
 CONSTRAINT [PK__mem_ControlPermi__68487DD7] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)
)
END
GO
/****** Object:  Table [dbo].[mem_PagePermission]    Script Date: 05/25/2009 10:20:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[mem_PagePermission]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
BEGIN
CREATE TABLE [dbo].[mem_PagePermission](
	[Id] [int] NOT NULL,
	[PageName] [nvarchar](256) COLLATE Latin1_General_BIN NOT NULL,
	[FolderName] [nvarchar](256) COLLATE Latin1_General_BIN NOT NULL,
 CONSTRAINT [PK__mem_PagePermissi__6B24EA82] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)
)
END
GO
/****** Object:  Table [dbo].[mem_MethodPermission]    Script Date: 05/25/2009 10:20:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[mem_MethodPermission]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
BEGIN
CREATE TABLE [dbo].[mem_MethodPermission](
	[Id] [int] NOT NULL,
	[MethodName] [nvarchar](256) COLLATE Latin1_General_BIN NOT NULL,
	[ClassName] [nvarchar](256) COLLATE Latin1_General_BIN NOT NULL,
 CONSTRAINT [PK__mem_ObjectPermis__6E01572D] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)
)
END
GO
/****** Object:  Table [dbo].[mem_FunctionByRoles]    Script Date: 05/25/2009 10:20:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[mem_FunctionByRoles]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
BEGIN
CREATE TABLE [dbo].[mem_FunctionByRoles](
	[FunctionId] [int] NOT NULL,
	[RolId] [int] NOT NULL,
 CONSTRAINT [PK_mem_FunctionByRoles] PRIMARY KEY CLUSTERED 
(
	[FunctionId] ASC
),
 CONSTRAINT [IX_mem_FunctionByRoles] UNIQUE NONCLUSTERED 
(
	[RolId] ASC
)
)
END
GO
/****** Object:  Default [DF__aspnet_Me__Passw__108B795B]    Script Date: 05/25/2009 10:20:14 ******/
IF Not EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF__aspnet_Me__Passw__108B795B]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[aspnet_Membership] ADD  CONSTRAINT [DF__aspnet_Me__Passw__108B795B]  DEFAULT (0) FOR [PasswordFormat]

END
GO
/****** Object:  Default [DF__aspnet_Ap__Appli__7B905C75]    Script Date: 05/25/2009 10:20:14 ******/
IF Not EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF__aspnet_Ap__Appli__7B905C75]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[aspnet_Applications] ADD  CONSTRAINT [DF__aspnet_Ap__Appli__7B905C75]  DEFAULT (newid()) FOR [ApplicationId]

END
GO
/****** Object:  Default [DF__aspnet_Us__UserI__7F60ED59]    Script Date: 05/25/2009 10:20:14 ******/
IF Not EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF__aspnet_Us__UserI__7F60ED59]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[aspnet_Users] ADD  CONSTRAINT [DF__aspnet_Us__UserI__7F60ED59]  DEFAULT (newid()) FOR [UserId]

END
GO
/****** Object:  Default [DF__aspnet_Us__Mobil__00551192]    Script Date: 05/25/2009 10:20:14 ******/
IF Not EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF__aspnet_Us__Mobil__00551192]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[aspnet_Users] ADD  CONSTRAINT [DF__aspnet_Us__Mobil__00551192]  DEFAULT (null) FOR [MobileAlias]

END
GO
/****** Object:  Default [DF__aspnet_Us__IsAno__014935CB]    Script Date: 05/25/2009 10:20:14 ******/
IF Not EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF__aspnet_Us__IsAno__014935CB]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[aspnet_Users] ADD  CONSTRAINT [DF__aspnet_Us__IsAno__014935CB]  DEFAULT (0) FOR [IsAnonymous]

END
GO
/****** Object:  ForeignKey [FK__mem_EntityPe__Id__66603565]    Script Date: 05/25/2009 10:20:14 ******/
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[FK__mem_EntityPe__Id__66603565]') AND type = 'F')
ALTER TABLE [dbo].[mem_EntityPermission]  WITH NOCHECK ADD  CONSTRAINT [FK__mem_EntityPe__Id__66603565] FOREIGN KEY([Id])
REFERENCES [dbo].[mem_Permission] ([Id])
GO
ALTER TABLE [dbo].[mem_EntityPermission] CHECK CONSTRAINT [FK__mem_EntityPe__Id__66603565]
GO
/****** Object:  ForeignKey [FK_mem_Function_mem_Function]    Script Date: 05/25/2009 10:20:14 ******/
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[FK_mem_Function_mem_Function]') AND type = 'F')
ALTER TABLE [dbo].[mem_Function]  WITH CHECK ADD  CONSTRAINT [FK_mem_Function_mem_Function] FOREIGN KEY([Parent])
REFERENCES [dbo].[mem_Function] ([Id])
GO
ALTER TABLE [dbo].[mem_Function] CHECK CONSTRAINT [FK_mem_Function_mem_Function]
GO
/****** Object:  ForeignKey [FK_mem_Function_mem_Site]    Script Date: 05/25/2009 10:20:14 ******/
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[FK_mem_Function_mem_Site]') AND type = 'F')
ALTER TABLE [dbo].[mem_Function]  WITH CHECK ADD  CONSTRAINT [FK_mem_Function_mem_Site] FOREIGN KEY([SiteId])
REFERENCES [dbo].[mem_Site] ([Id])
GO
ALTER TABLE [dbo].[mem_Function] CHECK CONSTRAINT [FK_mem_Function_mem_Site]
GO
/****** Object:  ForeignKey [FK__aspnet_Me__Appli__0EA330E9]    Script Date: 05/25/2009 10:20:14 ******/
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__0EA330E9]') AND type = 'F')
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD  CONSTRAINT [FK__aspnet_Me__Appli__0EA330E9] FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
ALTER TABLE [dbo].[aspnet_Membership] CHECK CONSTRAINT [FK__aspnet_Me__Appli__0EA330E9]
GO
/****** Object:  ForeignKey [FK__aspnet_Me__UserI__0F975522]    Script Date: 05/25/2009 10:20:14 ******/
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__UserI__0F975522]') AND type = 'F')
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD  CONSTRAINT [FK__aspnet_Me__UserI__0F975522] FOREIGN KEY([UserId])
REFERENCES [dbo].[aspnet_Users] ([UserId])
GO
ALTER TABLE [dbo].[aspnet_Membership] CHECK CONSTRAINT [FK__aspnet_Me__UserI__0F975522]
GO
/****** Object:  ForeignKey [FK_mem_FunctionByRoles_mem_Function]    Script Date: 05/25/2009 10:20:14 ******/
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[FK_mem_FunctionByRoles_mem_Function]') AND type = 'F')
ALTER TABLE [dbo].[mem_FunctionByRoles]  WITH NOCHECK ADD  CONSTRAINT [FK_mem_FunctionByRoles_mem_Function] FOREIGN KEY([FunctionId])
REFERENCES [dbo].[mem_Function] ([Id])
GO
ALTER TABLE [dbo].[mem_FunctionByRoles] CHECK CONSTRAINT [FK_mem_FunctionByRoles_mem_Function]
GO
/****** Object:  ForeignKey [FK_mem_FunctionByRoles_mem_Rol]    Script Date: 05/25/2009 10:20:14 ******/
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[FK_mem_FunctionByRoles_mem_Rol]') AND type = 'F')
ALTER TABLE [dbo].[mem_FunctionByRoles]  WITH NOCHECK ADD  CONSTRAINT [FK_mem_FunctionByRoles_mem_Rol] FOREIGN KEY([RolId])
REFERENCES [dbo].[mem_Rol] ([Id])
GO
ALTER TABLE [dbo].[mem_FunctionByRoles] CHECK CONSTRAINT [FK_mem_FunctionByRoles_mem_Rol]
GO
/****** Object:  ForeignKey [FK__mem_ObjectPe__Id__6EF57B66]    Script Date: 05/25/2009 10:20:14 ******/
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[FK__mem_ObjectPe__Id__6EF57B66]') AND type = 'F')
ALTER TABLE [dbo].[mem_MethodPermission]  WITH NOCHECK ADD  CONSTRAINT [FK__mem_ObjectPe__Id__6EF57B66] FOREIGN KEY([Id])
REFERENCES [dbo].[mem_Permission] ([Id])
GO
ALTER TABLE [dbo].[mem_MethodPermission] CHECK CONSTRAINT [FK__mem_ObjectPe__Id__6EF57B66]
GO
/****** Object:  ForeignKey [FK__mem_PagePerm__Id__6C190EBB]    Script Date: 05/25/2009 10:20:14 ******/
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[FK__mem_PagePerm__Id__6C190EBB]') AND type = 'F')
ALTER TABLE [dbo].[mem_PagePermission]  WITH NOCHECK ADD  CONSTRAINT [FK__mem_PagePerm__Id__6C190EBB] FOREIGN KEY([Id])
REFERENCES [dbo].[mem_Permission] ([Id])
GO
ALTER TABLE [dbo].[mem_PagePermission] CHECK CONSTRAINT [FK__mem_PagePerm__Id__6C190EBB]
GO
/****** Object:  ForeignKey [FK_mem_PermissionByFunction_mem_Function]    Script Date: 05/25/2009 10:20:14 ******/
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[FK_mem_PermissionByFunction_mem_Function]') AND type = 'F')
ALTER TABLE [dbo].[mem_PermissionByFunction]  WITH NOCHECK ADD  CONSTRAINT [FK_mem_PermissionByFunction_mem_Function] FOREIGN KEY([FunctionId])
REFERENCES [dbo].[mem_Function] ([Id])
GO
ALTER TABLE [dbo].[mem_PermissionByFunction] CHECK CONSTRAINT [FK_mem_PermissionByFunction_mem_Function]
GO
/****** Object:  ForeignKey [FK_mem_PermissionByFunction_mem_Permission]    Script Date: 05/25/2009 10:20:14 ******/
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[FK_mem_PermissionByFunction_mem_Permission]') AND type = 'F')
ALTER TABLE [dbo].[mem_PermissionByFunction]  WITH NOCHECK ADD  CONSTRAINT [FK_mem_PermissionByFunction_mem_Permission] FOREIGN KEY([PermissionId])
REFERENCES [dbo].[mem_Permission] ([Id])
GO
ALTER TABLE [dbo].[mem_PermissionByFunction] CHECK CONSTRAINT [FK_mem_PermissionByFunction_mem_Permission]
GO
/****** Object:  ForeignKey [FK_mem_Rol_mem_Site]    Script Date: 05/25/2009 10:20:14 ******/
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[FK_mem_Rol_mem_Site]') AND type = 'F')
ALTER TABLE [dbo].[mem_Rol]  WITH CHECK ADD  CONSTRAINT [FK_mem_Rol_mem_Site] FOREIGN KEY([SiteId])
REFERENCES [dbo].[mem_Site] ([Id])
GO
ALTER TABLE [dbo].[mem_Rol] CHECK CONSTRAINT [FK_mem_Rol_mem_Site]
GO
/****** Object:  ForeignKey [FK_mem_UsersByPermissions_MembershipUser]    Script Date: 05/25/2009 10:20:14 ******/
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[FK_mem_UsersByPermissions_MembershipUser]') AND type = 'F')
ALTER TABLE [dbo].[mem_UsersByPermissions]  WITH CHECK ADD  CONSTRAINT [FK_mem_UsersByPermissions_MembershipUser] FOREIGN KEY([UserId])
REFERENCES [dbo].[aspnet_Membership] ([UserId])
GO
ALTER TABLE [dbo].[mem_UsersByPermissions] CHECK CONSTRAINT [FK_mem_UsersByPermissions_MembershipUser]
GO
/****** Object:  ForeignKey [FK_mem_UsersByPermissions_mem_Permission]    Script Date: 05/25/2009 10:20:14 ******/
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[FK_mem_UsersByPermissions_mem_Permission]') AND type = 'F')
ALTER TABLE [dbo].[mem_UsersByPermissions]  WITH CHECK ADD  CONSTRAINT [FK_mem_UsersByPermissions_mem_Permission] FOREIGN KEY([PermissionId])
REFERENCES [dbo].[mem_Permission] ([Id])
GO
ALTER TABLE [dbo].[mem_UsersByPermissions] CHECK CONSTRAINT [FK_mem_UsersByPermissions_mem_Permission]
GO
/****** Object:  ForeignKey [FK_mem_UsersByRoles_Users]    Script Date: 05/25/2009 10:20:14 ******/
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[FK_mem_UsersByRoles_Users]') AND type = 'F')
ALTER TABLE [dbo].[mem_UsersByRoles]  WITH CHECK ADD  CONSTRAINT [FK_mem_UsersByRoles_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[aspnet_Membership] ([UserId])
GO
ALTER TABLE [dbo].[mem_UsersByRoles] CHECK CONSTRAINT [FK_mem_UsersByRoles_Users]
GO
/****** Object:  ForeignKey [FK_mem_UsersByRoles_mem_Rol]    Script Date: 05/25/2009 10:20:14 ******/
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[FK_mem_UsersByRoles_mem_Rol]') AND type = 'F')
ALTER TABLE [dbo].[mem_UsersByRoles]  WITH NOCHECK ADD  CONSTRAINT [FK_mem_UsersByRoles_mem_Rol] FOREIGN KEY([RolId])
REFERENCES [dbo].[mem_Rol] ([Id])
GO
ALTER TABLE [dbo].[mem_UsersByRoles] CHECK CONSTRAINT [FK_mem_UsersByRoles_mem_Rol]
GO
/****** Object:  ForeignKey [FK_mem_UsersBySite_MembershipUser]    Script Date: 05/25/2009 10:20:14 ******/
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[FK_mem_UsersBySite_MembershipUser]') AND type = 'F')
ALTER TABLE [dbo].[mem_UsersBySite]  WITH CHECK ADD  CONSTRAINT [FK_mem_UsersBySite_MembershipUser] FOREIGN KEY([UserId])
REFERENCES [dbo].[aspnet_Membership] ([UserId])
GO
ALTER TABLE [dbo].[mem_UsersBySite] CHECK CONSTRAINT [FK_mem_UsersBySite_MembershipUser]
GO
/****** Object:  ForeignKey [FK_mem_UsersBySite_mem_Site]    Script Date: 05/25/2009 10:20:14 ******/
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[FK_mem_UsersBySite_mem_Site]') AND type = 'F')
ALTER TABLE [dbo].[mem_UsersBySite]  WITH NOCHECK ADD  CONSTRAINT [FK_mem_UsersBySite_mem_Site] FOREIGN KEY([SiteId])
REFERENCES [dbo].[mem_Site] ([Id])
GO
ALTER TABLE [dbo].[mem_UsersBySite] CHECK CONSTRAINT [FK_mem_UsersBySite_mem_Site]
GO
/****** Object:  ForeignKey [FK__mem_ControlP__Id__693CA210]    Script Date: 05/25/2009 10:20:14 ******/
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[FK__mem_ControlP__Id__693CA210]') AND type = 'F')
ALTER TABLE [dbo].[mem_WebControlPermission]  WITH NOCHECK ADD  CONSTRAINT [FK__mem_ControlP__Id__693CA210] FOREIGN KEY([Id])
REFERENCES [dbo].[mem_Permission] ([Id])
GO
ALTER TABLE [dbo].[mem_WebControlPermission] CHECK CONSTRAINT [FK__mem_ControlP__Id__693CA210]
GO
/****** Object:  ForeignKey [FK__aspnet_Us__Appli__7E6CC920]    Script Date: 05/25/2009 10:20:14 ******/
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__Appli__7E6CC920]') AND type = 'F')
ALTER TABLE [dbo].[aspnet_Users]  WITH CHECK ADD  CONSTRAINT [FK__aspnet_Us__Appli__7E6CC920] FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
ALTER TABLE [dbo].[aspnet_Users] CHECK CONSTRAINT [FK__aspnet_Us__Appli__7E6CC920]
GO
