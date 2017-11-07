IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[cmb_t_swift_log]') AND type in (N'U'))
	DROP TABLE [dbo].[cmb_t_swift_log]
GO

CREATE TABLE [dbo].[cmb_t_swift_log](
	[swift_seq] [int] IDENTITY(1,1) NOT NULL,
	[swift_type] [nvarchar](50) NOT NULL,
	[content] [nvarchar](max) NOT NULL,
	[ins_id] [nvarchar](10) NOT NULL,
	[ins_time] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[swift_seq] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)

GO

