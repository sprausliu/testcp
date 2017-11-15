using CMBPayment;
using CMBPayment.Common;
using Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CMBPayment
{
    /// <summary>
    /// 共通用Contrller
    /// </summary>
    public class CommonController : BaseController
    {

        public CommonController()
        {
        }

        #region "方法"

        public UserInfoBean InitUserInfo(string inUserID)
        {
            UserInfoBean returnUserInfo = null;

            try
            {
                // DEBUG LOG START
                LogUtil.WriteDebugStartMessage();

                DataTable returnDt = new DataTable();
                DataTable returnDtRoles = new DataTable();
                SqlConnection cnn = this.GetConnection();
                CommonDataAccess lda = new CommonDataAccess(cnn);
                this.OpenConnection();

                //基本信息的取得
                returnDt = lda.GetUserBaseInfo(inUserID);
                if (returnDt.Rows.Count == 0)
                {
                    return returnUserInfo;
                }
                returnUserInfo = new UserInfoBean();
                returnUserInfo.UserId = returnDt.Rows[0]["USER_ID"].ToString();
                returnUserInfo.RoleId = returnDt.Rows[0]["ROLE_ID"].ToString();
                returnUserInfo.RoleName = returnDt.Rows[0]["ROLE_NM"].ToString();
                returnUserInfo.CMBAcc = returnDt.Rows[0]["cmb_account"].ToString();
                returnUserInfo.AmtLimit = returnDt.Rows[0]["amount_limit"].ToString();

                // DEBUG LOG END
                LogUtil.WriteDebugEndMessage();

            }
            catch (DataAccessException daex)
            {
                throw daex;
            }
            catch (Exception ex)
            {
                LogicException le = new LogicException(ex);
                throw le;
            }
            finally
            {
                this.CloseConnection();
            }

            return returnUserInfo;
        }

        #region "系统时间取得"
        /// <summary>
        /// 系统时间取得
        /// </summary>
        /// <param></param>
        /// <returns>returnDate</returns>
        public DateTime GetSystemDate()
        {
            DateTime returnDate;

            try
            {
                // DEBUG LOG START
                LogUtil.WriteDebugStartMessage();

                SqlConnection cnn = this.GetConnection();
                CommonDataAccess cda = new CommonDataAccess(cnn);
                this.OpenConnection();

                DataTable sysdateDT = cda.GetSystemDate();
                returnDate = (DateTime)sysdateDT.Rows[0]["SYSDATE"];

                LogUtil.WriteDebugMessage(returnDate.ToString(Constant.FormatInfo.FORMAT_DATE_TIME_M));
                // DEBUG LOG END
                LogUtil.WriteDebugEndMessage();
            }
            catch (DataAccessException daex)
            {
                throw daex;
            }
            catch (Exception ex)
            {
                LogicException le = new LogicException(ex);
                throw le;
            }
            finally
            {
                this.CloseConnection();
            }

            return returnDate;

        }

        #endregion

        #region "系统控制信息取得"
        /// <summary>
        /// 系统控制信息取得
        /// </summary>
        /// <param></param>
        /// <returns>returnDate</returns>
        public Dictionary<string, NameValueBean> GetSystemControlInfo()
        {
            Dictionary<string, NameValueBean> returnResult = new Dictionary<string, NameValueBean>();

            try
            {
                // DEBUG LOG START
                LogUtil.WriteDebugStartMessage();

                SqlConnection cnn = this.GetConnection();
                CommonDataAccess cda = new CommonDataAccess(cnn);
                this.OpenConnection();
                DataTable returnDT = cda.GetSystemConfigInfo();

                for (int i = 0; i < returnDT.Rows.Count; i++)
                {
                    //信息设定
                    returnResult.Add(returnDT.Rows[i]["CONF_KEY"].ToString(),
                        new NameValueBean(returnDT.Rows[i]["CONF_KEY"].ToString(), returnDT.Rows[i]["CONF_VALUE"].ToString()));
                }

                // DEBUG LOG END
                LogUtil.WriteDebugEndMessage();
            }
            catch (DataAccessException daex)
            {
                throw daex;
            }
            catch (Exception ex)
            {
                LogicException le = new LogicException(ex);
                throw le;
            }
            finally
            {
                this.CloseConnection();
            }

            return returnResult;

        }

        /// <summary>
        /// 系统控制信息取得
        /// </summary>
        /// <param name="inKey">指定Config的KEY</param>
        /// <returns>returnDate</returns>
        public NameValueBean GetSystemControlInfo(string inKey)
        {
            NameValueBean returnResult = null;

            try
            {
                // DEBUG LOG START
                LogUtil.WriteDebugStartMessage();

                SqlConnection cnn = this.GetConnection();
                CommonDataAccess cda = new CommonDataAccess(cnn);
                this.OpenConnection();
                DataTable returnDT = cda.GetSystemConfigInfo(inKey);

                for (int i = 0; i < returnDT.Rows.Count; i++)
                {
                    //信息设定
                    returnResult = new NameValueBean(returnDT.Rows[i]["CONF_KEY"].ToString(), returnDT.Rows[i]["CONF_VALUE"].ToString());
                }

                // DEBUG LOG END
                LogUtil.WriteDebugEndMessage();
            }
            catch (DataAccessException daex)
            {
                throw daex;
            }
            catch (Exception ex)
            {
                LogicException le = new LogicException(ex);
                throw le;
            }
            finally
            {
                this.CloseConnection();
            }

            return returnResult;

        }
        #endregion

        #region "编码MASTER取得"
        /// <summary>
        /// 编码MASTER取得
        /// 类别根据编码列表作成
        /// </summary>
        /// <param name="inCatgory">类别</param>
        /// <returns>List<NameValueBean></returns>
        public List<NameValueBean> GetCodeNameList(string inCatgory)
        {
            List<NameValueBean> nvlbList = new List<NameValueBean>();

            try
            {
                // DEBUG LOG START
                LogUtil.WriteDebugStartMessage();

                SqlConnection cnn = this.GetConnection();
                CommonDataAccess cda = new CommonDataAccess(cnn);
                this.OpenConnection();

                DataTable infoDT = cda.GetCodeMasterInfo(inCatgory, "");
                if (infoDT.Rows.Count > 0)
                {
                    for (int i = 0; i < infoDT.Rows.Count; i++)
                    {
                        NameValueBean nvb = new NameValueBean(infoDT.Rows[i]["CD_NM"].ToString(), infoDT.Rows[i]["CD"].ToString());
                        nvlbList.Add(nvb);
                    }
                }
                // DEBUG LOG END
                LogUtil.WriteDebugEndMessage();
            }
            catch (DataAccessException daex)
            {
                throw daex;
            }
            catch (Exception ex)
            {
                LogicException le = new LogicException(ex);
                throw le;
            }
            finally
            {
                this.CloseConnection();
            }
            return nvlbList;
        }

        /// <summary>
        /// 编码MASTER取得
        /// 类别和编码根据编码和名称取得
        /// </summary>
        /// <param name="inCatgory">类别</param>
        /// <returns>List<NameValueBean></returns>
        public NameValueBean GetCodeName(string inCatgory, string inCode)
        {
            NameValueBean returnItem = null;

            try
            {
                // DEBUG LOG START
                LogUtil.WriteDebugStartMessage();

                SqlConnection cnn = this.GetConnection();
                CommonDataAccess cda = new CommonDataAccess(cnn);
                this.OpenConnection();

                DataTable infoDT = cda.GetCodeMasterInfo(inCatgory, inCode);
                if (infoDT.Rows.Count > 0)
                {
                    returnItem = new NameValueBean(infoDT.Rows[0]["CD_NM"].ToString(), infoDT.Rows[0]["CD"].ToString());
                }
                // DEBUG LOG END
                LogUtil.WriteDebugEndMessage();
            }
            catch (DataAccessException daex)
            {
                throw daex;
            }
            catch (Exception ex)
            {
                LogicException le = new LogicException(ex);
                throw le;
            }
            finally
            {
                this.CloseConnection();
            }

            return returnItem;

        }
        
        #endregion




        #region "省 自治区 市 列表取得"
        /// <summary>
        /// 省自治区市列表取得
        /// </summary>
        /// <param ></param>
        /// <returns><NameValueBean></returns>
        public List<NameValueBean> GetAllProvList()
        {
            List<NameValueBean> nvlbList = new List<NameValueBean>();

            try
            {
                // DEBUG LOG START
                LogUtil.WriteDebugStartMessage();

                SqlConnection cnn = this.GetConnection();
                CommonDataAccess dac = new CommonDataAccess(cnn);
                this.OpenConnection();

                DataTable infoDT = dac.GetAllProvList();
                if (infoDT.Rows.Count > 0)
                {
                    for (int i = 0; i < infoDT.Rows.Count; i++)
                    {
                        NameValueBean nvb = new NameValueBean(infoDT.Rows[i]["PRCT_NM"].ToString(), infoDT.Rows[i]["PRCT_ID"].ToString());
                        nvlbList.Add(nvb);
                    }
                }

                // DEBUG LOG END
                LogUtil.WriteDebugEndMessage();
            }
            catch (DataAccessException daex)
            {
                throw daex;
            }
            catch (Exception ex)
            {
                LogicException le = new LogicException(ex);
                throw le;
            }
            finally
            {
                this.CloseConnection();
            }

            return nvlbList;
        }
        #endregion

        #region "市 区 取得"
        ///  市 区 取得
        /// </summary>
        /// <param name="inProvId"></param>
        /// <returns></returns>
        public List<NameValueBean> GetCityListByProvId(string inProvId)
        {
            List<NameValueBean> nvlbList = new List<NameValueBean>();

            try
            {
                // DEBUG LOG START
                LogUtil.WriteDebugStartMessage();

                SqlConnection cnn = this.GetConnection();
                CommonDataAccess cda = new CommonDataAccess(cnn);
                this.OpenConnection();

                DataTable infoDT = cda.GetCityListByProvId(inProvId);
                if (infoDT.Rows.Count > 0)
                {
                    for (int i = 0; i < infoDT.Rows.Count; i++)
                    {
                        NameValueBean nvb = new NameValueBean(infoDT.Rows[i]["PRCT_NM"].ToString(), infoDT.Rows[i]["PRCT_ID"].ToString());
                        nvlbList.Add(nvb);
                    }
                }

                // DEBUG LOG END
                LogUtil.WriteDebugEndMessage();
            }
            catch (DataAccessException daex)
            {
                throw daex;
            }
            catch (Exception ex)
            {
                LogicException le = new LogicException(ex);
                throw le;
            }
            finally
            {
                this.CloseConnection();
            }

            return nvlbList;
        }
        #endregion

        #region "根据区域编码取得区域名称"
        /// <summary>
        /// 根据区域编码取得区域名称
        /// </summary>
        /// <param name="inPrctId"></param>
        /// <returns></returns>
        public NameValueBean GetPrctNmByPrctId(string inPrctId)
        {
            NameValueBean retBean = null;

            try
            {
                // DEBUG LOG START
                LogUtil.WriteDebugStartMessage();

                SqlConnection cnn = this.GetConnection();
                CommonDataAccess cda = new CommonDataAccess(cnn);
                this.OpenConnection();

                DataTable infoDT = cda.GetPrctNmByPrctId(inPrctId);
                if (infoDT.Rows.Count > 0)
                {
                    retBean = new NameValueBean(infoDT.Rows[0]["PRCT_NM"].ToString(), infoDT.Rows[0]["PRCT_ID"].ToString());
                }

                // DEBUG LOG END
                LogUtil.WriteDebugEndMessage();
            }
            catch (DataAccessException daex)
            {
                throw daex;
            }
            catch (Exception ex)
            {
                LogicException le = new LogicException(ex);
                throw le;
            }
            finally
            {
                this.CloseConnection();
            }

            return retBean;
        }
        #endregion

        #region "区域列表取得"
        /// <summary>
        /// 区域列表取得
        /// </summary>
        /// <returns></returns>
        public DataTable GetPrctList()
        {

            DataTable retDt = null;

            try
            {
                // DEBUG LOG START
                LogUtil.WriteDebugStartMessage();

                SqlConnection cnn = this.GetConnection();
                CommonDataAccess dac = new CommonDataAccess(cnn);
                this.OpenConnection();

                retDt = dac.GetPrctList();

                // DEBUG LOG END
                LogUtil.WriteDebugEndMessage();
            }
            catch (DataAccessException daex)
            {
                throw daex;
            }
            catch (Exception ex)
            {
                LogicException le = new LogicException(ex);
                throw le;
            }
            finally
            {
                this.CloseConnection();
            }

            return retDt;
        }
        #endregion
        #region "获取前置机列表"
        public List<QZJMachine> GetQZJ() {
            List<QZJMachine> qzjList = new List<QZJMachine>();

            try
            {
                // DEBUG LOG START
                LogUtil.WriteDebugStartMessage();

                SqlConnection cnn = this.GetConnection();
                CommonDataAccess cda = new CommonDataAccess(cnn);
                this.OpenConnection();

                qzjList = cda.GetQZJ();
                // DEBUG LOG END
                LogUtil.WriteDebugEndMessage();
            }
            catch (DataAccessException daex)
            {
                throw daex;
            }
            catch (Exception ex)
            {
                LogicException le = new LogicException(ex);
                throw le;
            }
            finally
            {
                this.CloseConnection();
            }

            return qzjList;
        }
        #endregion
        #endregion

    }
}