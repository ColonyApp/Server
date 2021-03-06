ALTER TABLE [dbo].[UserTargetDataTable] DROP CONSTRAINT [DF__tmp_ms_xx__IsLog__5629CD9C]
GO
DROP TABLE [dbo].[UserTargetDataTable]
GO
SET ANSI_NULLS ON 
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserTargetDataTable](
	[UserId] [uniqueidentifier] NOT NULL,
	[TargetDataId] [uniqueidentifier] NOT NULL,
	[IsLogicalDelete] [bit] NOT NULL,
	[CreateUser] [uniqueidentifier] NULL,
	[CreateDate] [datetime2](7) NULL,
	[UpdateUser] [uniqueidentifier] NULL,
	[UpdateDate] [datetime2](7) NULL,
	[DeleteUser] [uniqueidentifier] NULL,
	[DeleteDate] [datetime2](7) NULL,
 CONSTRAINT [PK__tmp_ms_x__876F7B127A8E0513] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[TargetDataId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
ALTER TABLE [dbo].[UserTargetDataTable] ADD  CONSTRAINT [DF__tmp_ms_xx__IsLog__5629CD9C]  DEFAULT ((0)) FOR [IsLogicalDelete]
GO
UPDATE STATISTICS [dbo].[UserTargetDataTable]([PK__tmp_ms_x__876F7B127A8E0513]) WITH STATS_STREAM = 0x01000000020000000000000000000000CFF412130000000058000000000000000000000000000000240300002400000010000000000000000000000000000000240300002400000010000000000000000000000000000000, ROWCOUNT = 0, PAGECOUNT = 0
GO
