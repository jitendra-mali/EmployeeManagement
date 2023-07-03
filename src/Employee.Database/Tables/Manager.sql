CREATE TABLE [dbo].[Manager]
(
	[Id] INT NOT NULL IDENTITY(1,1),
	[EmployeeId] INT NOT NULL CONSTRAINT fk_Employee_Manager_EmployeeId_Employee REFERENCES [dbo].[Employee](Id),
	[AnnualSalary] DECIMAL(10,2) NOT NULL,
	[MaxExpenseAmount] DECIMAL(10,2)
	CONSTRAINT pk_Employee_Manager_Id PRIMARY KEY (Id)
)
