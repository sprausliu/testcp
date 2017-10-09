using Common;
using System;

namespace CMBPayment
{
    public partial class error : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LogUtil.WriteDebugStartMessage();

                this.Session.Clear();
            }
        }
    }
}
