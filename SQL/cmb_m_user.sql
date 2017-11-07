IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[cmb_m_user]') AND type in (N'U'))
	DROP TABLE [dbo].[cmb_m_user]
GO

CREATE TABLE [dbo].[cmb_m_user](
	[user_id] [nvarchar](10) NOT NULL,
	[role_id] [nvarchar](10) NOT NULL,
	[cmb_account] [nvarchar](50) NULL,
 CONSTRAINT [pk_cmb_m_user] PRIMARY KEY CLUSTERED 
(
	[user_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

insert into cmb_m_user values('1560002','maker',N'');
insert into cmb_m_user values('1560003','checker1',N'银企直连测试用户110');
insert into cmb_m_user values('1560004','checker2',N'银企直连测试用户110');

--update cmb_t_batch set appr1_id = '1560001'
--where batch_id = '0000000009'

delete from cmb_m_user;
insert into cmb_m_user values('1441154','maker',N'');
insert into cmb_m_user values('1455079','maker',N'');
insert into cmb_m_user values('1416021','checker1',N'');
insert into cmb_m_user values('1305931','checker1',N'');
insert into cmb_m_user values('1441180','checker2',N'');
insert into cmb_m_user values('1510855','checker2',N'');





