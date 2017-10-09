using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMBConversionTool
{
    public class PaymentReqt
    {
        public string YURREF { get; set; }  // 业务参考号
        public string EPTDAT { get; set; }  // 期望日
        public string EPTTIM { get; set; }  // 期望时间
        public string DBTACC { get; set; }  // 付方帐号
        public string DBTBBK { get; set; }  // 付方开户地区代码
        public string TRSAMT { get; set; }  // 交易金额
        public string CCYNBR { get; set; }  // 币种代码
        public string STLCHN { get; set; }  // 结算方式代码
        public string NUSAGE { get; set; }  // 用途
        public string BUSNAR { get; set; }  // 业务摘要
        public string CRTACC { get; set; }  // 收方帐号
        public string CRTNAM { get; set; }  // 收方帐户名
        public string LRVEAN { get; set; }  // 收方长户名
        public string BRDNBR { get; set; }  // 收方行号
        public string BNKFLG { get; set; }  // 系统内外标志
        public string CRTBNK { get; set; }  // 收方开户行
        public string CTYCOD { get; set; }  // 城市代码
        public string CRTADR { get; set; }  // 收方行地址
        public string CRTFLG { get; set; }  // 收方信息不检查标志
        public string NTFCH1 { get; set; }  // 收方电子邮件
        public string NTFCH2 { get; set; }  // 收方移动电话
        public string CRTSQN { get; set; }  // 收方编号
        public string TRSTYP { get; set; }  // 业务种类
        public string RCVCHK { get; set; }  // 行内收方账号户名校验
        public string RSV28Z { get; set; }  // 保留字段
    }

    public class PaymentResp
    {
        public string FUNNAM { get; set; }  // 函数名
        public string DATTYP { get; set; }  // 数据格式
        public string RETCOD { get; set; }  // 返回代码
        public string ERRMSG { get; set; }  // 错误信息

        public string SQRNBR { get; set; }  // 流水号
        public string YURREF { get; set; }  // 业务参考号
        public string REQNBR { get; set; }  // 流程实例号
        public string REQSTS { get; set; }  // 业务请求状态
        public string RTNFLG { get; set; }  // 业务处理结果
        public string OPRSQN { get; set; }  // 待处理操作序列
        public string OPRALS { get; set; }  // 操作别名
        public string ERRCOD { get; set; }  // 错误码
        public string ERRTXT { get; set; }  // 错误文本
    }

    public class NoticeResp
    {
        public string MSGTYP { get; set; }  // 通知类型
        public string MSGNBR { get; set; }  // 通知序号
        public string REQNBR { get; set; }  // 流程实例号
        public string FLWTYP { get; set; }  // 业务类型
        public string REQDTA { get; set; }  // 业务数据
        public string BBKNBR { get; set; }  // 分行地区码
        public string KEYVAL { get; set; }  // 帐号
        public string CCYNBR { get; set; }  // 币种代码
        public string YURREF { get; set; }  // 业务参考号
        public string ACCNAM { get; set; }  // 帐户名称
        public string ENDAMT { get; set; }  // 金额
        public string EPTDAT { get; set; }  // 期望日期
        public string EPTTIM { get; set; }  // 期望时间
        public string OPRDAT { get; set; }  // 经办日期
        public string RTNFLG { get; set; }  // 业务请求结果
        public string RTNDSP { get; set; }  // 业务结果描述
    }

    public class EnquiryResp
    {
        //public string C_BUSCOD { get; set; }  // 业务类型
        //public string BUSCOD { get; set; }  // 业务代码
        //public string BUSMOD { get; set; }  // 业务模式
        //public string C_DBTBBK { get; set; }  // 付方开户地区
        //public string DBTBBK { get; set; }  // 付方开户地区代码
        //public string DBTACC { get; set; }  // 付方帐号
        //public string DBTNAM { get; set; }  // 付方帐户名
        //public string C_DBTREL { get; set; }  // 付方公司名
        //public string DBTBNK { get; set; }  // 付方开户行
        //public string DBTADR { get; set; }  // 付方行地址
        //public string C_CRTBBK { get; set; }  // 收方开户地区
        //public string CRTBBK { get; set; }  // 收方开户地区代码
        //public string CRTACC { get; set; }  // 收方帐号
        //public string CRTNAM { get; set; }  // 收方帐户名
        //public string RCVBRD { get; set; }  // 收方大额行号
        //public string C_CRTREL { get; set; }  // 收方公司名
        //public string CRTBNK { get; set; }  // 收方开户行
        //public string CRTADR { get; set; }  // 收方行地址
        //public string C_GRPBBK { get; set; }  // 母公司开户地区
        //public string GRPBBK { get; set; }  // 母公司开户地区代码
        //public string GRPACC { get; set; }  // 母公司帐号
        //public string GRPNAM { get; set; }  // 母公司帐户名
        //public string C_CCYNBR { get; set; }  // 币种
        //public string CCYNBR { get; set; }  // 币种代码
        //public string TRSAMT { get; set; }  // 交易金额
        //public string EPTDAT { get; set; }  // 期望日
        //public string EPTTIM { get; set; }  // 期望时间
        //public string BNKFLG { get; set; }  // 系统内外标志
        //public string REGFLG { get; set; }  // 同城异地标志
        //public string C_STLCHN { get; set; }  // 结算方式
        //public string STLCHN { get; set; }  // 结算方式代码
        //public string NUSAGE { get; set; }  // 用途
        //public string NTFCH1 { get; set; }  // 收方电子邮件
        //public string NTFCH2 { get; set; }  // 收方移动电话

        public string OPRDAT { get; set; }  // 经办日期
        public string YURREF { get; set; }  // 业务参考号
        public string REQNBR { get; set; }  // 流程实例号
        public string BUSNAR { get; set; }  // 业务摘要
        public string C_REQSTS { get; set; }  // 业务请求状态
        public string REQSTS { get; set; }  // 业务请求状态代码
        public string C_RTNFLG { get; set; }  // 业务处理结果
        public string RTNFLG { get; set; }  // 业务处理结果代码
        public string OPRALS { get; set; }  // 操作别名
        public string RTNNAR { get; set; }  // 结果摘要
        public string RTNDAT { get; set; }  // 退票日期
        public string ATHFLG { get; set; }  // 是否有附件信息
        public string LGNNAM { get; set; }  // 经办用户登录名
        public string USRNAM { get; set; }  // 经办用户姓名

    }
}
