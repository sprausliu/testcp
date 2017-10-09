using Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace CMBPayment
{
    /// <summary>
    ///     共通数据访问用类
    /// </summary>
    /// <remarks>
    ///     共通数据访问用类です
    /// </remarks>
    public class CommonDataAccess : BaseBusinessDataAccess
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="conn">SQL接続</param>
        /// <returns></returns>
        public CommonDataAccess(SqlConnection conn)
            : base(conn)
        {
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="conn">SQL接続</param>
        /// <param name="inTrans">SQL事务</param>
        /// <returns></returns>
        public CommonDataAccess(SqlConnection inConn, SqlTransaction inTrans)
            : base(inConn, inTrans)
        {
        }

        #region "方法"

        #region "用户信息的取得"
        public DataTable GetUserBaseInfo(string inUserID)
        {

            DataTable returnDt = new DataTable();
            string sql = @"SELECT 
                            U.USER_ID,  
                            U.ROLE_ID,
                            R.ROLE_NM,
                            U.cmb_account,
                            R.amount_limit
                       FROM 
                            CMB_M_USER U
                       LEFT JOIN CMB_M_ROLE R
                       ON U.ROLE_ID = R.ROLE_ID
                       WHERE 
                            UPPER(U.USER_ID) = UPPER(@USER_ID)";

            try
            {
                LogUtil.WriteDebugStartMessage();
                List<SqlParameter> lsParas = new List<SqlParameter>();
                SqlParameter[] paras;

                //参数：用户ID
                lsParas.Add(new SqlParameter("@USER_ID", inUserID));
                paras = lsParas.ToArray();

                // 执行前，在LOG中输出SQL文。
                LogUtil.WriteDebugMessage(base.GetLogSQL(sql, paras));
                returnDt = base.ExecuteWithDataTable(sql, paras);
                // 执行后，在LOG中输出执行结果。
                LogUtil.WriteDebugMessage(base.GetExecuteLog(returnDt.Rows.Count));
                LogUtil.WriteDebugEndMessage();
            }
            catch (Exception ex)
            {
                DataAccessException dae = new DataAccessException(ex);
                throw dae;
            }

            return returnDt;
        }
        #endregion

        #region "系统日期取得"
        /// <summary>
        /// 系统时間取得処理
        /// </summary>
        /// param：无
        /// <returns>returnDt</returns>
        public DataTable GetSystemDate()
        {

            DataTable returnDt = new DataTable();
            string sql = "SELECT GETDATE() AS SYSDATE";

            try
            {
                // DEBUG LOG START
                LogUtil.WriteDebugStartMessage();

                // 执行前，在LOG中输出SQL文。
                LogUtil.WriteDebugMessage(base.GetLogSQL(sql, null));

                returnDt = this.ExecuteWithDataTable(sql, null);

                // 执行后，在LOG中输出执行结果。
                LogUtil.WriteDebugMessage(base.GetExecuteLog(returnDt.Rows.Count));

                // DEBUG LOG END
                LogUtil.WriteDebugEndMessage();

            }
            catch (Exception ex)
            {
                DataAccessException dae = new DataAccessException(ex);
                throw dae;
            }

            return returnDt;
        }
        #endregion

        #region "系统Config信息取得"
        /// <summary>
        /// 系统Config信息取得
        /// </summary>
        /// <returns>系统Config信息</returns>
        public DataTable GetSystemConfigInfo()
        {

            DataTable returnDt = new DataTable();
            string sql = "SELECT CONF_KEY,CONF_VALUE FROM CMB_M_SYSCONF";

            try
            {
                // DEBUG LOG START
                LogUtil.WriteDebugStartMessage();

                // 执行前，在LOG中输出SQL文。
                LogUtil.WriteDebugMessage(base.GetLogSQL(sql, null));

                returnDt = this.ExecuteWithDataTable(sql, null);

                // 执行后，在LOG中输出执行结果。
                LogUtil.WriteDebugMessage(base.GetExecuteLog(returnDt.Rows.Count));

                // DEBUG LOG END
                LogUtil.WriteDebugEndMessage();
            }
            catch (Exception ex)
            {
                DataAccessException dae = new DataAccessException(ex);
                throw dae;
            }

            return returnDt;
        }

        /// <summary>
        /// 系统Config信息取得
        /// </summary>
        /// <param name="inKey">指定Config的KEY</param>
        /// <returns>系统Config信息</returns>
        public DataTable GetSystemConfigInfo(string inKey)
        {

            DataTable returnDt = new DataTable();
            string sql = "SELECT CONF_KEY,CONF_VALUE FROM CMB_M_SYSCONF WHERE CONF_KEY = @SYSCONF_KEY";

            try
            {
                // DEBUG LOG START
                LogUtil.WriteDebugStartMessage();

                List<SqlParameter> lsParas = new List<SqlParameter>();
                SqlParameter[] paras;
                lsParas.Add(new SqlParameter("@SYSCONF_KEY", inKey));
                paras = lsParas.ToArray();

                // 执行前，在LOG中输出SQL文。
                LogUtil.WriteDebugMessage(base.GetLogSQL(sql, paras));

                returnDt = this.ExecuteWithDataTable(sql, paras);

                // 执行后，在LOG中输出执行结果。
                LogUtil.WriteDebugMessage(base.GetExecuteLog(returnDt.Rows.Count));

                // DEBUG LOG END
                LogUtil.WriteDebugEndMessage();
            }
            catch (Exception ex)
            {
                DataAccessException dae = new DataAccessException(ex);
                throw dae;
            }

            return returnDt;
        }
        #endregion

        #region "字典MASTER取得"
        /// <summary>
        /// 字典MASTER取得
        /// 类别根据编码列表作成
        /// </summary>
        /// <param name="inCatgory">类别</param>
        /// <returns>returnDt</returns>
        public DataTable GetCodeMasterInfo(string inCatgory, string inCode)
        {
            DataTable returnDt = new DataTable();
            string sql = @"";

            try
            {
                // DEBUG LOG START
                LogUtil.WriteDebugStartMessage();

                StringBuilder sqlB = new StringBuilder();
                List<SqlParameter> lsParas = new List<SqlParameter>();
                SqlParameter[] paras;

                sqlB.AppendLine(" SELECT ");
                sqlB.AppendLine(" CD,CD_NM,DISP_ORD AS ORD,TYPE,TYPE_NM,VALUE,COMMENT ");
                sqlB.AppendLine(" FROM CMB_M_CODE ");
                sqlB.AppendLine(" WHERE TYPE = @TYPE ");

                //参数：区分名
                lsParas.Add(new SqlParameter("@TYPE", inCatgory));
                //Code
                if (!string.IsNullOrEmpty(inCode))
                {
                    sqlB.AppendLine(" AND CD = @CD ");
                    //参数：区分名
                    lsParas.Add(new SqlParameter("@CD", inCode));
                }
                sqlB.AppendLine(" ORDER BY DISP_ORD ");

                sql = sqlB.ToString();
                paras = lsParas.ToArray();

                // 执行前，在LOG中输出SQL文。
                LogUtil.WriteDebugMessage(base.GetLogSQL(sql, paras));
                returnDt = this.ExecuteWithDataTable(sql, paras);

                // 执行后，在LOG中输出执行结果。
                LogUtil.WriteDebugMessage(base.GetExecuteLog(returnDt.Rows.Count));

                // DEBUG LOG END
                LogUtil.WriteDebugEndMessage();
            }
            catch (Exception ex)
            {
                DataAccessException dae = new DataAccessException(ex);
                throw dae;
            }

            return returnDt;
        }
        #endregion




        #region "省 自治区 市 LIST取得"
        /// <summary>
        /// 省 自治区 市 LIST取得
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllProvList()
        {

            DataTable returnDt = new DataTable();
            string sql = @"
				SELECT PRCT_ID, PRCT_NM
				FROM V_PRCT
                WHERE PRCT_LEVEL = '1'
                ORDER BY PRCT_ID ";

            try
            {
                // DEBUG LOG START
                LogUtil.WriteDebugStartMessage();

                // 执行前，在LOG中输出SQL文。
                LogUtil.WriteDebugMessage(base.GetLogSQL(sql, null));

                returnDt = this.ExecuteWithDataTable(sql, null);

                // 执行后，在LOG中输出执行结果。
                LogUtil.WriteDebugMessage(base.GetExecuteLog(returnDt.Rows.Count));
                // DEBUG LOG END
                LogUtil.WriteDebugEndMessage();
            }
            catch (Exception ex)
            {
                DataAccessException dae = new DataAccessException(ex);
                throw dae;
            }

            return returnDt;
        }
        #endregion

        #region "市 区 取得"
        /// <summary>
        ///  市 区 取得
        /// </summary>
        /// <param name="inProvId"></param>
        /// <returns></returns>
        public DataTable GetCityListByProvId(string inProvId)
        {
            DataTable returnDt = new DataTable();
            string sql = @"
				SELECT PRCT_ID, PRCT_NM
				FROM V_PRCT
				WHERE PRCT_LEVEL = '2' AND UP_PROV_ID = @UP_PROV_ID
				ORDER BY PRCT_ID ";

            try
            {
                // DEBUG LOG START
                LogUtil.WriteDebugStartMessage();
                List<SqlParameter> lsParas = new List<SqlParameter>();
                SqlParameter[] paras;

                lsParas.Add(new SqlParameter("@UP_PROV_ID", inProvId));
                paras = lsParas.ToArray();

                // 执行前，在LOG中输出SQL文。
                LogUtil.WriteDebugMessage(base.GetLogSQL(sql, paras));
                returnDt = this.ExecuteWithDataTable(sql, paras);

                // 执行后，在LOG中输出执行结果。
                LogUtil.WriteDebugMessage(base.GetExecuteLog(returnDt.Rows.Count));
                // DEBUG LOG END
                LogUtil.WriteDebugEndMessage();
            }
            catch (Exception ex)
            {
                DataAccessException dae = new DataAccessException(ex);
                throw dae;
            }

            return returnDt;
        }
        #endregion

        #region "根据区域编码取得区域名称"
        /// <summary>
        /// 根据区域编码取得区域名称
        /// </summary>
        /// <param name="inPrctId"></param>
        /// <returns></returns>
        public DataTable GetPrctNmByPrctId(string inPrctId)
        {
            DataTable returnDt = new DataTable();
            string sql = @"
				SELECT PRCT_ID, PRCT_NM
				FROM V_PRCT
				WHERE PRCT_ID = @PRCT_ID
				ORDER BY PRCT_ID ";

            try
            {
                // DEBUG LOG START
                LogUtil.WriteDebugStartMessage();
                List<SqlParameter> lsParas = new List<SqlParameter>();
                SqlParameter[] paras;

                lsParas.Add(new SqlParameter("@PRCT_ID", inPrctId));
                paras = lsParas.ToArray();

                // 执行前，在LOG中输出SQL文。
                LogUtil.WriteDebugMessage(base.GetLogSQL(sql, paras));
                returnDt = this.ExecuteWithDataTable(sql, paras);

                // 执行后，在LOG中输出执行结果。
                LogUtil.WriteDebugMessage(base.GetExecuteLog(returnDt.Rows.Count));
                // DEBUG LOG END
                LogUtil.WriteDebugEndMessage();
            }
            catch (Exception ex)
            {
                DataAccessException dae = new DataAccessException(ex);
                throw dae;
            }

            return returnDt;
        }
        #endregion

        #region "区域列表取得"
        /// <summary>
        /// 区域列表取得
        /// </summary>
        /// <returns></returns>
        public DataTable GetPrctList()
        {

            DataTable returnDt = new DataTable();
            string sql = @"
				SELECT PRCT_ID AS ID
					  ,PRCT_SNM AS NAME
					  ,PRCT_LEVEL AS [LEVEL]
					  ,LNG
					  ,LAT
					  ,UP_PROV_ID AS PARENT_ID
				FROM V_PRCT 
				ORDER BY PRCT_ID ";

            try
            {
                // DEBUG LOG START
                LogUtil.WriteDebugStartMessage();

                // 执行前，在LOG中输出SQL文。
                LogUtil.WriteDebugMessage(base.GetLogSQL(sql, null));

                returnDt = this.ExecuteWithDataTable(sql, null);

                // 执行后，在LOG中输出执行结果。
                LogUtil.WriteDebugMessage(base.GetExecuteLog(returnDt.Rows.Count));

                // DEBUG LOG END
                LogUtil.WriteDebugEndMessage();
            }
            catch (Exception ex)
            {
                DataAccessException dae = new DataAccessException(ex);
                throw dae;
            }

            return returnDt;
        }
        #endregion

        #endregion
    }
}

