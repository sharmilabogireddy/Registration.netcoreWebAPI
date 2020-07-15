USE [UserRegistration]
GO

INSERT INTO [dbo].[Roles] VALUES (1, 'Admin')
GO
INSERT INTO [dbo].[Roles] VALUES (2, 'TPA')
GO
INSERT INTO [dbo].[Roles] VALUES (3, 'SuperUser')
GO

INSERT INTO [dbo].[Users] VALUES (NEWID(),'admin' ,'admin@localhost.com' ,HASHBYTES('SHA2_256', 'admin123'), 'Y', 1 ,GETDATE(),NULL)
GO