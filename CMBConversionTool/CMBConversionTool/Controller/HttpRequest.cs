using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Web;
using System.Net;
using System.Configuration;
using CMBConversionTool;

namespace Common
{
    public class HttpRequest
    {

        public string SendRequest(string data)
        {
            string result = string.Empty;

            try
            {
                string postUrl = ConfigurationManager.AppSettings["URL"];
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
            ndLgnnam.InnerText = ConfigurationManager.AppSettings["CMB_AC"];
            ndInfo.AppendChild(ndLgnnam);
            // add info node ------------------------------

            // add SDKPAYRQX node ------------------------------
            XmlNode ndPayrqx = xmlDoc.CreateElement("SDKPAYRQX");
            rootNode.AppendChild(ndPayrqx);

            XmlNode ndBuscod = xmlDoc.CreateElement("BUSCOD");
            ndBuscod.InnerText = "N02031";
            ndInfo.AppendChild(ndBuscod);
            // add SDKPAYRQX node ------------------------------

            // set DCOPDPAYX info
            foreach (PaymentReqt item in list)
            {
                XmlNode node = xmlDoc.CreateElement("DCOPDPAYX");
                rootNode.AppendChild(node);

                XmlNode yurref = xmlDoc.CreateElement("YURREF");
                XmlNode dbtacc = xmlDoc.CreateElement("DBTACC");
                XmlNode dbtbbk = xmlDoc.CreateElement("DBTBBK");
                XmlNode trsamt = xmlDoc.CreateElement("TRSAMT");
                XmlNode ccynbr = xmlDoc.CreateElement("CCYNBR");
                XmlNode stlchn = xmlDoc.CreateElement("STLCHN");
                XmlNode nusage = xmlDoc.CreateElement("NUSAGE");
                XmlNode bnkflg = xmlDoc.CreateElement("BNKFLG");
                XmlNode crtacc = xmlDoc.CreateElement("CRTACC");
                XmlNode crtnam = xmlDoc.CreateElement("CRTNAM");
                XmlNode crtbnk = xmlDoc.CreateElement("CRTBNK");

                yurref.InnerText = item.YURREF;
                dbtacc.InnerText = item.DBTACC;
                dbtbbk.InnerText = item.DBTBBK;
                trsamt.InnerText = item.TRSAMT;
                ccynbr.InnerText = item.CCYNBR;
                stlchn.InnerText = item.STLCHN;
                nusage.InnerText = item.NUSAGE;
                bnkflg.InnerText = item.BNKFLG;
                crtacc.InnerText = item.CRTACC;
                crtnam.InnerText = item.CRTNAM;
                crtbnk.InnerText = item.CRTBNK;

                node.AppendChild(yurref);
                node.AppendChild(dbtacc);
                node.AppendChild(dbtbbk);
                node.AppendChild(trsamt);
                node.AppendChild(ccynbr);
                node.AppendChild(stlchn);
                node.AppendChild(nusage);
                node.AppendChild(bnkflg);
                node.AppendChild(crtacc);
                node.AppendChild(crtnam);
                node.AppendChild(crtbnk);
            }

            string strXml = this.ConvertXmlToString(xmlDoc);

            return strXml;
        }

        public void CheckPayResult(string result)
        {
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

                        XmlNodeList list = rootNode.SelectNodes("NTQPAYRQZ");
                        foreach (XmlNode node in list)
                        {
                            string sREQSTS = node.SelectSingleNode("REQSTS").InnerText;
                            string sRTNFLG = node.SelectSingleNode("RTNFLG").InnerText;
                            string sERRTXT = node.SelectSingleNode("ERRTXT").InnerText;

                            if (sREQSTS.Equals("FIN") && sRTNFLG.Equals("F"))
                            {
                                LogUtil.WriteInfoMessage("支付失败：" + sERRTXT);
                            }
                            else
                            {
                                LogUtil.WriteInfoMessage("支付已被银行受理（支付状态：" + sREQSTS + "）");
                            }
                        }

                    }
                    else if (sRetCod.Equals("-9"))
                    {
                        LogUtil.WriteInfoMessage("支付未知异常，请查询支付结果确认支付状态，错误信息："
                                + ndErrmsg.InnerText);
                    }
                    else
                    {
                        LogUtil.WriteInfoMessage("支付失败：" + ndErrmsg.InnerText);
                    }
                }
                else
                {
                    LogUtil.WriteInfoMessage("响应报文解析失败");
                }
            }
        }

        #endregion

        #region "GetNewNotice"

        public string GetNoticeRequestStr()
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
            ndFunnam.InnerText = "GetNewNotice";
            ndInfo.AppendChild(ndFunnam);

            XmlNode ndDattyp = xmlDoc.CreateElement("DATTYP");
            ndDattyp.InnerText = "2";
            ndInfo.AppendChild(ndDattyp);

            // set cmb account name
            XmlNode ndLgnnam = xmlDoc.CreateElement("LGNNAM");
            ndLgnnam.InnerText = ConfigurationManager.AppSettings["CMB_AC"];
            ndInfo.AppendChild(ndLgnnam);
            // add info node ------------------------------

            string strXml = this.ConvertXmlToString(xmlDoc);

            return strXml;
        }

        public List<NoticeResp> CheckNoticeResult(string result)
        {
            List<NoticeResp> listEnqu = new List<NoticeResp>();
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

                        XmlNodeList list = rootNode.SelectNodes("NTQNTCGTZ");
                        foreach (XmlNode node in list)
                        {
                            string sMSGTYP = node.SelectSingleNode("MSGTYP").InnerText;

                            if (sMSGTYP.Equals("NCDRTPAY"))
                            {
                                NoticeResp enqu = new NoticeResp();
                                enqu.MSGNBR = node.SelectSingleNode("MSGNBR").InnerText;
                                enqu.REQNBR = node.SelectSingleNode("REQNBR").InnerText;
                                enqu.FLWTYP = node.SelectSingleNode("FLWTYP").InnerText;
                                enqu.REQDTA = node.SelectSingleNode("REQDTA").InnerText;
                                enqu.BBKNBR = node.SelectSingleNode("BBKNBR").InnerText;
                                enqu.KEYVAL = node.SelectSingleNode("KEYVAL").InnerText;
                                enqu.CCYNBR = node.SelectSingleNode("CCYNBR").InnerText;
                                enqu.YURREF = node.SelectSingleNode("YURREF").InnerText;
                                enqu.ACCNAM = node.SelectSingleNode("ACCNAM").InnerText;
                                enqu.ENDAMT = node.SelectSingleNode("ENDAMT").InnerText;
                                enqu.EPTDAT = node.SelectSingleNode("EPTDAT").InnerText;
                                enqu.EPTTIM = node.SelectSingleNode("EPTTIM").InnerText;
                                enqu.OPRDAT = node.SelectSingleNode("OPRDAT").InnerText;
                                enqu.RTNFLG = node.SelectSingleNode("RTNFLG").InnerText;
                                enqu.RTNDSP = node.SelectSingleNode("RTNDSP").InnerText;
                            }

                        }

                    }
                    else if (sRetCod.Equals("-9"))
                    {
                        LogUtil.WriteInfoMessage("支付未知异常，请查询支付结果确认支付状态，错误信息："
                                + ndErrmsg.InnerText);
                    }
                    else
                    {
                        LogUtil.WriteInfoMessage("支付失败：" + ndErrmsg.InnerText);
                    }
                }
                else
                {
                    LogUtil.WriteInfoMessage("响应报文解析失败");
                }
            }

            return listEnqu;
        }

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
            ndLgnnam.InnerText = ConfigurationManager.AppSettings["CMB_AC"];
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

        public List<EnquiryResp> CheckEnquiryResult(string result)
        {
            List<EnquiryResp> listEnqu = new List<EnquiryResp>();
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
                            EnquiryResp enqu = new EnquiryResp();
                            enqu.OPRDAT = node.SelectSingleNode("OPRDAT").InnerText;
                            enqu.YURREF = node.SelectSingleNode("YURREF").InnerText;
                            enqu.REQNBR = node.SelectSingleNode("REQNBR").InnerText;
                            enqu.BUSNAR = node.SelectSingleNode("BUSNAR").InnerText;
                            enqu.C_REQSTS = node.SelectSingleNode("C_REQSTS").InnerText;
                            enqu.REQSTS = node.SelectSingleNode("REQSTS").InnerText;
                            enqu.C_RTNFLG = node.SelectSingleNode("C_RTNFLG").InnerText;
                            enqu.RTNFLG = node.SelectSingleNode("RTNFLG").InnerText;
                            enqu.OPRALS = node.SelectSingleNode("OPRALS").InnerText;
                            enqu.RTNNAR = node.SelectSingleNode("RTNNAR").InnerText;
                            enqu.RTNDAT = node.SelectSingleNode("RTNDAT").InnerText;
                            enqu.ATHFLG = node.SelectSingleNode("ATHFLG").InnerText;
                            enqu.LGNNAM = node.SelectSingleNode("LGNNAM").InnerText;
                            enqu.USRNAM = node.SelectSingleNode("USRNAM").InnerText;

                            if (enqu.REQSTS.Equals("FIN") )
                            {
                                if (enqu.RTNFLG.Equals("S"))
                                {
                                    LogUtil.WriteInfoMessage("支付成功！");
                                }
                                else if (enqu.RTNFLG.Equals("B"))
                                {
                                    LogUtil.WriteInfoMessage("支付退票!");
                                }
                                else
                                {
                                    LogUtil.WriteInfoMessage("支付失败!");
                                }
                            }
                            else
                            {
                                LogUtil.WriteInfoMessage("支付银行还在处理中！");
                            }

                            #region "backup"
                            //enqu.C_BUSCOD = node.SelectSingleNode("C_BUSCOD").InnerText;
                            //enqu.BUSCOD = node.SelectSingleNode("BUSCOD").InnerText;
                            //enqu.BUSMOD = node.SelectSingleNode("BUSMOD").InnerText;
                            //enqu.C_DBTBBK = node.SelectSingleNode("C_DBTBBK").InnerText;
                            //enqu.DBTBBK = node.SelectSingleNode("DBTBBK").InnerText;
                            //enqu.DBTACC = node.SelectSingleNode("DBTACC").InnerText;
                            //enqu.DBTNAM = node.SelectSingleNode("DBTNAM").InnerText;
                            //enqu.C_DBTREL = node.SelectSingleNode("C_DBTREL").InnerText;
                            //enqu.DBTBNK = node.SelectSingleNode("DBTBNK").InnerText;
                            //enqu.DBTADR = node.SelectSingleNode("DBTADR").InnerText;
                            //enqu.C_CRTBBK = node.SelectSingleNode("C_CRTBBK").InnerText;
                            //enqu.CRTBBK = node.SelectSingleNode("CRTBBK").InnerText;
                            //enqu.CRTACC = node.SelectSingleNode("CRTACC").InnerText;
                            //enqu.CRTNAM = node.SelectSingleNode("CRTNAM").InnerText;
                            //enqu.RCVBRD = node.SelectSingleNode("RCVBRD").InnerText;
                            //enqu.C_CRTREL = node.SelectSingleNode("C_CRTREL").InnerText;
                            //enqu.CRTBNK = node.SelectSingleNode("CRTBNK").InnerText;
                            //enqu.CRTADR = node.SelectSingleNode("CRTADR").InnerText;
                            //enqu.C_GRPBBK = node.SelectSingleNode("C_GRPBBK").InnerText;
                            //enqu.GRPBBK = node.SelectSingleNode("GRPBBK").InnerText;
                            //enqu.GRPACC = node.SelectSingleNode("GRPACC").InnerText;
                            //enqu.GRPNAM = node.SelectSingleNode("GRPNAM").InnerText;
                            //enqu.C_CCYNBR = node.SelectSingleNode("C_CCYNBR").InnerText;
                            //enqu.CCYNBR = node.SelectSingleNode("CCYNBR").InnerText;
                            //enqu.TRSAMT = node.SelectSingleNode("TRSAMT").InnerText;
                            //enqu.EPTDAT = node.SelectSingleNode("EPTDAT").InnerText;
                            //enqu.EPTTIM = node.SelectSingleNode("EPTTIM").InnerText;
                            //enqu.BNKFLG = node.SelectSingleNode("BNKFLG").InnerText;
                            //enqu.REGFLG = node.SelectSingleNode("REGFLG").InnerText;
                            //enqu.C_STLCHN = node.SelectSingleNode("C_STLCHN").InnerText;
                            //enqu.STLCHN = node.SelectSingleNode("STLCHN").InnerText;
                            //enqu.NUSAGE = node.SelectSingleNode("NUSAGE").InnerText;
                            //enqu.NTFCH1 = node.SelectSingleNode("NTFCH1").InnerText;
                            //enqu.NTFCH2 = node.SelectSingleNode("NTFCH2").InnerText;
                            #endregion
                        }
                    }
                    else if (sRetCod.Equals("-9"))
                    {
                        LogUtil.WriteInfoMessage("支付未知异常，请查询支付结果确认支付状态，错误信息："
                                + ndErrmsg.InnerText);
                    }
                    else
                    {
                        LogUtil.WriteInfoMessage("支付失败：" + ndErrmsg.InnerText);
                    }
                }
                else
                {
                    LogUtil.WriteInfoMessage("响应报文解析失败");
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
