CREATE TABLE [dbo].[Employee]
(
	[Id] INT NOT NULL IDENTITY(1,1),
	[SupId] INT NULL CONSTRAINT fk_Employee_Employee_SupId_Supervisor REFERENCES [dbo].[Supervisor](Id),
	[ManagerId] INT NULL CONSTRAINT fk_Employee_Employee_ManagerId_Manager REFERENCES [dbo].[Manager](Id),
	[FirstName] VARCHAR(30) NOT NULL,
	[LastName] VARCHAR(30) NOT NULL,
	[Address1] VARCHAR(100) NOT NULL,
	[PayPerHour] DECIMAL(5,2) NOT NULL,
	CONSTRAINT pk_Employee_Employee_Id PRIMARY KEY (Id)
)
