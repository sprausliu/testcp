using Common;
using Constant;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CMBPayment
{
    public partial class batch : BasePage
    {
        #region "属性"
        public BatchBean CurrentBean
        {
            get
            {
                if (Session[Constant.SessionKey.KEY_Batch_SEARCH_KEYWORD] != null)
                {
                    return (BatchBean)Session[Constant.SessionKey.KEY_Batch_SEARCH_KEYWORD];
                }
                else
                {
                    return new BatchBean();
                }
            }
            set
            {
                Session[Constant.SessionKey.KEY_Batch_SEARCH_KEYWORD] = value;
            }
        }
        #endregion

        #region "事件"
        protected void Page_Load(object sender, EventArgs e)
        {
            this.PageID = Constant.PagesURL.URL_BATCH;

            if (!IsPostBack)
            {
                UserInfoBean user = base.LoginUser;

                InitDisplayInfo();

                // 操作履歴
                LogUtil.WriteInfoMessage(
                    string.Format(ComUtil.GetGlobalResource(Constant.ResourcesKey.KEY_LMSG0003), base.LoginUser.UserId, base.PageID)
                );
            }

            lblError.Text = "";
            // 添加分页事件
            this.GPG00001.PageChanged += new GPG0000.PageChangedHander(GPG00001_PageChanged);
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LogUtil.WriteDebugStartMessage();

            BatchBean infoBean = new BatchBean();

            SetSearchCondition(infoBean);
            DisplayInfo(infoBean);

            // 分页设定
            base.SetChangePage(infoBean, 1, this.GPG00001);
            infoBean.IsSearched = true;
            this.CurrentBean = infoBean;

            // 操作履歴
            LogUtil.WriteInfoMessage(
                    string.Format(ComUtil.GetGlobalResource(Constant.ResourcesKey.KEY_LMSG0004), base.LoginUser.UserId,
                    ComUtil.GetGlobalResource(Constant.ResourcesKey.KEY_BUTTON_SEARCH))
                );
            LogUtil.WriteDebugEndMessage();
        }

        protected void GPG00001_PageChanged(object sender)
        {
            try
            {
                if (this.CurrentBean != null)
                {
                    BatchBean infoBean = this.CurrentBean;
                    int currentIndex = this.GPG00001.TryGetCurrentPageIndex();
                    infoBean.PageIndex = currentIndex;

                    MainController ctrl = new MainController();
                    DataTable dt = ctrl.GetBatchList(infoBean);

                    // GridView中设定
                    this.gvBatchInfo.DataSource = dt;
                    // DataSource中设定的值反映
                    this.gvBatchInfo.DataBind();

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

        protected void gvBatchInfo_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridViewRow row = null;

            if (e.CommandName.Equals("id"))
            {
                // goto payment page
                LinkButton lkBatchId = (LinkButton)e.CommandSource;
                BatchBean infoBean = this.CurrentBean;
                infoBean.BatchId = lkBatchId.Text;
                infoBean.IsEditMode = true;
                this.CurrentBean = infoBean;
                Response.Redirect(ComUtil.GetPageURL(Constant.PagesURL.URL_PAYMENTS));
            }
            else if (e.CommandName.Equals("Appr1"))
            {
                row = (GridViewRow)((Button)e.CommandSource).Parent.Parent;
                string batchId = ((LinkButton)row.FindControl("lkBatchId")).Text;
                string updDateKey = ((HiddenField)row.FindControl("hidUpdateDateKey")).Value;
                string appr2Id = ((Label)row.FindControl("lblApprId2")).Text;
                UpadateAppr1(batchId, updDateKey, appr2Id);
            }
            else if (e.CommandName.Equals("Appr2"))
            {
                row = (GridViewRow)((Button)e.CommandSource).Parent.Parent;
                string batchId = ((LinkButton)row.FindControl("lkBatchId")).Text;
                string updDateKey = ((HiddenField)row.FindControl("hidUpdateDateKey")).Value;
                string appr1Id = ((Label)row.FindControl("lblApprId1")).Text;

                UpadateAppr2(batchId, updDateKey, appr1Id);
            }
            else if (e.CommandName.Equals("Rejt"))
            {
                row = (GridViewRow)((Button)e.CommandSource).Parent.Parent;
                string batchId = ((LinkButton)row.FindControl("lkBatchId")).Text;
                string updDateKey = ((HiddenField)row.FindControl("hidUpdateDateKey")).Value;

                RejectBatch(batchId, updDateKey);
            }
            else if (e.CommandName.Equals("Payment"))
            {
                row = (GridViewRow)((Button)e.CommandSource).Parent.Parent;
                string batchId = ((LinkButton)row.FindControl("lkBatchId")).Text;
                string updDateKey = ((HiddenField)row.FindControl("hidUpdateDateKey")).Value;

                DoPayment(batchId, updDateKey);
            }
        }

        protected void gvBatchInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                UserInfoBean user = base.LoginUser;
                if (user.RoleId.Equals(Constant.ConstantInfo.ROLE_MAKER))
                {
                    e.Row.Cells[0].FindControl("lblApprId1").Visible = true;
                    e.Row.Cells[0].FindControl("lblApprId2").Visible = true;
                    e.Row.Cells[0].FindControl("btnPayment").Visible = false;
                }
                else if (user.RoleId.Equals(Constant.ConstantInfo.ROLE_CHEKER1) || user.RoleId.Equals(Constant.ConstantInfo.ROLE_CHEKER2))
                {

                    SetCmmdBatVisible(e);
                }

            }
        }

        protected void btnPayResult_Click(object sender, EventArgs e)
        {
            try
            {
                ViewPayResult();
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region "方法"

        private void SetCmmdBatVisible(GridViewRowEventArgs e)
        {
            UserInfoBean user = base.LoginUser;

            DataRowView dr = (DataRowView)e.Row.DataItem;
            string appr1nm = dr["appr1_id"].ToString();
            string appr2nm = dr["appr2_id"].ToString();

            double ttl_amt = ComUtil.ConvertToDouble(dr["ttl_amt"].ToString(), 0);
            double amtLmt = ComUtil.ConvertToDouble(user.AmtLimit, 0);

            string status = dr["status_id"].ToString();

            // 未授权 or 部分授权
            if (status.Equals(Constant.ConstantInfo.BTSTAT_01) || status.Equals(Constant.ConstantInfo.BTSTAT_02))
            {
                // 授权1按钮
                if (string.IsNullOrEmpty(appr1nm))
                {
                    e.Row.Cells[0].FindControl("btnAppr1").Visible = true;
                }
                else
                {
                    e.Row.Cells[0].FindControl("lblApprId1").Visible = true;
                }

                // 授权2按钮
                if (string.IsNullOrEmpty(appr2nm) &&
                    ((user.RoleId.Equals(Constant.ConstantInfo.ROLE_CHEKER1) && (amtLmt > ttl_amt))
                        || user.RoleId.Equals(Constant.ConstantInfo.ROLE_CHEKER2)))
                {
                    e.Row.Cells[0].FindControl("btnAppr2").Visible = true;
                }
                else
                {
                    if (string.IsNullOrEmpty(appr2nm) && (user.RoleId.Equals(Constant.ConstantInfo.ROLE_CHEKER1) && (amtLmt <= ttl_amt)))
                    {
                        e.Row.Cells[0].FindControl("lblOver").Visible = true;
                    }
                    e.Row.Cells[0].FindControl("lblApprId2").Visible = true;
                }

                // 拒绝按钮
                e.Row.Cells[0].FindControl("btnRejt").Visible = true;
            }
            else
            {
                e.Row.Cells[0].FindControl("lblApprId1").Visible = true;
                e.Row.Cells[0].FindControl("lblApprId2").Visible = true;

                // 已授权
                if (status.Equals(Constant.ConstantInfo.BTSTAT_03))
                {
                    (e.Row.Cells[0].FindControl("btnPayment") as Button).Enabled = true;
                }

                if (status.Equals(Constant.ConstantInfo.BTSTAT_97))
                {
                    e.Row.Cells[0].FindControl("lblRejcId").Visible = true;
                }
            }

        }

        private void InitDisplayInfo()
        {
            List<NameValueBean> listStatusType = new List<NameValueBean>(); //cc.GetCodeNameList("BATCH_STATUS");
            listStatusType.Insert(0, new NameValueBean("", string.Empty));
            listStatusType.Add(new NameValueBean("未授权", "01"));
            listStatusType.Add(new NameValueBean("部分授权", "02"));
            listStatusType.Add(new NameValueBean("已授权", "03"));
            listStatusType.Add(new NameValueBean("已发送", "04"));
            listStatusType.Add(new NameValueBean("已完成", "05"));

            base.BindDropDownListData(ddlStatusType, listStatusType, string.Empty, true);
            BatchBean infoBean = this.CurrentBean;

            if (infoBean == null || infoBean.PageSize == 0 || !infoBean.IsSearched)
            {
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
                SetConditonToPage(infoBean);
                // 画面再检索
                DisplayInfo(infoBean);
                // 分页设定
                base.SetChangePage(infoBean, infoBean.PageIndex, this.GPG00001);
            }

        }

        private void SearchBatch()
        {
            BatchBean infoBean = this.CurrentBean;
            // 画面再检索
            DisplayInfo(infoBean);
            // 分页设定
            base.SetChangePage(infoBean, infoBean.PageIndex, this.GPG00001);
        }

        private void UpadateAppr1(string batchId, string updDateKey, string appr2Id)
        {
            if (appr2Id.Equals(base.LoginUser.UserId))
            {
                lblError.Text = ComUtil.GetGlobalResource(Constant.ResourcesKey.KEY_LMSG0009);
                return;
            }

            BatchItemBean itemBean = new BatchItemBean();
            itemBean.BatchId = batchId;
            if (string.IsNullOrEmpty(appr2Id))
            {
                itemBean.StatusId = ConstantInfo.BTSTAT_02;
            }
            else
            {
                itemBean.StatusId = ConstantInfo.BTSTAT_03;
            }
            itemBean.Appr1Id = base.LoginUser.UserId;
            itemBean.UpdateUserId = base.LoginUser.UserId;
            itemBean.UpdateDateKey = updDateKey;

            MainController ctrl = new MainController();
            if (ctrl.UpdateBatchInfo(itemBean))
            {
                lblError.Text = ComUtil.GetGlobalResource(Constant.ResourcesKey.KEY_IMSG0001);
            }
            else
            {
                lblError.Text = ComUtil.GetGlobalResource(Constant.ResourcesKey.KEY_WMSG0011);
            }

            this.SearchBatch();
        }

        private void UpadateAppr2(string batchId, string updDateKey, string appr1Id)
        {
            if (appr1Id.Equals(base.LoginUser.UserId))
            {
                lblError.Text = ComUtil.GetGlobalResource(Constant.ResourcesKey.KEY_LMSG0009);
                return;
            }

            BatchItemBean itemBean = new BatchItemBean();
            itemBean.BatchId = batchId;
            if (string.IsNullOrEmpty(appr1Id))
            {
                itemBean.StatusId = ConstantInfo.BTSTAT_02;
            }
            else
            {
                itemBean.StatusId = ConstantInfo.BTSTAT_03;
            }
            itemBean.Appr2Id = base.LoginUser.UserId;
            itemBean.UpdateUserId = base.LoginUser.UserId;
            itemBean.UpdateDateKey = updDateKey;

            MainController ctrl = new MainController();
            if (ctrl.UpdateBatchInfo(itemBean))
            {
                lblError.Text = ComUtil.GetGlobalResource(Constant.ResourcesKey.KEY_IMSG0001);
            }
            else
            {
                lblError.Text = ComUtil.GetGlobalResource(Constant.ResourcesKey.KEY_WMSG0011);
            }

            this.SearchBatch();
        }

        private void RejectBatch(string batchId, string updDateKey)
        {
            BatchItemBean itemBean = new BatchItemBean();
            itemBean.BatchId = batchId;
            itemBean.StatusId = ConstantInfo.BTSTAT_97;
            itemBean.RejcId = base.LoginUser.UserId;
            itemBean.UpdateUserId = base.LoginUser.UserId;
            itemBean.UpdateDateKey = updDateKey;

            MainController ctrl = new MainController();
            if (ctrl.UpdateBatchInfo(itemBean))
            {
                lblError.Text = ComUtil.GetGlobalResource(Constant.ResourcesKey.KEY_IMSG0001);
            }
            else
            {
                lblError.Text = ComUtil.GetGlobalResource(Constant.ResourcesKey.KEY_WMSG0011);
            }

            this.SearchBatch();
        }

        private void DoPayment(string batchId, string updDateKey)
        {
            string ipAddr = txtIpAddr.Text.Trim();
            string port = txtPort.Text.Trim();
            if (string.IsNullOrEmpty(ipAddr) || string.IsNullOrEmpty(port))
            {
                lblError.Text = "请填写前置机的IP地址及端口号。";
                return;
            }

            try
            {
                List<List<PaymentReqt>> all = new List<List<PaymentReqt>>();
                MainController ctrl = new MainController();
                List<PaymentReqt> payList = ctrl.GetPaymentsByBatchId(batchId);

                int payCnt = payList.Count;
                int index = 0;
                int time = payCnt / 30;
                int mod = payCnt % 30;
                if (mod > 0)
                {
                    time = time + 1;
                }
                for (int i = 0; i < time; i++)
                {
                    List<PaymentReqt> pay30List = new List<PaymentReqt>();
                    for (int j = 0; j < 30; j++)
                    {
                        index = 30 * i + j;

                        if (index >= payCnt)
                        {
                            break;
                        }
                        else
                        {
                            PaymentReqt pay30 = payList[index];
                            pay30List.Add(pay30);
                        }
                    }
                    all.Add(pay30List);
                }

                string errorMsg = "";
                for (int i = 0; i < all.Count; i++)
                {
                    List<PaymentReqt> pay30List = all[i];

                    errorMsg = errorMsg + PostHttpRest(pay30List, ipAddr, port);
                }

                if (string.IsNullOrEmpty(errorMsg))
                {
                    lblError.Text = lblError.Text = ComUtil.GetGlobalResource(Constant.ResourcesKey.KEY_IMSG0001); ;
                }
                else
                {
                    lblError.Text = errorMsg;
                }

                SearchBatch();
            }
            catch (Exception ex)
            {
                LogUtil.WriteErrorMessage(ex);
                throw ex;
            }
        }

        private void ViewPayResult()
        {
            string ipAddr = txtIpAddr.Text.Trim();
            string port = txtPort.Text.Trim();
            string start = txtDayStart.Text;
            string end = txtDayEnd.Text;

            if (string.IsNullOrEmpty(ipAddr) || string.IsNullOrEmpty(port))
            {
                lblError.Text = "请填写前置机的IP地址及端口号。";
                return;
            }
            if (string.IsNullOrEmpty(start) || string.IsNullOrEmpty(end))
            {
                lblError.Text = "请填写查询开始和结束日期";
                return;
            }

            try
            {
                HttpRequest request = new HttpRequest();

                string reqtXml = request.GetEnquiryRequestStr(start, end);
                string respXml = request.SendRequest(reqtXml, ipAddr, port);
                string errorMsg = "";
                List<PaymentReqt> payList = request.CheckEnquiryResult(respXml, out errorMsg);

                MainController ctrl = new MainController();

                ctrl.InsertXmlLog(reqtXml, "GetPaymentInfo_REQUEST");
                ctrl.InsertXmlLog(respXml, "GetPaymentInfo_RESPONSE");

                string batchId = payList[0].batch_id;
                ctrl.UpdateBatchAndPaymentStatus(payList, batchId, Constant.ConstantInfo.BTSTAT_05);

                if (string.IsNullOrEmpty(errorMsg))
                {
                    lblError.Text = lblError.Text = ComUtil.GetGlobalResource(Constant.ResourcesKey.KEY_IMSG0001); ;
                }
                else
                {
                    lblError.Text = errorMsg;
                }

                SearchBatch();
            }
            catch (Exception ex)
            {
                LogUtil.WriteErrorMessage(ex);
                lblError.Text = ex.Message;
            }
        }

        private string PostHttpRest(List<PaymentReqt> list, string ip, string port)
        {
            string result = "";
            try
            {
                HttpRequest request = new HttpRequest();
                string reqtXml = request.GetPayRequestStr(list);
                string respXml = request.SendRequest(reqtXml, ip, port);
                result = request.CheckPayResult(respXml, list);

                MainController ctrl = new MainController();

                ctrl.InsertXmlLog(reqtXml, "DCPAYMNT_REQUEST");
                ctrl.InsertXmlLog(respXml, "DCPAYMNT_RESPONSE");

                string batchId = list[0].batch_id;
                ctrl.UpdateBatchAndPaymentStatus(list, batchId, Constant.ConstantInfo.BTSTAT_04);
            }
            catch (Exception ex)
            {
                LogUtil.WriteErrorMessage(ex);
                result = ex.Message;
            }

            return result;
        }

        private void SetSearchCondition(BatchBean infoBean)
        {
            infoBean.StatusId = ddlStatusType.SelectedValue;

            infoBean.PageIndex = 1;
            infoBean.PageSize = SystemInfoController.GetControlInfo(Constant.SystemControlInfo.KEY_MAX_PAGE_CNT).ToInt();
        }

        private void SetConditonToPage(BatchBean infoBean)
        {
            if (infoBean == null)
            {
                return;
            }

            if (!string.IsNullOrEmpty(infoBean.StatusId))
            {
                ddlStatusType.SelectedValue = infoBean.StatusId;
            }
        }

        private void DisplayInfo(BatchBean infoBean)
        {
            MainController ctrl = new MainController();
            DataTable dt = ctrl.GetBatchList(infoBean);
            BindGridviewInfo(dt);
        }

        private void BindGridviewInfo(DataTable dt)
        {
            bool hasData = dt.Rows.Count != 0;

            gvBatchInfo.DataSource = hasData ? dt : CreateBlankTable();
            gvBatchInfo.DataBind();

            if (!hasData)
            {
                gvBatchInfo.Rows[0].Cells[0].ColumnSpan = gvBatchInfo.Columns.Count;
                gvBatchInfo.Rows[0].FindControl("lblEmptyData").Visible = true;
                ((Label)gvBatchInfo.Rows[0].FindControl("lblEmptyData")).Text =
                    ComUtil.GetGlobalResource(Constant.ResourcesKey.INFO_SEARCH_NODATA);
                gvBatchInfo.Rows[0].FindControl("lkBatchId").Visible = false;
                for (int i = 1; i < gvBatchInfo.Columns.Count; i++)
                {
                    gvBatchInfo.Rows[0].Cells[i].Visible = false;
                }
            }
        }

        private DataTable CreateBlankTable()
        {
            DataTable blankTable = new DataTable();
            blankTable.Columns.Add(new DataColumn("batch_id"));
            blankTable.Columns.Add(new DataColumn("pay_cnt"));
            blankTable.Columns.Add(new DataColumn("ttl_amt"));
            blankTable.Columns.Add(new DataColumn("status_id"));
            blankTable.Columns.Add(new DataColumn("status"));
            blankTable.Columns.Add(new DataColumn("maker_id"));
            blankTable.Columns.Add(new DataColumn("maker_time"));
            blankTable.Columns.Add(new DataColumn("appr1_id"));
            blankTable.Columns.Add(new DataColumn("appr1_time"));
            blankTable.Columns.Add(new DataColumn("appr2_id"));
            blankTable.Columns.Add(new DataColumn("appr2_time"));
            blankTable.Columns.Add(new DataColumn("rejc_id"));
            blankTable.Columns.Add(new DataColumn("rejc_time"));
            blankTable.Columns.Add(new DataColumn("update_time"));
            blankTable.Rows.Add(blankTable.NewRow());
            return blankTable;
        }

        #endregion

    }
}