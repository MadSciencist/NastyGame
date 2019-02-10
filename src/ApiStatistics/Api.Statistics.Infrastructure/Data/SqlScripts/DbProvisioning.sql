USE master
GO

IF NOT EXISTS (
    SELECT [name]
        FROM sys.databases
        WHERE [name] = N'StatisticsServiceDb'
)
CREATE DATABASE StatisticsServiceDb
GO

USE StatisticsServiceDb;

-- Create a new table called '[UserGames]' in schema '[dbo]'
-- Drop the table if it already exists
IF OBJECT_ID('[dbo].[UserGames]', 'U') IS NOT NULL
DROP TABLE [dbo].[UserGames]
GO
-- Create the table in the specified schema
CREATE TABLE [dbo].[UserGames]
(
    [GameId] int IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [PlayerId] INT NOT NULL,
    [KilledById] INT,
    [KilledBy] NVARCHAR(255),
    [StartTime] DATETIME2,
    [EndTime] DATETIME2
);
GO

-- Create a new table called '[GameVictims]' in schema '[dbo]'
-- Drop the table if it already exists
IF OBJECT_ID('[dbo].[GameVictims]', 'U') IS NOT NULL
DROP TABLE [dbo].[GameVictims]
GO
-- Create the table in the specified schema
CREATE TABLE [dbo].[GameVictims]
(
    [Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [GameId] INT NOT NULL,
    [MurdererId] INT NOT NULL,
    [VictimId] INT,
    [VictimName] NVARCHAR(255) NOT NULL,
    CONSTRAINT FK_GameVictims_UserGames FOREIGN KEY (GameId)
    REFERENCES [dbo].[UserGames] (GameId)
    ON UPDATE NO ACTION
    ON DELETE NO ACTION
);
GO