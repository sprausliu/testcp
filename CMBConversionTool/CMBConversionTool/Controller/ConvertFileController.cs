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

                            string bank = "";
                            string bankName = "";
                            string bankProv = "";
                            string bankCity = "";
                            GetBankName(arr[15], out bank, out bankName, out bankProv, out bankCity);

                            dr["业务参考号"] = DateTime.Now.ToString("yyyyMMdd") + arr[4];   //Customer Reference
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
                            dr["收方行号"] = arr[15];       //
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
    }
}
