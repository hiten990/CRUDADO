CREATE DATABASE [UserManagement]
GO

USE [UserManagement]
GO

CREATE TABLE [dbo].[UserMaster](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[Gender] [nvarchar](50) NULL,
	[City] [nvarchar](50) NULL,
	[DateOfBirth] [nvarchar](50) NULL,
 CONSTRAINT [PK__UserMast__3214EC07D9AF9298] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
USE [master]
GO
ALTER DATABASE [UserManagement] SET  READ_WRITE 
GO
