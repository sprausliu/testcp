using Common;
using System;
using System.Collections.Generic;

namespace CMBPayment
{
    /// <summary>
    ///     系统信息用类
    /// </summary>
    /// <remarks>
    ///     系统信息用类です
    /// </remarks>
    public class SystemInfoController
    {

        #region 【private变量】
        //系统日期
        private static DateTime sysDate;
        //编码MASTER信息
        private static Dictionary<string, NameValueBean> codeInfo;
        #endregion

        #region "属性"
        /// <summary>
        /// 系统日期
        /// </summary>
        public static DateTime SystemDate
        {
            get
            {
                DateTime dt = DateTime.Now;
                if (dt.Year != sysDate.Year || dt.Month != sysDate.Month || dt.Day != sysDate.Day)
                {
                    sysDate = GetSystemDate();
                }
                return sysDate;
            }
            set { sysDate = value; }
        }

        /// <summary>
        /// 编码MASTER信息
        /// </summary>
        public static Dictionary<string, NameValueBean> ControlInfo
        {
            get
            {
                if (codeInfo == null)
                {
                    codeInfo = InitControlInfo();
                }
                return codeInfo;
            }
            set
            {
                codeInfo = value;
            }
        }
        #endregion

        #region "方法"
        /// <summary>
        /// 系统信息
        /// </summary>
        private static Dictionary<string, NameValueBean> InitControlInfo()
        {
            Dictionary<string, NameValueBean> returnResult = new Dictionary<string, NameValueBean>();

            CommonController comCtrl = new CommonController();
            returnResult = comCtrl.GetSystemControlInfo();

            return returnResult;
        }

        /// <summary>
        /// 系统信息
        /// </summary>
        public static NameValueBean GetControlInfo(string inKey)
        {
            NameValueBean returnResult = new NameValueBean("", "");
            ControlInfo.TryGetValue(inKey, out returnResult);
            if (returnResult == null)
            {
                returnResult = new NameValueBean(inKey, "");
            }
            return returnResult;
        }

        /// <summary>
        /// 系统信息
        /// </summary>
        private static DateTime GetSystemDate()
        {
            CommonController comCtrl = new CommonController();
            return comCtrl.GetSystemDate();
        }

        #endregion

    }
}
