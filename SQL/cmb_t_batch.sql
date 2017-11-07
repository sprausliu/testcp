IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[cmb_t_batch]') AND type in (N'U'))
	DROP TABLE [dbo].[cmb_t_batch]
GO

CREATE TABLE [dbo].[cmb_t_batch](
	batch_id nvarchar(10) NOT NULL,
	pay_cnt int NOT NULL,
	ttl_amt numeric(30,2) NOT NULL,
	status_id nvarchar(10) NULL,
	maker_id nvarchar(10) NULL,
	maker_time datetime NULL,
	appr1_id nvarchar(10) NULL,
	appr1_time datetime NULL,
	appr2_id nvarchar(10) NULL,
	appr2_time datetime NULL,
	rejc_id nvarchar(10) NULL,
	rejc_time datetime NULL,
	update_id nvarchar(10) NULL,
	update_time datetime NULL,
	
 CONSTRAINT [pk_cmb_t_batch] PRIMARY KEY CLUSTERED 
(
	[batch_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

