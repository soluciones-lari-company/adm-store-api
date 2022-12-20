CREATE TABLE [dbo].[IncommingPayment]
(
	[Id] INT identity(1,1) NOT NULL PRIMARY KEY,
	Customer int not null,
	DocNum int null,
	Total decimal(10,2) not null,
	PaymentDate date not null,
	BussinesAccount int not null,
	Canceled bit default 0,
	CanceledDate datetime null,
	CanceledBy nvarchar(200) null,
	Comments nvarchar(max) null,
	CreatedBy nvarchar(200) not null,
	CreatedAt datetime not null,
	UpdatedAt datetime not null,
	foreign key(Customer) references Customer (Id),
	foreign key(DocNum) references SalesOrder (DocNum),
	foreign key(BussinesAccount) references BussinesAccount (Id)
)
