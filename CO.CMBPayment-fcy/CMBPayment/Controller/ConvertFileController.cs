using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using Common;
using System.Data;

namespace CMBPayment
{
    public class ConvertFileController
    {
        public void ReadFolderFiles()
        {

        }

        public List<PaymentReqt> ConvertDecryptedFile(string path)
        {
            try
            {
                List<PaymentReqt> payList = new List<PaymentReqt>();

                List<string> fileRows = new List<string>();
                if (File.Exists(path))
                {
                    using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                    {
                        StreamReader sr = new StreamReader(fs);
                        string s;
                        while ((s = sr.ReadLine()) != null)
                        {
                            string first = s.Substring(0, 1);
                            if (first.Equals("P") || first.Equals("I"))
                            {
                                fileRows.Add(s);
                            }
                        }
                    }
                    for (int i = 0; i < fileRows.Count; i++)
                    {
                        string s = fileRows[i];
                        string[] arr = s.Split(',');

                        if (arr[0] != "P")
                        {
                            continue;
                        }

                        string invoice = "";
                        for (int j = i + 1; j < fileRows.Count; j++)
                        {
                            string s2 = fileRows[j];
                            if (s2.Substring(0, 1).Equals("I"))
                            {
                                invoice = invoice + " " + s2.Split(',')[1];
                                i++;
                            }
                            else
                            {
                                break;
                            }
                        }

                        string error = "";
                        bool hasErr = false;

                        PaymentReqt pay = new PaymentReqt();
                        Vendor v = GetBankName(arr[15]);

                        // 业务参考号 否
                        pay.YURREF = DateTime.Now.ToString("yyyyMMdd") + GetRandom(i);    //Customer Reference
                        // 期望日
                        pay.EPTDAT = FormatDate(arr[9]);    //Payment Date DD/MM/YYYY
                        // 付方帐号 否
                        pay.DBTACC = SystemInfoController.GetControlInfo(Constant.SystemControlInfo.KEY_DEBIT_ACC).ItemValue;
                        // 付方开户地区代码 否
                        pay.DBTBBK = SystemInfoController.GetControlInfo(Constant.SystemControlInfo.KEY_DEBIT_CITY).ItemValue;
                        // 交易金额 否
                        pay.TRSAMT = arr[38];    //Invoice/Gross Amount
                        if (string.IsNullOrEmpty(arr[38]))
                        {
                            hasErr = true;
                            error = error + "39列:交易金额不能为空。";
                        }
                        // 币种代码 否
                        pay.CCYNBR = GetCRYCd(arr[37]);    //Payment Currency
                        // 结算方式代码 否
                        pay.STLCHN = "N";
                        // 用途 否
                        pay.NUSAGE = invoice.Trim();
                        // 业务摘要
                        pay.BUSNAR = arr[56];
                        // 收方帐号 否
                        pay.CRTACC = arr[19];   //Payee A/C No.
                        if (string.IsNullOrEmpty(arr[19]))
                        {
                            hasErr = true;
                            error = error + "20列:收方帐号不能为空。";
                        }
                        // 收方帐户名 收方帐户名与收方长户名不能同时为空
                        if (!ComUtil.Check_IsOvermaxLength(arr[49], 62))
                        {
                            pay.CRTNAM = arr[49];   //Payee Name
                            pay.LRVEAN = "";
                        }
                        else
                        {
                            pay.CRTNAM = "";
                            pay.LRVEAN = arr[49];   //Payee Name
                        }
                        if (string.IsNullOrEmpty(arr[49]))
                        {
                            hasErr = true;
                            error = error + "50列:收方帐户名不能为空。";
                        }

                        // 系统内外标志 Y：招行；N：非招行； 否
                        pay.BNKFLG = (v.cmb_flg == "1" ? "Y" : "N");
                        // 人行自动支付收方联行号
                        pay.BRDNBR = arr[15];
                        // 收方开户行 跨行支付（BNKFLG=N）必填
                        pay.CRTBNK = v.bank_name;   //Payee Bank Code
                        if (pay.BNKFLG.Equals("N"))
                        {
                            if (string.IsNullOrEmpty(pay.CRTBNK))
                            {
                                hasErr = true;
                                error = error + "收方开户行跨行支付时不能为空。";
                            }
                        }
                        // 收方行地址 跨行支付（BNKFLG=N）必填
                        pay.CRTADR = v.bank_prov + v.bank_city;
                        if (pay.BNKFLG.Equals("N"))
                        {
                            if (string.IsNullOrEmpty(pay.CRTADR))
                            {
                                hasErr = true;
                                error = error + "收方行地址跨行支付时不能为空。";
                            }
                        }

                        // 收方电子邮件
                        pay.NTFCH1 = arr[62];      //Email ID

                        // 行内收方账号户名校验 1：校验
                        //pay.RCVCHK = "1";

                        pay.HasError = hasErr;
                        pay.comment = error;

                        payList.Add(pay);
                    }
                }
                return payList;
            }
            catch (Exception ex)
            {
                // format error!
                throw ex;
            }
        }

        private string GetRandom(int idx)
        {
            int hour = DateTime.Now.Hour;
            int Minute = DateTime.Now.Minute;
            int Second = DateTime.Now.Second;
            int Ms = DateTime.Now.Millisecond;

            string Ran = DateTime.Now.ToString("HHmmssfff") + (idx + 1).ToString();

            return Ran;
        }

        public List<PaymentReqt> GetSucPaymentList(List<PaymentReqt> payList)
        {
            List<PaymentReqt> list = new List<PaymentReqt>();
            var list1 = (from p in payList
                         where p.HasError == false
                         select p);
            if (list1 != null)
            {
                list = list1.ToList();
            }
            return list;
        }

        public List<PaymentReqt> GetFailPaymentList(List<PaymentReqt> payList)
        {
            List<PaymentReqt> list = new List<PaymentReqt>();
            var list1 = (from p in payList
                         where p.HasError == true
                         select p);
            if (list1 != null)
            {
                list = list1.ToList();
            }
            return list;
        }

        private Vendor GetBankName(string bankCd)
        {
            Vendor v = new Vendor();

            MainController ctrl = new MainController();
            DataTable dt = ctrl.GetBankName(bankCd);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    v.bank_name = dt.Rows[0][0].ToString();
                    v.bank = dt.Rows[0][1].ToString();
                    v.bank_prov = dt.Rows[0][2].ToString();
                    v.bank_city = dt.Rows[0][3].ToString();
                    v.cmb_flg = dt.Rows[0][4].ToString();
                }
            }
            return v;
        }

        private string FormatDate(string inStr)
        {
            string retStr = "";
            if (!string.IsNullOrEmpty(inStr) && inStr.Length == 10)
            {
                string[] arr = inStr.Split('/');
                DateTime d = new DateTime(int.Parse(arr[2]), int.Parse(arr[1]), int.Parse(arr[0]));
                retStr = d.ToString("yyyyMMdd");
            }
            return retStr;
        }

        private int FindNextPay(List<string> fileRows, int start, List<string> invoiceList)
        {
            int rowAdd = 0;

            for (int j = start + 1; j < fileRows.Count; j++)
            {
                string s = fileRows[j];

                if (s.Substring(0, 1).Equals("I"))
                {
                    invoiceList.Add(s);
                }
                else
                {
                    rowAdd = j - start;
                    break;
                }
            }

            return rowAdd;
        }

        private string GetCityCd(string city)
        {
            string cd = "";
            if (city.Equals("SHA"))
            {
                cd = "21";
            }
            return cd;
        }

        private string GetCRYCd(string cry)
        {
            string cd = "";
            switch (cry)
            {
                case "CNY":
                    cd = "10";
                    break;
                case "HKD":
                    cd = "21";
                    break;
                case "AUD":
                    cd = "29";
                    break;
                case "USD":
                    cd = "32";
                    break;
                case "EUR":
                    cd = "35";
                    break;
                case "CAD":
                    cd = "39";
                    break;
                case "GBP":
                    cd = "43";
                    break;
                case "JPY":
                    cd = "65";
                    break;
                case "SGD":
                    cd = "69";
                    break;
                case "NOK":
                    cd = "83";
                    break;
                case "DKK":
                    cd = "85";
                    break;
                case "CHF":
                    cd = "87";
                    break;
                case "SEK":
                    cd = "88";
                    break;
                default:
                    break;
            }
            return cd;
        }
    }
}
