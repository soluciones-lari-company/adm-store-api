CREATE TABLE [dbo].[Purchase]
(
	[Id] UNIQUEIDENTIFIER  NOT NULL PRIMARY KEY default NEWID(),
	IdSupplier UNIQUEIDENTIFIER not null,
	DocDate date not null,
	SubTotal decimal(10,2) not null default 0,
	DiscountPercentage decimal(10,2) not null default 0,
	DiscountTotal decimal(10,2) not null default 0,
	Total decimal(10,2) not null,
	Comments nvarchar(max) null,
	CreatedBy nvarchar(20) not null,
	CreatedAt datetime not null,
	UpdatedAt datetime not null
)
