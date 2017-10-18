using Common;
using Constant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CMBPayment
{
    public partial class payedit : BasePage
    {

        public PaymentBean CurrentBean
        {
            get
            {
                if (Session[Constant.SessionKey.KEY_Payment_SEARCH_KEYWORD] != null)
                {
                    return (PaymentBean)Session[Constant.SessionKey.KEY_Payment_SEARCH_KEYWORD];
                }
                else
                {
                    return new PaymentBean();
                }
            }
            set
            {
                Session[Constant.SessionKey.KEY_Payment_SEARCH_KEYWORD] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                UserInfoBean user = base.LoginUser;

                this.txtUsage.Text = CurrentBean.Usage;

            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            LogUtil.WriteDebugStartMessage();

            SavePayment();

            LogUtil.WriteDebugEndMessage();
        }

        private void SavePayment()
        {
            string usage = txtUsage.Text.Trim();
            if (string.IsNullOrEmpty(usage))
            {
                lblError.Text = "请填写交易附言内容！";
                return;
            }

            if (ComUtil.Check_IsOvermaxLength(usage,62))
            {
                lblError.Text = "交易附言最大只能填写62个字节的文字。";
                return;
            }

            PaymentReqt pay = new PaymentReqt();
            pay.NUSAGE = usage;
            pay.YURREF = this.CurrentBean.PaymentId;
            pay.user_id = base.LoginUser.UserId;

            MainController ctrl = new MainController();
            ctrl.UpdatePaymentUsage(pay);
            //lblError.Text = ComUtil.GetGlobalResource(Constant.ResourcesKey.KEY_IMSG0001);

            this.CurrentBean.IsSaved = true;

            this.Response.Redirect("payment.aspx");
        }

        protected void btnReturn_Click(object sender, EventArgs e)
        {
            LogUtil.WriteDebugStartMessage();

            this.Response.Redirect("payment.aspx");

            LogUtil.WriteDebugEndMessage();
        }
    }
}