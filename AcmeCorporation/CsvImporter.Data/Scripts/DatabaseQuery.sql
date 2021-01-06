USE master
GO
IF EXISTS(select * from sys.databases where name='AcmeCorpDB')
DROP DATABASE AcmeCorpDB
GO

CREATE DATABASE AcmeCorpDB;
GO
USE AcmeCorpDB;
GO

CREATE TABLE dbo.Items(
		[PointOfSale]			nvarchar(50) not null,
		[Product]				nvarchar(50) not null,
		[Date]					datetime not null,
		[Stock]					int not null,
		[Price]					int not null
);
GO
