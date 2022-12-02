CREATE TABLE [dbo].[Customer]
(
	[Id] INT identity(1,1) NOT NULL PRIMARY KEY,
	FullName nvarchar(500) not null,
	PhoneNumber NVARCHAR(15) NOT NULL,
	Email nvarchar(200) null,
	Group1 int not null,
	Group2 int not null,
	Group3 int not null,
	CreatedBy nvarchar(200) not null,
	CreatedAt datetime not null,
	UpdatedAt datetime not null,
)
