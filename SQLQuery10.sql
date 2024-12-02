

--first appartment (available)

insert into dbo.Units (Id, Location, Name,Available,AvailableShares,DownPayment,StartUnitPrice,CurrentUnitROI,MonthlyPayment
,AvilableDate,ExitDate,DeveloperId,CurrentUnitPrice
)
values (NEWID(),'Mostakbal City' , '2-Bedroom Garden Apartment in Park Central',0,40,29251,13295466,
0.4, 5136,'2024-12-12 14:00:00', '2028-12-12 14:00:00' ,'3A7D9872-6A8F-4216-BB34-72690AA92934',14625012)

insert into dbo.UnitImages values('6CB2E33C-BC76-46BB-B4BD-6087FE4AAED0','a7cadb4b-2c20-4780-826a-c087ce080977.png')

insert into dbo.UnitImages values('6CB2E33C-BC76-46BB-B4BD-6087FE4AAED0','e2d48f66-d1e0-435b-a567-b9bc26191b07.png')

insert into dbo.UnitImages values('6CB2E33C-BC76-46BB-B4BD-6087FE4AAED0','fce92256-6561-4338-a785-5309b25715cf.png')

insert into dbo.UnitDescriptions values('6CB2E33C-BC76-46BB-B4BD-6087FE4AAED0',3,2 ,233)

insert into dbo.UnitView values ('6CB2E33C-BC76-46BB-B4BD-6087FE4AAED0',0)

insert into dbo.UnitView values ('6CB2E33C-BC76-46BB-B4BD-6087FE4AAED0',1)

insert into dbo.UnitView values ('6CB2E33C-BC76-46BB-B4BD-6087FE4AAED0',4)

insert into dbo.UnitView values ('6CB2E33C-BC76-46BB-B4BD-6087FE4AAED0',5)

--second appartment (funded)
insert into dbo.Units (Id, Location, Name,Available,AvailableShares,DownPayment,StartUnitPrice,CurrentUnitROI,MonthlyPayment
,AvilableDate,ExitDate,DeveloperId,CurrentUnitPrice
)
values (NEWID(),' Ras El Hekma' , '3-Bedroom Penthouse in Seashell Ras Elhekma',1,0,29251,22240002,
0.15, 5136,'2024-7-12 14:00:00', '2028-7-12 14:00:00' ,'45BCF182-977E-4C81-BA02-633C25116D78',24019202)

insert into dbo.UnitImages values('3315E7EA-2EE5-414E-8A9E-43016CE05F5F','e2d48f66-d1e0-435b-a567-b9bc26191b07.png')

insert into dbo.UnitImages values('3315E7EA-2EE5-414E-8A9E-43016CE05F5F','1c94e532-e601-4fd6-950d-16f17d487d08.png')

insert into dbo.UnitImages values('3315E7EA-2EE5-414E-8A9E-43016CE05F5F','e2e1a4ab-01bf-478f-afb3-af371dee5275.png')

insert into dbo.UnitDescriptions values('3315E7EA-2EE5-414E-8A9E-43016CE05F5F',4,3 ,202)

insert into dbo.UnitView values ('3315E7EA-2EE5-414E-8A9E-43016CE05F5F',0)

insert into dbo.UnitView values ('3315E7EA-2EE5-414E-8A9E-43016CE05F5F',1)

insert into dbo.UnitView values ('3315E7EA-2EE5-414E-8A9E-43016CE05F5F',4)
insert into dbo.UnitView values ('3315E7EA-2EE5-414E-8A9E-43016CE05F5F',3)
insert into dbo.UnitView values ('3315E7EA-2EE5-414E-8A9E-43016CE05F5F',5)


--third appartment (exited)

insert into dbo.Units (Id, Location, Name,Available,AvailableShares,DownPayment,StartUnitPrice,CurrentUnitROI,MonthlyPayment
,AvilableDate,ExitDate,DeveloperId,CurrentUnitPrice
)
values (NEWID(),'6 october' , '2-Bedroom Apartment in Solana',2,0,12500,10000000 ,
0.5, 6597,'2021-5-07 14:00:00', '2024-3-08 14:00:00' ,'130ACC58-861D-46AD-A576-B372C459B3B2',15000000)

insert into dbo.UnitImages values('D2A15747-F877-434A-AC75-29E625AB55E8','fa797c45-83ff-4c9a-bd63-538e6640ea4f.png')

insert into dbo.UnitImages values('D2A15747-F877-434A-AC75-29E625AB55E8','f52ec384-f955-46f4-9abd-519fbe8f797e.png')

insert into dbo.UnitImages values('D2A15747-F877-434A-AC75-29E625AB55E8','98a9130c-a3da-4102-88b0-ec589497c4d7.png')

insert into dbo.UnitDescriptions values('D2A15747-F877-434A-AC75-29E625AB55E8',3,2 ,131 )

insert into dbo.UnitView values ('D2A15747-F877-434A-AC75-29E625AB55E8',4)
insert into dbo.UnitView values ('D2A15747-F877-434A-AC75-29E625AB55E8',3)
insert into dbo.UnitView values ('D2A15747-F877-434A-AC75-29E625AB55E8',2)
insert into dbo.UnitView values ('D2A15747-F877-434A-AC75-29E625AB55E8',5)

delete from AspNetUsers

delete from Tokens