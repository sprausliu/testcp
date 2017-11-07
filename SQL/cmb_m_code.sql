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
insert into cmb_m_code values ('BATCH_STATUS','','01',N'δ��Ȩ','',1,'');
insert into cmb_m_code values ('BATCH_STATUS','','02',N'������Ȩ','',2,'');
insert into cmb_m_code values ('BATCH_STATUS','','03',N'����Ȩ','',3,'');
insert into cmb_m_code values ('BATCH_STATUS','','04',N'�ѷ���','',4,'');
insert into cmb_m_code values ('BATCH_STATUS','','05',N'�����','',5,'');
insert into cmb_m_code values ('BATCH_STATUS','','97',N'���ܾ�','',97,'');
insert into cmb_m_code values ('BATCH_STATUS','','98',N'������','',98,'');
insert into cmb_m_code values ('BATCH_STATUS','','99',N'δ�ɹ�','',99,'');

insert into cmb_m_code values ('PAY_STATUS','','01',N'δ֧��','',0,'');
insert into cmb_m_code values ('PAY_STATUS','','02',N'������','',0,'');
insert into cmb_m_code values ('PAY_STATUS','','S',N'�ɹ�','',1,'');
insert into cmb_m_code values ('PAY_STATUS','','F',N'ʧ��','',2,'');
insert into cmb_m_code values ('PAY_STATUS','','B',N'��Ʊ','',3,'');

insert into cmb_m_code values ('CCY_TYPE','','10',N'�����','',1,'');

insert into cmb_m_code values ('DEBIT_ACCOUNT','','CNY','122904286510202',N'��������ҵ�������޹�˾',1,'');--�����
insert into cmb_m_code values ('DEBIT_ACCOUNT','','USD','122904286532801',N'��������ҵ�������޹�˾',2,'');--��Ԫ
insert into cmb_m_code values ('DEBIT_ACCOUNT','','HKD','122904286521601',N'��������ҵ�������޹�˾',3,'');--�۱�
insert into cmb_m_code values ('DEBIT_ACCOUNT','','GBP','122904286543802',N'��������ҵ�������޹�˾',4,'');--Ӣ��
insert into cmb_m_code values ('DEBIT_ACCOUNT','','SGD','122904286569901',N'��������ҵ�������޹�˾',5,'');--�¼���Ԫ
insert into cmb_m_code values ('DEBIT_ACCOUNT','','AUD','122904286529701',N'��������ҵ�������޹�˾',6,'');--��Ԫ
