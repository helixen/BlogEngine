USE [BlogEngine.BD]
GO
SET IDENTITY_INSERT [dbo].[USERS] ON 
GO
INSERT [dbo].[USERS] ([id], [username], [password], [role]) VALUES (1, N'fredm', N'123456', 0)
GO
INSERT [dbo].[USERS] ([id], [username], [password], [role]) VALUES (2, N'joewk', N'654321', 1)
GO
SET IDENTITY_INSERT [dbo].[USERS] OFF
GO
