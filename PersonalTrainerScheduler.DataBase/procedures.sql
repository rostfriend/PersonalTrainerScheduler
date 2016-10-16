USE PersonalTrainerScheduler
GO

CREATE PROCEDURE spGetAllTrainers
AS
BEGIN
SELECT * FROM Trainer
ORDER BY LastName
END
GO


CREATE PROCEDURE spGetAllOccupations
AS
BEGIN
SELECT * FROM Occupations
ORDER BY Occupation
END
GO

CREATE PROCEDURE spGetAllCustomers
AS
BEGIN
SELECT * FROM Customer
ORDER BY LastName
END
GO

CREATE PROCEDURE spGetOccupationsByTrainerId
	@trainerId int
AS
	SELECT TrainerOccupation.OccupationId,Occupation FROM TrainerOccupation
	JOIN Occupations
	ON Occupations.Id = TrainerOccupation.OccupationId
	WHERE TrainerId = @trainerId;
GO


CREATE PROCEDURE spGetCustomersByLastName
	@lastName NVARCHAR(40)
AS
BEGIN
	SELECT * FROM Customer
	WHERE LastName LIKE '%'+@lastName+'%'
	ORDER BY LastName;
END;
GO


CREATE PROCEDURE spGetAllTrainingSessionsByCustomerId
	@customerId int
AS
BEGIN
	SELECT t.FirstName, t.LastName, s.Id, s.TrainingSessionDateTimeStart
	FROM Customer c INNER JOIN TrainingSession s
		ON c.Id = s.CustomerId
		INNER JOIN Trainer t
		ON t.Id = s.TrainerId
	WHERE c.Id = @customerId
	ORDER BY s.TrainingSessionDateTimeStart;
END;
GO


CREATE PROCEDURE spGetTrainersScheduleByTheDay
	@trainerId int,
	@date DATE
AS
BEGIN
	SELECT c.FirstName, c.LastName, s.Id, s.TrainingSessionDateTimeStart
	FROM Trainer t INNER JOIN TrainingSession s
		ON t.Id = s.TrainerId
		INNER JOIN Customer c
		ON c.Id = s.CustomerId
	WHERE  CAST(s.TrainingSessionDateTimeStart AS DATE) = CAST(@date AS DATE)
	AND t.Id = @trainerId
	ORDER BY s.TrainingSessionDateTimeStart;
END;
GO


CREATE PROCEDURE spGetFreeTrainersByDateTime
	@desiredDateTime DATETIME
AS
BEGIN
	SELECT t.Id, t.FirstName, t.LastName, t.DateOfBirth, t.PhoneNumber, t.StartOfWorkTime, t.EndOfWorkTime
	FROM Trainer t INNER JOIN TrainingSession s
	ON t.Id = s.TrainerId
	WHERE CAST(t.StartOfWorkTime AS TIME) <= CAST(@desiredDateTime AS TIME)
	AND CAST(t.EndOfWorkTime AS TIME) > CAST(@desiredDateTime AS TIME)
	AND CAST(@desiredDateTime AS DATETIME) NOT IN (SELECT TrainingSessionDateTimeStart FROM TrainingSession WHERE TrainingSession.TrainerId = t.Id)
	GROUP BY t.Id, t.FirstName, t.LastName, t.DateOfBirth, t.PhoneNumber, t.StartOfWorkTime, t.EndOfWorkTime
	ORDER BY t.LastName
END;
GO

CREATE PROCEDURE spRegisterTrainingSession
	@trainerId int,
	@customerId int,
	@trainingSessionDateTimeStart DATETIME
AS
BEGIN
	IF EXISTS (SELECT * FROM TrainingSession WHERE @customerId = customerId AND @trainingSessionDateTimeStart = TrainingSessionDateTimeStart)
		BEGIN;
			THROW 50001, 'Customer is already registered for training session on that time!', 5;
		END;
	INSERT INTO TrainingSession VALUES
		(@trainerId, @customerId, @trainingSessionDateTimeStart)
END;
GO


CREATE PROCEDURE spDeleteCustomerById
	@customerId INT
AS
BEGIN
	DELETE FROM Customer WHERE Id = @customerId
END;
GO

CREATE PROCEDURE spDeleteTrainerById
	@trainerId INT
AS
BEGIN
	DELETE FROM Trainer WHERE Id = @trainerId
END;
GO

CREATE PROCEDURE spDeleteTrainingSessionById
	@sessionId INT
AS
BEGIN
	DELETE FROM TrainingSession WHERE Id = @sessionId
END;
GO

CREATE PROCEDURE spAddNewCustomer
	@firstName NVARCHAR(40),
	@lastName NVARCHAR(40),
	@dateOfBirth DATE,
	@phoneNumber NVARCHAR(12),
	@adress NVARCHAR(100)
AS
BEGIN
	IF EXISTS (SELECT * FROM Customer 
			   WHERE FirstName = @firstName AND LastName = @lastName AND DateOfBirth = @dateOfBirth AND PhoneNumber = @phoneNumber)
		BEGIN;
			THROW 50011, 'Customer already exists!', 5;
		END;
	INSERT INTO Customer VALUES
		(@firstName, @lastName, CAST(@dateOfBirth AS DATE), @phoneNumber, @adress)
END;
GO


CREATE PROCEDURE spModifyCustomerById
	@customerId INT,
	@firstName NVARCHAR(40),
	@lastName NVARCHAR(40),
	@dateOfBirth DATE,
	@phoneNumber NVARCHAR(12),
	@adress NVARCHAR(100)
AS
BEGIN
	UPDATE Customer
	SET FirstName = @firstName, LastName = @lastName, DateOfBirth = @dateOfBirth, PhoneNumber = @phoneNumber, Adress = @adress
	WHERE Id = @customerId
END;
GO



CREATE PROCEDURE spAddNewTrainer
	@firstName NVARCHAR(40),
	@lastName NVARCHAR(40),
	@dateOfBirth DATE,
	@phoneNumber NVARCHAR(12),
	@startOfWorkTime TIME,
	@endOfWorkTime TIME,
	@occupationId INT
AS
BEGIN

	IF (@startOfWorkTime > @endOfWorkTime)
		BEGIN;
			THROW 50021, 'Start of work time must be less than end of work time!', 5;
		END;

	IF EXISTS (SELECT * FROM Trainer 
			   WHERE FirstName = @firstName AND LastName = @lastName AND DateOfBirth = @dateOfBirth AND PhoneNumber = @phoneNumber)
		BEGIN;
			THROW 50011, 'Trainer already exists!', 4;
		END;
	INSERT INTO Trainer VALUES
		(@firstName, @lastName, @dateOfBirth, @phoneNumber, @startOfWorkTime, @endOfWorkTime)
	INSERT INTO TrainerOccupation VALUES
		(SCOPE_IDENTITY(),@occupationId)
END;
GO


CREATE PROCEDURE spModifyTrainerById
	@trainerId INT,
	@firstName NVARCHAR(40),
	@lastName NVARCHAR(40),
	@dateOfBirth DATE,
	@phoneNumber NVARCHAR(12),
	@startOfWorkTime TIME,
	@endOfWorkTime TIME

AS
BEGIN
	IF (@startOfWorkTime > @endOfWorkTime)
		BEGIN;
			THROW 50021, 'Start of work time must be less than end of work time!', 4;
		END;
	UPDATE Trainer 
	SET FirstName = @firstName, LastName = @lastName, DateOfBirth = @dateOfBirth, PhoneNumber = @phoneNumber, StartOfWorkTime = @startOfWorkTime, EndOfWorkTime = @endOfWorkTime
	WHERE Id = @trainerId	
END;
GO


CREATE PROCEDURE spGetManagerByLogin
	@login NVARCHAR(40),
	@password NVARCHAR(40)
AS
BEGIN
	SELECT Id, FirstName, LastName, [Login]
	FROM Manager
	WHERE [Login] = @login AND [Password] = @password;
END
GO


