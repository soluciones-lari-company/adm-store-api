/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
--ALTER DATABASE SCOPED CONFIGURATION SET IDENTITY_CACHE = ON

insert into BussinesAccount(AccountName,Comments,Balance,CreatedAt,UpdatedAt,CreatedBy) values('','',0,GETDATE(),GETDATE(),'USER-SYS')

insert into Supplier(CardCode,SuplierName,SupplierStatus,CreatedBy,CreatedAt,UpdatedAt) values('S0001','Joyeria Servin oro','A','USER-SYS',GETDATE(),GETDATE())
insert into Supplier(CardCode,SuplierName,SupplierStatus,CreatedBy,CreatedAt,UpdatedAt) values('S0002','Joyeria Servin plata','A','USER-SYS',GETDATE(),GETDATE())
insert into Supplier(CardCode,SuplierName,SupplierStatus,CreatedBy,CreatedAt,UpdatedAt) values('S0003','Joyeria Thiago','A','USER-SYS',GETDATE(),GETDATE())
insert into Supplier(CardCode,SuplierName,SupplierStatus,CreatedBy,CreatedAt,UpdatedAt) values('S0004','Joyeria Epoka de oro','A','USER-SYS',GETDATE(),GETDATE())
insert into Supplier(CardCode,SuplierName,SupplierStatus,CreatedBy,CreatedAt,UpdatedAt) values('S0005','Joyeria Dajaos','A','USER-SYS',GETDATE(),GETDATE())
insert into Supplier(CardCode,SuplierName,SupplierStatus,CreatedBy,CreatedAt,UpdatedAt) values('S0006','Joyeria Prims','A','USER-SYS',GETDATE(),GETDATE())

insert into PurchaseOrder(DocDate,DocStatus,DocTotal,Supplier,Canceled,CanceledBy,CandeledDate,CreatedBy,CreatedAt,UpdatedAt)
    values('2022-10-21','C',0,'S0004',0,null,null,'USER-SYS',GETDATE(),GETDATE())

declare @orderIdCreated int

select top 1 @orderIdCreated = DocNum from PurchaseOrder where Supplier = 'S0004' order by CreatedAt desc


insert into PurchaseOrderItem(DocNum,LineNum,ItemCode,Reference1,ItemBy,WeightItem,PriceByGrs,[DescriptionItem],UnitPrice,Quantity,Total,Comments,FactorRevenue,PublicPrice,isSold,CreatedBy,CreatedAt,UpdatedAt)
    values(@orderIdCreated,1,'9GO81','979','GRS','1.7','590','A05 Diflor lasser/argolla 10k - gargantilla oro florentino corazon 02-55-08','1003',1,'1003','stock','200','2006','0','USER-SYS',GETDATE(),GETDATE()),
(@orderIdCreated,2,'9AO82','979','GRS','3.5','590','A05 Diflor lasser/argolla 10k - arracada oro 02-23-10','2065',1,'2065','Dona Mica','160','3304','0','USER-SYS',GETDATE(),GETDATE()),
(@orderIdCreated,3,'9AO83','979','GRS','5.2','590','A05 Diflor lasser/argolla 10k - arracada oro florentino 02-23-11','3068',1,'3068','Sandra Cipriano','180','5522.4','0','USER-SYS',GETDATE(),GETDATE()),
(@orderIdCreated,4,'9AN84','979','GRS','0.8','607','A01 Huggie Especial 10kt - Anillo oro infinito 02-03-10','485.6',1,'485.6','Sandra Cipriano','160','776.96','0','USER-SYS',GETDATE(),GETDATE()),
(@orderIdCreated,5,'9AO85','979','GRS','1','576','A03 Boleado 10kt - arracada 02-22-04','576',1,'576','Dona pati','180','1036.8','0','USER-SYS',GETDATE(),GETDATE()),
(@orderIdCreated,6,'9MO86','979','GRS','0.4','607','A01 Huggie Especial 10kt - cristo de oro 02-32-04','242.8',1,'242.8','Amanda de vicente','290','704.12','0','USER-SYS',GETDATE(),GETDATE()),
(@orderIdCreated,7,'9MO87','979','GRS','0.9','568','A07 Medalla/Arracada basica 10kt - Medalla de oro con piedra virgen 02-30-08','511.2',1,'511.2','Amanda de vicente','240','1226.88','0','USER-SYS',GETDATE(),GETDATE()),
(@orderIdCreated,8,'9EO88','979','GRS','2.1','576','A03 Boleado 10kt - esclava nino 02-44-03 14 cm','1209.6',1,'1209.6','Claudia atanacio','170','2056.32','0','USER-SYS',GETDATE(),GETDATE()),
(@orderIdCreated,9,'9EO89','979','GRS','5.4','576','A03 Boleado 10kt - esclava caballero 02-49-01 con placa 20 cm','3110.4',1,'3110.4','Claudia atanacio','150','4665.6','0','USER-SYS',GETDATE(),GETDATE()),
(@orderIdCreated,10,'9EO90','979','GRS','1.7','576','A03 Boleado 10kt - esclava nina 02-44-05 14 cm','979.2',1,'979.2','Ana Karina','210','2056.32','0','USER-SYS',GETDATE(),GETDATE()),
(@orderIdCreated,11,'9AO91','979','GRS','0.7','576','A03 Boleado 10kt - arracada oro 02-22-02','403.2',1,'403.2','stock','200','806.4','0','USER-SYS',GETDATE(),GETDATE()),
(@orderIdCreated,12,'9AO92','979','GRS','0.6','590','A05 Diflor lasser/argolla 10k - arracada oro 02-22-01','354',1,'354','stock','200','708','0','USER-SYS',GETDATE(),GETDATE()),
(@orderIdCreated,13,'9MO93','979','GRS','0.9','576','A03 Boleado 10kt - Medalla oro virgen 02-29-09','518.4',1,'518.4','Guadalupe atanacio vazquez','150','777.6','0','USER-SYS',GETDATE(),GETDATE()),
(@orderIdCreated,14,'9MO94','979','GRS','1.1','576','A03 Boleado 10kt - Medalla oro sagrado corazon 02-29-13','633.6',1,'633.6','Guadalupe atanacio vazquez','150','950.4','0','USER-SYS',GETDATE(),GETDATE()),
(@orderIdCreated,15,'9CO95','979','GRS','3.1','557','C02 Cadena color 10kt - Cadena oro florentino 02-41-02 60cm','1726.7',1,'1726.7','Guadalupe atanacio vazquez','165','2849.06','0','USER-SYS',GETDATE(),GETDATE()),
(@orderIdCreated,16,'9CO96','979','GRS','3.2','557','C02 Cadena color 10kt - Cadena oro florentino 02-41-03 45cm','1782.4',1,'1782.4','Amanda de vicente','170','3030.08','0','USER-SYS',GETDATE(),GETDATE()),
(@orderIdCreated,17,'9CO97','979','GRS','3.4','557','C02 Cadena color 10kt - Cadena oro florentino 02-41-03 45cm','1893.8',1,'1893.8','Guadalupe atanacio vazquez','135','2556.63','0','USER-SYS',GETDATE(),GETDATE()),
(@orderIdCreated,18,'9CO98','979','GRS','3','557','C02 Cadena color 10kt - Cadena oro florentino 02-41-03 60cm','1671',1,'1671','Amanda de vicente','170','2840.7','0','USER-SYS',GETDATE(),GETDATE()),
(@orderIdCreated,19,'9RO99','979','GRS','4.6','590','A05 Diflor lasser/argolla 10k - rosario oro florentino 02-56-03','2714',1,'2714','stock','200','5428','0','USER-SYS',GETDATE(),GETDATE())


update PurchaseOrder Set DocTotal = (select sum(Total) from PurchaseOrderItem where DocNum = @orderIdCreated) where DocNum = @orderIdCreated


insert into Supplier(CardCode,SuplierName,SupplierStatus,CreatedBy,CreatedAt,UpdatedAt) values('S0007','Joyeria Gala','H','USER-SYS',GETDATE(),GETDATE())

insert into PurchaseOrder(DocDate,DocStatus,DocTotal,Supplier,Canceled,CanceledBy,CandeledDate,CreatedBy,CreatedAt,UpdatedAt)
    values('2022-10-21','C',0,'S0007',0,null,null,'USER-SYS',GETDATE(),GETDATE())

insert into PurchaseOrderItem(DocNum,LineNum,ItemCode,Reference1,ItemBy,WeightItem,PriceByGrs,[DescriptionItem],UnitPrice,Quantity,Total,Comments,FactorRevenue,PublicPrice,isSold,CreatedBy,CreatedAt,UpdatedAt)
    values(@orderIdCreated,1,'9GO81','0','NA','0','0','Servicio reparacion de joyeria','0',1,'0','este articulo seara usado para fines de servicio de repacion de joyeria','0','0','0','USER-SYS',GETDATE(),GETDATE())