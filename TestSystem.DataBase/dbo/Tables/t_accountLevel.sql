CREATE TABLE [dbo].[t_accountLevel]
(
	[f_id]		INT	IDENTITY	(1, 1)	NOT NULL,
	[f_accountId] INT					NOT NULL,
	[f_level] INT						NOT NULL,
	[f_createTime] DATETIME				NOT NULL,
	[f_updateTime] DATETIME				NOT NULL,
	CONSTRAINT [PK_accountLevel] PRIMARY KEY CLUSTERED ([f_id] ASC)
)
