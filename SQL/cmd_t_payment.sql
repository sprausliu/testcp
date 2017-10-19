IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[cmb_t_payment]') AND type in (N'U'))
	DROP TABLE [dbo].[cmb_t_payment]
GO

CREATE TABLE [dbo].[cmb_t_payment](
	YURREF nvarchar(30) NOT NULL,		--业务参考号
	batch_id nvarchar(10) NOT NULL,		--batch id
	status_id nvarchar(10) NULL,		--状态
	comment nvarchar(1000) NULL,		--备注
	EPTDAT nvarchar(10) NULL,			--期望日
	OPRDAT nvarchar(10) NULL,			--经办日/支付日
	DBTACC nvarchar(35) NOT NULL,		--付方帐号
	DBTBBK nvarchar(2) NOT NULL,		--付方开户地区代码
	TRSAMT numeric(15,2) NOT NULL,		--交易金额
	CCYNBR nvarchar(2) NOT NULL,		--币种代码
	NUSAGE nvarchar(62) NOT NULL,		--用途
	BUSNAR nvarchar(100) NULL,			--业务摘要
	CRTACC nvarchar(35) NOT NULL,		--收方帐号
	CRTNAM nvarchar(31) NULL,			--收方帐户名
	LRVEAN nvarchar(50) NULL,			--收方长户名
	BRDNBR nvarchar(30) NULL,			--人行自动支付收方联行号/收方行号
	BNKFLG nvarchar(1) NOT NULL,		--系统内外标志/Y：招行；N：非招行；
	CRTBNK nvarchar(31) NULL,			--收方开户行 跨行支付必填
	CTYCOD nvarchar(4) NULL,			--城市代码
	CRTADR nvarchar(31) NULL,			--收方行地址 跨行支付必填
	NTFCH1 nvarchar(36) NULL,			--收方电子邮件
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
insert into cmb_t_payment values ('0000000001','0000000001','0','','2017/09/18','','3122334444555','22',15123445.11,'01',N'付款',''
,'1234567890',N'sc bank china','','30000000000000','Y',N'渣打银行天津分行','','','','1560003',getdate());
insert into cmb_t_payment values ('0000000002','0000000002','0','','2017/09/18','','3122334444555','22',15123445.11,'01',N'付款',''
,'1234567890',N'sc bank china','','30000000000000','Y',N'渣打银行天津分行','','','','1560003',getdate());
insert into cmb_t_payment values ('0000000003','0000000003','0','','2017/09/18','','3122334444555','22',15123445.11,'01',N'付款',''
,'1234567890',N'sc bank china','','30000000000000','Y',N'渣打银行天津分行','','','','1560003',getdate());
insert into cmb_t_payment values ('0000000004','0000000004','0','','2017/09/18','','3122334444555','22',15123445.11,'01',N'付款',''
,'1234567890',N'sc bank china','','30000000000000','Y',N'渣打银行天津分行','','','','1560003',getdate());
insert into cmb_t_payment values ('0000000005','0000000005','0','','2017/09/18','','3122334444555','22',15123445.11,'01',N'付款',''
,'1234567890',N'sc bank china','','30000000000000','Y',N'渣打银行天津分行','','','','1560003',getdate());
insert into cmb_t_payment values ('0000000006','0000000006','0','','2017/09/18','','3122334444555','22',15123445.11,'01',N'付款',''
,'1234567890',N'sc bank china','','30000000000000','Y',N'渣打银行天津分行','','','','1560003',getdate());
insert into cmb_t_payment values ('0000000007','0000000007','0','','2017/09/18','','3122334444555','22',15123445.11,'01',N'付款',''
,'1234567890',N'sc bank china','','30000000000000','Y',N'渣打银行天津分行','','','','1560003',getdate());
insert into cmb_t_payment values ('0000000008','0000000008','0','','2017/09/18','','3122334444555','22',15123445.11,'01',N'付款',''
,'1234567890',N'sc bank china','','30000000000000','Y',N'渣打银行天津分行','','','','1560003',getdate());
insert into cmb_t_payment values ('0000000009','0000000009','0','','2017/09/18','','3122334444555','22',15123445.11,'01',N'付款',''
,'1234567890',N'sc bank china','','30000000000000','Y',N'渣打银行天津分行','','','','1560003',getdate());
insert into cmb_t_payment values ('0000000010','0000000010','0','','2017/09/18','','3122334444555','22',15123445.11,'01',N'付款',''
,'1234567890',N'sc bank china','','30000000000000','Y',N'渣打银行天津分行','','','','1560003',getdate());
insert into cmb_t_payment values ('0000000011','0000000011','0','','2017/09/18','','3122334444555','22',15123445.11,'01',N'付款',''
,'1234567890',N'sc bank china','','30000000000000','Y',N'渣打银行天津分行','','','','1560003',getdate());
insert into cmb_t_payment values ('0000000012','0000000012','0','','2017/09/18','','3122334444555','22',15123445.11,'01',N'付款',''
,'1234567890',N'sc bank china','','30000000000000','Y',N'渣打银行天津分行','','','','1560003',getdate());