USE [SM283_DB_Caihongtang_amcor]
GO
/****** Object:  Table [dbo].[UserRole]    Script Date: 05/17/2018 13:58:39 ******/
SET IDENTITY_INSERT [dbo].[UserRole] ON
INSERT [dbo].[UserRole] ([Id], [UserID], [RoleID]) VALUES (1, 1, 1)
SET IDENTITY_INSERT [dbo].[UserRole] OFF
/****** Object:  Table [dbo].[UserInfo]    Script Date: 05/17/2018 13:58:39 ******/
SET IDENTITY_INSERT [dbo].[UserInfo] ON
INSERT [dbo].[UserInfo] ([ID], [UserName], [Password]) VALUES (1, N'admin', N'VVtrsssssFP')
INSERT [dbo].[UserInfo] ([ID], [UserName], [Password]) VALUES (2, N'codeuser', N'yhssssssMNtv')
SET IDENTITY_INSERT [dbo].[UserInfo] OFF
/****** Object:  Table [dbo].[RoleInfo]    Script Date: 05/17/2018 13:58:39 ******/
SET IDENTITY_INSERT [dbo].[RoleInfo] ON
INSERT [dbo].[RoleInfo] ([Id], [RoleName]) VALUES (1, N'管理员')
SET IDENTITY_INSERT [dbo].[RoleInfo] OFF
/****** Object:  Table [dbo].[IPAllowList]    Script Date: 05/17/2018 13:58:39 ******/
SET IDENTITY_INSERT [dbo].[IPAllowList] ON
INSERT [dbo].[IPAllowList] ([Id], [Type], [Version], [Address]) VALUES (1, 1, 4, N'101.227.72.133')
INSERT [dbo].[IPAllowList] ([Id], [Type], [Version], [Address]) VALUES (2, 1, 4, N'103.6.223.136')
INSERT [dbo].[IPAllowList] ([Id], [Type], [Version], [Address]) VALUES (3, 1, 4, N'218.14.115.21')
SET IDENTITY_INSERT [dbo].[IPAllowList] OFF
/****** Object:  Table [dbo].[Config]    Script Date: 05/17/2018 13:58:39 ******/
SET IDENTITY_INSERT [dbo].[Config] ON
INSERT [dbo].[Config] ([Id], [DataType], [Value], [Literal]) VALUES (1, 100, N'loginTitle', N'串码传输平台')
INSERT [dbo].[Config] ([Id], [DataType], [Value], [Literal]) VALUES (2, 101, N'codeAdminEmail', N'jack.song@esmartwave.com')
INSERT [dbo].[Config] ([Id], [DataType], [Value], [Literal]) VALUES (3, 102, N'programmerEmail', N'jack.song@esmartwave.com')
INSERT [dbo].[Config] ([Id], [DataType], [Value], [Literal]) VALUES (4, 103, N'DefaultGetPassEmail', N'jack.song@esmartwave.co')
INSERT [dbo].[Config] ([Id], [DataType], [Value], [Literal]) VALUES (5, 104, N'DefaultBagNum', N'10000')
SET IDENTITY_INSERT [dbo].[Config] OFF
