CREATE TABLE [dbo].[Supervisor]
(
	[Id] INT NOT NULL IDENTITY(1,1),
	[EmployeeId] INT NOT NULL CONSTRAINT fk_Employee_Superviosr_EmployeeId_Employee REFERENCES [dbo].[Employee](Id),
	[ManagerId] INT NOT NULL CONSTRAINT fk_Employee_Superviosr_EmployeeId_Manager REFERENCES [dbo].[Manager](Id),
	[AnnualSalary] DECIMAL(10,2) NOT NULL,
	CONSTRAINT pk_Employee_Supervisor_Id PRIMARY KEY (Id)
)
