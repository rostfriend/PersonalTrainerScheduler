
CREATE DATABASE PersonalTrainerScheduler
GO

USE PersonalTrainerScheduler
GO

CREATE TABLE Manager
(
	Id INT NOT NULL IDENTITY(1,1),
	FirstName NVARCHAR(25) NOT NULL,
	LastName NVARCHAR(40) NOT NULL,
	[Login] NVARCHAR(50) NOT NULL,
	[Password] NVARCHAR(50) NOT NULL, 

	CONSTRAINT PK_Manager_Id PRIMARY KEY (Id),
	CONSTRAINT UQ_Manager_Login UNIQUE ([Login])
);

CREATE TABLE Trainer
(
	Id INT NOT NULL IDENTITY(1,1),
	FirstName NVARCHAR(25) NOT NULL,
	LastName NVARCHAR(40) NOT NULL,
	DateOfBirth DATE NOT NULL,
	PhoneNumber NVARCHAR(12),
	StartOfWorkTime TIME NOT NULL,
	EndOfWorkTime TIME NOT NULL,

	CONSTRAINT PK_Trainer_Id PRIMARY KEY (Id),
	CONSTRAINT UQ_Trainer_PhoneNumberUnique UNIQUE (PhoneNumber),
	CONSTRAINT CK_Trainer_PhoneNumberLike CHECK (PhoneNumber LIKE '([0-9][0-9][0-9])[0-9][0-9][0-9][0-9][0-9][0-9][0-9]')
);

CREATE TABLE Customer
(
	Id INT NOT NULL IDENTITY(1,1),
	FirstName NVARCHAR(25) NOT NULL,
	LastName NVARCHAR(40) NOT NULL,
	DateOfBirth DATE NOT NULL,
	PhoneNumber NVARCHAR(12),
	Adress NVARCHAR(100) NOT NULL,

	CONSTRAINT PK_Customer_Id PRIMARY KEY (Id),
	CONSTRAINT CK_Customer_DateOfBirthNOtMoteThanToday CHECK (DateOfBirth < GETDATE()),
	CONSTRAINT UQ_Customer_CustomerUnique UNIQUE (FirstName, LastName, DateOfBirth, Adress),
	CONSTRAINT UQ_Customer_PhoneNumberUnique UNIQUE (PhoneNumber),
	CONSTRAINT CK_Customer_PhoneNumberLike CHECK (PhoneNumber LIKE '([0-9][0-9][0-9])[0-9][0-9][0-9][0-9][0-9][0-9][0-9]')
);

CREATE TABLE TrainingSession
(
	Id INT NOT NULL IDENTITY(1,1),
	TrainerId INT NOT NULL,
	CustomerId INT NOT NULL,
	TrainingSessionDateTimeStart DATETIME NOT NULL,

	CONSTRAINT PK_TrainingSession PRIMARY KEY (Id),
	CONSTRAINT FK_TrainingSession_TrainerId_Trainer_Id FOREIGN KEY (TrainerId) REFERENCES Trainer (Id) ON DELETE CASCADE ,
	CONSTRAINT FK_TrainingSession_CustomerId_Customer_Id FOREIGN KEY (CustomerId) REFERENCES Customer (Id)  ON DELETE CASCADE,

);

CREATE TABLE Occupations
(
	Id INT NOT NULL IDENTITY(1,1),
	Occupation NVARCHAR(50) NOT NULL
	
	CONSTRAINT PK_Occupations PRIMARY KEY (Id),
);

CREATE TABLE TrainerOccupation
(	
	Id INT NOT NULL IDENTITY(1,1),
	TrainerId INT NOT NULL,
	OccupationId INT NOT NULL

	CONSTRAINT PK_TrainerOccupation PRIMARY KEY (Id), 
	CONSTRAINT FK_TrainerOccupation_TrainerId_Trainer_Id FOREIGN KEY (TrainerId) REFERENCES Trainer (Id) ON DELETE CASCADE, 
	CONSTRAINT FK_TrainerOccupation_OccupationiD_Occupations_Id FOREIGN KEY (OccupationiD) REFERENCES Occupations (Id)
);





