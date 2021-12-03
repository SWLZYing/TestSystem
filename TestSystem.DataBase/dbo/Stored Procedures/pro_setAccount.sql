/*
	描述: 建立使用者帳號(Account)
	建立日期: 2021-12-02

	exec pro_setAccount 
		@acc = 'USER001', @pwd = 'PWD001', @nickname = 'User001'
*/

CREATE PROCEDURE [dbo].[pro_setAccount]
@acc NVARCHAR(20),
@pwd NVARCHAR(50),
@nickname NVARCHAR(20)
AS
	DECLARE @id INT

    INSERT INTO t_account(f_account, f_password, f_nickname, f_lastLoginTime, f_createTime, f_updateTime)
	VALUES (@acc, @pwd, @nickname, GETDATE(), GETDATE(), GETDATE())

	SET @id = scope_identity()

    INSERT INTO t_accountLevel(f_accountId, f_level, f_createTime, f_updateTime)
	VALUES (@id, 1, GETDATE(), GETDATE())
RETURN 0
GO
GRANT EXECUTE
    ON OBJECT::[dbo].[pro_setAccount] TO PUBLIC
    AS [dbo];
