using System;

namespace Common {

    /// <summary>
    /// 表数据GRIDVIEW显示用方法
    /// </summary>
    public partial class ComUtil {


        /// <summary>
        /// 数字逗号分割显示
        /// </summary>
        /// <param name="inConvertStr">项目</param>
        /// <returns>/returns>
        public static string ShowNumber (string inConvertStr) {
            string returnStr = "";
            //空的場合
            if (!string.IsNullOrEmpty(inConvertStr)) {
                try {
                    returnStr = ConvertToNum(inConvertStr, 0).ToString(Constant.FormatInfo.FORMAT_NUMBER_COMMA);
                } catch {
                }
            }
            return returnStr;
        }

        /// <summary>
        /// 数字逗号删除
        /// </summary>
        /// <param name="inConvertStr">项目</param>
        /// <returns></returns>
        public static string RepalceNumberByComma (string inConvertStr) {
            string returnStr = "";
            //空的場合
            if (!string.IsNullOrEmpty(inConvertStr)) {
                try {
                    returnStr = inConvertStr.Replace(Constant.FormatInfo.COMMA, "");
                } catch {
                }
            }
            return returnStr;
        }

        /// <summary>
        /// 日期yyyy/MM/dd显示
        /// </summary>
        /// <param name="inConvertStr">项目</param>
        /// <returns></returns>
        public static string ShowDate (object inConvertStr) {
            string returnStr = "";
            //空的場合
            if (inConvertStr != null && !string.IsNullOrEmpty(inConvertStr.ToString())) {
                try {
                    returnStr = ConvertToDate(inConvertStr).ToString(Constant.FormatInfo.FORMAT_DATE);
                } catch {
                }
            }
            return returnStr;
        }

        /// <summary>
        /// 日期HH/MI24/SS显示
        /// </summary>
        /// <param name="inConvertStr">项目</param>
        /// <returns></returns>
        public static string ShowTime (object inConvertStr) {
            string returnStr = "";
            //空的場合
            if (inConvertStr != null && !string.IsNullOrEmpty(inConvertStr.ToString())) {
                try {
                    returnStr = ConvertToDate(inConvertStr).ToString(Constant.FormatInfo.FORMAT_TIME);
                } catch {
                }
            }
            return returnStr;
        }

        /// <summary>
        /// 日期YYYY/MM/DD HHMISS.fff显示
        /// </summary>
        /// <param name="inConvertStr">项目</param>
        /// <returns></returns>
        public static string ShowDateTime (object inConvertStr) {
            string returnStr = "";
            //空的場合
            if (inConvertStr != null && !string.IsNullOrEmpty(inConvertStr.ToString())) {
                try {
                    returnStr = Convert.ToDateTime(inConvertStr).ToString(Constant.FormatInfo.FORMAT_DATE_TIME_M);
                } catch {
                }
            }
            return returnStr;
        }

        /// <summary>
        /// 日期YYYY/MM/DD HH:MI24:SS显示
        /// </summary>
        /// <param name="inConvertStr">项目</param>
        /// <returns></returns>
        public static string ShowDateTime2 (object inConvertStr) {
            string returnStr = "";
            //空的場合
            if (inConvertStr != null && !string.IsNullOrEmpty(inConvertStr.ToString())) {
                try {
                    returnStr = ConvertToDate(inConvertStr).ToString(Constant.FormatInfo.FORMAT_DATE_TIME);
                } catch {
                }
            }
            return returnStr;
        }

        /// <summary>
        /// 日期YYYY/MM/DD显示
        /// </summary>
        /// <param name="inConvertStr">项目</param>
        /// <returns></returns>
        public static string ShowDateWithDefaultValue (object inConvertStr) {
            string returnStr = ShowDate(inConvertStr);
            if (string.IsNullOrEmpty(returnStr)) {
                returnStr = ComUtil.GetGlobalResource(Constant.ResourcesKey.KEY_IMSG0003);
            }
            return returnStr;
        }

        /// <summary>
        /// 日期YYYY/MM/DD显示
        /// </summary>
        /// <param name="inConvertStr">项目</param>
        /// <returns></returns>
        public static string ShowTimeWithDefaultValue (object inConvertStr) {
            string returnStr = ShowTime(inConvertStr);
            if (string.IsNullOrEmpty(returnStr)) {
                returnStr = ComUtil.GetGlobalResource(Constant.ResourcesKey.KEY_IMSG0011);
            }
            return returnStr;
        }

        /// <summary>
        /// 日期YYYY/MM/DD HH:MI:SS显示
        /// </summary>
        /// <param name="inConvertStr">项目</param>
        /// <returns></returns>
        public static string ShowDateTimeWithDefaultValue (object inConvertStr) {
            string returnStr = ShowDateTime2(inConvertStr);
            if (string.IsNullOrEmpty(returnStr)) {
                returnStr = ComUtil.GetGlobalResource(Constant.ResourcesKey.KEY_IMSG0012);
            }
            return returnStr;
        }

        /// <summary>
        /// 数据空的場合、默认值「-」显示
        /// </summary>
        /// <param name="inConvertStr">项目</param>
        /// <returns></returns>
        //public static string ShowBlankDataWithDefaultValue(string inConvertStr) {
        //    string returnStr = inConvertStr;
        //    if (string.IsNullOrEmpty(inConvertStr)) {
        //        returnStr = Constant.StringJoinMark.StrikeLine;
        //    }
        //    return returnStr;
        //}

		/// <summary>
		/// 数量的格式和単位追加、没有值場合、「-」设定
		/// </summary>
		/// <param name="value">检索后的数量</param>
		/// <param name="unit">単位</param>
		/// <returns>格式化后的数量</returns>
		public static string FormatDMNumberData(string value, string unit, params object[] defaultValue) {
			string line = string.Empty;
			if (defaultValue.Length == 0) {
				//-
				line = ComUtil.GetGlobalResource(Constant.ResourcesKey.KEY_IMSG0008);
			}
			if (string.IsNullOrEmpty(value) || Convert.ToInt32(value) == 0) {
				value = line;
			} else {
				value = ComUtil.ShowNumber(value) + unit;
			}
			return value;
		}
    }
}
