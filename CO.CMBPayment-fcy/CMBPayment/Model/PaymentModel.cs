using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMBPayment
{
    public class PaymentReqt
    {
        public string YURREF { get; set; }  // 业务参考号
        public string EPTDAT { get; set; }  // 期望日
        public string OPRDAT { get; set; }  // 经办日/支付日
        //public string EPTTIM { get; set; }  // 期望时间
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
        public string BRDNBR { get; set; }  // 人行自动支付收方联行号/收方行号
        public string BNKFLG { get; set; }  // 系统内外标志/Y：招行；N：非招行；
        public string CRTBNK { get; set; }  // 收方开户行 跨行支付必填
        //public string CTYCOD { get; set; }  // 城市代码
        public string CRTADR { get; set; }  // 收方行地址 跨行支付必填
        //public string CRTFLG { get; set; }  // 收方信息不检查标志
        public string NTFCH1 { get; set; }  // 收方电子邮件
        //public string NTFCH2 { get; set; }  // 收方移动电话
        //public string CRTSQN { get; set; }  // 收方编号
        //public string TRSTYP { get; set; }  // 业务种类
        public string RCVCHK { get; set; }  // 行内收方账号户名校验

        public string batch_id { get; set; }
        public string status_id { get; set; }
        public string user_id { get; set; }
        public string comment { get; set; }
        public bool HasError { get; set; }

        public string CNRCOD { get; set; }  //汇入国家
        public string ORTTYP { get; set; }  //汇款类型
        public string APPNAM { get; set; }  //填报人名称 
        public string APPTEL { get; set; }  //填报人电话 
        public string TRSCD1 { get; set; }  //交易编码１
        public string CNTNBR { get; set; }  //合同号
        public string INVNBR { get; set; }  //发票号

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

    public class EnquiryResp
    {
        public string OPRDAT { get; set; }  // 经办日期
        public string YURREF { get; set; }  // 业务参考号
        public string REQNBR { get; set; }  // 流程实例号
        public string BUSNAR { get; set; }  // 业务摘要
        public string C_REQSTS { get; set; }  // 业务请求状态
        public string REQSTS { get; set; }  // 业务请求状态代码
        public string C_RTNFLG { get; set; }  // 业务处理结果
        public string RTNFLG { get; set; }  // 业务处理结果代码
        //public string OPRALS { get; set; }  // 操作别名
        public string RTNNAR { get; set; }  // 结果摘要
        public string RTNDAT { get; set; }  // 退票日期
    }

    public class Vendor
    {
        public string bank_no { get; set; }
        public string bank_name { get; set; }
        public string bank { get; set; }
        public string bank_prov { get; set; }
        public string bank_city { get; set; }
        public string cmb_flg { get; set; }  

    }
}
