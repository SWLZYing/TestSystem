/*
	描述: 更新使用者帳號(Account)
	建立日期: 2021-12-02

	exec pro_resetAccount 1, 'ResetPWD001'
*/

CREATE PROCEDURE [dbo].[pro_resetAccount]
@id INT,
@pwd NVARCHAR(50)
AS
	UPDATE t_account WITH(ROWLOCK)
	   SET f_password = @pwd
		 , f_updateTime = GETDATE()
	 WHERE f_id = @id
RETURN 0
GO
GRANT EXECUTE
    ON OBJECT::[dbo].[pro_resetAccount] TO PUBLIC
    AS [dbo];
