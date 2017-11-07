using CMBPayment;
namespace Common
{
	public class BaseUserControl : System.Web.UI.UserControl {

		#region "方法"
		/// <summary>
		/// 登录用户信息
		/// </summary>
        protected UserInfoBean LoginUser
        {
            get
            {
                if (this.Session[Constant.SessionKey.LOGIN_USER_INFO] != null)
                {
                    return (UserInfoBean)this.Session[Constant.SessionKey.LOGIN_USER_INFO];
                }
                return new UserInfoBean();
            }
        }

		#endregion
	}
}
