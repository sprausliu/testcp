using Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace CMBPayment
{
    public class MainDataAccess : BaseBusinessDataAccess
    {
        #region "CONSTRUCTOR"
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="inCnn"></param>
        public MainDataAccess(SqlConnection inCnn)
            : base(inCnn)
        {
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="inCnn"></param>
        /// <param name="inTran"></param>
        public MainDataAccess(SqlConnection inCnn, SqlTransaction inTran)
            : base(inCnn, inTran)
        {
        }
        #endregion

        #region "METHOD"

        #region "Get batch info"
        public DataTable GetBatchList(BatchBean infoBean)
        {
            DataTable dtRst = null;
            try
            {
                LogUtil.WriteDebugStartMessage();

                string searchCols = this.GetBatchInfoColsSql();
                string searchTables = this.GetBatchInfoTablesSql();
                string searchOrder = "a.batch_id desc";
                string searchCondition = this.GetBatchInfoConditon(infoBean);

                dtRst = base.ExecutePagingProc(infoBean, searchCols, searchTables, searchOrder, searchCondition);

                LogUtil.WriteDebugEndMessage();
            }
            catch (Exception ex)
            {
                DataAccessException dae = new DataAccessException(ex);
                throw dae;
            }
            return dtRst;
        }

        private string GetBatchInfoColsSql()
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("	  A.batch_id");
            sql.AppendLine("	, A.pay_cnt");
            sql.AppendLine("	, A.ttl_amt");
            sql.AppendLine("	, A.status_id");
            sql.AppendLine("	, MC1.CD_NM AS status");
            sql.AppendLine("	, A.maker_id");
            sql.AppendLine("	, convert(varchar,A.maker_time,103) maker_time");
            sql.AppendLine("	, A.appr1_id");
            sql.AppendLine("	, convert(varchar,A.appr1_time,103) appr1_time");
            sql.AppendLine("	, A.appr2_id");
            sql.AppendLine("	, convert(varchar,A.appr2_time,103) appr2_time");
            sql.AppendLine("	, A.rejc_id");
            sql.AppendLine("	, convert(varchar,A.rejc_time,103) rejc_time");
            sql.AppendLine("	, convert(varchar,A.update_time,121) update_time");
            return sql.ToString();
        }

        private string GetBatchInfoTablesSql()
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("	cmb_t_batch a");
            sql.AppendLine(" left join cmb_m_code mc1 ON mc1.type = 'BATCH_STATUS' AND mc1.cd = a.status_id");
            return sql.ToString();
        }

        private string GetBatchInfoConditon(BatchBean infoBean)
        {
            StringBuilder condition = new StringBuilder();
            condition.Append(" 1 = 1 ");

            if (!string.IsNullOrEmpty(infoBean.StatusId))
            {
                condition.Append(" AND A.status_id  = '" + ComUtil.ConvertToSQLInjection(infoBean.StatusId) + "'");
            }
            return condition.ToString();
        }
        #endregion

        #region "Get success batch info"
        public DataTable GetSucBatchList(BatchBean infoBean)
        {
            DataTable dtRst = null;
            try
            {
                LogUtil.WriteDebugStartMessage();

                string searchCols = this.GetSucBatchInfoColsSql();
                string searchTables = this.GetSucBatchInfoTablesSql();
                string searchOrder = "a.batch_id desc";
                string searchCondition = this.GetSucBatchInfoConditon(infoBean);

                dtRst = base.ExecutePagingProc(infoBean, searchCols, searchTables, searchOrder, searchCondition);

                LogUtil.WriteDebugEndMessage();
            }
            catch (Exception ex)
            {
                DataAccessException dae = new DataAccessException(ex);
                throw dae;
            }
            return dtRst;
        }

        private string GetSucBatchInfoColsSql()
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("	  A.batch_id");
            sql.AppendLine("	, A.pay_cnt");
            sql.AppendLine("	, A.ttl_amt");
            sql.AppendLine("	, A.status_id");
            sql.AppendLine("	, MC1.CD_NM AS status");
            sql.AppendLine("	, A.maker_id");
            sql.AppendLine("	, convert(varchar,A.maker_time,103) maker_time");
            sql.AppendLine("	, A.appr1_id");
            sql.AppendLine("	, convert(varchar,A.appr1_time,103) appr1_time");
            sql.AppendLine("	, A.appr2_id");
            sql.AppendLine("	, convert(varchar,A.appr2_time,103) appr2_time");
            sql.AppendLine("	, A.rejc_id");
            sql.AppendLine("	, convert(varchar,A.rejc_time,103) rejc_time");
            sql.AppendLine("	, convert(varchar,A.update_time,121) update_time");
            return sql.ToString();
        }

        private string GetSucBatchInfoTablesSql()
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("	cmb_t_batch a");
            sql.AppendLine(" left join cmb_m_code mc1 ON mc1.type = 'BATCH_STATUS' AND mc1.cd = a.status_id");
            return sql.ToString();
        }

        private string GetSucBatchInfoConditon(BatchBean infoBean)
        {
            StringBuilder condition = new StringBuilder();
            condition.Append(" 1 = 1 ");
            condition.Append(" AND A.status_id in ('01') ");
            return condition.ToString();
        }
        #endregion

        #region "Get failed batch info"
        public DataTable GetFailBatchList(BatchBean infoBean)
        {
            DataTable dtRst = null;
            try
            {
                LogUtil.WriteDebugStartMessage();

                string searchCols = this.GetFailBatchInfoColsSql();
                string searchTables = this.GetFailBatchInfoTablesSql();
                string searchOrder = "a.batch_id desc";
                string searchCondition = this.GetFailBatchInfoConditon(infoBean);

                dtRst = base.ExecutePagingProc(infoBean, searchCols, searchTables, searchOrder, searchCondition);

                LogUtil.WriteDebugEndMessage();
            }
            catch (Exception ex)
            {
                DataAccessException dae = new DataAccessException(ex);
                throw dae;
            }
            return dtRst;
        }

        private string GetFailBatchInfoColsSql()
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("	  A.batch_id");
            sql.AppendLine("	, A.pay_cnt");
            sql.AppendLine("	, A.ttl_amt");
            sql.AppendLine("	, A.status_id");
            sql.AppendLine("	, MC1.CD_NM AS status");
            sql.AppendLine("	, A.maker_id");
            sql.AppendLine("	, convert(varchar,A.maker_time,103) maker_time");
            sql.AppendLine("	, A.appr1_id");
            sql.AppendLine("	, convert(varchar,A.appr1_time,103) appr1_time");
            sql.AppendLine("	, A.appr2_id");
            sql.AppendLine("	, convert(varchar,A.appr2_time,103) appr2_time");
            sql.AppendLine("	, A.rejc_id");
            sql.AppendLine("	, convert(varchar,A.rejc_time,103) rejc_time");
            sql.AppendLine("	, convert(varchar,A.update_time,121) update_time");
            return sql.ToString();
        }

        private string GetFailBatchInfoTablesSql()
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("	cmb_t_batch a");
            sql.AppendLine(" left join cmb_m_code mc1 ON mc1.type = 'BATCH_STATUS' AND mc1.cd = a.status_id");
            return sql.ToString();
        }

        private string GetFailBatchInfoConditon(BatchBean infoBean)
        {
            StringBuilder condition = new StringBuilder();
            condition.Append(" 1 = 1 ");
            condition.Append(" AND A.status_id in ('97','98','99') ");
            return condition.ToString();
        }
        #endregion

        #region "Get one batch info"
        public DataTable GetBatchInfo(string batchId)
        {
            //string bankName = "";
            DataTable dt = new DataTable();

            StringBuilder sb = new StringBuilder();
            sb.AppendLine(" select A.batch_id");
            sb.AppendLine("	, A.pay_cnt");
            sb.AppendLine("	, A.ttl_amt");
            sb.AppendLine("	, A.status_id");
            sb.AppendLine("	, MC1.CD_NM AS status");
            sb.AppendLine("	, A.maker_id");
            sb.AppendLine("	, convert(varchar,A.maker_time,103) maker_time");
            sb.AppendLine("	, A.appr1_id");
            sb.AppendLine("	, convert(varchar,A.appr1_time,103) appr1_time");
            sb.AppendLine("	, A.appr2_id");
            sb.AppendLine("	, convert(varchar,A.appr2_time,103) appr2_time");
            sb.AppendLine("	, A.rejc_id");
            sb.AppendLine("	, convert(varchar,A.rejc_time,103) rejc_time");
            sb.AppendLine("	, convert(varchar,A.update_time,121) update_time");
            sb.AppendLine(" from cmb_t_batch a");
            sb.AppendLine(" left join cmb_m_code mc1 ON mc1.type = 'BATCH_STATUS' AND mc1.cd = a.status_id");
            sb.AppendLine(" where a.batch_id = @batch_id; ");

            string sql = sb.ToString();

            try
            {
                // DEBUG LOG START
                LogUtil.WriteDebugStartMessage();

                List<SqlParameter> lsParas = new List<SqlParameter>();
                SqlParameter[] paras;
                lsParas.Add(new SqlParameter("@batch_id", batchId));
                paras = lsParas.ToArray();

                // 执行前，在LOG中输出SQL文。
                LogUtil.WriteDebugMessage(base.GetLogSQL(sql, paras));

                dt = this.ExecuteWithDataTable(sql, paras);

                // 执行后，在LOG中输出执行结果。
                LogUtil.WriteDebugMessage(base.GetExecuteLog(dt.Rows.Count));

                // DEBUG LOG END
                LogUtil.WriteDebugEndMessage();
            }
            catch (Exception ex)
            {
                DataAccessException dae = new DataAccessException(ex);
                throw dae;
            }

            return dt;
        }

        #endregion

        #region "GetBankName"
        /// <summary>
        /// Get all task list
        /// </summary>
        /// <returns></returns>
        public DataTable GetBankName(string bankCd)
        {
            //string bankName = "";
            DataTable dt = new DataTable();

            string sql = @"
                select bank_name 
                  ,case when charindex(N'行',bank_name) = 0 
	                then (case when charindex(N'社',bank_name) = 0 
			                   then substring(bank_name,1,charindex(N'库',bank_name)) 
		                  else substring(bank_name,1,charindex(N'社',bank_name)) end)
	                else substring(bank_name,1,charindex(N'行',bank_name)) 
                 end as bank, bank_prov, bank_city, cmb_flg
                from cmb_m_vendor where bank_no like @bank_no; ";

            try
            {
                // DEBUG LOG START
                LogUtil.WriteDebugStartMessage();

                List<SqlParameter> lsParas = new List<SqlParameter>();
                SqlParameter[] paras;
                lsParas.Add(new SqlParameter("@bank_no", bankCd + "%"));
                paras = lsParas.ToArray();

                // 执行前，在LOG中输出SQL文。
                LogUtil.WriteDebugMessage(base.GetLogSQL(sql, paras));

                dt = this.ExecuteWithDataTable(sql, paras);

                // 执行后，在LOG中输出执行结果。
                LogUtil.WriteDebugMessage(base.GetExecuteLog(dt.Rows.Count));

                // DEBUG LOG END
                LogUtil.WriteDebugEndMessage();
            }
            catch (Exception ex)
            {
                DataAccessException dae = new DataAccessException(ex);
                throw dae;
            }

            return dt;
        }
        #endregion

        #region "GetPaymentList"
        public DataTable GetPaymentList(PaymentBean infoBean)
        {
            DataTable dtRst = null;
            try
            {
                LogUtil.WriteDebugStartMessage();

                string searchCols = this.GetPaymentInfoColsSql();
                string searchTables = this.GetPaymentInfoTablesSql();
                string searchOrder = "a.YURREF desc";
                string searchCondition = this.GetPaymentInfoConditon(infoBean);

                dtRst = base.ExecutePagingProc(infoBean, searchCols, searchTables, searchOrder, searchCondition);

                LogUtil.WriteDebugEndMessage();
            }
            catch (Exception ex)
            {
                DataAccessException dae = new DataAccessException(ex);
                throw dae;
            }
            return dtRst;
        }

        private string GetPaymentInfoColsSql()
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("	  A.batch_id");
            sql.AppendLine("	, A.YURREF");
            sql.AppendLine("	, A.status_id");
            sql.AppendLine("	, A.comment");
            sql.AppendLine("	, A.EPTDAT");
            sql.AppendLine("	, A.OPRDAT");
            sql.AppendLine("	, A.DBTACC");
            sql.AppendLine("	, A.DBTBBK");
            sql.AppendLine("	, A.TRSAMT");
            sql.AppendLine("	, A.CCYNBR");
            sql.AppendLine("	, A.NUSAGE");
            sql.AppendLine("	, A.BUSNAR");
            sql.AppendLine("	, A.CRTACC");
            sql.AppendLine("	, A.CRTNAM");
            sql.AppendLine("	, A.LRVEAN");
            sql.AppendLine("	, A.BRDNBR");
            sql.AppendLine("	, A.BNKFLG");
            sql.AppendLine("	, A.CRTBNK");
            sql.AppendLine("	, A.CTYCOD");
            sql.AppendLine("	, A.CRTADR");
            sql.AppendLine("	, A.NTFCH1");
            sql.AppendLine("	, A.update_id");
            sql.AppendLine("	, convert(varchar,A.update_time,121) update_time");
            sql.AppendLine("	, mc1.cd_nm as status");
            return sql.ToString();
        }

        private string GetPaymentInfoTablesSql()
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("	cmb_t_payment a");
            sql.AppendLine(" left join cmb_m_code mc1 ON mc1.type = 'PAY_STATUS' AND mc1.cd = a.status_id");
            return sql.ToString();
        }

        private string GetPaymentInfoConditon(PaymentBean infoBean)
        {
            StringBuilder condition = new StringBuilder();
            condition.Append(" 1 = 1 and a.del_flg = '0' ");

            if (!string.IsNullOrEmpty(infoBean.BatchId))
            {
                condition.Append(" AND A.batch_id  = '" + ComUtil.ConvertToSQLInjection(infoBean.BatchId) + "'");
            }
            return condition.ToString();
        }
        #endregion

        #region "GetAllPaymentsByBatchId"
        /// <summary>
        /// Get all task list
        /// </summary>
        /// <returns></returns>
        public List<PaymentReqt> GetPaymentsByBatchId(string batchId)
        {
            //string bankName = "";
            List<PaymentReqt> payList = new List<PaymentReqt>();
            DataTable dt = new DataTable();
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("	select A.batch_id");
            sb.AppendLine("	, A.YURREF");
            sb.AppendLine("	, A.status_id");
            sb.AppendLine("	, A.comment");
            sb.AppendLine("	, A.EPTDAT");
            sb.AppendLine("	, A.OPRDAT");
            sb.AppendLine("	, A.DBTACC");
            sb.AppendLine("	, A.DBTBBK");
            sb.AppendLine("	, A.TRSAMT");
            sb.AppendLine("	, A.CCYNBR");
            sb.AppendLine("	, A.NUSAGE");
            sb.AppendLine("	, A.BUSNAR");
            sb.AppendLine("	, A.CRTACC");
            sb.AppendLine("	, A.CRTNAM");
            sb.AppendLine("	, A.LRVEAN");
            sb.AppendLine("	, A.BRDNBR");
            sb.AppendLine("	, A.BNKFLG");
            sb.AppendLine("	, A.CRTBNK");
            sb.AppendLine("	, A.CRTADR");
            sb.AppendLine("	, A.NTFCH1");
            sb.AppendLine(" from cmb_t_payment a");
            sb.AppendLine(" where A.batch_id  = @batch_id and a.del_flg = '0' ");
            string sql = sb.ToString();

            try
            {
                // DEBUG LOG START
                LogUtil.WriteDebugStartMessage();

                List<SqlParameter> lsParas = new List<SqlParameter>();
                SqlParameter[] paras;
                lsParas.Add(new SqlParameter("@batch_id", batchId));
                paras = lsParas.ToArray();

                // 执行前，在LOG中输出SQL文。
                LogUtil.WriteDebugMessage(base.GetLogSQL(sql, paras));

                dt = this.ExecuteWithDataTable(sql, paras);
                payList = base.ConvertToList<PaymentReqt>(dt);

                // 执行后，在LOG中输出执行结果。
                LogUtil.WriteDebugMessage(base.GetExecuteLog(dt.Rows.Count));

                // DEBUG LOG END
                LogUtil.WriteDebugEndMessage();
            }
            catch (Exception ex)
            {
                DataAccessException dae = new DataAccessException(ex);
                throw dae;
            }

            return payList;
        }
        #endregion

        #region "insert or update"

        public int UpdateBatchInfo(BatchItemBean itemBean)
        {
            int effectCount = 0;
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(" UPDATE cmb_t_batch");
            sb.AppendLine(" SET update_time = GETDATE()");
            sb.AppendLine("    ,update_id = @UPD_ID");
            if (!string.IsNullOrEmpty(itemBean.StatusId))
            {
                sb.AppendLine("    ,status_id = @status_id");
            }
            if (!string.IsNullOrEmpty(itemBean.Appr1Id))
            {
                sb.AppendLine("    ,appr1_id = @appr1_id");
                sb.AppendLine("    ,appr1_time = getdate()");
            }
            if (!string.IsNullOrEmpty(itemBean.Appr2Id))
            {
                sb.AppendLine("    ,appr2_id = @appr2_id");
                sb.AppendLine("    ,appr2_time = getdate()");
            }
            if (!string.IsNullOrEmpty(itemBean.RejcId))
            {
                sb.AppendLine("    ,rejc_id = @rejc_id");
                sb.AppendLine("    ,rejc_time = getdate()");
            }
            sb.AppendLine(" WHERE batch_id = @batch_id");
            //sb.AppendLine("AND update_time = @UPD_TIME ");

            string updateSql = sb.ToString();

            try
            {
                // DEBUG LOG START
                LogUtil.WriteDebugStartMessage();

                List<SqlParameter> lsParas = new List<SqlParameter>();
                SqlParameter[] paras;

                lsParas.Add(new SqlParameter("@batch_id", itemBean.BatchId));
                lsParas.Add(new SqlParameter("@UPD_ID", itemBean.UpdateUserId));
                //lsParas.Add(new SqlParameter("@UPD_TIME", itemBean.UpdateDateKey));
                if (!string.IsNullOrEmpty(itemBean.StatusId))
                {
                    lsParas.Add(new SqlParameter("@status_id", itemBean.StatusId));
                }
                if (!string.IsNullOrEmpty(itemBean.Appr1Id))
                {
                    lsParas.Add(new SqlParameter("@appr1_id", itemBean.Appr1Id));
                }
                if (!string.IsNullOrEmpty(itemBean.Appr2Id))
                {
                    lsParas.Add(new SqlParameter("@appr2_id", itemBean.Appr2Id));
                }
                if (!string.IsNullOrEmpty(itemBean.RejcId))
                {
                    lsParas.Add(new SqlParameter("@rejc_id", itemBean.RejcId));
                }
                paras = lsParas.ToArray();

                effectCount = base.ExecuteNonQuery(updateSql, paras);

                // DEBUG LOG END
                LogUtil.WriteDebugEndMessage();
            }
            catch (Exception ex)
            {
                DataAccessException dae = new DataAccessException(ex);
                throw dae;
            }

            return effectCount;
        }

        public int UpdatePaymentInfo(PaymentReqt itemBean)
        {
            int effectCount = 0;
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(" UPDATE cmb_t_payment");
            sb.AppendLine(" SET update_time = GETDATE()");
            sb.AppendLine("    ,update_id = @UPD_ID");
            if (!string.IsNullOrEmpty(itemBean.status_id))
            {
                sb.AppendLine("    ,status_id = @status_id");
            }
            if (!string.IsNullOrEmpty(itemBean.comment))
            {
                sb.AppendLine("    ,comment = @comment");
            }
            if (!string.IsNullOrEmpty(itemBean.OPRDAT))
            {
                sb.AppendLine("    ,OPRDAT = @OPRDAT");
            }
            sb.AppendLine(" WHERE YURREF = @YURREF ");

            string updateSql = sb.ToString();

            try
            {
                // DEBUG LOG START
                LogUtil.WriteDebugStartMessage();

                List<SqlParameter> lsParas = new List<SqlParameter>();
                SqlParameter[] paras;

                lsParas.Add(new SqlParameter("@YURREF", itemBean.YURREF));
                lsParas.Add(new SqlParameter("@UPD_ID", itemBean.user_id));
                if (!string.IsNullOrEmpty(itemBean.status_id))
                {
                    lsParas.Add(new SqlParameter("@status_id", itemBean.status_id));
                }
                if (!string.IsNullOrEmpty(itemBean.comment))
                {
                    lsParas.Add(new SqlParameter("@comment", itemBean.comment));
                }
                if (!string.IsNullOrEmpty(itemBean.OPRDAT))
                {
                    lsParas.Add(new SqlParameter("@OPRDAT", itemBean.OPRDAT));
                }
                paras = lsParas.ToArray();

                effectCount = base.ExecuteNonQuery(updateSql, paras);

                // DEBUG LOG END
                LogUtil.WriteDebugEndMessage();
            }
            catch (Exception ex)
            {
                DataAccessException dae = new DataAccessException(ex);
                throw dae;
            }

            return effectCount;
        }

        public string GetNewBatchId()
        {
            // 检索用SQL文
            string strNewBatchId;
            string sql = @" SELECT RIGHT('0000000000' + 
                CONVERT(NVARCHAR, ISNULL(MAX(CONVERT(INT, batch_id)), 0) + 1), 10) AS NEW_BATCH_ID
                FROM cmb_t_batch(UPDLOCK); ";
            try
            {
                // DEBUG LOG START
                LogUtil.WriteDebugStartMessage();

                // 执行前，在LOG中输出SQL文。
                LogUtil.WriteDebugMessage(base.GetLogSQL(sql, null));

                DataTable dtReturn = base.ExecuteWithDataTable(sql, null);
                strNewBatchId = dtReturn.Rows[0]["NEW_BATCH_ID"].ToString();

                // 执行后，在LOG中输出执行结果。
                LogUtil.WriteDebugMessage(base.GetExecuteLog(dtReturn.Rows.Count));

                // DEBUG LOG END
                LogUtil.WriteDebugEndMessage();
            }
            catch (Exception ex)
            {
                DataAccessException dataAccessException = new DataAccessException(ex);
                throw dataAccessException;
            }
            return strNewBatchId;
        }

        public string GetNewPaymentId()
        {
            // 检索用SQL文
            string strNewPaymentId;
            string sql = @" SELECT RIGHT('000000000000' + 
                CONVERT(NVARCHAR, ISNULL(MAX(CONVERT(INT, YURREF)), 0) + 1), 12) AS NEW_PAYMENT_ID
                FROM cmb_t_payment(UPDLOCK); ";
            try
            {
                // DEBUG LOG START
                LogUtil.WriteDebugStartMessage();

                // 执行前，在LOG中输出SQL文。
                LogUtil.WriteDebugMessage(base.GetLogSQL(sql, null));

                DataTable dtReturn = base.ExecuteWithDataTable(sql, null);
                strNewPaymentId = dtReturn.Rows[0]["NEW_PAYMENT_ID"].ToString();

                // 执行后，在LOG中输出执行结果。
                LogUtil.WriteDebugMessage(base.GetExecuteLog(dtReturn.Rows.Count));

                // DEBUG LOG END
                LogUtil.WriteDebugEndMessage();
            }
            catch (Exception ex)
            {
                DataAccessException dataAccessException = new DataAccessException(ex);
                throw dataAccessException;
            }
            return strNewPaymentId;
        }

        public void InsertBatchInfo(BatchItemBean itemBean)
        {

            string sql = @"insert into cmb_t_batch
                   (batch_id,
	                pay_cnt,
	                ttl_amt,
	                status_id,
	                maker_id,
	                maker_time,
	                update_id,
	                update_time)
                values (
	                @batch_id,
	                @pay_cnt,
	                @ttl_amt,
	                @status_id,
	                @maker_id,
	                getdate(),
	                @update_id,
	                getdate()
                );";

            try
            {
                // DEBUG LOG START
                LogUtil.WriteDebugStartMessage();

                List<SqlParameter> lsParas = new List<SqlParameter>();
                SqlParameter[] paras;
                lsParas.Add(new SqlParameter("@batch_id", itemBean.BatchId));
                lsParas.Add(new SqlParameter("@pay_cnt", itemBean.PayCnt));
                lsParas.Add(new SqlParameter("@ttl_amt", itemBean.TtlAmt));
                lsParas.Add(new SqlParameter("@status_id", itemBean.StatusId));
                lsParas.Add(new SqlParameter("@maker_id", itemBean.UpdateUserId));
                lsParas.Add(new SqlParameter("@update_id", itemBean.UpdateUserId));
                paras = lsParas.ToArray();

                // 执行前，在LOG中输出SQL文。
                LogUtil.WriteDebugMessage(base.GetLogSQL(sql.ToString(), paras));

                base.ExecuteNonQuery(sql.ToString(), paras);

                // DEBUG LOG END
                LogUtil.WriteDebugEndMessage();
            }
            catch (Exception ex)
            {
                DataAccessException dae = new DataAccessException(ex);
                throw dae;
            }

        }

        public void InsertPaymentInfo(PaymentReqt itemBean)
        {
            string sql = @"
                insert into cmb_t_payment
                    (YURREF 
	                ,batch_id 
	                ,status_id 
	                ,comment 
	                ,EPTDAT 
	                ,OPRDAT 
	                ,DBTACC 
	                ,DBTBBK 
	                ,TRSAMT 
	                ,CCYNBR 
	                ,NUSAGE 
	                ,BUSNAR 
	                ,CRTACC 
	                ,CRTNAM 
	                ,LRVEAN 
	                ,BRDNBR 
	                ,BNKFLG 
	                ,CRTBNK 
	                ,CRTADR 
	                ,NTFCH1 
	                ,update_id 
	                ,update_time 
                ) values (
                     @YURREF 
	                ,@batch_id 
	                ,@status_id 
	                ,@comment 
	                ,@EPTDAT 
	                ,@OPRDAT 
	                ,@DBTACC 
	                ,@DBTBBK 
	                ,@TRSAMT 
	                ,@CCYNBR 
	                ,@NUSAGE 
	                ,@BUSNAR 
	                ,@CRTACC 
	                ,@CRTNAM 
	                ,@LRVEAN 
	                ,@BRDNBR 
	                ,@BNKFLG 
	                ,@CRTBNK 
	                ,@CRTADR 
	                ,@NTFCH1 
	                ,@update_id 
	                ,getdate() 
                );";

            try
            {
                // DEBUG LOG START
                LogUtil.WriteDebugStartMessage();

                List<SqlParameter> lsParas = new List<SqlParameter>();
                SqlParameter[] paras;
                lsParas.Add(new SqlParameter("@YURREF", itemBean.YURREF));
                lsParas.Add(new SqlParameter("@batch_id", itemBean.batch_id));
                lsParas.Add(new SqlParameter("@status_id", Constant.ConstantInfo.PAYSTAT_01));
                lsParas.Add(new SqlParameter("@comment", base.GetNullEmpty(itemBean.comment)));
                lsParas.Add(new SqlParameter("@EPTDAT", base.GetNullEmpty(itemBean.EPTDAT)));
                lsParas.Add(new SqlParameter("@OPRDAT", base.GetNullEmpty(itemBean.OPRDAT)));
                lsParas.Add(new SqlParameter("@DBTACC", itemBean.DBTACC));
                lsParas.Add(new SqlParameter("@DBTBBK", itemBean.DBTBBK));
                lsParas.Add(new SqlParameter("@TRSAMT", itemBean.TRSAMT));
                lsParas.Add(new SqlParameter("@CCYNBR", itemBean.CCYNBR));
                lsParas.Add(new SqlParameter("@NUSAGE", itemBean.NUSAGE));
                lsParas.Add(new SqlParameter("@BUSNAR", base.GetNullEmpty(itemBean.BUSNAR)));
                lsParas.Add(new SqlParameter("@CRTACC", itemBean.CRTACC));
                lsParas.Add(new SqlParameter("@CRTNAM", base.GetNullEmpty(itemBean.CRTNAM)));
                lsParas.Add(new SqlParameter("@LRVEAN", base.GetNullEmpty(itemBean.LRVEAN)));
                lsParas.Add(new SqlParameter("@BRDNBR", base.GetNullEmpty(itemBean.BRDNBR)));
                lsParas.Add(new SqlParameter("@BNKFLG", itemBean.BNKFLG));
                lsParas.Add(new SqlParameter("@CRTBNK", base.GetNullEmpty(itemBean.CRTBNK)));
                lsParas.Add(new SqlParameter("@CRTADR", base.GetNullEmpty(itemBean.CRTADR)));
                lsParas.Add(new SqlParameter("@NTFCH1", base.GetNullEmpty(itemBean.NTFCH1)));
                lsParas.Add(new SqlParameter("@update_id", itemBean.user_id));
                paras = lsParas.ToArray();

                // 执行前，在LOG中输出SQL文。
                LogUtil.WriteDebugMessage(base.GetLogSQL(sql.ToString(), paras));

                base.ExecuteNonQuery(sql.ToString(), paras);

                // DEBUG LOG END
                LogUtil.WriteDebugEndMessage();
            }
            catch (Exception ex)
            {
                DataAccessException dae = new DataAccessException(ex);
                throw dae;
            }

        }

        public void InsertXmlLog(string xml, string type)
        {

            string sql = @"insert into cmb_t_swift_log
                   (swift_type,
	                content,
	                ins_id,
	                ins_time)
                values (
	                @swift_type,
	                @content,
	                @ins_id,
	                getdate()
                );";

            try
            {
                // DEBUG LOG START
                LogUtil.WriteDebugStartMessage();

                List<SqlParameter> lsParas = new List<SqlParameter>();
                SqlParameter[] paras;
                lsParas.Add(new SqlParameter("@swift_type", type));
                lsParas.Add(new SqlParameter("@content", xml));
                lsParas.Add(new SqlParameter("@ins_id", base.LoginUser.UserId));
                paras = lsParas.ToArray();

                // 执行前，在LOG中输出SQL文。
                LogUtil.WriteDebugMessage(base.GetLogSQL(sql.ToString(), paras));

                base.ExecuteNonQuery(sql.ToString(), paras);

                // DEBUG LOG END
                LogUtil.WriteDebugEndMessage();
            }
            catch (Exception ex)
            {
                DataAccessException dae = new DataAccessException(ex);
                throw dae;
            }

        }

        public int UpdatePaymentUsage(PaymentReqt pay)
        {
            int effectCount = 0;
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(" UPDATE cmb_t_payment");
            sb.AppendLine(" SET update_time = GETDATE()");
            sb.AppendLine("    ,update_id = @UPD_ID");
            sb.AppendLine("    ,NUSAGE = @NUSAGE");
            sb.AppendLine(" WHERE YURREF = @YURREF ");

            string updateSql = sb.ToString();

            try
            {
                // DEBUG LOG START
                LogUtil.WriteDebugStartMessage();

                List<SqlParameter> lsParas = new List<SqlParameter>();
                SqlParameter[] paras;

                lsParas.Add(new SqlParameter("@YURREF", pay.YURREF));
                lsParas.Add(new SqlParameter("@NUSAGE", pay.NUSAGE));
                lsParas.Add(new SqlParameter("@UPD_ID", pay.user_id));
                paras = lsParas.ToArray();

                effectCount = base.ExecuteNonQuery(updateSql, paras);

                // DEBUG LOG END
                LogUtil.WriteDebugEndMessage();
            }
            catch (Exception ex)
            {
                DataAccessException dae = new DataAccessException(ex);
                throw dae;
            }

            return effectCount;
        }

        public int DeletePaymentInfo(string payId)
        {
            int effectCount = 0;
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(" UPDATE cmb_t_payment");
            sb.AppendLine(" SET update_time = GETDATE()");
            sb.AppendLine("    ,update_id = @UPD_ID");
            sb.AppendLine("    ,del_flg = '1'");
            sb.AppendLine(" WHERE YURREF = @YURREF ");

            string updateSql = sb.ToString();

            try
            {
                // DEBUG LOG START
                LogUtil.WriteDebugStartMessage();

                List<SqlParameter> lsParas = new List<SqlParameter>();
                SqlParameter[] paras;

                lsParas.Add(new SqlParameter("@YURREF", payId));
                lsParas.Add(new SqlParameter("@UPD_ID", base.LoginUser.UserId));
                paras = lsParas.ToArray();

                effectCount = base.ExecuteNonQuery(updateSql, paras);

                // DEBUG LOG END
                LogUtil.WriteDebugEndMessage();
            }
            catch (Exception ex)
            {
                DataAccessException dae = new DataAccessException(ex);
                throw dae;
            }

            return effectCount;
        }

        public int UpdateBatchPayCnt(string batchId)
        {
            int effectCount = 0;
            string updateSql = @"update cmb_t_batch
                set pay_cnt = (
	                select count(1) from 
	                cmb_t_payment where batch_id = @batch_id and del_flg = '0')
                    ,update_time = GETDATE()
                    ,update_id = @UPD_ID
                where batch_id = @batch_id ";

            try
            {
                // DEBUG LOG START
                LogUtil.WriteDebugStartMessage();

                List<SqlParameter> lsParas = new List<SqlParameter>();
                SqlParameter[] paras;

                lsParas.Add(new SqlParameter("@batch_id", batchId));
                lsParas.Add(new SqlParameter("@UPD_ID", base.LoginUser.UserId));
                paras = lsParas.ToArray();

                effectCount = base.ExecuteNonQuery(updateSql, paras);

                // DEBUG LOG END
                LogUtil.WriteDebugEndMessage();
            }
            catch (Exception ex)
            {
                DataAccessException dae = new DataAccessException(ex);
                throw dae;
            }

            return effectCount;
        }
        #endregion

        #endregion
    }
}
