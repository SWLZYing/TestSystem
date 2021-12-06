/*
	描述: 建立使用者帳號(Account)
	建立日期: 2021-12-02

	exec pro_setAccount 
		@acc = 'USER002', 
		@pwd = 'PWD002', 
		@nickname = 'User002'
*/

CREATE PROCEDURE [dbo].[pro_setAccount]
@acc NVARCHAR(20),
@pwd NVARCHAR(50),
@nickname NVARCHAR(20)
AS
    INSERT INTO t_account(f_account, f_password, f_nickname, f_lastLoginTime, f_createTime, f_updateTime)
	OUTPUT inserted.*
	VALUES (@acc, @pwd, @nickname, GETDATE(), GETDATE(), GETDATE())
RETURN 0
GO
GRANT EXECUTE
    ON OBJECT::[dbo].[pro_setAccount] TO PUBLIC
    AS [dbo];
