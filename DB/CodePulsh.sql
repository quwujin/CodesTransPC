USE [YS152_DB_DNQF_ZQSH]
GO
/****** Object:  StoredProcedure [dbo].[sp_Page]    Script Date: 05/16/2018 19:31:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Page]
(
@tblName   varchar(255),       -- 表名
@CountFields varchar(20)='a.id', ----'主健，统计用'
@ReturnFileds varchar(1000) = '*',  -- 外层需要返回的列
@SelectFileds varchar(1000)='',      -- 内层要返回的列
@PageSize   int = 40,          -- 页尺寸
@PageIndex  int = 1,           -- 页码
@doCount  bit = 0,   -- 返回记录总数, 非 0 值则返回
@strWhere  varchar(1500)='',  -- 查询条件 (注意: 不要加 where)
@JoinTable  varchar(4500)='' , -- 查询条件 (注意: 不要加 where)
@OrderString varchar(1500)=' order by id desc'
)
AS
declare @strSQL   varchar(5000)       -- 主语句
declare @strTmp   varchar(110)        -- 临时变量
declare @strOrder varchar(400)        -- 排序类型
 
if @doCount != 0
	 begin

		set @strSQL ='	SELECT   count('+@CountFields+ ') '
			+'	FROM '
			+@tblName+' a '
			+' '+@JoinTable
			+' where 1=1 '+ @strWhere  
end 
else

	begin
		declare @BNum int
		declare @ENum int

		set @BNum = (@PageIndex-1)*@PageSize+1
		set @ENum = @PageIndex*@PageSize


		set @strSQL ='	SELECT   '+@ReturnFileds+ ' '
			+'	FROM '
			+'	(select top 9999999 ROW_NUMBER() Over('+@OrderString+' ) as rowNum, '+ @SelectFileds +' from '+@tblName+' a '
			+' '+@JoinTable
			+' where 1=1 '+ @strWhere  
			+'	) as t '
			+'	where rowNum>= '+ convert(varchar(10),@BNum) +' and rowNum<= '+convert(varchar(10),@ENum)  +'order by rowNum'
end
exec(@strSQL)
GO
/****** Object:  Table [dbo].[RoleInfo]    Script Date: 05/16/2018 19:30:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoleInfo](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [nvarchar](500) COLLATE Chinese_PRC_CI_AS NULL,
 CONSTRAINT [PK_ROLEINFO] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[IPAllowList]    Script Date: 05/16/2018 19:30:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[IPAllowList](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Type] [int] NULL,
	[Version] [int] NULL,
	[Address] [nvarchar](500) COLLATE Chinese_PRC_CI_AS NULL,
 CONSTRAINT [PK_IPALLOWLIST] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ExecutePlan]    Script Date: 05/16/2018 19:30:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ExecutePlan](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CodeType] [int] NULL,
	[MaxCount] [int] NULL,
	[EachCount] [int] NULL,
	[BatchCodeBegin] [int] NULL,
	[EffectiveDateBegin] [datetime] NULL,
	[GeneratedCount] [int] NULL,
	[CurrentBatchCode] [int] NULL,
	[Status] [int] NULL,
	[CustomerEmail] [nvarchar](500) COLLATE Chinese_PRC_CI_AS NULL,
 CONSTRAINT [PK_EXECUTEPLAN] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DownloadLog]    Script Date: 05/16/2018 19:30:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DownloadLog](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IPAddress] [nvarchar](500) COLLATE Chinese_PRC_CI_AS NULL,
	[Url] [nvarchar](4000) COLLATE Chinese_PRC_CI_AS NULL,
	[UserId] [int] NULL,
	[CreatedTime] [datetime] NULL,
	[UpdatedTime] [datetime] NULL,
 CONSTRAINT [PK_DOWNLOADLOG] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Config]    Script Date: 05/16/2018 19:30:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Config](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DataType] [int] NULL,
	[Value] [nvarchar](4000) COLLATE Chinese_PRC_CI_AS NULL,
	[Literal] [nvarchar](4000) COLLATE Chinese_PRC_CI_AS NULL,
 CONSTRAINT [PK_CONFIG] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[codedata15]    Script Date: 05/16/2018 19:30:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[codedata15](
	[CodeDataId] [int] IDENTITY(1,1) NOT NULL,
	[CodeData] [varchar](50) COLLATE Chinese_PRC_CS_AS NULL,
	[ShortCodeData] [varchar](50) COLLATE Chinese_PRC_CS_AS NULL,
	[CodeIndex] [varchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[BatchNumber] [varchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[BoxNumberId] [int] NULL,
	[ContentStatusId] [int] NULL,
	[UpdateOn] [datetime] NULL,
	[Createon] [datetime] NULL,
	[ApployInfoId] [int] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CodeData]    Script Date: 05/16/2018 19:30:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CodeData](
	[CodeDataId] [int] IDENTITY(1,1) NOT NULL,
	[CodeData] [varchar](50) COLLATE Chinese_PRC_CS_AS NULL,
	[ShortCodeData] [varchar](50) COLLATE Chinese_PRC_CS_AS NULL,
	[CodeIndex] [varchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[BatchNumber] [varchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[BoxNumberId] [int] NULL,
	[ContentStatusId] [int] NULL,
	[UpdateOn] [datetime] NULL,
	[Createon] [datetime] NULL,
	[ApployInfoId] [int] NULL,
 CONSTRAINT [PK_CodeData] PRIMARY KEY CLUSTERED 
(
	[CodeDataId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_CodeData] ON [dbo].[CodeData] 
(
	[CodeData] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_CodeData_App] ON [dbo].[CodeData] 
(
	[ApployInfoId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BoxNumber]    Script Date: 05/16/2018 19:30:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[BoxNumber](
	[BoxNumberId] [int] IDENTITY(1,1) NOT NULL,
	[BoxNumber] [varchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[ShortBoxNumber] [varchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[IndexCode] [varchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[BatchNumber] [varchar](100) COLLATE Chinese_PRC_CI_AS NULL,
	[ContentStatusId] [varchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[IsBind] [bit] NULL,
	[BindTime] [datetime] NULL,
	[UpdateOn] [datetime] NULL,
	[CreateOn] [datetime] NULL,
	[ApployInfoId] [int] NULL,
 CONSTRAINT [PK_BoxNumber] PRIMARY KEY CLUSTERED 
(
	[BoxNumberId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
CREATE NONCLUSTERED INDEX [IX_BoxNumber] ON [dbo].[BoxNumber] 
(
	[BoxNumber] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BoxLog]    Script Date: 05/16/2018 19:30:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BoxLog](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OpenId] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[BoxNumber] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[Text] [nvarchar](20) COLLATE Chinese_PRC_CI_AS NULL,
	[CreateTime] [datetime] NULL,
	[IP] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NULL,
 CONSTRAINT [PK_BoxLog] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BindUser]    Script Date: 05/16/2018 19:30:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BindUser](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OpenId] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[Mob] [nvarchar](12) COLLATE Chinese_PRC_CI_AS NULL,
	[CreateTime] [datetime] NULL,
	[StatusId] [int] NULL,
	[Ip] [nvarchar](20) COLLATE Chinese_PRC_CI_AS NULL,
 CONSTRAINT [PK_BindUser] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ApployInfo]    Script Date: 05/16/2018 19:30:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ApployInfo](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](4000) COLLATE Chinese_PRC_CI_AS NULL,
	[FileName] [nvarchar](500) COLLATE Chinese_PRC_CI_AS NULL,
	[ApployTime] [datetime] NULL,
	[BathCode] [nvarchar](500) COLLATE Chinese_PRC_CI_AS NULL,
	[Secret] [nvarchar](500) COLLATE Chinese_PRC_CI_AS NULL,
	[CustomerEmail] [nvarchar](500) COLLATE Chinese_PRC_CI_AS NULL,
	[Type] [int] NULL,
	[Url] [nvarchar](500) COLLATE Chinese_PRC_CI_AS NULL,
	[Status] [int] NULL,
	[CreatedTime] [datetime] NULL,
	[UpdateTime] [datetime] NULL,
	[DownTime] [datetime] NULL,
	[CompleteTime] [datetime] NULL,
	[Remarks] [nvarchar](4000) COLLATE Chinese_PRC_CI_AS NULL,
	[AutoCheck] [bit] NULL,
	[CheckResult] [varchar](200) COLLATE Chinese_PRC_CI_AS NULL,
 CONSTRAINT [PK_APPLOYINFO] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ApployCheckLog]    Script Date: 05/16/2018 19:30:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ApployCheckLog](
	[CheckLogId] [int] IDENTITY(1,1) NOT NULL,
	[ApployId] [int] NULL,
	[TypeCode] [varchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[FileName] [varchar](500) COLLATE Chinese_PRC_CI_AS NULL,
	[Number] [bigint] NULL,
	[Message] [varchar](500) COLLATE Chinese_PRC_CI_AS NULL,
	[CreateOn] [datetime] NULL,
	[UpdateOn] [datetime] NULL,
 CONSTRAINT [PK_ApployCheckLog] PRIMARY KEY CLUSTERED 
(
	[CheckLogId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ActionLog]    Script Date: 05/16/2018 19:30:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ActionLog](
	[ActionLogId] [int] IDENTITY(1,1) NOT NULL,
	[ActionName] [varchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[UserName] [varchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[ActionResult] [varchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[KeyData] [varchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[Notes] [varchar](5000) COLLATE Chinese_PRC_CI_AS NULL,
	[CreateOn] [datetime] NULL,
 CONSTRAINT [PK_ActionLog] PRIMARY KEY CLUSTERED 
(
	[ActionLogId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[UserRole]    Script Date: 05/16/2018 19:30:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRole](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NULL,
	[RoleID] [int] NULL,
 CONSTRAINT [PK_USERROLE] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserInfo]    Script Date: 05/16/2018 19:30:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserInfo](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[Password] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
 CONSTRAINT [PK_USERINFO] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UpInfo]    Script Date: 05/16/2018 19:30:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UpInfo](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](4000) COLLATE Chinese_PRC_CI_AS NULL,
	[FileName] [nvarchar](500) COLLATE Chinese_PRC_CI_AS NULL,
	[ApployTime] [datetime] NULL,
	[BathCode] [nvarchar](500) COLLATE Chinese_PRC_CI_AS NULL,
	[Secret] [nvarchar](500) COLLATE Chinese_PRC_CI_AS NULL,
	[CustomerEmail] [nvarchar](500) COLLATE Chinese_PRC_CI_AS NULL,
	[Type] [int] NULL,
	[Url] [nvarchar](500) COLLATE Chinese_PRC_CI_AS NULL,
	[Status] [int] NULL,
	[CreatedTime] [datetime] NULL,
	[UpdateTime] [datetime] NULL,
	[DownTime] [datetime] NULL,
	[CompleteTime] [datetime] NULL,
	[Remarks] [nvarchar](4000) COLLATE Chinese_PRC_CI_AS NULL,
 CONSTRAINT [PK_UPINFO] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UpFileAPI]    Script Date: 05/16/2018 19:30:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[UpFileAPI](
	[UpFileId] [int] IDENTITY(1,1) NOT NULL,
	[FileName] [varchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[FilePath] [varchar](100) COLLATE Chinese_PRC_CI_AS NULL,
	[DataFileName] [varchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[DataFilePath] [varchar](100) COLLATE Chinese_PRC_CI_AS NULL,
	[IsBind] [bit] NULL,
	[ResponseCode] [int] NULL,
	[ResponseMsg] [varchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[UpdateOn] [datetime] NULL,
	[CreateOn] [datetime] NULL,
 CONSTRAINT [PK_UpFileAPI] PRIMARY KEY CLUSTERED 
(
	[UpFileId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  StoredProcedure [dbo].[spRecoverData]    Script Date: 05/16/2018 19:31:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spRecoverData]
	@apployInfoId int
AS
-------------------------------------------------------------------------------------------------------------------------------------
-- Summary: 恢复串码（或箱码）资源
-- Author: Titus Zhu
-- Created Date: 2017-02-03
-------------------------------------------------------------------------------------------------------------------------------------
BEGIN
	IF EXISTS(select * from ApployInfo where id = @apployInfoId)
	BEGIN
		
		DECLARE @type int
		DECLARE @count int 
		DECLARE @strType nvarchar(500)
		DECLARE @CompleteTime datetime
		DECLARE @Status int

		SELECT @type = [Type], @CompleteTime = CompleteTime, @Status = [Status] FROM ApployInfo WHERE id = @apployInfoId
		
		IF @CompleteTime IS NOT NULL
		BEGIN
			IF @Status = 0 
			BEGIN
				IF @type = 1 
				BEGIN
					SELECT @strType = '串码'

					SELECT @count = COUNT(1)
					FROM CodeData
					WHERE ContentStatusId = 1 AND ApployInfoId = @apployInfoId

					UPDATE CodeData SET ContentStatusId = 2, ApployInfoId = NULL
					WHERE ContentStatusId = 1 AND ApployInfoId = @apployInfoId

				END
				ELSE
				BEGIN
					SELECT @strType = '箱码'

					SELECT @count = COUNT(1)
					FROM BoxNumber
					WHERE ContentStatusId = 1 AND ApployInfoId = @apployInfoId

					UPDATE BoxNumber SET ContentStatusId = 2, ApployInfoId = NULL
					WHERE ContentStatusId = 1 AND ApployInfoId = @apployInfoId
				END
		
				DELETE FROM ApployInfo WHERE Id = @apployInfoId

				PRINT '已恢复' + cast(@count as nvarchar(500)) + @strType
			END
			ELSE IF @Status = 2
			BEGIN
				PRINT '发布ID:' +  cast(@apployInfoId as nvarchar(40)) + '是已经下载确认的版本，不允许恢复数据！'
			END
			ELSE
			BEGIN
				PRINT '发布ID:' +  cast(@apployInfoId as nvarchar(40)) + '的状态，不允许恢复！请检查该记录是否确实需要恢复'
			END
		END
		ELSE
		BEGIN
			PRINT '发布ID:' + cast(@apployInfoId as nvarchar(40)) + '尚未执行完毕'
		END
	END
	ELSE
	BEGIN
		PRINT '不存在该发布ID'
	END

END
GO
/****** Object:  Default [DF__ApployInf__Statu__1BFD2C07]    Script Date: 05/16/2018 19:30:55 ******/
ALTER TABLE [dbo].[ApployInfo] ADD  DEFAULT ((0)) FOR [Status]
GO
/****** Object:  Default [DF__DownloadL__Creat__1CF15040]    Script Date: 05/16/2018 19:30:56 ******/
ALTER TABLE [dbo].[DownloadLog] ADD  DEFAULT (getdate()) FOR [CreatedTime]
GO
/****** Object:  Default [DF__ExecutePl__CodeT__1DE57479]    Script Date: 05/16/2018 19:30:56 ******/
ALTER TABLE [dbo].[ExecutePlan] ADD  DEFAULT ((1)) FOR [CodeType]
GO
/****** Object:  Default [DF__IPAllowLis__Type__1ED998B2]    Script Date: 05/16/2018 19:30:56 ******/
ALTER TABLE [dbo].[IPAllowList] ADD  DEFAULT ((1)) FOR [Type]
GO
/****** Object:  Default [DF__IPAllowLi__Versi__1FCDBCEB]    Script Date: 05/16/2018 19:30:56 ******/
ALTER TABLE [dbo].[IPAllowList] ADD  DEFAULT ((4)) FOR [Version]
GO
/****** Object:  Default [DF__UpInfo__Status__20C1E124]    Script Date: 05/16/2018 19:30:56 ******/
ALTER TABLE [dbo].[UpInfo] ADD  DEFAULT ((0)) FOR [Status]
GO
