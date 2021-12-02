/*
	描述: 取得使用者帳號(Account)
	建立日期: 2021-12-02

	exec pro_getAccount 1
*/

CREATE PROCEDURE [dbo].[pro_getAccount]
@id INT
AS
	SELECT f_id, f_account, f_nickname, f_lastLoginTime, f_createTime, f_updateTime 
	  FROM t_account WITH(NOLOCK)
	 WHERE f_id = @id
RETURN 0
GO
GRANT EXECUTE
    ON OBJECT::[dbo].[pro_getAccount] TO PUBLIC
    AS [dbo];
