IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[cmb_m_role]') AND type in (N'U'))
	DROP TABLE [dbo].[cmb_m_role]
GO

CREATE TABLE [dbo].[cmb_m_role](
	[role_id] [nvarchar](10) NOT NULL,
	[role_nm] [nvarchar](50) NOT NULL,
	[amount_limit] [nvarchar](50) NULL,
 CONSTRAINT [pk_cmb_m_role] PRIMARY KEY CLUSTERED 
(
	[role_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

delete from cmb_m_role;
insert into cmb_m_role values ('maker','Maker','');
insert into cmb_m_role values ('checker1','Checker1','15000000');
insert into cmb_m_role values ('checker2','Checker2','');
--insert into cmb_m_role values ('admin','Administrator','');
