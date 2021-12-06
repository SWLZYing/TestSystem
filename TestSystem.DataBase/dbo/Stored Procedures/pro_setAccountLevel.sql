/*
	描述: 建立使用者帳號等級(AccountLevel)
	建立日期: 2021-12-06

	帳號建立時，預設等級為1

	exec pro_setAccount 
		@accId = '2'
*/

CREATE PROCEDURE [dbo].[pro_setAccountLevel]
@accId INT
AS
    INSERT INTO t_accountLevel(f_accountId, f_level, f_createTime, f_updateTime)
	OUTPUT inserted.*
	VALUES (@accId, 1, GETDATE(), GETDATE())
RETURN 0
GO
GRANT EXECUTE
    ON OBJECT::[dbo].[pro_setAccountLevel] TO PUBLIC
    AS [dbo];
