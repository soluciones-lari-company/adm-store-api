CREATE TABLE [dbo].[BussinesAccountHistory]
(
	[Id] INT identity(1,1) NOT NULL PRIMARY KEY,
	BussinesAccount int not null,
	Total decimal(10,2) not null,
	HistoryType varchar(1) not null,
	DocRefType nvarchar(3) null,
	DocRefNum int not null,
	Cancel bit default 0,
	Comments nvarchar(max) null,
	CreatedBy nvarchar(200) not null,
	CreatedAt datetime not null,
	UpdatedAt datetime not null,
	foreign key(BussinesAccount) references BussinesAccount(Id)
)
