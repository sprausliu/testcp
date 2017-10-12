using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Data;
using System.Configuration;

namespace CMBConversionTool
{
    public class ConvertFileController
    {

        #region "本币"
        public DataTable ConvertDecryptedFile(string path)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add(new DataColumn("业务参考号"));
                dt.Columns.Add(new DataColumn("收款人编号"));
                dt.Columns.Add(new DataColumn("收款人帐号"));
                dt.Columns.Add(new DataColumn("收款人名称"));
                dt.Columns.Add(new DataColumn("收方开户支行"));
                dt.Columns.Add(new DataColumn("收款人所在省"));
                dt.Columns.Add(new DataColumn("收款人所在市"));
                dt.Columns.Add(new DataColumn("收方邮件地址"));
                dt.Columns.Add(new DataColumn("收方移动电话"));
                dt.Columns.Add(new DataColumn("币种"));
                dt.Columns.Add(new DataColumn("付款分行"));
                dt.Columns.Add(new DataColumn("结算方式"));
                dt.Columns.Add(new DataColumn("业务种类"));
                dt.Columns.Add(new DataColumn("付方帐号"));
                dt.Columns.Add(new DataColumn("期望日"));
                dt.Columns.Add(new DataColumn("期望时间"));
                dt.Columns.Add(new DataColumn("用途"));
                dt.Columns.Add(new DataColumn("金额"));
                dt.Columns.Add(new DataColumn("收方行号"));
                dt.Columns.Add(new DataColumn("收方开户银行"));
                dt.Columns.Add(new DataColumn("业务摘要"));

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
                            if (first.Equals("P"))
                            {
                                fileRows.Add(s);
                            }
                        }
                    }

                    for (int i = 0; i < fileRows.Count; i++)
                    {
                        string s = fileRows[i];
                        string[] arr = s.Split(',');

                        if (arr[0].Equals("P"))
                        {
                            //PaymentReqt pay = new PaymentReqt();
                            DataRow dr = dt.NewRow();

                            string bankCd = arr[15];
                            if (bankCd.Equals("SCBLCNSXSHA"))
                            {
                                bankCd = "671110000017";
                            }
                            string bank = "";
                            string bankName = "";
                            string bankProv = "";
                            string bankCity = "";
                            GetBankName(bankCd, out bank, out bankName, out bankProv, out bankCity);

                            dr["业务参考号"] = DateTime.Now.ToString("yyyyMMdd") + GetRandom();   //Customer Reference
                            dr["收款人编号"] = "";
                            dr["收款人帐号"] = arr[19];   //Payee A/C No.
                            dr["收款人名称"] = arr[49];   //Payee Name
                            dr["收方开户支行"] = bankName;   //Payee Bank Code
                            dr["收款人所在省"] = bankProv;
                            dr["收款人所在市"] = bankCity;
                            dr["收方邮件地址"] = arr[62];      //Email ID
                            dr["收方移动电话"] = "";
                            dr["币种"] = "人民币";
                            dr["付款分行"] = "天津";
                            dr["结算方式"] = "普通";
                            dr["业务种类"] = "普通汇兑";
                            dr["付方帐号"] = ConfigurationManager.AppSettings["DEBIT_ACC"];
                            dr["期望日"] = FormatDate(arr[9]);    //Payment Date DD/MM/YYYY
                            dr["期望时间"] = "080000";       //
                            dr["用途"] = ConfigurationManager.AppSettings["USAGE"];
                            dr["金额"] = arr[38];           //Invoice/Gross Amount
                            dr["收方行号"] = bankCd;
                            dr["收方开户银行"] = bank;
                            dr["业务摘要"] = arr[56];

                            dt.Rows.Add(dr);
                        }
                    }
                }
                return dt;
            }
            catch (Exception ex)
            {
                // format error!
                throw ex;
            }
        }
        #endregion

        #region "外币"
        public DataTable ConvertDecryptedFileFCY(string path)
        {
            try
            {
                DataTable dt = new DataTable();

                dt.Columns.Add(new DataColumn("汇款帐号分行名"));
                dt.Columns.Add(new DataColumn("汇款帐号号码"));
                dt.Columns.Add(new DataColumn("汇款帐号币种"));
                dt.Columns.Add(new DataColumn("汇款帐号公司名"));
                dt.Columns.Add(new DataColumn("汇款方式"));
                dt.Columns.Add(new DataColumn("期望日"));
                dt.Columns.Add(new DataColumn("期望时间"));
                dt.Columns.Add(new DataColumn("电汇票汇信汇"));
                dt.Columns.Add(new DataColumn("发电等级"));

                dt.Columns.Add(new DataColumn("网上申请编号前缀"));
                dt.Columns.Add(new DataColumn("网上申请编号"));

                dt.Columns.Add(new DataColumn("汇款币种"));
                dt.Columns.Add(new DataColumn("汇款金额"));
                dt.Columns.Add(new DataColumn("到帐币种"));
                dt.Columns.Add(new DataColumn("到帐币种对应金额"));

                dt.Columns.Add(new DataColumn("现汇金额"));
                dt.Columns.Add(new DataColumn("现汇账号"));
                dt.Columns.Add(new DataColumn("购汇金额"));
                dt.Columns.Add(new DataColumn("购汇账号"));
                dt.Columns.Add(new DataColumn("其他金额"));
                dt.Columns.Add(new DataColumn("其他账号"));

                dt.Columns.Add(new DataColumn("扣费帐号分行名"));
                dt.Columns.Add(new DataColumn("扣费帐号号码"));
                dt.Columns.Add(new DataColumn("扣费帐号币种"));
                dt.Columns.Add(new DataColumn("扣费帐号公司名"));
                dt.Columns.Add(new DataColumn("其他注意事项"));
                dt.Columns.Add(new DataColumn("代理行SWIFT代码"));
                dt.Columns.Add(new DataColumn("收款银行之代理行名称及地址"));
                dt.Columns.Add(new DataColumn("收款人开户银行在其代理行帐号"));

                dt.Columns.Add(new DataColumn("收款行SWIFT代码"));
                dt.Columns.Add(new DataColumn("收款人开户行名称及地址"));
                dt.Columns.Add(new DataColumn("收款人帐号"));
                dt.Columns.Add(new DataColumn("收款人名称"));
                dt.Columns.Add(new DataColumn("收款人地址"));
                dt.Columns.Add(new DataColumn("收款行类型"));
                dt.Columns.Add(new DataColumn("汇款附言"));
                dt.Columns.Add(new DataColumn("国内外费用承担"));
                dt.Columns.Add(new DataColumn("收款人常驻国家(地区)代码"));
                dt.Columns.Add(new DataColumn("本笔款项是否为保税货物项下付款"));
                dt.Columns.Add(new DataColumn("结算方式"));

                dt.Columns.Add(new DataColumn("交易编码1"));
                dt.Columns.Add(new DataColumn("相应币种金额1"));
                dt.Columns.Add(new DataColumn("交易附言1"));
                dt.Columns.Add(new DataColumn("交易编码2"));
                dt.Columns.Add(new DataColumn("相应币种金额2"));
                dt.Columns.Add(new DataColumn("交易附言2"));
                dt.Columns.Add(new DataColumn("合同号"));
                dt.Columns.Add(new DataColumn("发票号"));
                dt.Columns.Add(new DataColumn("外汇局批件号"));
                dt.Columns.Add(new DataColumn("申请人姓名"));
                dt.Columns.Add(new DataColumn("申请人电话"));

                MainFormController ctrl = new MainFormController();
                List<Currency> cryList = ctrl.GetCRYList();

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

                        if (arr[0].Equals("P"))
                        {

                            // get invoices
                            //List<string> invoiceList = new List<string>();
                            string invoice = "";
                            for (int j = i + 1; j < fileRows.Count; j++)
                            {
                                string s2 = fileRows[j];
                                if (s2.Substring(0, 1).Equals("I"))
                                {
                                    //invoiceList.Add(s2);
                                    invoice = invoice + " " + s2.Split(',')[1];
                                    i++;
                                }
                                else
                                {
                                    break;
                                }
                            }

                            DataRow dr = dt.NewRow();

                            string cryCd = arr[37];             //Payment Currency
                            Currency cry = MatchCRY(cryCd, cryList);

                            dr["汇款帐号分行名"] = ConfigurationManager.AppSettings["DEBIT_BANK"];
                            dr["汇款帐号号码"] = cry.AccNo;
                            dr["汇款帐号币种"] = cry.CRY;
                            dr["汇款帐号公司名"] = cry.AccComp;
                            dr["汇款方式"] = cry.PayType;         //O:原币汇款, X:购汇后汇款
                            dr["期望日"] = FormatDate(arr[9]);    //Payment Date DD/MM/YYYY
                            dr["期望时间"] = "080000";
                            dr["电汇票汇信汇"] = "TT";
                            dr["发电等级"] = "N";

                            dr["网上申请编号前缀"] = ConfigurationManager.AppSettings["NUM_PREFIX"];
                            dr["网上申请编号"] = DateTime.Now.ToString("yyyyMMdd") + GetRandom();

                            dr["汇款币种"] = cryCd;
                            dr["汇款金额"] = arr[38];           //Invoice/Gross Amount
                            dr["到帐币种"] = cryCd;
                            dr["到帐币种对应金额"] = arr[38];           //Invoice/Gross Amount

                            dr["扣费帐号分行名"] = "天津";
                            dr["扣费帐号号码"] = cry.AccNo;
                            dr["扣费帐号币种"] = cry.CRY;
                            dr["扣费帐号公司名"] = cry.AccComp;

                            dr["收款行SWIFT代码"] = arr[15];     //Payee Bank Code
                            dr["收款人开户行名称及地址"] = "";
                            dr["收款人帐号"] = arr[19];          //Payee A/C No.
                            dr["收款人名称"] = arr[10];          //Payee Name in BO
                            string addr1 = arr[11].Trim();
                            string addr2 = arr[12].Trim();
                            string addr3 = arr[13].Trim();
                            dr["收款人地址"] = addr1 + (string.IsNullOrEmpty(addr1) ? "" : " ") + addr1 + (string.IsNullOrEmpty(addr2) ? "" : " ") + addr3;
                            dr["收款行类型"] = "O";      //O:招行系统外 
                            dr["汇款附言"] = arr[56];
                            dr["国内外费用承担"] = "S";       //O:汇款人, B:收款人, S:共同
                            dr["收款人常驻国家(地区)代码"] = GetCountryCode(addr3);
                            dr["本笔款项是否为保税货物项下付款"] = "N";    //N:否
                            dr["结算方式"] = "O";       //O:其它

                            dr["发票号"] = invoice.Trim();

                            dt.Rows.Add(dr);
                        }
                    }
                }
                return dt;
            }
            catch (Exception ex)
            {
                // format error!
                throw ex;
            }
        }
        #endregion

        #region "other"

        private string GetRandom()
        {
            //Random ro = new Random(10);
            long tick = DateTime.Now.Ticks;
            Random ran = new Random((int)(tick & 0xffffffffL) | (int)(tick >> 32));

            int red = ran.Next(0, 99999999);
            return red.ToString().PadLeft(10, '0');
        }

        private string GetCountryCode(string name)
        {
            string cd = "";
            MainFormController ctrl = new MainFormController();
            cd = ctrl.GetCountryCode(name);

            return cd;
        }

        private Currency MatchCRY(string cryCd, List<Currency> list)
        {
            Currency cry = new Currency();
            cry = list.First(x => x.CRY.ToUpper().Equals(cryCd.ToUpper()));
            if (cry == null)
            {
                cry = list.First(x => x.CRY.ToUpper().Equals("USD"));
                cry.PayType = "X";   //O:原币汇款, X:购汇后汇款
            }
            else
            {
                cry.PayType = "O";  //O:原币汇款, X:购汇后汇款
            }
            return cry;
        }

        private void GetBankName(string bankCd, out string bank, out string bankName, out string bankProv, out string bankCity)
        {
            bank = "";
            bankName = "";
            bankProv = "";
            bankCity = "";

            MainFormController ctrl = new MainFormController();
            DataTable dt = ctrl.GetBankName(bankCd);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    bankName = dt.Rows[0][0].ToString();
                    bank = dt.Rows[0][1].ToString();
                    bankProv = dt.Rows[0][2].ToString();
                    bankCity = dt.Rows[0][3].ToString();
                }
            }
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

        #endregion

    }
}
