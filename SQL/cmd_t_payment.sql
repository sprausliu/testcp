IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[cmb_t_payment]') AND type in (N'U'))
	DROP TABLE [dbo].[cmb_t_payment]
GO

CREATE TABLE [dbo].[cmb_t_payment](
	YURREF nvarchar(30) NOT NULL,		--ҵ��ο���
	batch_id nvarchar(10) NOT NULL,		--batch id
	status_id nvarchar(10) NULL,		--״̬
	comment nvarchar(1000) NULL,		--��ע
	EPTDAT nvarchar(10) NULL,			--������
	OPRDAT nvarchar(10) NULL,			--������/֧����
	DBTACC nvarchar(35) NOT NULL,		--�����ʺ�
	DBTBBK nvarchar(2) NOT NULL,		--����������������
	TRSAMT numeric(15,2) NOT NULL,		--���׽��
	CCYNBR nvarchar(2) NOT NULL,		--���ִ���
	NUSAGE nvarchar(62) NOT NULL,		--��;
	BUSNAR nvarchar(100) NULL,			--ҵ��ժҪ
	CRTACC nvarchar(35) NOT NULL,		--�շ��ʺ�
	CRTNAM nvarchar(31) NULL,			--�շ��ʻ���
	LRVEAN nvarchar(50) NULL,			--�շ�������
	BRDNBR nvarchar(30) NULL,			--�����Զ�֧���շ����к�/�շ��к�
	BNKFLG nvarchar(1) NOT NULL,		--ϵͳ�����־/Y�����У�N�������У�
	CRTBNK nvarchar(31) NULL,			--�շ������� ����֧������
	CTYCOD nvarchar(4) NULL,			--���д���
	CRTADR nvarchar(31) NULL,			--�շ��е�ַ ����֧������
	NTFCH1 nvarchar(36) NULL,			--�շ������ʼ�
	update_id nvarchar(10) NULL,
	update_time datetime NULL,
	del_flg nvarchar(1) default ('0') NOT NULL,
 CONSTRAINT [pk_cmb_t_payment] PRIMARY KEY CLUSTERED 
(
	YURREF ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

delete from cmb_t_payment;
insert into cmb_t_payment values ('0000000001','0000000001','0','','2017/09/18','','3122334444555','22',15123445.11,'01',N'����',''
,'1234567890',N'sc bank china','','30000000000000','Y',N'��������������','','','','1560003',getdate());
insert into cmb_t_payment values ('0000000002','0000000002','0','','2017/09/18','','3122334444555','22',15123445.11,'01',N'����',''
,'1234567890',N'sc bank china','','30000000000000','Y',N'��������������','','','','1560003',getdate());
insert into cmb_t_payment values ('0000000003','0000000003','0','','2017/09/18','','3122334444555','22',15123445.11,'01',N'����',''
,'1234567890',N'sc bank china','','30000000000000','Y',N'��������������','','','','1560003',getdate());
insert into cmb_t_payment values ('0000000004','0000000004','0','','2017/09/18','','3122334444555','22',15123445.11,'01',N'����',''
,'1234567890',N'sc bank china','','30000000000000','Y',N'��������������','','','','1560003',getdate());
insert into cmb_t_payment values ('0000000005','0000000005','0','','2017/09/18','','3122334444555','22',15123445.11,'01',N'����',''
,'1234567890',N'sc bank china','','30000000000000','Y',N'��������������','','','','1560003',getdate());
insert into cmb_t_payment values ('0000000006','0000000006','0','','2017/09/18','','3122334444555','22',15123445.11,'01',N'����',''
,'1234567890',N'sc bank china','','30000000000000','Y',N'��������������','','','','1560003',getdate());
insert into cmb_t_payment values ('0000000007','0000000007','0','','2017/09/18','','3122334444555','22',15123445.11,'01',N'����',''
,'1234567890',N'sc bank china','','30000000000000','Y',N'��������������','','','','1560003',getdate());
insert into cmb_t_payment values ('0000000008','0000000008','0','','2017/09/18','','3122334444555','22',15123445.11,'01',N'����',''
,'1234567890',N'sc bank china','','30000000000000','Y',N'��������������','','','','1560003',getdate());
insert into cmb_t_payment values ('0000000009','0000000009','0','','2017/09/18','','3122334444555','22',15123445.11,'01',N'����',''
,'1234567890',N'sc bank china','','30000000000000','Y',N'��������������','','','','1560003',getdate());
insert into cmb_t_payment values ('0000000010','0000000010','0','','2017/09/18','','3122334444555','22',15123445.11,'01',N'����',''
,'1234567890',N'sc bank china','','30000000000000','Y',N'��������������','','','','1560003',getdate());
insert into cmb_t_payment values ('0000000011','0000000011','0','','2017/09/18','','3122334444555','22',15123445.11,'01',N'����',''
,'1234567890',N'sc bank china','','30000000000000','Y',N'��������������','','','','1560003',getdate());
insert into cmb_t_payment values ('0000000012','0000000012','0','','2017/09/18','','3122334444555','22',15123445.11,'01',N'����',''
,'1234567890',N'sc bank china','','30000000000000','Y',N'��������������','','','','1560003',getdate());