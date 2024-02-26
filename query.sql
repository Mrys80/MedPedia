CREATE TABLE dbo.Balances( 
[Id] int  IDENTITY(1,1) NOT NULL PRIMARY KEY CLUSTERED,
  [ProductId] int  NOT NULL,
  [Buy] float  NOT NULL,
  [Sell] float  NOT NULL,
  [AverageBuy] bigint  NOT NULL,
  [AverageSell] bigint  NOT NULL,
  [DProfit] bigint  NOT NULL,
  [SProfit] bigint  NOT NULL,
  [RegDate] datetime2(7)  NOT NULL,
  [RegUserId] int  NOT NULL,
  [IsDeleted] bit  NOT NULL,
	[FromDate] datetime2 GENERATED ALWAYS AS ROW START,
  [ToDate] datetime2 GENERATED ALWAYS AS ROW END,
  PERIOD FOR SYSTEM_TIME (FromDate, ToDate)
 )
WITH (SYSTEM_VERSIONING = ON (HISTORY_TABLE = dbo.BalancesHistory))
-------------------------------------------------------------------------------
SELECT p.Id,
       p.Title,
       p.TypeId,
       p.Buyable,
       p.IsEnable,
       p.Sellable,
       p.MinOrder,
       p.MaxOrder,
       p.IncreaseDecreasePrice,
       p.QrDifferentPrice,
       pp.SellPrice,
       pp.BuyPrice
FROM dbo.Products p
         JOIN dbo.ProductPricesView pp ON p.Id = pp.ProductId
WHERE p.IsDeleted = 0
------------------------------------------------------------------------

SELECT pp.* FROM dbo.ProductPrices pp
                     JOIN (
    SELECT ProductId , MAX(Id) AS maxId
    FROM dbo.ProductPrices
    WHERE IsDeleted = 0
    GROUP BY ProductId

) p ON pp.Id = p.maxId