/*
	描述: 取得所有example
	建立日期: 2021-12-01

	exec pro_getExample
*/

CREATE PROCEDURE [dbo].[pro_getExample]
AS
	SELECT f_id, f_content FROM t_example WITH(NOLOCK)
RETURN 0
GO
GRANT EXECUTE
    ON OBJECT::[dbo].[pro_getExample] TO PUBLIC
    AS [dbo];
