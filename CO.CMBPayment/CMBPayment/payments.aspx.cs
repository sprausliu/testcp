using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using Constant;
using System.Data;
using System.Threading;

namespace CMBPayment
{
    public partial class payments : BasePage
    {
        #region "属性"
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
        #endregion

        #region "事件"
        protected void Page_Load(object sender, EventArgs e)
        {

            this.PageID = Constant.PagesURL.URL_LOGIN;

            if (!IsPostBack)
            {
                UserInfoBean user = base.LoginUser;

                InitDisplayInfo();

                // 操作履歴
                LogUtil.WriteInfoMessage(
                    string.Format(ComUtil.GetGlobalResource(Constant.ResourcesKey.KEY_LMSG0003), base.LoginUser.UserId, base.PageID)
                );
            }

            // 添加分页事件
            this.GPG00001.PageChanged += new GPG0000.PageChangedHander(GPG00001_PageChanged);
        }

        protected void GPG00001_PageChanged(object sender)
        {
            try
            {
                if (this.CurrentBean != null)
                {
                    PaymentBean infoBean = this.CurrentBean;
                    int currentIndex = this.GPG00001.TryGetCurrentPageIndex();
                    infoBean.PageIndex = currentIndex;

                    MainController ctrl = new MainController();
                    DataTable dt = ctrl.GetPaymentList(infoBean);

                    // GridView中设定
                    this.gvPaymentInfo.DataSource = dt;
                    // DataSource中设定的值反映
                    this.gvPaymentInfo.DataBind();

                    // 分页的设定
                    base.SetChangePage(infoBean, currentIndex, this.GPG00001);

                }
                LogUtil.WriteDebugEndMessage();
            }
            catch (ThreadAbortException)
            {
            }
            catch (Exception ex)
            {
                LogUtil.WriteErrorMessage(ex);
                // 迁移到错误画面
                string strMSG = String.Format(
                        ComUtil.GetGlobalResource(Constant.ResourcesKey.KEY_ACTION_BUTTON_CLICK),
                        ComUtil.GetGlobalResource(Constant.ResourcesKey.KEY_BUTTON_PAGGING));
                base.MoveToErrorPage(ex, strMSG);
            }
        }

        protected void gvPaymentInfo_RowCommand(object sender, GridViewCommandEventArgs e)
        {
        }

        protected void gvPaymentInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
        }

        protected void btnReturn_Click(object sender, EventArgs e)
        {
            LogUtil.WriteDebugStartMessage();

            if (base.LoginUser.RoleId.Equals(Constant.ConstantInfo.ROLE_MAKER))
            {
                this.Response.Redirect("maker.aspx");

            }
            else if (base.LoginUser.RoleId.Equals(Constant.ConstantInfo.ROLE_CHEKER1) || base.LoginUser.RoleId.Equals(Constant.ConstantInfo.ROLE_CHEKER2))
            {

                this.Response.Redirect("batch.aspx");
            }

            // 操作履歴
            LogUtil.WriteInfoMessage(
                    string.Format(ComUtil.GetGlobalResource(Constant.ResourcesKey.KEY_LMSG0004), base.LoginUser.UserId,
                    ComUtil.GetGlobalResource(Constant.ResourcesKey.KEY_BUTTON_CANCEL))
                );
            LogUtil.WriteDebugEndMessage();
        }
        #endregion

        #region "方法"
        private void InitDisplayInfo()
        {
            BatchBean batchBean = (BatchBean)Session[Constant.SessionKey.KEY_Batch_SEARCH_KEYWORD];

            PaymentBean infoBean = this.CurrentBean;
            infoBean.BatchId = batchBean.BatchId;
            UserInfoBean user = base.LoginUser;

            if (infoBean == null || infoBean.PageSize == 0 || !infoBean.IsSearched)
            {
                //// 分页初期设定
                //base.SetChangePage(infoBean, 1, this.GPG00001);
                //BindGridviewInfo(new DataTable());

                SetSearchCondition(infoBean);
                DisplayInfo(infoBean);

                // 分页设定
                base.SetChangePage(infoBean, 1, this.GPG00001);
                infoBean.IsSearched = true;
                this.CurrentBean = infoBean;
            }
            else
            {
                // 画面条件重新设定
                //SetConditonToPage(infoBean);
                // 画面再检索
                DisplayInfo(infoBean);
                // 分页设定
                base.SetChangePage(infoBean, infoBean.PageIndex, this.GPG00001);
            }

        }

        private void SetSearchCondition(PaymentBean infoBean)
        {
            infoBean.PageIndex = 1;
            infoBean.PageSize = SystemInfoController.GetControlInfo(Constant.SystemControlInfo.KEY_MAX_PAGE_CNT).ToInt();
        }

        private void DisplayInfo(PaymentBean infoBean)
        {
            MainController ctrl1 = new MainController();
            DataTable dt = ctrl1.GetBatchInfo(infoBean.BatchId);
            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                lblBatchId.Text = GetDdData(dr["batch_id"]);
                lblStatus.Text = GetDdData(dr["status"]);
                lblMakerId.Text = GetDdData(dr["maker_id"]);
                lblMakerTime.Text = GetDdData(dr["maker_time"]);
                lblAppr1Id.Text = GetDdData(dr["appr1_id"]);
                lblAppr2Id.Text = GetDdData(dr["appr2_id"]);
                lblAppr1Time.Text = GetDdData(dr["appr1_time"]);
                lblAppr2Time.Text = GetDdData(dr["appr2_time"]);
                lblTtlAmt.Text = GetDdData(dr["ttl_amt"]);
                lblPayCnt.Text = GetDdData(dr["pay_cnt"]);
            }

            MainController ctrl2 = new MainController();
            DataTable dtPay = ctrl2.GetPaymentList(infoBean);
            BindGridviewInfo(dtPay);
        }

        private void BindGridviewInfo(DataTable dt)
        {
            bool hasData = dt.Rows.Count != 0;

            gvPaymentInfo.DataSource = hasData ? dt : CreateBlankTable();
            gvPaymentInfo.DataBind();

            if (!hasData)
            {
                gvPaymentInfo.Rows[0].Cells[0].ColumnSpan = gvPaymentInfo.Columns.Count;
                gvPaymentInfo.Rows[0].FindControl("lblEmptyData").Visible = true;
                ((Label)gvPaymentInfo.Rows[0].FindControl("lblEmptyData")).Text =
                    ComUtil.GetGlobalResource(Constant.ResourcesKey.INFO_SEARCH_NODATA);
                gvPaymentInfo.Rows[0].FindControl("lkBatchId").Visible = false;
                for (int i = 1; i < gvPaymentInfo.Columns.Count; i++)
                {
                    gvPaymentInfo.Rows[0].Cells[i].Visible = false;
                }
            }
        }

        private DataTable CreateBlankTable()
        {
            DataTable blankTable = new DataTable();
            blankTable.Columns.Add(new DataColumn("YURREF"));
            blankTable.Columns.Add(new DataColumn("batch_id"));
            blankTable.Columns.Add(new DataColumn("status_id"));
            blankTable.Columns.Add(new DataColumn("comment"));
            blankTable.Columns.Add(new DataColumn("EPTDAT"));
            blankTable.Columns.Add(new DataColumn("OPRDAT"));
            blankTable.Columns.Add(new DataColumn("DBTACC"));
            blankTable.Columns.Add(new DataColumn("DBTBBK"));
            blankTable.Columns.Add(new DataColumn("TRSAMT"));
            blankTable.Columns.Add(new DataColumn("CCYNBR"));
            blankTable.Columns.Add(new DataColumn("NUSAGE"));
            blankTable.Columns.Add(new DataColumn("BUSNAR"));
            blankTable.Columns.Add(new DataColumn("CRTACC"));
            blankTable.Columns.Add(new DataColumn("CRTNAM"));
            blankTable.Columns.Add(new DataColumn("LRVEAN"));
            blankTable.Columns.Add(new DataColumn("BRDNBR"));
            blankTable.Columns.Add(new DataColumn("BNKFLG"));
            blankTable.Columns.Add(new DataColumn("CRTBNK"));
            blankTable.Columns.Add(new DataColumn("CTYCOD"));
            blankTable.Columns.Add(new DataColumn("CRTADR"));
            blankTable.Columns.Add(new DataColumn("NTFCH1"));
            blankTable.Columns.Add(new DataColumn("update_id"));
            blankTable.Columns.Add(new DataColumn("update_time"));
            blankTable.Rows.Add(blankTable.NewRow());
            return blankTable;

        }

        private string GetDdData(object obj)
        {
            string retVal = string.Empty;
            if (obj == null || obj == DBNull.Value)
            {
                return retVal;
            }
            else
            {
                retVal = obj.ToString();
            }
            return retVal;
        }

        #endregion

    }
}