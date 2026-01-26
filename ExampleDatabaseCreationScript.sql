USE [master]
GO
/****** Object:  Database [ExampleDatabase]    Script Date: 26/01/2026 07:22:00 ******/
CREATE DATABASE [ExampleDatabase]
GO

USE [ExampleDatabase]
GO
/****** Object:  Table [dbo].[ExampleTable]    Script Date: 26/01/2026 07:22:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ExampleTable](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Field1] [nvarchar](max) NOT NULL,
	[Field2] [nvarchar](max) NOT NULL,
	[Field3] [int] NOT NULL,
 CONSTRAINT [PK_ExampleTable] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[ExampleTable] ON 
GO
INSERT [dbo].[ExampleTable] ([Id], [Field1], [Field2], [Field3]) VALUES (4, N'Lorem', N'Cat1', 4)
GO
INSERT [dbo].[ExampleTable] ([Id], [Field1], [Field2], [Field3]) VALUES (5, N'Ipsum', N'Cat2', 3)
GO
INSERT [dbo].[ExampleTable] ([Id], [Field1], [Field2], [Field3]) VALUES (7, N'Dolor', N'Cat3', 5)
GO
INSERT [dbo].[ExampleTable] ([Id], [Field1], [Field2], [Field3]) VALUES (8, N'Sit', N'Cat1', 6)
GO
INSERT [dbo].[ExampleTable] ([Id], [Field1], [Field2], [Field3]) VALUES (10, N'Amet', N'Cat2', 7)
GO
INSERT [dbo].[ExampleTable] ([Id], [Field1], [Field2], [Field3]) VALUES (11, N'Consectetur', N'Cat3', 4)
GO
INSERT [dbo].[ExampleTable] ([Id], [Field1], [Field2], [Field3]) VALUES (12, N'Adipiscing', N'Cat1', 3)
GO
SET IDENTITY_INSERT [dbo].[ExampleTable] OFF
GO
USE [master]
GO
ALTER DATABASE [ExampleDatabase] SET  READ_WRITE 
GO