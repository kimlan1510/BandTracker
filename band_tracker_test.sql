USE [band_tracker_test]
GO
/****** Object:  Table [dbo].[bands]    Script Date: 6/16/2017 4:39:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[bands](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](255) NULL,
	[type] [varchar](255) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[venues]    Script Date: 6/16/2017 4:39:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[venues](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](255) NULL,
	[location] [varchar](255) NULL,
	[capacity] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[venues_bands]    Script Date: 6/16/2017 4:39:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[venues_bands](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[venues_id] [int] NULL,
	[bands_id] [int] NULL
) ON [PRIMARY]
GO
