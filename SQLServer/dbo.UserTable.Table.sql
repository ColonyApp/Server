ALTER TABLE [dbo].[UserTable] DROP CONSTRAINT [DF__UserTable__IsLog__3C69FB99]
GO
DROP TABLE [dbo].[UserTable]
GO
SET ANSI_NULLS ON 
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserTable](
	[Id] [uniqueidentifier] NOT NULL,
	[Nickname] [nvarchar](300) NOT NULL,
	[MailAddress] [nvarchar](300) NOT NULL,
	[IsLogicalDelete] [bit] NOT NULL,
	[CreateUser] [uniqueidentifier] NULL,
	[CreateDate] [datetime2](7) NULL,
	[UpdateUser] [uniqueidentifier] NULL,
	[UpdateDate] [datetime2](7) NULL,
	[DeleteUser] [uniqueidentifier] NULL,
	[DeleteDate] [datetime2](7) NULL,
 CONSTRAINT [PK__UserTabl__3214EC077B7511B3] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
ALTER TABLE [dbo].[UserTable] ADD  CONSTRAINT [DF__UserTable__IsLog__3C69FB99]  DEFAULT ((0)) FOR [IsLogicalDelete]
GO
UPDATE STATISTICS [dbo].[UserTable]([PK__UserTabl__3214EC077B7511B3]) WITH STATS_STREAM = 0x010000000100000000000000000000007DE0E9F90000000040000000000000000000000000000000240300002400000010000000000000000000000000000000, ROWCOUNT = 0, PAGECOUNT = 0
GO
