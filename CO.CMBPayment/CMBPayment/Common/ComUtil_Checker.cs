using System;
using System.Text;
using System.Text.RegularExpressions;

namespace Common {

    /// <summary>
    /// 半角英数符号CHECK关联
    /// </summary>
    public partial class ComUtil {

        /// <summary>
        /// 半角英数符号CHECK : 密码
        /// </summary>
        /// <param name="pwd"></param>
        ///  <returns></returns>
        public static bool Check_PasswdPolicy (String pwd) {
            //return Regex.IsMatch(pwd, "^[0-9a-zA-Z]+$");
            return Regex.IsMatch(pwd, @"^(?=.*\d)(?=.*[a-zA-Z])(?=.*[~!@#$%^&*\\;',./_+|{}\[\]:""<>?()=-])[\da-zA-Z~!@#$%^&*\\;',./_+|{}\[\]:""<>?()=-]{3,}$");
        }

        /// <summary>
        /// 半角英数符号CHECK
        /// </summary>
        /// <param name="strChek"></param>
        ///  <returns></returns>
        public static bool Check_IsEnglishNumSign (String strChek) {
            //英数CHECK
            if (!string.IsNullOrEmpty(strChek)) {
                return Regex.IsMatch(strChek, @"^[a-zA-Z0-9!-/:-@¥[-`{-~]+$");
            }
            return true;

        }

        /// <summary>
        /// 整数CHECK
        /// </summary>
        /// <param name="strChek"></param>
        /// <returns></returns>
        public static bool Check_IsNumber2(String strChek) {
            //数字CHECK
            if (!string.IsNullOrEmpty(strChek)) {
                return Regex.IsMatch(strChek, @"^-?\d+$");
            }
            return true;
        }

        /// <summary>
        /// 百分率CHECK
        /// </summary>
        /// <param name="strChek"></param>
        /// <returns></returns>
        public static bool Check_IsPercent(String strChek) {
            //百分率CHECK
            if (!string.IsNullOrEmpty(strChek)) {
                return Regex.IsMatch(strChek, @"^(-?\d+)(\.\d+)?%$");
            }
            return true;
        }

        /// <summary>
        /// 邮件地址的CHECK
        /// </summary>
        /// <param name="strChek"></param>
        ///  <returns></returns>
        public static bool Check_IsEmail(String strChek) {
            //邮件地址CHECK
            if (!string.IsNullOrEmpty(strChek)) {
                return Regex.IsMatch(strChek, @"^([\w-\.]+@([\w-]+\.)+[\w-]{2,4})?$");
            }
            return true;

        }

		/// <summary>
		/// 日期类型CHECK
		/// </summary>
		/// <param name="strChek"></param>
		/// <returns></returns>
		public static bool Check_IsDate(string strChek) {

			if (!string.IsNullOrEmpty(strChek)) {
				return ComUtil.IsDateStringFormat(strChek);
			}
			return true;
		}

        /// <summary>
        /// 文字位数CHECK
        /// </summary>
        /// <param name="inCheckStr">CHECK文字</param>
        /// <param name="inMaxlength">位数</param>
        ///  <returns>false:maxlenth超える</returns>
        public static bool Check_IsOvermaxLength (String inCheckStr, int inMaxlength) {

            int currentLength = Encoding.GetEncoding("GBK").GetByteCount(inCheckStr);

            if (string.IsNullOrEmpty(inCheckStr)) {
                return false;
            }
            return currentLength > inMaxlength;
        }

        /// <summary>
        /// 整数数值CHECK
        /// -2^31 (-2,147,483,648) 从 2^31 - 1 (2,147,483,647) 
        /// 小数点有的場合返回FALSE。
        /// 空、NULL的場合返回FALSE。
        /// </summary>
        /// <param name="inCheckStr">CHECK文字</param>
        ///  <returns>false:全角ではない</returns>
        public static bool Check_IsNumber (String inCheckStr) {
            //系统的文字编码：Default
            //服务器的OS和关联从
            string temp = inCheckStr.Trim().Replace(Constant.FormatInfo.COMMA, "");

            if (string.IsNullOrEmpty(temp)) {
                return false;
            }
            if (temp.IndexOf('.') >= 0) {
                return false;
            }
            int outNum;
            try {
                outNum = Convert.ToInt32(temp);
            } catch {
                return false;
            }
            return true;
        }
        public static bool Check_IsNumberOrNull(String inCheckStr) {
            if (string.IsNullOrEmpty(inCheckStr)) {
                return true;
            }
            return Check_IsNumber(inCheckStr);
        }

        public static bool Check_IsDouble(string inCheckStr) {
            if (string.IsNullOrEmpty(inCheckStr)) {
                return false;
            }
            try {
                double outDouble = Convert.ToDouble(inCheckStr);
            } catch{
                return false;
            }
            return true;
        }

        /// <summary>
        /// 邮件地址CHECK
        /// </summary>
        /// <param name="mailAddress"></param>
        /// <returns></returns>
        public static bool Check_IsCorrectMailAddress (string mailAddress) {
            if (!string.IsNullOrEmpty(mailAddress)) {
                //邮件地址
                string strRegex = @"^([a-zA-Z0-9]+[_|\-|\.]?)*[a-zA-Z0-9]+@([a-zA-Z0-9]+[_|\-|\.]?)*[a-zA-Z0-9]+\.[a-zA-Z]{2,3}$";
                Regex regex = new Regex(strRegex);
                Match match = regex.Match(mailAddress);
                if (!match.Success) {
                    return false;
                }
            }
            return true;
        }
    }
}
