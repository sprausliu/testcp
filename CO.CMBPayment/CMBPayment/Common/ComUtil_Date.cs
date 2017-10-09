using System;
using System.Collections.Generic;
using System.Globalization;



namespace Common {

    /// <summary>
    /// 日期
    /// </summary>
    public partial class ComUtil {

        /// <summary>
        /// 現在日期取得
        /// </summary>
        /// <returns>yyyy/MM/dd</returns>
        public static string GetNowDate () {
            return System.DateTime.Now.ToString("yyyy/MM/dd");
        }

        /// <summary>
        /// 日期格式
        /// </summary>
        /// <param name="inDateStr"></param>
        ///  <returns>日期</returns>
        public static DateTime ConvertToDate (object inDateStr) {
            int year = 0;
            int month = 1;
            int day = 1;
            string tmpDate = inDateStr.ToString();
            if (tmpDate.Length == 8) {
                year = Convert.ToInt32(tmpDate.Substring(0, 4));
                month = Convert.ToInt32(tmpDate.Substring(4, 2));
                day = Convert.ToInt32(tmpDate.Substring(6, 2));
            } else if (tmpDate.Length == 6) {
                year = Convert.ToInt32(tmpDate.Substring(0, 4));
                month = Convert.ToInt32(tmpDate.Substring(4, 2));
            } else {
                return Convert.ToDateTime(tmpDate);
            }
            return new DateTime(year, month, day);
        }

        /// <summary>
        /// 日期格式(TO字符串)
        /// </summary>
        /// <param name="inDateTime"></param>
        ///  <returns>yyyyMMdd</returns>
        public static string FormatDateToString (DateTime inDateTime) {
            string returnStr = "";
            returnStr = inDateTime.ToString(Constant.FormatInfo.FORMAT_DATE_NOMARK);
            return returnStr;
        }

        /// <summary>
        /// 日期格式(TO字符串)
        /// </summary>
        /// <param name="inDateTime"></param>
        ///  <returns>returnStr(YYYY/MM/DD)</returns>
        public static string FormatDateToStringWithMark (DateTime inDateTime) {
            string returnStr = "";
            returnStr = inDateTime.ToString(Constant.FormatInfo.FORMAT_DATE);
            return returnStr;
        }

        /// <summary>
        /// 日期格式(TO字符串)
        /// </summary>
        /// <param name="inDateTime"></param>
        ///  <returns>returnStr(YYYY/MM/DD)</returns>
        public static string FormatDateToMillisecondString (DateTime inDateTime) {
            string returnStr = "";
            returnStr = inDateTime.ToString(Constant.FormatInfo.FORMAT_DATE_TIME_M);
            return returnStr;
        }

        /// <summary>
        /// 日期格式CHECK
        /// (YYYY/MM/DD)
        /// </summary>
        /// <param name="inDateTime"></param>
        ///  <returns></returns>
        public static bool IsDateStringFormat (string inDateTime) {
            bool returnStr = true;
            try {
                //空以外的場合
                if (!string.IsNullOrEmpty(inDateTime)) {
                    string[] strYMD = inDateTime.Split('/');
                    if (strYMD.Length != 3) {
                        return false;
                    }
                    Convert.ToDateTime(inDateTime);
                }
            } catch {
                returnStr = false;
            }
            return returnStr;
        }

        /// <summary>
        /// 日期大小CHECK
        /// </summary>
        /// <param name="inDateTimeFrom">開始日</param>
        /// <param name="inDateTimeTo">終了日</param>
        /// <param name="inDateDefaultValue">日期默认值</param>
        /// <returns>
        /// inDateTimeFrom > inDateTimeTo
        /// </returns>
        public static bool Check_DateIsReverse (
            string inDateTimeFrom, string inDateTimeTo, string inDateDefaultValue) {

            bool returnStr = true;
            try {
                if (string.IsNullOrEmpty(inDateTimeFrom) || string.IsNullOrEmpty(inDateTimeTo) ||
                    inDateTimeFrom == inDateDefaultValue || inDateTimeTo == inDateDefaultValue) {
                    return false;
                }
                DateTime from = ConvertToDate(inDateTimeFrom);
                DateTime to = ConvertToDate(inDateTimeTo);
                return from > to;
            } catch {
                returnStr = true;
            }
            return returnStr;
        }
        /// <summary>
        /// 日期大小CHECK
        /// </summary>
        /// <param name="inDateTimeFrom">開始日</param>
        /// <param name="inDateTimeTo">終了日</param>
        /// <param name="inDateDefaultValue">日期默认值</param>
        /// <returns></returns>
        public static bool Check_DateIsOver(
            string inDateTimeFrom, string inDateTimeTo, string inDateDefaultValue) {

            bool returnStr = true;
            try {
                if (string.IsNullOrEmpty(inDateTimeFrom) || string.IsNullOrEmpty(inDateTimeTo) ||
                    inDateTimeFrom == inDateDefaultValue || inDateTimeTo == inDateDefaultValue) {
                    return false;
                }
                DateTime from = Convert.ToDateTime(inDateTimeFrom);
                DateTime to = Convert.ToDateTime(inDateTimeTo);
                return from > to;
            } catch {
                returnStr = true;
            }
            return returnStr;
        }

        /// <summary>
		/// 根据输入日期、日期取得、不正日期的場合、空返回
        /// </summary>
        /// <param name="inputDate">输入日期</param>
        /// <returns>正确的日期的場合、输入日期、其他的場合、空</returns>
        public static string CheckDate(string inputDate) {
            string retDate = inputDate;
            if (!string.IsNullOrEmpty(inputDate)) {
                if (!ComUtil.IsDateStringFormat(inputDate)) {
                    retDate = string.Empty;
                }
            }
            return retDate;
        }

        /// <summary>
        /// YYYYMM形式日期 月加算
        /// </summary>
        /// <param name="date"></param>
        /// <param name="addMonths"></param>
        /// <returns></returns>
        public static string YyyymmAddMonths(string date, int addMonths) {
            string yyyyMm = string.Empty;
            try {
                yyyyMm = DateTime.ParseExact((date + "01"), "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None).AddMonths(addMonths).ToString("yyyyMM");
            } catch (Exception) {
                yyyyMm = string.Empty;
            }
            return yyyyMm;
        }
    
    }

}
