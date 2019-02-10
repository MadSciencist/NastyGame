USE StatisticsServiceDb;

-- Create a new stored procedure called 'InsertGame' in schema 'dbo'
-- Drop the stored procedure if it already exists
IF EXISTS (
SELECT *
    FROM INFORMATION_SCHEMA.ROUTINES
WHERE SPECIFIC_SCHEMA = N'dbo'
    AND SPECIFIC_NAME = N'InsertGame'
    AND ROUTINE_TYPE = N'PROCEDURE'
)
DROP PROCEDURE dbo.InsertGame
GO

CREATE PROCEDURE dbo.InsertGame
    @PlayerId INT,
    @StartTime DATETIME2
AS
    INSERT INTO [dbo].[UserGames] VALUES (
        @PlayerId,
		null,
        null,
        @StartTime,
        null
    );

	SELECT TOP(1) * 
    FROM [dbo].[UserGames] 
    WHERE [dbo].[UserGames].[GameId] = SCOPE_IDENTITY()
GO


-- Create a new stored procedure called 'InsertVictim' in schema 'dbo'
-- Drop the stored procedure if it already exists
IF EXISTS (
SELECT *
    FROM INFORMATION_SCHEMA.ROUTINES
WHERE SPECIFIC_SCHEMA = N'dbo'
    AND SPECIFIC_NAME = N'InsertVictim'
    AND ROUTINE_TYPE = N'PROCEDURE'
)
DROP PROCEDURE dbo.InsertVictim
GO
-- Create the stored procedure in the specified schema
CREATE PROCEDURE dbo.InsertVictim
    @UserId int,
    @VictimId int,
    @VictimName nvarchar(255)

AS
    DECLARE @key int;
    SET @key = (
        SELECT TOP(1) [GameId]
        FROM [dbo].[UserGames]
        WHERE [PlayerId] = @Userid
        ORDER BY [dbo].[UserGames].[StartTime]);

    INSERT INTO [dbo].[GameVictims] VALUES (@key, @userId, @VictimId, @VictimName);

	SELECT TOP(1) * 
    FROM [dbo].[GameVictims] 
    WHERE [Id] = SCOPE_IDENTITY()
GO

-- Create a new stored procedure called 'EndGameSession' in schema 'dbo'
-- Drop the stored procedure if it already exists
IF EXISTS (
SELECT *
    FROM INFORMATION_SCHEMA.ROUTINES
WHERE SPECIFIC_SCHEMA = N'dbo'
    AND SPECIFIC_NAME = N'EndGameSession'
    AND ROUTINE_TYPE = N'PROCEDURE'
)
DROP PROCEDURE dbo.EndGameSession
GO
-- Create the stored procedure in the specified schema
CREATE PROCEDURE dbo.EndGameSession
    @UserId int,
    @KilledById int,
	@KilledBy nvarchar(255),
	@EndTime DATETIME2
AS
    UPDATE TOP(1) [dbo].[UserGames]
    SET [EndTime] = @EndTime, [KilledBy] = @KilledBy, [KilledById] = @KilledById
    WHERE [dbo].[UserGames].[GameId] = (
        SELECT TOP(1) [GameId]
        FROM [dbo].[UserGames]
        WHERE [PlayerId] = @UserId
        ORDER BY [StartTime] DESC)

	SELECT TOP(1) * 
    FROM [dbo].[UserGames] 
    WHERE [dbo].[UserGames].[GameId] = SCOPE_IDENTITY()
GO
