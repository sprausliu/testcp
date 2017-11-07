using Common;
using System;
using System.Collections.Generic;

namespace CMBPayment
{

    public class UserInfoBean
    {

        #region 【private变量】

        //角色ID
        private string roleId;
        //角色名
        private string roleName;
        //用户ID
        private string userId;
        //用户名
        private string userName;
        private string departName;

        #endregion

        #region "属性"

        /// <summary>
        /// 用户ID
        /// </summary>
        public String UserId
        {
            get
            {
                return userId;
            }
            set
            {
                userId = value;
            }
        }

        /// <summary>
        /// 用户名
        /// </summary>
        public String UserName
        {
            get
            {
                return userName;
            }
            set
            {
                userName = value;
            }
        }

        public String Department
        {
            get
            {
                return departName;
            }
            set
            {
                departName = value;
            }
        }
        
        /// <summary>
        /// 角色ID
        /// </summary>
        public String RoleId
        {
            get
            {
                return roleId;
            }
            set
            {
                roleId = value;
            }
        }

        /// <summary>
        /// 角色名
        /// </summary>
        public String RoleName
        {
            get
            {
                return roleName;
            }
            set
            {
                roleName = value;
            }
        }

        public Staff StaffInfo { get; set; }

        public string CMBAcc { get; set; }
        public string AmtLimit { get; set; }

        #endregion

    }
}
