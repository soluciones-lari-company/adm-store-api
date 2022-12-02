CREATE TABLE [dbo].[SalesOrder]
(
	DocNum INT identity(1,1) NOT NULL PRIMARY KEY,
	Customer int not null,
	DocDate date not null,
	DocTotal decimal(10,2) not null,
	Discount decimal(10,2) not null,
	TotalDiscount decimal(10,2) null,
	DocTotalFinal decimal(10,2) not null,
	DocStatus varchar(1) not null,
	MethodPayment nvarchar(3) not null,
	Canceled bit not null,
	CandeledDate DateTime not null,
	CanceledBy nvarchar(200) not null,
	Comments nvarchar(max) null,
	CreatedBy nvarchar(200) not null,
	CreatedAt datetime not null,
	UpdatedAt datetime not null,
	Foreign key(Customer) references Customer(Id)
)
