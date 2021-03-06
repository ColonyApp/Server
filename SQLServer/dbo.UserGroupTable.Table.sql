ALTER TABLE [dbo].[UserGroupTable] DROP CONSTRAINT [DF__UserGroup__IsLog__398D8EEE]
GO
DROP TABLE [dbo].[UserGroupTable] 
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserGroupTable](
	[UserId] [uniqueidentifier] NOT NULL,
	[GroupId01] [uniqueidentifier] NOT NULL,
	[GroupId02] [uniqueidentifier] NULL,
	[GroupId03] [uniqueidentifier] NULL,
	[GroupId04] [uniqueidentifier] NULL,
	[GroupId05] [uniqueidentifier] NULL,
	[GroupId06] [uniqueidentifier] NULL,
	[GroupId07] [uniqueidentifier] NULL,
	[GroupId08] [uniqueidentifier] NULL,
	[GroupId09] [uniqueidentifier] NULL,
	[GroupId10] [uniqueidentifier] NULL,
	[GroupId11] [uniqueidentifier] NULL,
	[GroupId12] [uniqueidentifier] NULL,
	[GroupId13] [uniqueidentifier] NULL,
	[GroupId14] [uniqueidentifier] NULL,
	[GroupId15] [uniqueidentifier] NULL,
	[GroupId16] [uniqueidentifier] NULL,
	[GroupId17] [uniqueidentifier] NULL,
	[GroupId18] [uniqueidentifier] NULL,
	[GroupId19] [uniqueidentifier] NULL,
	[GroupId20] [uniqueidentifier] NULL,
	[GroupId21] [uniqueidentifier] NULL,
	[GroupId22] [uniqueidentifier] NULL,
	[GroupId23] [uniqueidentifier] NULL,
	[GroupId24] [uniqueidentifier] NULL,
	[GroupId25] [uniqueidentifier] NULL,
	[GroupId26] [uniqueidentifier] NULL,
	[GroupId27] [uniqueidentifier] NULL,
	[GroupId28] [uniqueidentifier] NULL,
	[GroupId29] [uniqueidentifier] NULL,
	[GroupId30] [uniqueidentifier] NULL,
	[GroupId31] [uniqueidentifier] NULL,
	[GroupId32] [uniqueidentifier] NULL,
	[GroupId33] [uniqueidentifier] NULL,
	[GroupId34] [uniqueidentifier] NULL,
	[GroupId35] [uniqueidentifier] NULL,
	[GroupId36] [uniqueidentifier] NULL,
	[GroupId37] [uniqueidentifier] NULL,
	[GroupId38] [uniqueidentifier] NULL,
	[GroupId39] [uniqueidentifier] NULL,
	[GroupId40] [uniqueidentifier] NULL,
	[GroupId41] [uniqueidentifier] NULL,
	[GroupId42] [uniqueidentifier] NULL,
	[GroupId43] [uniqueidentifier] NULL,
	[GroupId44] [uniqueidentifier] NULL,
	[GroupId45] [uniqueidentifier] NULL,
	[GroupId46] [uniqueidentifier] NULL,
	[GroupId47] [uniqueidentifier] NULL,
	[GroupId48] [uniqueidentifier] NULL,
	[GroupId49] [uniqueidentifier] NULL,
	[GroupId50] [uniqueidentifier] NULL,
	[IsLogicalDelete] [bit] NOT NULL,
	[CreateUser] [uniqueidentifier] NULL,
	[CreateDate] [datetime2](7) NULL,
	[UpdateUser] [uniqueidentifier] NULL,
	[UpdateDate] [datetime2](7) NULL,
	[DeleteUser] [uniqueidentifier] NULL,
	[DeleteDate] [datetime2](7) NULL,
 CONSTRAINT [PK__UserGrou__19832715C3ABC42A] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[GroupId01] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
ALTER TABLE [dbo].[UserGroupTable] ADD  CONSTRAINT [DF__UserGroup__IsLog__398D8EEE]  DEFAULT ((0)) FOR [IsLogicalDelete]
GO
UPDATE STATISTICS [dbo].[UserGroupTable]([PK__UserGrou__19832715C3ABC42A]) WITH STATS_STREAM = 0x01000000020000000000000000000000CFF412130000000058000000000000000000000000000000240300002400000010000000000000000000000000000000240300002400000010000000000000000000000000000000, ROWCOUNT = 0, PAGECOUNT = 0
GO
