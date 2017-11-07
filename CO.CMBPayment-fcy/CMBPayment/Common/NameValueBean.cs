using System;
using System.Collections.Generic;

namespace Common
{
    /// <summary>
    ///     共通变量用类(text、value)
    /// </summary>
    /// <remarks>
    ///     共通变量用类です
    /// </remarks>
    public class NameValueBean
    {

        #region "变量"
        //名称
        private string itemName;
        //值
        private string itemValue;
        //扩展属性
        private Dictionary<string, string> attributes;
        #endregion

        public NameValueBean()
        {
            this.itemName = "";
            this.itemValue = "";
            this.attributes = new Dictionary<string, string>();
        }

        public NameValueBean(string inItemName, string inItemValue)
        {
            this.itemName = inItemName;
            this.itemValue = inItemValue;
            this.attributes = new Dictionary<string, string>();
        }

        #region "属性"
        /// <summary>
        /// 名称
        /// </summary>
        public string ItemName
        {
            get
            {
                return this.itemName;
            }
            set
            {
                this.itemName = value;
            }
        }

        /// <summary>
        /// 值
        /// </summary>
        public string ItemValue
        {
            get
            {
                return this.itemValue;
            }
            set
            {
                this.itemValue = value;
            }
        }

        /// <summary>
        /// 扩展属性
        /// </summary>
        public Dictionary<string, string> Attributes
        {
            get
            {
                if (this.attributes == null)
                {
                    this.attributes = new Dictionary<string, string>();
                }
                return this.attributes;
            }
            set
            {
                this.attributes = (value == null ? new Dictionary<string, string>() : value);
            }
        }
        #endregion

        #region "方法"
        public int ToInt()
        {
            return Convert.ToInt32(itemValue);
        }

        public double ToDouble()
        {
            return Convert.ToDouble(itemValue);
        }

        /// <summary>
        /// 值Int中变换
        /// </summary>
        /// <param name="defValue"></param>
        /// <returns></returns>
        public int ItemValueToInt(int defValue)
        {
            try
            {
                int v = 0;
                if (int.TryParse(this.itemValue, out v)) return v;
            }
            catch { }
            return defValue;
        }
        #endregion

    }
}