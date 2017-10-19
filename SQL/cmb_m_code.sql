IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[cmb_m_code]') AND type in (N'U'))
	DROP TABLE [dbo].[cmb_m_code];
GO

CREATE TABLE [dbo].[cmb_m_code](
	[type] [nvarchar](50) NOT NULL,
	[type_nm] [nvarchar](100) NOT NULL,
	[cd] [nvarchar](30) NOT NULL,
	[cd_nm] [nvarchar](100) NOT NULL,
	[value] [nvarchar](1000) NULL,
	[disp_ord] [int] NULL,
	[comment] [nvarchar](1000) NULL,
 CONSTRAINT [pk_cmb_m_code] PRIMARY KEY CLUSTERED 
(
	[type] ASC,
	[cd] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

delete from cmb_m_code;
insert into cmb_m_code values ('BATCH_STATUS','','01',N'未授权','',1,'');
insert into cmb_m_code values ('BATCH_STATUS','','02',N'部分授权','',2,'');
insert into cmb_m_code values ('BATCH_STATUS','','03',N'已授权','',3,'');
insert into cmb_m_code values ('BATCH_STATUS','','04',N'已发送','',4,'');
insert into cmb_m_code values ('BATCH_STATUS','','05',N'已完成','',5,'');
insert into cmb_m_code values ('BATCH_STATUS','','97',N'被拒绝','',97,'');
insert into cmb_m_code values ('BATCH_STATUS','','98',N'超上限','',98,'');
insert into cmb_m_code values ('BATCH_STATUS','','99',N'未成功','',99,'');

insert into cmb_m_code values ('PAY_STATUS','','01',N'未支付','',0,'');
insert into cmb_m_code values ('PAY_STATUS','','02',N'被受理','',0,'');
insert into cmb_m_code values ('PAY_STATUS','','S',N'成功','',1,'');
insert into cmb_m_code values ('PAY_STATUS','','F',N'失败','',2,'');
insert into cmb_m_code values ('PAY_STATUS','','B',N'退票','',3,'');

insert into cmb_m_code values ('CCY_TYPE','','10',N'人民币','',1,'');

insert into cmb_m_code values ('DEBIT_ACCOUNT','','CNY','122904286510202',N'渣打环球商业服务有限公司',1,'');--人民币
insert into cmb_m_code values ('DEBIT_ACCOUNT','','USD','122904286532801',N'渣打环球商业服务有限公司',2,'');--美元
insert into cmb_m_code values ('DEBIT_ACCOUNT','','HKD','122904286521601',N'渣打环球商业服务有限公司',3,'');--港币
insert into cmb_m_code values ('DEBIT_ACCOUNT','','GBP','122904286543802',N'渣打环球商业服务有限公司',4,'');--英镑
insert into cmb_m_code values ('DEBIT_ACCOUNT','','SGD','122904286569901',N'渣打环球商业服务有限公司',5,'');--新加坡元
insert into cmb_m_code values ('DEBIT_ACCOUNT','','AUD','122904286529701',N'渣打环球商业服务有限公司',6,'');--澳元
