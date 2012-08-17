USE [InfoSchema]

/****** Object:  Table [dbo].[PeopleKind]    Script Date: 08/16/2012 15:36:00 ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [dbo].[PeopleKind](
	[Id] [uniqueidentifier] NOT NULL,
	[Kind] [nchar](10) NOT NULL,
 CONSTRAINT [PK_PeopleKind] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

/****** Object:  Table [dbo].[People]    Script Date: 08/16/2012 15:36:00 ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [dbo].[People](
	[Id] [uniqueidentifier] NOT NULL,
	[Title] [nchar](10) NOT NULL,
	[Age] [int] NOT NULL,
	[Alias] [nchar](10) NOT NULL,
	[Kind] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_People] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [IX_PeopleAlias] UNIQUE NONCLUSTERED 
(
	[Alias] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

/****** Object:  ForeignKey [FK_PeopleKind]    Script Date: 08/16/2012 15:36:00 ******/
ALTER TABLE [dbo].[People]  WITH CHECK ADD  CONSTRAINT [FK_PeopleKind] FOREIGN KEY([Kind])
REFERENCES [dbo].[PeopleKind] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE

ALTER TABLE [dbo].[People] CHECK CONSTRAINT [FK_PeopleKind]

