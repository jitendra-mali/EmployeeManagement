﻿/*
Deployment script for Employee

This code was generated by a tool.
Changes to this file may cause incorrect behavior and will be lost if
the code is regenerated.
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "Employee"
:setvar DefaultFilePrefix "Employee"
:setvar DefaultDataPath "C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\"
:setvar DefaultLogPath "C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\"

GO
:on error exit
GO
/*
Detect SQLCMD mode and disable script execution if SQLCMD mode is not supported.
To re-enable the script after enabling SQLCMD mode, execute the following:
SET NOEXEC OFF; 
*/
:setvar __IsSqlCmdEnabled "True"
GO
IF N'$(__IsSqlCmdEnabled)' NOT LIKE N'True'
    BEGIN
        PRINT N'SQLCMD mode must be enabled to successfully execute this script.';
        SET NOEXEC ON;
    END


GO
USE [$(DatabaseName)];


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET ANSI_NULLS ON,
                ANSI_PADDING ON,
                ANSI_WARNINGS ON,
                ARITHABORT ON,
                CONCAT_NULL_YIELDS_NULL ON,
                QUOTED_IDENTIFIER ON,
                ANSI_NULL_DEFAULT ON,
                CURSOR_DEFAULT LOCAL 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET PAGE_VERIFY NONE 
            WITH ROLLBACK IMMEDIATE;
    END


GO
ALTER DATABASE [$(DatabaseName)]
    SET TARGET_RECOVERY_TIME = 0 SECONDS 
    WITH ROLLBACK IMMEDIATE;


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET QUERY_STORE (QUERY_CAPTURE_MODE = ALL, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 367), MAX_STORAGE_SIZE_MB = 100) 
            WITH ROLLBACK IMMEDIATE;
    END


GO
PRINT N'Creating Table [dbo].[Employee]...';


GO
CREATE TABLE [dbo].[Employee] (
    [Id]         UNIQUEIDENTIFIER NOT NULL,
    [SupId]      UNIQUEIDENTIFIER NULL,
    [ManagerId]  UNIQUEIDENTIFIER NULL,
    [FirstName]  VARCHAR (30)     NOT NULL,
    [LastName]   VARCHAR (30)     NOT NULL,
    [Address1]   VARCHAR (100)    NOT NULL,
    [PayPerHour] DECIMAL (5, 2)   NOT NULL,
    CONSTRAINT [pk_Employee_Employee_Id] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating Table [dbo].[Manager]...';


GO
CREATE TABLE [dbo].[Manager] (
    [Id]           UNIQUEIDENTIFIER NOT NULL,
    [EmployeeId]   UNIQUEIDENTIFIER NOT NULL,
    [AnnualSalary] DECIMAL (5, 2)   NOT NULL,
    CONSTRAINT [pk_Employee_Manager_Id] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating Table [dbo].[Supervisor]...';


GO
CREATE TABLE [dbo].[Supervisor] (
    [Id]           UNIQUEIDENTIFIER NOT NULL,
    [EmployeeId]   UNIQUEIDENTIFIER NOT NULL,
    [AnnualSalary] DECIMAL (5, 2)   NOT NULL,
    CONSTRAINT [pk_Employee_Supervisor_Id] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating Foreign Key [dbo].[fk_Employee_Employee_SupId_Supervisor]...';


GO
ALTER TABLE [dbo].[Employee] WITH NOCHECK
    ADD CONSTRAINT [fk_Employee_Employee_SupId_Supervisor] FOREIGN KEY ([SupId]) REFERENCES [dbo].[Supervisor] ([Id]);


GO
PRINT N'Creating Foreign Key [dbo].[fk_Employee_Employee_ManagerId_Manager]...';


GO
ALTER TABLE [dbo].[Employee] WITH NOCHECK
    ADD CONSTRAINT [fk_Employee_Employee_ManagerId_Manager] FOREIGN KEY ([ManagerId]) REFERENCES [dbo].[Manager] ([Id]);


GO
PRINT N'Creating Foreign Key [dbo].[fk_Employee_Manager_EmployeeId_Employee]...';


GO
ALTER TABLE [dbo].[Manager] WITH NOCHECK
    ADD CONSTRAINT [fk_Employee_Manager_EmployeeId_Employee] FOREIGN KEY ([EmployeeId]) REFERENCES [dbo].[Employee] ([Id]);


GO
PRINT N'Creating Foreign Key [dbo].[fk_Employee_Superviosr_EmployeeId_Employee]...';


GO
ALTER TABLE [dbo].[Supervisor] WITH NOCHECK
    ADD CONSTRAINT [fk_Employee_Superviosr_EmployeeId_Employee] FOREIGN KEY ([EmployeeId]) REFERENCES [dbo].[Employee] ([Id]);


GO
PRINT N'Checking existing data against newly created constraints';


GO
USE [$(DatabaseName)];


GO
ALTER TABLE [dbo].[Employee] WITH CHECK CHECK CONSTRAINT [fk_Employee_Employee_SupId_Supervisor];

ALTER TABLE [dbo].[Employee] WITH CHECK CHECK CONSTRAINT [fk_Employee_Employee_ManagerId_Manager];

ALTER TABLE [dbo].[Manager] WITH CHECK CHECK CONSTRAINT [fk_Employee_Manager_EmployeeId_Employee];

ALTER TABLE [dbo].[Supervisor] WITH CHECK CHECK CONSTRAINT [fk_Employee_Superviosr_EmployeeId_Employee];


GO
PRINT N'Update complete.';


GO