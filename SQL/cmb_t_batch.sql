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

delete from cmb_t_batch;
insert into cmb_t_batch values ('0000000001',20,2000000.11,'01','1560003',getdate(),null,null,null,null,null,null,'1560003',getdate());
insert into cmb_t_batch values ('0000000002',20,2000000.11,'01','1560003',getdate(),null,null,null,null,null,null,'1560003',getdate());
insert into cmb_t_batch values ('0000000003',20,2000000.11,'01','1560003',getdate(),null,null,null,null,null,null,'1560003',getdate());
insert into cmb_t_batch values ('0000000004',20,2000000.11,'01','1560003',getdate(),null,null,null,null,null,null,'1560003',getdate());
insert into cmb_t_batch values ('0000000005',20,2000000.11,'01','1560003',getdate(),null,null,null,null,null,null,'1560003',getdate());
insert into cmb_t_batch values ('0000000006',20,2000000.11,'01','1560003',getdate(),null,null,null,null,null,null,'1560003',getdate());
insert into cmb_t_batch values ('0000000007',20,2000000.11,'01','1560003',getdate(),null,null,null,null,null,null,'1560003',getdate());
insert into cmb_t_batch values ('0000000008',20,2000000.11,'01','1560003',getdate(),null,null,null,null,null,null,'1560003',getdate());
insert into cmb_t_batch values ('0000000009',20,2000000.11,'01','1560003',getdate(),null,null,null,null,null,null,'1560003',getdate());
insert into cmb_t_batch values ('0000000010',20,2000000.11,'01','1560003',getdate(),null,null,null,null,null,null,'1560003',getdate());
insert into cmb_t_batch values ('0000000011',20,2000000.11,'01','1560003',getdate(),null,null,null,null,null,null,'1560003',getdate());
insert into cmb_t_batch values ('0000000012',20,2000000.11,'01','1560003',getdate(),null,null,null,null,null,null,'1560003',getdate());
