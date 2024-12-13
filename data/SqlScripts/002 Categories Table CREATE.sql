USE [Todoist]
GO 

CREATE TABLE [dbo].[Todos](
	[Id] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[Todo] [nvarchar](350) NOT NULL,
	[Completed] [bit] NOT NULL,
	[Priority] [int] NULL,
	[Latitude] [decimal](10, 7) NULL,
	[Longitude] [decimal](10, 7) NULL,
	[DueDate] [datetime] NULL,
	[Category] [nvarchar](36) NULL,
 CONSTRAINT [PK_Todos] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

