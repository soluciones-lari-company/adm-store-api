CREATE TABLE [dbo].[Supplier]
(
	[Id] UNIQUEIDENTIFIER  NOT NULL PRIMARY KEY default NEWID(),
	SupplierName nvarchar(200) not null,
	SupplierAddress nvarchar(max) null,
	Comments nvarchar(max) null,
	CreatedBy nvarchar(20) not null,
	CreatedAt datetime not null,
	UpdatedAt datetime not null
)
