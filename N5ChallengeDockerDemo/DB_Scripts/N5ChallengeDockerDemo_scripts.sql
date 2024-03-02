
/*****************************************************************************/
/********************initial data********************************************/
/****************************************************************************/

INSERT INTO Employee (employeeName)
VALUES
    ('John Smith'),
    ('Jane Doe'),
    ('Michael Johnson'),
    ('Eugenio Derbez'),
    ('Patrick Perez'),
    ('Marlon Bronson'),
    ('Jack Palance'),
    ('Martin Sang'),
    ('Domenica Tabacci'),
    ('Fernando Ramirez')

INSERT INTO PermissionType (permissionTypename)
VALUES
    ('Vacation'),
    ('Sick Leave'),
    ('Work From Home')


INSERT INTO Permission (EmployeeId, PermissionTypeId, permissionName)
VALUES
    (1, 1, 'Approved vacation for next week'),
    (2, 2, 'Sick leave due to flu'),
    (3, 3, 'Work from home request')

exec GetPermissions 1
exec GetPermissionList
exec GetEmployeeList

exec RequestPermission 10, 1, 'Two weeks vacation' 

exec ModifyPermission 5, "Working from Home" 

select * from Employees
select * from Permissions
select * from PermissionTypes


/****************************************************************************/
/********************STORED PROCEDURES***************************************/
/****************************************************************************/

USE [N5demo]
GO
/****** Object:  StoredProcedure [dbo].[GetPermissions]    Script Date: 2/3/2024 13:31:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[GetPermissions]
	@EmployeeId int
AS
	SELECT e.employeeName as empName, p.permissionName as perName, pt.permissionTypename as ptName, p.PermissionId, e.EmployeeId, pt.PermissionTypeId
	FROM Employee e	
	INNER JOIN permission p
	on e.EmployeeId = p.EmployeeId
	inner join permissiontype pt
	on pt.PermissionTypeId = p.PermissionTypeId
	where e.EmployeeId = @EmployeeId

RETURN 0


CREATE PROCEDURE RequestPermission
	@EmployeeId int,
	@PermissionID int,
	@Name nvarchar(50)
AS
	INSERT INTO Permission (EmployeeId, PermissionTypeId, permissionName)
	VALUES
		(@EmployeeId, @PermissionID, @Name)

		if @@ROWCOUNT > 0
		begin
			return @@IDENTITY --success
		end
		else
		begin
			return 0 --failure
		end


GO
/****** Object:  StoredProcedure [dbo].[GetPermissions]    Script Date: 2/3/2024 13:15:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[GetPermission]
	@EmployeeId int
AS
	SELECT e.employeename as empName, p.permissionname as perName, pt.permissionTypename as ptName, p.PermissionId, e.EmployeeId, pt.PermissionTypeId
	FROM Employee e	
	INNER JOIN permission p
	on e.employeeid = p.EmployeeId
	inner join permissiontype pt
	on pt.PermissionTypeId = p.PermissionTypeId
	where e.employeeid = @EmployeeId

RETURN 0


GO
/****** Object:  StoredProcedure [dbo].[GetPermissionList]    Script Date: 2/3/2024 13:15:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[GetPermissionList]
	
AS
	SELECT permissionTypeid, permissionTypename
	FROM PermissionType	

RETURN 0


CREATE PROCEDURE ModifyPermission
	@PermissionId int,
	@Name nvarchar(50)
AS
	UPDATE Permission
	SET permissionName = @Name
	WHERE PermissionId = @PermissionId

	if @@ROWCOUNT > 0
	begin
		RETURN 1
	end
	else
	begin
		RETURN 0
	end


