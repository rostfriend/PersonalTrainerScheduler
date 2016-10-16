USE PersonalTrainerScheduler
GO

SET IDENTITY_INSERT Manager ON;

INSERT INTO Manager(Id, FirstName,LastName,[Login],[Password])
	VALUES
	(1,'Rostyslav','Starko','rostfriend','f22bd35e57f562d8350286472d38b6bd'),--rs1926994
	(2,'Roman','Tkach', 'romasyk','9b107d4ea0ca53707a6acf6c45873217');--roma2000

SET IDENTITY_INSERT Manager OFF;
GO

SET IDENTITY_INSERT Trainer ON;

INSERT INTO Trainer(Id,FirstName,LastName,DateOfBirth,PhoneNumber,StartOfWorkTime,EndOfWorkTime) 
	VALUES
	(1,'Oleksander','Denysenko','1986-04-20','(096)2354637','09:00','22:00'),
	(2,'Dmytro','Mysiv','1990-02-07','(067)8536574','10:00','19:00'),
	(3, 'Andriana','Grach','1987-04-24','(067)3426234','9:00','18:00'),
	(4,'Myroslava','Pavliv','1990-09-15','(055)1546378','9:00','18:00'),
	(5,'Oleh','Katrych','1984-08-17','(095)5437312','10:00','19:00');

SET IDENTITY_INSERT Trainer OFF;
GO

SET IDENTITY_INSERT Customer ON;

INSERT INTO Customer(Id,FirstName,LastName,DateOfBirth,PhoneNumber,Adress)
	VALUES
	(1,'Rostyslav','Karpiv','1990-03-15','(050)2354634','Shevchenka 3'),
	(2,'Nadija','Kupryn','1988-03-13','(067)2355634','Sakharova 10'),
	(3,'Andrij','Kmit','1986-04-20','(090)6356463','Doroshenka 21'),
	(4,'Evgen','Malyk','1980-01-12','(055)2776547','Konotopska 17'),
	(5,'Stan','Svarovski','1990-11-22','(096)8365473','Chuprynky 65'),
	(6,'Anna','Solonko','1982-10-11','(063)6427567','Lomonosova 70'),
	(7,'Ivan','Sirko','1992-09-05','(054)6547385','Chornovola 93'),
	(8,'Oleksander','Malyk','1991-04-09','(067)6548365','Pokhyla 55'),
	(9,'Stepan','Rudenko','1969-05-13','(087)2546474','Vitovskogo 36'),
	(10,'Stanislav','Polova','1972-11-15','(050)5464634','Kopernyka 5'),
	(11,'Denys','Gavryliv','1976-08-17','(095)5435462','Zolota 22'),
	(12,'Roman','Styrl','1979-10-17','(095)4365462','Valova 10'),
	(13,'Anastasia','Chorna','1976-08-17','(050)6566466','Vosma 9'),
	(14,'Denys','Gavryliv','1979-07-19','(065)6565456','Dmytrenka 5'),
	(15,'Serg','Kronos','1985-03-13','(055)5492456','Schastja 07');

SET IDENTITY_INSERT Customer OFF;
GO

SET IDENTITY_INSERT TrainingSession ON;

INSERT INTO TrainingSession(Id,TrainerId,CustomerId,TrainingSessionDateTimeStart)
	VALUES
	(1, 1, 1, '2016-10-13 10:00'),
	(2, 2, 2, '2016-10-13 15:00'),
	(3, 1, 3, '2016-10-13 16:00'),
	(4, 2, 4, '2016-10-14 17:00'),
	(5, 2, 5, '2016-10-15 10:00'),
	(6, 3, 6, '2016-10-16 12:00'),
	(8, 5, 8, '2016-10-17 14:00'),
	(9, 1, 9, '2016-10-18 15:00'),
	(10, 4, 10, '2016-10-17 10:00'),
	(11, 5, 11, '2016-10-18 11:00'),
	(12, 4, 12, '2016-10-15 12:00'),
	(13, 5, 13, '2016-10-17 13:00'),
	(14, 2, 14, '2016-10-15 14:00'),
	(15, 3, 15, '2016-10-18 15:00')

SET IDENTITY_INSERT TrainingSession OFF;
GO

SET IDENTITY_INSERT Occupations ON;

INSERT INTO Occupations(Id,Occupation)
	VALUES
	(1,'Bodybuilder'),
	(2,'Powerlifter'),
	(3,'Fitness Model'),
	(4,'Swimmer'),
	(5,'Bikini'),
	(6,'Stongman'),
	(7,'Mans Physic');

SET IDENTITY_INSERT Occupations OFF;
GO

SET IDENTITY_INSERT TrainerOccupation ON;

INSERT INTO TrainerOccupation(Id,TrainerId,OccupationId)
	VALUES
	(1, 1, 1),
	(2, 1, 2),
	(3, 1, 3),
	(4, 2, 4),
	(5, 2, 7),
	(6, 3, 5),
	(7, 4, 3),
	(8, 4, 4),
	(9, 4, 5),
	(10, 5, 2),
	(11, 5, 6);

SET IDENTITY_INSERT TrainerOccupation OFF;
GO