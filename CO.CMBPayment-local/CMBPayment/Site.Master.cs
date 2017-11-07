using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CMBPayment
{
    public partial class Site : System.Web.UI.MasterPage
    {
        public UserInfoBean LoginUser
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


        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void linkLogout_Click(object sender, EventArgs e)
        {
            if (this.Session[Constant.SessionKey.LOGIN_USER_INFO] != null)
            {
                LogUtil.WriteInfoMessage(string.Format("{0} log out.", LoginUser.UserId));
            }

            Session.Clear();
            Session.Abandon();
            Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", ""));

            MoveURL(ComUtil.GetPageURL(Constant.PagesURL.URL_LOGIN).ToLower());
        }

        protected void MoveURL(string inPageUrl)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(),
                System.Guid.NewGuid().ToString(),
                "MovePageWithNoHistory('" + inPageUrl + "');",
                true);
        }
    }
}