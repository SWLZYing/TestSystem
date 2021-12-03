/*
	描述: 更新使用者帳號(Account)
	建立日期: 2021-12-02

	有變更密碼：exec pro_updateAccount 
					@id = 1, @pwd = '123456789', @nickname = 'testUser001'
	無變更密碼：exec pro_updateAccount 
					@id = 1, @pwd = '', @nickname = 'testUser001'
*/

CREATE PROCEDURE [dbo].[pro_updateAccount]
@id INT,
@pwd NVARCHAR(50),
@nickname NVARCHAR(20)
AS
	UPDATE t_account WITH(ROWLOCK)
	   SET f_password = CASE WHEN @pwd = '' THEN f_password ELSE @pwd END
	     , f_nickname = @nickname
		 , f_updateTime = GETDATE()
	 WHERE f_id = @id
RETURN 0
GO
GRANT EXECUTE
    ON OBJECT::[dbo].[pro_updateAccount] TO PUBLIC
    AS [dbo];
