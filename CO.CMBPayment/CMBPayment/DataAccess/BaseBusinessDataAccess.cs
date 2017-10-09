using Common;
using System.Data.SqlClient;

namespace CMBPayment
{
    /// <summary>
    ///     业务用共通数据访问用类
    /// </summary>
    /// <remarks>
    ///     業務用共通数据访问用类です
    /// </remarks>
    public class BaseBusinessDataAccess : BaseDataAccess {

        /// <summary>
        /// 登录用户信息
        /// </summary>
        protected UserInfoBean LoginUser
        {
            get {
                if (System.Web.HttpContext.Current.Session[Constant.SessionKey.LOGIN_USER_INFO] != null) {
                    return (UserInfoBean)System.Web.HttpContext.Current.Session[Constant.SessionKey.LOGIN_USER_INFO];
                }
                return new UserInfoBean();
            }
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="conn">SQL接続</param>
        /// <returns></returns>
        public BaseBusinessDataAccess(SqlConnection inConn)
            : base(inConn) {
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="conn">SQL接続</param>
        /// <param name="inTrans">SQL事务</param>
        /// <returns></returns>
        public BaseBusinessDataAccess(SqlConnection inConn, SqlTransaction inTrans)
            : base(inConn, inTrans) {
        }

    }
}