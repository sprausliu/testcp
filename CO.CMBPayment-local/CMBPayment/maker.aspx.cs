using Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CMBPayment
{
    public partial class maker : BasePage
    {
        #region "属性"
        public BatchBean CurrentBean1
        {
            get
            {
                if (Session[Constant.SessionKey.KEY_Maker_SEARCH_KEYWORD1] != null)
                {
                    return (BatchBean)Session[Constant.SessionKey.KEY_Maker_SEARCH_KEYWORD1];
                }
                else
                {
                    return new BatchBean();
                }
            }
            set
            {
                Session[Constant.SessionKey.KEY_Maker_SEARCH_KEYWORD1] = value;
            }
        }
        public BatchBean CurrentBean2
        {
            get
            {
                if (Session[Constant.SessionKey.KEY_Maker_SEARCH_KEYWORD2] != null)
                {
                    return (BatchBean)Session[Constant.SessionKey.KEY_Maker_SEARCH_KEYWORD2];
                }
                else
                {
                    return new BatchBean();
                }
            }
            set
            {
                Session[Constant.SessionKey.KEY_Maker_SEARCH_KEYWORD2] = value;
            }
        }
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
            try
            {

                if (!IsPostBack)
                {
                    UserInfoBean user = base.LoginUser;

                    InitDisplayInfo();
                }

                lblError.Text = "";

                // 添加分页事件
                this.GPG00001.PageChanged += new GPG0000.PageChangedHander(GPG00001_PageChanged);
                this.GPG00002.PageChanged += new GPG0000.PageChangedHander(GPG00002_PageChanged);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnManualUpload_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtil.WriteDebugStartMessage();

                string savePath = Path.Combine(Server.MapPath(""), ConfigurationManager.AppSettings["upload_path"]);
                string backPath = Path.Combine(Server.MapPath(""), ConfigurationManager.AppSettings["backup_path"]);

                if (!System.IO.Directory.Exists(savePath))
                {
                    System.IO.Directory.CreateDirectory(savePath);
                }
                if (!System.IO.Directory.Exists(backPath))
                {
                    System.IO.Directory.CreateDirectory(savePath);
                }

                if (!this.fileLoad.HasFile)
                {

                    this.lblError.Text = "请选择上传文件！";
                    return;
                }

                string fileName = fileLoad.FileName;
                string fullPath = Path.Combine(savePath, fileName);
                string fullBackPath = Path.Combine(backPath, fileName);
                //this.FILE_PATH = fullPath;
                fileLoad.SaveAs(fullPath);

                UploadDataFile(fullPath);

                // backup file
                FileInfo file = new FileInfo(fullPath);
                ComUtil.MoveServerFile(fullPath, fullBackPath, false);

                LogUtil.WriteDebugEndMessage();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnAutoUpload_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtil.WriteDebugStartMessage();

                string savePath = Path.Combine(Server.MapPath(""), ConfigurationManager.AppSettings["upload_path"]);
                string backPath = Path.Combine(Server.MapPath(""), ConfigurationManager.AppSettings["backup_path"]);

                if (!System.IO.Directory.Exists(savePath))
                {
                    System.IO.Directory.CreateDirectory(savePath);
                }
                if (!System.IO.Directory.Exists(backPath))
                {
                    System.IO.Directory.CreateDirectory(savePath);
                }

                string fullPath = GetLatestFiles(savePath);

                if (string.IsNullOrEmpty(fullPath))
                {
                    lblError.Text = "文件夹下无文件。";
                    return;
                }

                UploadDataFile(fullPath);

                // backup file
                FileInfo file = new FileInfo(fullPath);
                string fullBackPath = Path.Combine(backPath, file.Name);
                ComUtil.MoveServerFile(fullPath, fullBackPath, false);

                LogUtil.WriteDebugEndMessage();
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void GPG00001_PageChanged(object sender)
        {
            try
            {
                if (this.CurrentBean1 != null)
                {
                    BatchBean infoBean = this.CurrentBean1;
                    int currentIndex = this.GPG00001.TryGetCurrentPageIndex();
                    infoBean.PageIndex = currentIndex;

                    MainController ctrl = new MainController();
                    DataTable dt = ctrl.GetBatchList(infoBean);

                    // GridView中设定
                    this.gvSucBatchInfo.DataSource = dt;
                    // DataSource中设定的值反映
                    this.gvSucBatchInfo.DataBind();

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

        protected void GPG00002_PageChanged(object sender)
        {
            try
            {
                if (this.CurrentBean2 != null)
                {
                    BatchBean infoBean = this.CurrentBean2;
                    int currentIndex = this.GPG00002.TryGetCurrentPageIndex();
                    infoBean.PageIndex = currentIndex;

                    MainController ctrl = new MainController();
                    DataTable dt = ctrl.GetBatchList(infoBean);

                    // GridView中设定
                    this.gvSucBatchInfo.DataSource = dt;
                    // DataSource中设定的值反映
                    this.gvSucBatchInfo.DataBind();

                    // 分页的设定
                    base.SetChangePage(infoBean, currentIndex, this.GPG00002);

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

        protected void gvSucBatchInfo_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("id"))
            {
                // goto payment page
                LinkButton lkBatchId = (LinkButton)e.CommandSource;
                BatchBean infoBean = this.CurrentBean;
                infoBean.BatchId = lkBatchId.Text;
                infoBean.IsEditMode = true;
                Session[Constant.SessionKey.KEY_Payment_SEARCH_KEYWORD] = null;
                this.CurrentBean = infoBean;
                Response.Redirect(ComUtil.GetPageURL(Constant.PagesURL.URL_PAYMENT));
            }
        }

        protected void gvSucBatchInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void gvFailBatchInfo_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            // goto payment page
            LinkButton lkBatchId = (LinkButton)e.CommandSource;
            BatchBean infoBean = this.CurrentBean;
            infoBean.BatchId = lkBatchId.Text;
            infoBean.IsEditMode = true;
            Session[Constant.SessionKey.KEY_Payment_SEARCH_KEYWORD] = null;
            this.CurrentBean = infoBean;
            Response.Redirect(ComUtil.GetPageURL(Constant.PagesURL.URL_PAYMENT));
        }

        protected void gvFailBatchInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
        #endregion

        #region "方法"

        private string GetLatestFiles(string Path)
        {
            try
            {
                var query = (from f in Directory.GetFiles(Path)
                             let fi = new FileInfo(f)
                             orderby fi.CreationTime descending
                             select fi.FullName).First();

                if (query != null)
                {
                    return query.ToString();
                }
                else
                {
                    return "";
                }
            }
            catch
            {
                return "";
            }
            
        }

        private void UploadDataFile(string filePath)
        {
            ConvertFileController conv = new ConvertFileController();

            List<PaymentReqt> payList = conv.ConvertDecryptedFile(filePath);
            List<PaymentReqt> paySucList = conv.GetSucPaymentList(payList);
            List<PaymentReqt> payFailList = conv.GetFailPaymentList(payList);

            MainController ctrl = new MainController();
            if (paySucList.Count > 0)
            {
                ctrl.InsertBatchAndPaymentInfo(paySucList, Constant.ConstantInfo.BTSTAT_01);
                lblError.Text = ComUtil.GetGlobalResource(Constant.ResourcesKey.KEY_IMSG0001);
            }
            if (payFailList.Count > 0)
            {
                ctrl.InsertBatchAndPaymentInfo(payFailList, Constant.ConstantInfo.BTSTAT_99);
                lblError.Text = ComUtil.GetGlobalResource(Constant.ResourcesKey.KEY_IMSG0001);
            }

            SearchBatch();
        }

        private void InitDisplayInfo()
        {
            BatchBean infoBean1 = this.CurrentBean1;
            if (infoBean1 == null || infoBean1.PageSize == 0 || !infoBean1.IsSearched)
            {
                SetSearchCondition(infoBean1);
                DisplayInfo1(infoBean1);

                // 分页设定
                base.SetChangePage(infoBean1, 1, this.GPG00001);
                infoBean1.IsSearched = true;
                this.CurrentBean1 = infoBean1;
            }
            else
            {
                // 画面条件重新设定
                SetConditonToPage(infoBean1);
                // 画面再检索
                DisplayInfo1(infoBean1);
                // 分页设定
                base.SetChangePage(infoBean1, infoBean1.PageIndex, this.GPG00001);
            }

            BatchBean infoBean2 = this.CurrentBean2;
            if (infoBean2 == null || infoBean2.PageSize == 0 || !infoBean2.IsSearched)
            {
                SetSearchCondition(infoBean2);
                DisplayInfo2(infoBean2);

                // 分页设定
                base.SetChangePage(infoBean2, 1, this.GPG00002);
                infoBean2.IsSearched = true;
                this.CurrentBean2 = infoBean2;
            }
            else
            {
                // 画面条件重新设定
                SetConditonToPage(infoBean2);
                // 画面再检索
                DisplayInfo2(infoBean2);
                // 分页设定
                base.SetChangePage(infoBean2, infoBean2.PageIndex, this.GPG00002);
            }

        }

        private void SearchBatch()
        {
            BatchBean infoBean1 = this.CurrentBean1;
            // 画面再检索
            DisplayInfo1(infoBean1);
            // 分页设定
            base.SetChangePage(infoBean1, infoBean1.PageIndex, this.GPG00001);

            BatchBean infoBean2 = this.CurrentBean2;
            // 画面再检索
            DisplayInfo2(infoBean2);
            // 分页设定
            base.SetChangePage(infoBean2, infoBean2.PageIndex, this.GPG00002);
        }

        private void SetSearchCondition(BatchBean infoBean)
        {
            infoBean.PageIndex = 1;
            infoBean.PageSize = SystemInfoController.GetControlInfo(Constant.SystemControlInfo.KEY_MAX_PAGE_CNT).ToInt();
        }

        private void SetConditonToPage(BatchBean infoBean)
        {
            if (infoBean == null)
            {
                return;
            }
        }

        private void DisplayInfo1(BatchBean infoBean)
        {
            MainController ctrl = new MainController();
            DataTable dt = ctrl.GetSucBatchList(infoBean);
            BindGridviewInfo1(dt);
        }

        private void DisplayInfo2(BatchBean infoBean)
        {
            MainController ctrl = new MainController();
            DataTable dt = ctrl.GetFailBatchList(infoBean);
            BindGridviewInfo2(dt);
        }

        private void BindGridviewInfo1(DataTable dt)
        {
            bool hasData = dt.Rows.Count != 0;

            gvSucBatchInfo.DataSource = hasData ? dt : CreateBlankTable();
            gvSucBatchInfo.DataBind();

            if (!hasData)
            {
                gvSucBatchInfo.Rows[0].Cells[0].ColumnSpan = gvSucBatchInfo.Columns.Count;
                gvSucBatchInfo.Rows[0].FindControl("lblEmptyData").Visible = true;
                ((Label)gvSucBatchInfo.Rows[0].FindControl("lblEmptyData")).Text =
                    ComUtil.GetGlobalResource(Constant.ResourcesKey.INFO_SEARCH_NODATA);
                gvSucBatchInfo.Rows[0].FindControl("lkBatchId").Visible = false;
                for (int i = 1; i < gvSucBatchInfo.Columns.Count; i++)
                {
                    gvSucBatchInfo.Rows[0].Cells[i].Visible = false;
                }
            }
        }

        private void BindGridviewInfo2(DataTable dt)
        {
            bool hasData = dt.Rows.Count != 0;

            gvFailBatchInfo.DataSource = hasData ? dt : CreateBlankTable();
            gvFailBatchInfo.DataBind();

            if (!hasData)
            {
                gvFailBatchInfo.Rows[0].Cells[0].ColumnSpan = gvFailBatchInfo.Columns.Count;
                gvFailBatchInfo.Rows[0].FindControl("lblEmptyData").Visible = true;
                ((Label)gvFailBatchInfo.Rows[0].FindControl("lblEmptyData")).Text =
                    ComUtil.GetGlobalResource(Constant.ResourcesKey.INFO_SEARCH_NODATA);
                gvFailBatchInfo.Rows[0].FindControl("lkBatchId").Visible = false;
                for (int i = 1; i < gvFailBatchInfo.Columns.Count; i++)
                {
                    gvFailBatchInfo.Rows[0].Cells[i].Visible = false;
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