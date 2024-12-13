USE [Todoist]
GO

/****** Object:  Table [dbo].[Todos]    Script Date: 13/12/2024 03:31:41 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Todos](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[Todo] [nvarchar](350) NOT NULL,
	[Completed] [bit] NOT NULL,
	[Priority] [int] NULL,
	[Latitude] [decimal](10, 7) NULL,
	[Longitude] [decimal](10, 7) NULL,
	[DueDate] [datetime] NULL,
	[Category] [nvarchar](36) NULL,
	[CreateDate] [datetime] NULL,
	[LastUpdateDate] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[LastUpdatedBy] [int] NULL,
	[Deleted] [bit] NULL,
 CONSTRAINT [PK_Todos] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Todos] ADD  CONSTRAINT [DF_Todos_Priority]  DEFAULT ((3)) FOR [Priority]
GO

ALTER TABLE [dbo].[Todos] ADD  CONSTRAINT [DF_Todos_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO

ALTER TABLE [dbo].[Todos] ADD  CONSTRAINT [DF_Todos_Deleted]  DEFAULT ((0)) FOR [Deleted]
GO

