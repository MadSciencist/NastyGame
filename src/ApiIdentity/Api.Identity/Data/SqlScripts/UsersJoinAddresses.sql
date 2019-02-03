USE NastyGameDb

SELECT * FROM [dbo].[Users]
LEFT JOIN [dbo].[Addresses] ON [dbo].[Users].[UserId] = [dbo].[Addresses].UserId
WHERE [dbo].[Users].[Login] = 'admin'
GO