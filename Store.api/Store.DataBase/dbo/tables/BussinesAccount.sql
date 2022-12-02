CREATE TABLE [dbo].[BussinesAccount]
(
	[Id] INT identity(1,1) NOT NULL PRIMARY KEY,
	AccountName nvarchar(150) not null,
	Balance decimal(10,2) not null,
	Comments nvarchar(max) null,
	CreatedBy nvarchar(200) not null,
	CreatedAt datetime not null,
	UpdatedAt datetime not null,
)
