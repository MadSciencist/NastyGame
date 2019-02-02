USE NastyGameDb
GO

BEGIN TRY
    BEGIN TRANSACTION

        DECLARE @UserId int;

        INSERT INTO [dbo].[Users] ([Login], [Password], [Name], [LastName], [Email], [BirthDate], [JoinDate])
        VALUES ('admin', 'admin', 'AdminName', 'AdminLastName', 'AdminEmail', '2000/01/01', '2000/01/01');

        SELECT @UserId = SCOPE_IDENTITY();
        PRINT(@UserId)

        INSERT INTO [dbo].[Addresses] ([UserId], [PostalCode], [AddressLine1], [AddressLine2])
        VALUES (@UserId, '999-99', 'FirstRow', 'SecondRow')

    COMMIT
END TRY
BEGIN CATCH
    ROLLBACK
END CATCH

GO