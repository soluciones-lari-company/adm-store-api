CREATE TABLE [dbo].[SalesOrderItem]
(
	[Id] INT identity(1,1) NOT NULL PRIMARY KEY,
	DocNum int not null,
	ItemCode nvarchar(50) null,
	UnitPrice decimal(10,2) not null,
	Quantity int not null,
	Total decimal(10,2) not null,
	LineNum int not null,
	Reference1 nvarchar(50) null,
	Reference2 nvarchar(50) null,
	Comments nvarchar(max) null,
	CreatedBy nvarchar(200) not null,
	CreatedAt datetime not null,
	UpdatedAt datetime not null,
	foreign key(DocNum) references SalesOrder(DocNum)
)
