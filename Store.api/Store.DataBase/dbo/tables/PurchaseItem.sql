CREATE TABLE [dbo].[PurchaseItem]
(
	Serie nvarchar(15) NOT NULL PRIMARY KEY,
	Descripcion nvarchar(max) not null,
	UnitPrice decimal(10,2) not null default 0,
	Quantity int not null default 0,
	Total decimal(10,2) not null default 0,
	NoLine int not null,
	UnitPriceForSale decimal(10,2) not null,
	SoldUnits int not null,
	ItemCode nvarchar(30) null,
	CreatedBy nvarchar(20) not null,
	CreatedAt datetime not null,
	UpdatedAt datetime not null
)
