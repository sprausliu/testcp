using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CMBPayment
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            txtUserName.Focus();

            if (!IsPostBack)
            {
                Session.Clear();
            }

            this.lblError.Text = "";
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {

            string userNm = txtUserName.Text;
            string passwd = txtPassword.Text;
            if (string.IsNullOrEmpty(userNm) || string.IsNullOrEmpty(passwd) )
            {
                this.lblError.Text = "Please input PWID and password!";
                return;
            }

            Staff staff = ADOperation.IsAuthenticated(userNm, passwd);

            if (staff == null)
            {
                this.lblError.Text = "Authentication failed!";
                return;
            }
            else
            {

                CommonController lc = new CommonController();
                UserInfoBean userInfo = lc.InitUserInfo(staff.BankID);
                userInfo.UserName = staff.DisplayName;
                userInfo.Department = staff.Department;
                userInfo.StaffInfo = staff;
                this.Session[Constant.SessionKey.LOGIN_USER_INFO] = userInfo;

                if (userInfo.RoleId.Equals(Constant.ConstantInfo.ROLE_MAKER))
                {
                    this.Response.Redirect("maker.aspx");

                }
                else if (userInfo.RoleId.Equals(Constant.ConstantInfo.ROLE_CHEKER1) || userInfo.RoleId.Equals(Constant.ConstantInfo.ROLE_CHEKER2))
                {

                    this.Response.Redirect("batch.aspx");
                }
            }
        }

       
    }
}