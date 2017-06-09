CREATE TABLE [dbo].[Customers] (
	[Id]          INT            IDENTITY (1, 1) NOT NULL,
	[Surname]     NVARCHAR (50)  NOT NULL,
	[Name]        NVARCHAR (50)  NOT NULL,
	[PhoneNumber] NVARCHAR (50)  NOT NULL,
	[Address]     NVARCHAR (100) NOT NULL,
	[Version]     ROWVERSION     NOT NULL,
	PRIMARY KEY CLUSTERED ([Id] ASC), 
	CONSTRAINT [CK_Customers_Surname] CHECK ([Surname] <> ''),
	CONSTRAINT [CK_Customers_Name] CHECK ([Name] <> ''),
	CONSTRAINT [CK_Customers_PhoneNumber] CHECK ([PhoneNumber] <> '')
);
GO

INSERT INTO [dbo].[Customers] ([Surname], [Name], [PhoneNumber], [Address]) VALUES('sn1', 'nm1', '1234', 'A1')
INSERT INTO [dbo].[Customers] ([Surname], [Name], [PhoneNumber], [Address]) VALUES('sn2', 'nm2', '2234', '')
INSERT INTO [dbo].[Customers] ([Surname], [Name], [PhoneNumber], [Address]) VALUES('sn3', 'nm3', '3234', '')
INSERT INTO [dbo].[Customers] ([Surname], [Name], [PhoneNumber], [Address]) VALUES('sn4', 'nm4', '4234', 'A4')
INSERT INTO [dbo].[Customers] ([Surname], [Name], [PhoneNumber], [Address]) VALUES('sn5', 'nm5', '5234', '')
INSERT INTO [dbo].[Customers] ([Surname], [Name], [PhoneNumber], [Address]) VALUES('sn6', 'nm6', '6234', 'A6')
INSERT INTO [dbo].[Customers] ([Surname], [Name], [PhoneNumber], [Address]) VALUES('sn7', 'nm7', '7234', 'A7')
INSERT INTO [dbo].[Customers] ([Surname], [Name], [PhoneNumber], [Address]) VALUES('sn8', 'nm8', '8234', 'A8')
INSERT INTO [dbo].[Customers] ([Surname], [Name], [PhoneNumber], [Address]) VALUES('sn9', 'nm9', '9234', 'A9')
INSERT INTO [dbo].[Customers] ([Surname], [Name], [PhoneNumber], [Address]) VALUES('sn10', 'nm10', '1023', '')