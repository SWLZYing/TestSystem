/*
	描述: 登入
	建立日期: 2021-12-03

	狀態 
		0: 查無帳號
		1: 登入成功
		2: 密碼錯誤

	exec pro_signIn 
		@acc = 'USER001', 
		@pwd = 'PWD001'
*/

CREATE PROCEDURE [dbo].[pro_signIn]
@acc NVARCHAR(20),
@pwd NVARCHAR(50)
AS
	DECLARE @dbId INT
	DECLARE @dbPwd NVARCHAR(50)

	SELECT @dbId = f_id, @dbPwd = f_password
	  FROM t_account WITH(NOLOCK)
	 WHERE f_account = @acc

	IF @dbId IS NULL
	BEGIN
		SELECT 0
	END
	ELSE
	BEGIN
		IF @dbPwd != @pwd
		BEGIN
			SELECT 2
		END
		ELSE
		BEGIN
			SELECT 1

			UPDATE t_account WITH(ROWLOCK)
			   SET f_lastLoginTime = GETDATE() 
			     , f_updateTime = GETDATE()
			OUTPUT inserted.*
			 WHERE f_id = @dbId

			INSERT INTO t_loginRecord(f_accountId, f_loginTime, f_createTime, f_updateTime)
			VALUES (@dbId, GETDATE(), GETDATE(), GETDATE())
		END
	END
RETURN 0
GO
GRANT EXECUTE
    ON OBJECT::[dbo].[pro_signIn] TO PUBLIC
    AS [dbo];
