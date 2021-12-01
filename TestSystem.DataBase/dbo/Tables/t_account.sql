CREATE TABLE [dbo].[t_account]
(
	[f_id]		INT	IDENTITY	(1, 1)	NOT NULL,
	[f_account] NVARCHAR(20)			NOT NULL,
	[f_password] NVARCHAR(50)			NOT NULL,
	[f_nickname] NVARCHAR(20)			NOT NULL,
	[f_lastLoginTime] DATETIME			NOT NULL,
	[f_createTime] DATETIME				NOT NULL,
	[f_updateTime] DATETIME				NOT NULL,
	CONSTRAINT [PK_account] PRIMARY KEY CLUSTERED ([f_id] ASC)
)
