CREATE TABLE [dbo].[t_example]
(
	[f_id]		INT	IDENTITY	(1, 1)	NOT NULL,
	[f_content] NVARCHAR(50)			NOT NULL,
	CONSTRAINT [PK_example] PRIMARY KEY CLUSTERED ([f_id] ASC)
)
