/*
部署後指令碼樣板							
--------------------------------------------------------------------------------------
 此檔案包含要附加到組建指令碼的 SQL 陳述式		
 使用 SQLCMD 語法可將檔案包含在部署後指令碼中			
 範例:      :r .\myfile.sql								
 使用 SQLCMD 語法可參考部署後指令碼中的變數		
 範例:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

INSERT INTO t_example (f_content) 
VALUES    ('TEST001')
        , ('TEST002')

INSERT INTO t_account (f_account, f_password, f_nickname, f_lastLoginTime, f_createTime, f_updateTime)
VALUES ('USER001', 'PWD001', 'User001', GETDATE(), GETDATE(), GETDATE())

INSERT INTO t_accountLevel (f_accountId, f_level, f_createTime, f_updateTime)
VALUES (1, 1, GETDATE(), GETDATE())
