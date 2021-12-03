/*
	描述: 登入
	建立日期: 2021-12-03

	exec pro_signIn 
		@acc = 'USER001', @pwd = 'PWD001'
*/

CREATE PROCEDURE [dbo].[pro_signIn]
@acc NVARCHAR(20),
@pwd NVARCHAR(50)
AS
	DECLARE @id INT

	SELECT @id = f_id 
	  FROM t_account WITH(NOLOCK)
	 WHERE f_account = @acc
	   AND f_password = @pwd

    INSERT INTO t_loginRecord(f_accountId, f_loginTime, f_createTime, f_updateTime)
	VALUES (@id, GETDATE(), GETDATE(), GETDATE())
RETURN 0
GO
GRANT EXECUTE
    ON OBJECT::[dbo].[pro_signIn] TO PUBLIC
    AS [dbo];
