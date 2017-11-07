IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[cmb_m_sysconf]') AND type in (N'U'))
	DROP TABLE [dbo].[cmb_m_sysconf]
GO

CREATE TABLE [dbo].[cmb_m_sysconf](
	[conf_key] [nvarchar](50) NOT NULL,
	[conf_value] [nvarchar](200) NOT NULL,
	[comment] [nvarchar](200) NULL,
);
alter table cmb_m_sysconf add constraint pk_cmb_m_sysconf primary key(conf_key);

delete from cmb_m_sysconf;
insert into cmb_m_sysconf values('MAX_PAGE_CNT','10','');
insert into cmb_m_sysconf values('DEBIT_ACC','755915712310104','');
insert into cmb_m_sysconf values('DEBIT_CITY','75','');
