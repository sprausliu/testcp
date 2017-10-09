using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Web;
using System.Net;
using System.Configuration;
using Common;

namespace CMBPayment
{
    public class HttpRequest
    {
        protected UserInfoBean LoginUser
        {
            get
            {
                if (System.Web.HttpContext.Current.Session[Constant.SessionKey.LOGIN_USER_INFO] != null)
                {
                    return (UserInfoBean)System.Web.HttpContext.Current.Session[Constant.SessionKey.LOGIN_USER_INFO];
                }
                return new UserInfoBean();
            }
        }

        public string SendRequest(string data, string ip, string port)
        {
            string result = string.Empty;

            try
            {
                string postUrl = string.Format("http://{0}:{1}/", ip, port);
                var request = WebRequest.Create(postUrl);
                request.Method = "POST";
                request.ContentType = "TEXT/XML";
                request.Headers.Add("CharSet:GBK");
                var encoding = Encoding.GetEncoding("GBK");

                if (data != null)
                {
                    byte[] bytes = encoding.GetBytes(data);
                    request.ContentLength = bytes.Length;

                    using (Stream requestStream = request.GetRequestStream())
                    {
                        requestStream.Write(bytes, 0, bytes.Length);
                    }
                }

                using (HttpWebResponse wr = request.GetResponse() as HttpWebResponse)
                {
                    using (StreamReader reader = new StreamReader(wr.GetResponseStream(), encoding))
                    {
                        result = reader.ReadToEnd();
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtil.WriteErrorMessage(ex);
                throw ex;
            }

            return result;
        }

        #region "Payement"

        public string GetPayRequestStr(List<PaymentReqt> list)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.CreateXmlDeclaration("1.0", "GBK", "");

            // add root node
            XmlNode rootNode = xmlDoc.CreateElement("CMBSDKPGK");
            xmlDoc.AppendChild(rootNode);

            // add INFO node ------------------------------
            XmlNode ndInfo = xmlDoc.CreateElement("INFO");
            rootNode.AppendChild(ndInfo);

            XmlNode ndFunnam = xmlDoc.CreateElement("FUNNAM");
            ndFunnam.InnerText = "DCPAYMNT";
            ndInfo.AppendChild(ndFunnam);

            XmlNode ndDattyp = xmlDoc.CreateElement("DATTYP");
            ndDattyp.InnerText = "2";
            ndInfo.AppendChild(ndDattyp);

            // set cmb account name
            XmlNode ndLgnnam = xmlDoc.CreateElement("LGNNAM");
            ndLgnnam.InnerText = LoginUser.CMBAcc;  //ConfigurationManager.AppSettings["CMB_AC"];
            ndInfo.AppendChild(ndLgnnam);
            // add info node ------------------------------

            // add SDKPAYRQX node ------------------------------
            XmlNode ndPayrqx = xmlDoc.CreateElement("SDKPAYRQX");
            rootNode.AppendChild(ndPayrqx);

            XmlNode ndBuscod = xmlDoc.CreateElement("BUSCOD");
            ndBuscod.InnerText = "N02031";
            ndPayrqx.AppendChild(ndBuscod);
            // add SDKPAYRQX node ------------------------------

            // set DCOPDPAYX info
            foreach (PaymentReqt item in list)
            {
                XmlNode node = xmlDoc.CreateElement("DCOPDPAYX");
                rootNode.AppendChild(node);

                XmlNode yurref = xmlDoc.CreateElement("YURREF");    // 业务参考号
                XmlNode eptdat = xmlDoc.CreateElement("EPTDAT");    // 期望日
                XmlNode dbtacc = xmlDoc.CreateElement("DBTACC");    // 付方帐号
                XmlNode dbtbbk = xmlDoc.CreateElement("DBTBBK");    // 付方开户地区代码
                XmlNode trsamt = xmlDoc.CreateElement("TRSAMT");    // 交易金额
                XmlNode ccynbr = xmlDoc.CreateElement("CCYNBR");    // 币种代码
                XmlNode stlchn = xmlDoc.CreateElement("STLCHN");    // 结算方式代码
                XmlNode nusage = xmlDoc.CreateElement("NUSAGE");    // 用途
                XmlNode busnar = xmlDoc.CreateElement("BUSNAR");    // 业务摘要
                XmlNode crtacc = xmlDoc.CreateElement("CRTACC");    // 收方帐号
                XmlNode crtnam = xmlDoc.CreateElement("CRTNAM");    // 收方帐户名
                XmlNode lrvean = xmlDoc.CreateElement("LRVEAN");    // 收方长户名
                XmlNode brdnbr = xmlDoc.CreateElement("BRDNBR");    // 人行自动支付收方联行号/收方行号
                XmlNode bnkflg = xmlDoc.CreateElement("BNKFLG");    // 系统内外标志/Y：招行；N：非招行；
                XmlNode crtbnk = xmlDoc.CreateElement("CRTBNK");    // 收方开户行 跨行支付必填
                XmlNode crtadr = xmlDoc.CreateElement("CRTADR");    // 收方行地址 跨行支付必填
                XmlNode ntfch1 = xmlDoc.CreateElement("NTFCH1");    // 收方电子邮件
                XmlNode rcvchk = xmlDoc.CreateElement("RCVCHK");    // 行内收方账号户名校验

                yurref.InnerText = item.YURREF;    // 业务参考号
                eptdat.InnerText = item.EPTDAT;    // 期望日
                dbtacc.InnerText = item.DBTACC;    // 付方帐号
                dbtbbk.InnerText = item.DBTBBK;    // 付方开户地区代码
                trsamt.InnerText = item.TRSAMT;    // 交易金额
                ccynbr.InnerText = item.CCYNBR;    // 币种代码
                stlchn.InnerText = "N";    // 结算方式代码
                nusage.InnerText = item.NUSAGE;    // 用途
                busnar.InnerText = item.BUSNAR;    // 业务摘要
                crtacc.InnerText = item.CRTACC;    // 收方帐号
                crtnam.InnerText = item.CRTNAM;    // 收方帐户名
                lrvean.InnerText = item.LRVEAN;    // 收方长户名
                brdnbr.InnerText = item.BRDNBR;    // 人行自动支付收方联行号/收方行号
                bnkflg.InnerText = item.BNKFLG;    // 系统内外标志/Y：招行；N：非招行；
                crtbnk.InnerText = item.CRTBNK;    // 收方开户行 跨行支付必填
                crtadr.InnerText = item.CRTADR;    // 收方行地址 跨行支付必填
                ntfch1.InnerText = item.NTFCH1;    // 收方电子邮件
                rcvchk.InnerText = "1";            // 行内收方账号户名校验

                node.AppendChild(yurref);
                node.AppendChild(eptdat);
                node.AppendChild(dbtacc);
                node.AppendChild(dbtbbk);
                node.AppendChild(trsamt);
                node.AppendChild(ccynbr);
                node.AppendChild(stlchn);
                node.AppendChild(nusage);
                node.AppendChild(busnar);
                node.AppendChild(crtacc);
                node.AppendChild(crtnam);
                node.AppendChild(lrvean);
                node.AppendChild(brdnbr);
                node.AppendChild(bnkflg);
                node.AppendChild(crtbnk);
                node.AppendChild(crtadr);
                node.AppendChild(ntfch1);
                node.AppendChild(rcvchk);
            }

            string strXml = this.ConvertXmlToString(xmlDoc);

            return strXml;
        }

        public string CheckPayResult(string resp, List<PaymentReqt> payList)
        {
            string res = "";
            if (!string.IsNullOrEmpty(resp))
            {

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(resp);

                XmlNode rootNode = xmlDoc.SelectSingleNode("CMBSDKPGK");
                XmlNode ndRetcod = xmlDoc.SelectSingleNode("/CMBSDKPGK/INFO/RETCOD");
                XmlNode ndErrmsg = xmlDoc.SelectSingleNode("/CMBSDKPGK/INFO/ERRMSG");

                if (ndRetcod != null)
                {
                    string sRetCod = ndRetcod.InnerText;

                    if (sRetCod.Equals("0"))
                    {
                        bool isSuc = true;
                        XmlNodeList list = rootNode.SelectNodes("NTQPAYRQZ");
                        foreach (XmlNode node in list)
                        {
                            string sYURREF = node.SelectSingleNode("YURREF").InnerText;
                            string sREQSTS = node.SelectSingleNode("REQSTS").InnerText;
                            string sRTNFLG = node.SelectSingleNode("RTNFLG").InnerText;
                            string sERRTXT = node.SelectSingleNode("ERRTXT").InnerText;

                            PaymentReqt pay = (from p in payList
                                               where p.YURREF == sYURREF
                                               select p).First();

                            if (sREQSTS.Equals("FIN") && sRTNFLG.Equals("F"))
                            {
                                LogUtil.WriteInfoMessage("支付失败：" + sERRTXT);

                                pay.status_id = Constant.ConstantInfo.PAYSTAT_F;
                                pay.comment = "支付失败：" + sERRTXT;
                            }
                            else
                            {
                                LogUtil.WriteInfoMessage("支付已被银行受理（支付状态：" + sREQSTS + "）");
                                pay.status_id = Constant.ConstantInfo.PAYSTAT_02;
                                pay.comment = "支付已被银行受理（支付状态：" + sREQSTS + "）";
                            }
                        }
                        if (!isSuc)
                        {
                            res = "部分支付未知异常，请查询支付结果确认支付状态。";
                        }
                    }
                    else if (sRetCod.Equals("-9"))
                    {
                        LogUtil.WriteInfoMessage("支付未知异常，请查询支付结果确认支付状态，错误信息："
                                + ndErrmsg.InnerText);
                        res = "支付未知异常，请查询支付结果确认支付状态，错误信息："
                                + ndErrmsg.InnerText;
                    }
                    else
                    {
                        LogUtil.WriteInfoMessage("支付失败：" + ndErrmsg.InnerText);
                        res = "支付失败：" + ndErrmsg.InnerText;
                    }
                }
                else
                {
                    LogUtil.WriteInfoMessage("响应报文解析失败");
                    res = "响应报文解析失败";
                }
            }

            return res;
        }

        #endregion

        #region "GetNewNotice"

        //public string GetNoticeRequestStr()
        //{
        //    XmlDocument xmlDoc = new XmlDocument();
        //    xmlDoc.CreateXmlDeclaration("1.0", "GBK", "");

        //    // add root node
        //    XmlNode rootNode = xmlDoc.CreateElement("CMBSDKPGK");
        //    xmlDoc.AppendChild(rootNode);

        //    // add INFO node ------------------------------
        //    XmlNode ndInfo = xmlDoc.CreateElement("INFO");
        //    rootNode.AppendChild(ndInfo);

        //    XmlNode ndFunnam = xmlDoc.CreateElement("FUNNAM");
        //    ndFunnam.InnerText = "GetNewNotice";
        //    ndInfo.AppendChild(ndFunnam);

        //    XmlNode ndDattyp = xmlDoc.CreateElement("DATTYP");
        //    ndDattyp.InnerText = "2";
        //    ndInfo.AppendChild(ndDattyp);

        //    // set cmb account name
        //    XmlNode ndLgnnam = xmlDoc.CreateElement("LGNNAM");
        //    ndLgnnam.InnerText = ConfigurationManager.AppSettings["CMB_AC"];
        //    ndInfo.AppendChild(ndLgnnam);
        //    // add info node ------------------------------

        //    string strXml = this.ConvertXmlToString(xmlDoc);

        //    return strXml;
        //}

        //public List<NoticeResp> CheckNoticeResult(string result)
        //{
        //    List<NoticeResp> listEnqu = new List<NoticeResp>();
        //    if (!string.IsNullOrEmpty(result))
        //    {

        //        XmlDocument xmlDoc = new XmlDocument();
        //        xmlDoc.LoadXml(result);

        //        XmlNode rootNode = xmlDoc.SelectSingleNode("CMBSDKPGK");
        //        XmlNode ndRetcod = xmlDoc.SelectSingleNode("/CMBSDKPGK/INFO/RETCOD");
        //        XmlNode ndErrmsg = xmlDoc.SelectSingleNode("/CMBSDKPGK/INFO/ERRMSG");

        //        if (ndRetcod != null)
        //        {
        //            string sRetCod = ndRetcod.InnerText;

        //            if (sRetCod.Equals("0"))
        //            {

        //                XmlNodeList list = rootNode.SelectNodes("NTQNTCGTZ");
        //                foreach (XmlNode node in list)
        //                {
        //                    string sMSGTYP = node.SelectSingleNode("MSGTYP").InnerText;

        //                    if (sMSGTYP.Equals("NCDRTPAY"))
        //                    {
        //                        NoticeResp enqu = new NoticeResp();
        //                        enqu.MSGNBR = node.SelectSingleNode("MSGNBR").InnerText;
        //                        enqu.REQNBR = node.SelectSingleNode("REQNBR").InnerText;
        //                        enqu.FLWTYP = node.SelectSingleNode("FLWTYP").InnerText;
        //                        enqu.REQDTA = node.SelectSingleNode("REQDTA").InnerText;
        //                        enqu.BBKNBR = node.SelectSingleNode("BBKNBR").InnerText;
        //                        enqu.KEYVAL = node.SelectSingleNode("KEYVAL").InnerText;
        //                        enqu.CCYNBR = node.SelectSingleNode("CCYNBR").InnerText;
        //                        enqu.YURREF = node.SelectSingleNode("YURREF").InnerText;
        //                        enqu.ACCNAM = node.SelectSingleNode("ACCNAM").InnerText;
        //                        enqu.ENDAMT = node.SelectSingleNode("ENDAMT").InnerText;
        //                        enqu.EPTDAT = node.SelectSingleNode("EPTDAT").InnerText;
        //                        enqu.EPTTIM = node.SelectSingleNode("EPTTIM").InnerText;
        //                        enqu.OPRDAT = node.SelectSingleNode("OPRDAT").InnerText;
        //                        enqu.RTNFLG = node.SelectSingleNode("RTNFLG").InnerText;
        //                        enqu.RTNDSP = node.SelectSingleNode("RTNDSP").InnerText;
        //                    }

        //                }

        //            }
        //            else if (sRetCod.Equals("-9"))
        //            {
        //                LogUtil.WriteInfoMessage("支付未知异常，请查询支付结果确认支付状态，错误信息："
        //                        + ndErrmsg.InnerText);
        //            }
        //            else
        //            {
        //                LogUtil.WriteInfoMessage("支付失败：" + ndErrmsg.InnerText);
        //            }
        //        }
        //        else
        //        {
        //            LogUtil.WriteInfoMessage("响应报文解析失败");
        //        }
        //    }

        //    return listEnqu;
        //}

        #endregion

        #region "Enquiry"

        public string GetEnquiryRequestStr(string beginDay, string endDay)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.CreateXmlDeclaration("1.0", "GBK", "");

            // add root node
            XmlNode rootNode = xmlDoc.CreateElement("CMBSDKPGK");
            xmlDoc.AppendChild(rootNode);

            // add INFO node ------------------------------
            XmlNode ndInfo = xmlDoc.CreateElement("INFO");
            rootNode.AppendChild(ndInfo);

            XmlNode ndFunnam = xmlDoc.CreateElement("FUNNAM");
            ndFunnam.InnerText = "GetPaymentInfo";
            ndInfo.AppendChild(ndFunnam);

            XmlNode ndDattyp = xmlDoc.CreateElement("DATTYP");
            ndDattyp.InnerText = "2";
            ndInfo.AppendChild(ndDattyp);

            // set cmb account name
            XmlNode ndLgnnam = xmlDoc.CreateElement("LGNNAM");
            ndLgnnam.InnerText = LoginUser.CMBAcc;      //ConfigurationManager.AppSettings["CMB_AC"];
            ndInfo.AppendChild(ndLgnnam);
            // add info node ------------------------------

            // add SDKPAYQYX node ------------------------------
            XmlNode ndPayqyx = xmlDoc.CreateElement("SDKPAYQYX");
            rootNode.AppendChild(ndPayqyx);

            XmlNode ndBuscod = xmlDoc.CreateElement("BUSCOD");
            ndBuscod.InnerText = "N02031";
            ndPayqyx.AppendChild(ndBuscod);

            XmlNode ndBegin = xmlDoc.CreateElement("BGNDAT");
            ndBegin.InnerText = beginDay;
            ndPayqyx.AppendChild(ndBegin);

            XmlNode ndEnd = xmlDoc.CreateElement("ENDDAT");
            ndEnd.InnerText = endDay;
            ndPayqyx.AppendChild(ndEnd);
            // add SDKPAYQYX node ------------------------------

            string strXml = this.ConvertXmlToString(xmlDoc);

            return strXml;
        }

        public List<PaymentReqt> CheckEnquiryResult(string result, out string errorMsg)
        {
            errorMsg = "";
            List<PaymentReqt> listEnqu = new List<PaymentReqt>();
            if (!string.IsNullOrEmpty(result))
            {

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(result);

                XmlNode rootNode = xmlDoc.SelectSingleNode("CMBSDKPGK");
                XmlNode ndRetcod = xmlDoc.SelectSingleNode("/CMBSDKPGK/INFO/RETCOD");
                XmlNode ndErrmsg = xmlDoc.SelectSingleNode("/CMBSDKPGK/INFO/ERRMSG");

                if (ndRetcod != null)
                {
                    string sRetCod = ndRetcod.InnerText;

                    if (sRetCod.Equals("0"))
                    {
                        XmlNodeList list = rootNode.SelectNodes("NTQPAYQYZ");
                        foreach (XmlNode node in list)
                        {
                            //1.	请求返回RETCOD为0表示查询成功；否则查询失败。
                            //2.	查询成功，但是返回信息为空串，表示不存在任何匹配条件的交易。
                            //3.	查询成功，返回数据判断方法：返回的每笔信息中REQSTS如果不等于’FIN’表示该笔支付银行还在处理中，REQSTS=’FIN’时再判断RTNFLG，RTNFLG为’S’时表示成功，’B’表示退票，其他作为失败处理；
                            //4.	根据业务参考号查询成功，但是返回记录数不止一条。由于失败的业务参考号可以重用，因此当用户使用同一业务参考号发送失败多次后，有可能会产生相同业务参考号的多笔记录，用户可以通过返回信息的REQNBR(流程实例号)的大小判断最近一次经办的结果（最大的流程实例号为最近一次经办的结果）。
                            //5.	对于凌晨作出的支付，在查询支付结果时，起止日期请设置临近的两天的进行查询。
                            PaymentReqt enqu = new PaymentReqt();
                            enqu.OPRDAT = node.SelectSingleNode("OPRDAT").InnerText;    //经办日期
                            enqu.YURREF = node.SelectSingleNode("YURREF").InnerText;    //业务参考号
                            string REQSTS = node.SelectSingleNode("REQSTS").InnerText;    //业务请求状态代码
                            string RTNFLG = node.SelectSingleNode("RTNFLG").InnerText;    //业务处理结果代码
                            string RTNNAR = node.SelectSingleNode("RTNNAR").InnerText;    //结果摘要

                            if (REQSTS.Equals("FIN"))
                            {
                                if (RTNFLG.Equals("S"))
                                {
                                    LogUtil.WriteInfoMessage("支付成功！");
                                    enqu.status_id = Constant.ConstantInfo.PAYSTAT_S;
                                    enqu.comment = "支付成功！";
                                }
                                else if (RTNFLG.Equals("B"))
                                {
                                    LogUtil.WriteInfoMessage("支付退票!");
                                    enqu.status_id = Constant.ConstantInfo.PAYSTAT_B;
                                    enqu.comment = "支付退票！";
                                }
                                else
                                {
                                    LogUtil.WriteInfoMessage("支付失败!");
                                    enqu.status_id = Constant.ConstantInfo.PAYSTAT_F;
                                    enqu.comment = "支付失败！";
                                }
                            }
                            else
                            {
                                LogUtil.WriteInfoMessage("支付银行还在处理中！");
                                enqu.status_id = Constant.ConstantInfo.PAYSTAT_02;
                                enqu.comment = "支付银行还在处理中！";
                            }

                            listEnqu.Add(enqu);
                        }
                    }
                    else if (sRetCod.Equals("-9"))
                    {
                        LogUtil.WriteInfoMessage("支付未知异常，请查询支付结果确认支付状态，错误信息："
                                + ndErrmsg.InnerText);
                        errorMsg = "支付未知异常，请查询支付结果确认支付状态，错误信息："
                                + ndErrmsg.InnerText;
                    }
                    else
                    {
                        LogUtil.WriteInfoMessage("支付失败：" + ndErrmsg.InnerText);
                        errorMsg = "支付失败：" + ndErrmsg.InnerText;
                    }
                }
                else
                {
                    LogUtil.WriteInfoMessage("响应报文解析失败");
                    errorMsg = "响应报文解析失败";
                }
            }

            return listEnqu;
        }
        #endregion

        private string ConvertXmlToString(XmlDocument xmlDoc)
        {
            string xmlString = string.Empty;
            using (MemoryStream stream = new MemoryStream())
            {
                XmlTextWriter writer = new XmlTextWriter(stream, Encoding.GetEncoding("GBK"));
                writer.Formatting = Formatting.Indented;
                xmlDoc.Save(writer);
                using (StreamReader sr = new StreamReader(stream, Encoding.GetEncoding("GBK")))
                {
                    stream.Position = 0;
                    xmlString = sr.ReadToEnd();
                }
            }
            return xmlString;
        }
    }
}
