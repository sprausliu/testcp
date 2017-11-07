using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Web;


namespace Common {

    /// <summary>
    /// Web关联
    /// </summary>
    public partial class ComUtil {

        /// <summary>
        /// URL的取得
        /// </summary>
        /// <param name="inPageID">画面ID</param>
        /// <param name="inPageType">画面类型</param>
        ///  <returns>画面的URL</returns>
        public static string GetPageURL (string inPageID) {
            return inPageID.ToLower() + "." + Constant.PagesType.TYPE_ASPX.ToLower();
        }

        /// <summary>
        /// URL的取得
        /// </summary>
        /// <param name="inPageID">画面ID</param>
        /// <param name="inPageType">画面类型</param>
        ///  <returns>画面的URL</returns>
        public static string GetPageURL (string inPageID, string inPageType) {
            return inPageID + "." + inPageType;
        }


        /// <summary>
        /// HTML代码的作成
        /// </summary>
        /// <param name="inConvertStr">想变换的文字列</param>
        ///  <returns>变换后的文字列</returns>
        public static string HtmlEscape (object inConvertStr) {
            if (inConvertStr == null)
                return "";
            string returnReslut = inConvertStr.ToString();
            returnReslut = HttpUtility.HtmlEncode(returnReslut);
            return returnReslut;
        }

        /// <summary>
        /// HTML代码的作成(復活)
        /// </summary>
        /// <param name="inDecodeStr">復活したい文字列</param>
        ///  <returns>復活文字列</returns>
        public static string HtmlDecode (string inDecodeStr) {
            if (string.IsNullOrEmpty(inDecodeStr))
                return inDecodeStr;
            return HttpUtility.HtmlDecode(inDecodeStr);
        }

        /// <summary>
        /// HTML文字的作成
		/// 从DB取得文字 变换成HTML显示用文字
        /// 例：\r\n ⇒　<BR/>
        /// </summary>
        /// <param name="inDecodeStr">想復活文字列</param>
        /// <returns>復活后的文字列</returns>
        public static string ConvertToHTML(string inStr) {
            if (string.IsNullOrEmpty(inStr))
                return inStr;
            string retStr = inStr;
            retStr = retStr.Replace("\r\n", "<br/>");
            retStr = retStr.Replace("\n", "<br/>");
            return retStr;
        }

        /// <summary>
        /// Json的变换
        /// object→json
        /// </summary>
        /// <param name="inConvertObj">json化対象</param>
        ///  <returns>Json</returns>
        public static string ConvertToJsonByObject (object inConvertObj) {
            string returnReslut = "";
            System.Web.Script.Serialization.JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();
            returnReslut = js.Serialize(inConvertObj);
            return returnReslut;
        }

        /// <summary>
        /// Json的变换
        /// object→json
        /// 返回值：Dictionary
        /// </summary>
        /// <param name="inConvertStr">Json</param>
        ///  <returns>Object</returns>
        public static object ConvertToObjectByJson (string inConvertStr) {
            object returnReslut = "";
            System.Web.Script.Serialization.JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();
            returnReslut = js.DeserializeObject(inConvertStr);
            return returnReslut;
        }

        /// <summary>
        /// Json的变换
        /// object→json
        /// 返回值：指定类型的対象
        /// </summary>
        /// <param name="inConvertStr">Json</param>
        ///  <returns>Object</returns>
        public static outType ConvertToObjectByJson<outType> (string inConvertStr) {
            System.Web.Script.Serialization.JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();
            return js.Deserialize<outType>(inConvertStr);
        }

        /// <summary>
		/// 变换成DataTablejson
        /// </summary>
        /// <param name="inConvertObj">json化DataTable</param>
        ///  <returns>Json</returns>
        public static string ConvertDataTableToJson(DataTable inConvertDT) {
            List<Dictionary<string, object>> dtList = new List<Dictionary<string, object>>();

            //HEADER信息
            Dictionary<string, object> header = new Dictionary<string, object>();
            for (int j = 0; j < inConvertDT.Columns.Count; j++) {
                header.Add(inConvertDT.Columns[j].ColumnName, inConvertDT.Columns[j].Caption);
            }
            dtList.Add(header);

            //明細信息
            for (int i = 0; i < inConvertDT.Rows.Count; i++) {
                Dictionary<string, object> result = new Dictionary<string, object>();
                for (int j = 0; j < inConvertDT.Columns.Count; j++) {
                    result.Add(inConvertDT.Columns[j].ColumnName, inConvertDT.Rows[i][j].ToString());
                }
                dtList.Add(result);
            }

            string returnReslut = "";
            System.Web.Script.Serialization.JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();
            returnReslut = js.Serialize(dtList);
            return returnReslut;
        }

        /// <summary>
        /// Json的变换
        /// </summary>
        /// <param name="inConvertStr">Json</param>
        ///  <returns>Object</returns>
        public static DataTable ConvertJsonToDataTable(string inConvertStr) {
            DataTable retData = new DataTable();

            ArrayList returnReslut;
            System.Web.Script.Serialization.JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();
            returnReslut = js.Deserialize<ArrayList>(inConvertStr);

            foreach (Dictionary<string, object> drow in returnReslut) {
                //HEADER
                if (retData.Columns.Count == 0) {
                    foreach (string key in drow.Keys) {
                        retData.Columns.Add(key);
                    }
                } else {
                    //明細
                    DataRow row = retData.NewRow();
                    foreach (string key in drow.Keys) {

                        row[key] = drow[key];
                    }
                    retData.Rows.Add(row);
                }
            }

            return retData;
        }

        /// <summary>
        /// ブラウザ検知
        /// </summary>
        ///  <returns>True:対応対象</returns>
        public static bool IsSupportBrowser () {
            bool returnResult = false;
            string[] browserTypeKey = { "IE", "AppleMAC-Safari" };
            string[] browserVerKey = { "8.0", "5.0" };

            HttpBrowserCapabilities browser = HttpContext.Current.Request.Browser;
            String browserrType = browser.Browser;
            String browserrVer = browser.Version;
            double currentVer;

            for (int i = 0; i < browserTypeKey.Length; i++) {
                bool isSupportVer = browserVerKey[i].CompareTo(browserrVer) <= 0;
                if(double.TryParse(browserrVer, out currentVer)) {
                    isSupportVer = double.Parse(browserVerKey[i]).CompareTo(currentVer) <= 0;
                }
                //浏览器和版本確認
                if (browserTypeKey[i].Equals(browserrType) && isSupportVer) {
                    return true;
                }
            }

            return returnResult;
        }

        /// <summary>
		/// 智能机的CHECK
        /// </summary>
        ///  <returns>True:智能机</returns>
        public static bool IsSmartPhone () {
            bool returnResult = false;
            string[] keywords = { "Android", "iPhone", "iPod", "iPad", "Windows Phone" };
            string agent = HttpContext.Current.Request.UserAgent;
            if (!agent.Contains("Windows NT") && !agent.Contains("Macintosh")) {
                foreach (string item in keywords) {
                    if (agent.Contains(item)) {
                        return true;
                    }
                }
            }

            return returnResult;
        }
    }
}
