ALTER TABLE [dbo].[TargetDataTable] DROP CONSTRAINT [DF__tmp_ms_xx__IsLog__59063A47]
GO
DROP TABLE [dbo].[TargetDataTable] 
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TargetDataTable](
	[Id] [uniqueidentifier] NOT NULL,
	[Mode] [int] NOT NULL,
	[Tags] [nvarchar](max) NULL,
	[OccurrenceDateTime] [datetime2](7) NULL,
	[WhatAttribute] [nvarchar](max) NULL,
	[WhenAttribute] [datetime2](7) NULL,
	[WhyAttribute] [nvarchar](max) NULL,
	[WhoAttribute] [uniqueidentifier] NOT NULL,
	[WhereAttribute] [nvarchar](max) NULL,
	[WhomAttribute] [nvarchar](max) NULL,
	[HowAttribute] [nvarchar](max) NULL,
	[HowMuchAttribute] [nvarchar](max) NULL,
	[HowManyAttribute] [nvarchar](max) NULL,
	[GroupNames] [nvarchar](max) NULL,
	[IsLogicalDelete] [bit] NOT NULL,
	[CreateUser] [uniqueidentifier] NULL,
	[CreateDate] [datetime2](7) NULL,
	[UpdateUser] [uniqueidentifier] NULL,
	[UpdateDate] [datetime2](7) NULL,
	[DeleteUser] [uniqueidentifier] NULL,
	[DeleteDate] [datetime2](7) NULL,
 CONSTRAINT [PK__tmp_ms_x__3214EC07994460EC] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
ALTER TABLE [dbo].[TargetDataTable] ADD  CONSTRAINT [DF__tmp_ms_xx__IsLog__59063A47]  DEFAULT ((0)) FOR [IsLogicalDelete]
GO
UPDATE STATISTICS [dbo].[TargetDataTable]([PK__tmp_ms_x__3214EC07994460EC]) WITH STATS_STREAM = 0x010000000100000000000000000000007DE0E9F90000000040000000000000000000000000000000240300002400000010000000000000000000000000000000, ROWCOUNT = 0, PAGECOUNT = 0
GO
