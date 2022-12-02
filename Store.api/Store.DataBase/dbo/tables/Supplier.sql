CREATE TABLE [dbo].[Supplier]
(
	CardCode NVARCHAR(20) NOT NULL PRIMARY KEY,
	SuplierName NVARCHAR(150) NOT NULL,
	SupplierStatus varchar(1) NOT NULL,
	CreatedBy nvarchar(200) not null,
	CreatedAt datetime not null,
	UpdatedAt datetime not null
)
