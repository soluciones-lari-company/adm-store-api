CREATE TABLE [dbo].[PurchaseOrderItem]
(
    [Id]     INT             IDENTITY (1, 1) NOT NULL,
    [DocNum]     INT,
    [ItemCode]   NVARCHAR (50)   NOT NULL,
    [DescriptionItem]   NVARCHAR (MAX)  NULL,
    [UnitPrice]  DECIMAL (10, 2) NOT NULL,
    [Quantity]   INT             NOT NULL,
    [Total]      DECIMAL (10, 2) NOT NULL,
    [LineNum]    INT             NOT NULL,

    ItemBy NVARCHAR(3) not null,

    WeightItem decimal(10,2) default 0 not null,
    PriceByGrs decimal(10,2) default 0  NOT NULL,
    
    FactorRevenue decimal(10,2) default 0  not null,
    PublicPrice decimal(10,2) default 0 not null,
    
    isSold bit default 0 not null,
    
    [Reference1] NVARCHAR (50)   NULL,
    [Comments]   NVARCHAR (MAX)  NULL,
    [CreatedBy]  NVARCHAR (200)  NOT NULL,
    [CreatedAt]  DATETIME        NOT NULL,
    [UpdatedAt]  DATETIME        NOT NULL,
    PRIMARY KEY CLUSTERED (Id ASC),
    FOREIGN KEY ([DocNum]) REFERENCES [dbo].[PurchaseOrder] ([DocNum])
)
