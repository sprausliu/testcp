using Common;
using System;
using System.ComponentModel;
using System.Web.UI;

namespace CMBPayment
{

    [DefaultEvent("PageChanged")]
    public partial class GPG0000 : BaseUserControl
    {

        #region "变量和定数"
        const string javascript = @"$(function() {
                        $("".GPG0000_LNK"").each(
                            function(i) {
                                var state = $("".GPG0000_LNK"").eq(i).attr(""disabled"");
                                if (state == ""disabled"") {
                                    $("".GPG0000_SPAN"").eq(i).addClass(""disabled"");
                                } else {
                                    $("".GPG0000_SPAN"").eq(i).removeClass(""disabled"");
                                }
                            }
                        );
                    });";
        #endregion

        //分页的按钮的事件的作成
        public delegate void PageChangedHander(object sender);
        public event PageChangedHander PageChanged;


        #region "属性"
        private int pageSize = 1;
        public int PageSize
        {
            get
            {
                return pageSize;
            }
            set
            {
                pageSize = value;
            }
        }

        /// <summary>
        /// 选择画面Index 从１开始
        /// </summary>
        public int PageIndex
        {
            get
            {
                return TryGetCurrentPageIndex();
            }
        }

        private int pageCount = 1;
        /// <summary>
        /// 画面数
        /// </summary>
        public int PageCount
        {
            get
            {
                return pageCount;
            }
            set
            {
                if (value <= 0)
                {
                    pageCount = 1;
                }
                else
                {
                    pageCount = value;
                }
            }
        }

        private int itemsCount = 0;
        /// <summary>
        /// 数据总件数
        /// </summary>
        public int ItemsCount
        {
            get
            {
                return itemsCount;
            }
            set
            {
                itemsCount = value;
            }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            Control updatepanelCon = FindUpdatePanel(this.Parent);
            if (updatepanelCon == null)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(),
                    System.Guid.NewGuid().ToString(),
                    javascript,
                    true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(updatepanelCon, this.GetType(),
                    System.Guid.NewGuid().ToString(),
                    javascript,
                    true);
            }
        }

        /// <summary>
        /// 最初画面
        /// </summary>
        protected void lnkStart_Click(object sender, EventArgs e)
        {
            int selectedIndex = 1;
            this.TrySetCurrentPageIndex(selectedIndex);
            SetLinkEnable();

            //分页的按钮的事件的作成
            if (PageChanged != null)
            {
                PageChanged(sender);
            }
        }

        /// <summary>
        /// 前画面
        /// </summary>
        protected void lnkPrev_Click(object sender, EventArgs e)
        {
            int selectedIndex = this.TryGetCurrentPageIndex();
            this.TrySetCurrentPageIndex(selectedIndex - 1);
            SetLinkEnable();
            //分页的按钮的事件的作成
            if (PageChanged != null)
            {
                PageChanged(sender);
            }
        }

        /// <summary>
        /// 次画面
        /// </summary>
        protected void lnlNext_Click(object sender, EventArgs e)
        {
            int selectedIndex = this.TryGetCurrentPageIndex();
            this.TrySetCurrentPageIndex(selectedIndex + 1);
            SetLinkEnable();
            //分页的按钮的事件的作成
            if (PageChanged != null)
            {
                PageChanged(sender);
            }
        }

        /// <summary>
        /// 最終画面
        /// </summary>
        protected void lnkEnd_Click(object sender, EventArgs e)
        {
            int selectedIndex = this.ddlPages.Items.Count;
            this.TrySetCurrentPageIndex(selectedIndex + 1);
            SetLinkEnable();
            //分页的按钮的事件的作成
            if (PageChanged != null)
            {
                PageChanged(sender);
            }
        }

        protected void ddlPages_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetLinkEnable();
            //分页的按钮的事件的作成
            if (PageChanged != null)
            {
                PageChanged(sender);
            }
        }

        #region "方法"
        /// <summary>
        /// 親的控件的取得
        /// </summary>
        /// <param name="inParent">親Controller</param>
        public Control FindUpdatePanel(Control inParent)
        {
            Control returnCon = null;
            if (inParent == null)
            {
                return null;
            }
            else if (inParent.GetType() == typeof(UpdatePanel))
            {
                returnCon = inParent;
            }
            else
            {
                returnCon = FindUpdatePanel(inParent.Parent);
            }
            return returnCon;
        }

        /// <summary>
        /// 当前控件的再描画
        /// 初期化
        /// </summary>
        public void Refresh()
        {
            this.ddlPages.Items.Clear();
            int showPageCnt;
            int maxPageCnt = SystemInfoController.GetControlInfo(Constant.SystemControlInfo.KEY_MAX_PAGE_CNT).ToInt();
            if (this.PageCount > maxPageCnt)
            {
                showPageCnt = maxPageCnt;
            }
            else
            {
                showPageCnt = this.PageCount;
            }

            //画面的追加
            for (int i = 0; i < showPageCnt; i++)
            {
                this.ddlPages.Items.Add((i + 1).ToString());
            }
            this.lblSumCNT.Text = this.ItemsCount.ToString(Constant.FormatInfo.FORMAT_NUMBER_COMMA);
            this.lblPageCount.Text = this.PageCount.ToString(Constant.FormatInfo.FORMAT_NUMBER_COMMA);
            //默认选择
            if (this.ddlPages.Items.Count > 0)
            {
                this.ddlPages.SelectedIndex = 0;
            }
            SetLinkEnable();
        }

        public int TryGetCurrentPageIndex()
        {
            if (this.ddlPages.Items.Count == 0)
            {
                return 1;
            }
            else
            {
                return this.ddlPages.SelectedIndex + 1;
            }
        }

        public bool TrySetCurrentPageIndex(int inCurrentPage)
        {
            bool returnValue = false;
            //参数
            if (inCurrentPage <= 0)
            {
                inCurrentPage = 1;
            }
            if (this.ddlPages.Items.Count == 0)
            {
                returnValue = false;
            }
            else
            {
                if (this.ddlPages.Items.Count > inCurrentPage)
                {
                    this.ddlPages.SelectedIndex = inCurrentPage - 1;
                }
                else
                {
                    this.ddlPages.SelectedIndex = this.ddlPages.Items.Count - 1;
                }
                returnValue = true;
            }
            SetLinkEnable();
            return returnValue;
        }

        public int TryGetItemsCount()
        {
            string strItemsCNT = this.lblSumCNT.Text;
            if (string.IsNullOrEmpty(strItemsCNT))
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(strItemsCNT.Replace(",", ""));
            }
        }

        /// <summary>
        /// 画面数的取得
        /// </summary>
        public int TryGetPageCount()
        {
            return this.ddlPages.Items.Count;
        }

        private void SetLinkEnable()
        {
            if (this.ddlPages.Items.Count > 0)
            {
                //按钮的活性设定
                //最初画面的場合
                if (this.ddlPages.Items[0].Selected)
                {
                    this.lnkStart.Enabled = false;
                    this.lnkPrev.Enabled = false;
                }
                else
                {
                    this.lnkStart.Enabled = true;
                    this.lnkPrev.Enabled = true;
                }
                //最終画面的場合
                if (this.ddlPages.Items[this.ddlPages.Items.Count - 1].Selected)
                {
                    this.lnlNext.Enabled = false;
                    this.lnkEnd.Enabled = false;
                }
                else
                {
                    this.lnlNext.Enabled = true;
                    this.lnkEnd.Enabled = true;
                }
                this.ddlPages.Enabled = true;
            }
            else
            {
                this.lnkStart.Enabled = false;
                this.lnkPrev.Enabled = false;
                this.lnlNext.Enabled = false;
                this.lnkEnd.Enabled = false;
                this.ddlPages.Enabled = false;
            }
        }
        #endregion



    }
}