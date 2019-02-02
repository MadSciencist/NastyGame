CREATE DATABASE NastyGameDb
GO

USE NastyGameDb

-- Create a new table called '[Users]' in schema '[dbo]'
-- Drop the table if it already exists
IF OBJECT_ID('[dbo].[Users]', 'U') IS NOT NULL
DROP TABLE [dbo].[Users]
GO
-- Create the table in the specified schema
CREATE TABLE [dbo].[Users]
(
    [UserId] int IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [Login] NVARCHAR(255) NOT NULL,
    [Password] NVARCHAR(255) NOT NULL,
    [Name] NVARCHAR(100),
    [LastName] NVARCHAR(100),
    [Email] NVARCHAR(100),
    [BirthDate] DATETIME,
    [JoinDate] DATETIME
);
GO

-- Create a new table called '[Addresses]' in schema '[dbo]'
-- Drop the table if it already exists
IF OBJECT_ID('[dbo].[Addresses]', 'U') IS NOT NULL
DROP TABLE [dbo].[Addresses]
GO
-- Create the table in the specified schema
CREATE TABLE [dbo].[Addresses]
(
    [AddressId] int IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [UserId] int NOT NULL, --Foreign key
    [PostalCode] NVARCHAR(10),
    [AddressLine1] NVARCHAR(255),
    [AddressLine2] NVARCHAR(255)

    CONSTRAINT FK_Adress_Users FOREIGN KEY (UserId)
    REFERENCES [dbo].[Users] (UserId)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION
);
GO