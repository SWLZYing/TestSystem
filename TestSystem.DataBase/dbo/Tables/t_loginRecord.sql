CREATE TABLE [dbo].[t_loginRecord]
(
	[f_id]		INT	IDENTITY	(1, 1)	NOT NULL,
	[f_accountId] INT					NOT NULL,
	[f_loginTime] DATETIME				NOT NULL,
	[f_createTime] DATETIME				NOT NULL,
	[f_updateTime] DATETIME				NOT NULL,
	CONSTRAINT [PK_loginRecord] PRIMARY KEY CLUSTERED ([f_id] ASC)
)
