using CMBPayment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;

namespace Common
{
    public class BasePage : System.Web.UI.Page
    {
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

        /// <summary>
        /// 画面ID
        /// </summary>
        public string PageID
        {
            get;
            set;
        }

        public CommonController cc = new CommonController();

        protected override void OnInit(EventArgs e)
        {

            base.OnInit(e);

            this.Response.Buffer = true;
            this.Response.Expires = -1;
            this.Response.ExpiresAbsolute = DateTime.Now.AddSeconds(-1.0);

            // 除外画面
            if (IsExtraURL() == false)
            {
                if (CheckSessionTimeOut())
                {
                    string strUrl = ComUtil.GetPageURL(Constant.PagesURL.URL_SESSION_TIME_OUT);
                    this.Response.Redirect(strUrl);
                }
            }
        }

        protected bool CheckSessionTimeOut()
        {
            if (this.Session[Constant.SessionKey.LOGIN_USER_INFO] == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        protected bool IsExtraURL()
        {
            string currentUrl = this.Request.Url.ToString().ToLower();
            //登录URL
            //SESSION超时
            string[] extUrls = new string[] { 
                ComUtil.GetPageURL(Constant.PagesURL.URL_LOGIN).ToLower(),
                ComUtil.GetPageURL(Constant.PagesURL.URL_SESSION_TIME_OUT).ToLower(),
                ComUtil.GetPageURL(Constant.PagesURL.URL_ERROR_PAGE).ToLower(),
            };
            for (int i = 0; i < extUrls.Length; i++)
            {
                if (currentUrl.IndexOf(extUrls[i]) > 0)
                {
                    return true;
                }
            }
            return false;
        }

        public void MoveToErrorPage(Exception exception, string inProcessName)
        {
            string errorUrl = ComUtil.GetPageURL(Constant.PagesURL.URL_ERROR_PAGE);
            StringBuilder builder = new StringBuilder();

            builder.Append("?").Append(Constant.PagesURL.PARAM_PAGEID).Append("=").Append(Server.UrlEncode(PageID));
            builder.Append("&").Append(Constant.PagesURL.PARAM_PROCCESS_NAME).Append("=").Append(Server.UrlEncode(inProcessName));
            this.Response.Redirect(errorUrl + builder.ToString());
        }

        /// <summary>
        /// 分页设定
        /// </summary>
        public void SetChangePage(BasePagingBean pagingBean, int currentPageIndex, GPG0000 GPG00001)
        {
            GPG00001.PageCount = pagingBean.PageCount;
            GPG00001.ItemsCount = pagingBean.ItemsCount;
            GPG00001.Refresh();
            GPG00001.TrySetCurrentPageIndex(currentPageIndex);
        }

        public void BindDropDownListData(DropDownList inDDL, List<NameValueBean> inData, string inSelectValue, bool inIsAddDef)
        {

            inDDL.DataSource = null;
            inDDL.Items.Clear();
            int selectedIndex = 0;

            for (int i = 0; i < inData.Count; i++)
            {
                inDDL.Items.Add(new ListItem(inData[i].ItemName, inData[i].ItemValue));
                if (inData[i].ItemValue == inSelectValue)
                {
                    selectedIndex = i;
                }
                //扩展属性
                if (inData[i].Attributes != null)
                {
                    foreach (string eachItem in inData[i].Attributes.Keys)
                    {
                        inDDL.Items[i].Attributes[eachItem] = inData[i].Attributes[eachItem];
                    }
                }
            }
            //默认值：第一行选择
            if (inDDL.Items.Count > 0)
            {
                inDDL.SelectedIndex = selectedIndex;
            }
            if (inIsAddDef && inDDL.Items.Count > 0)
            {
                inDDL.Items[0].Attributes["class"] = "def";
            }
        }
    }
}