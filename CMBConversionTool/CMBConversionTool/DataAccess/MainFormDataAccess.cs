using Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace CMBConversionTool
{
    public class MainFormDataAccess : BaseDataAccess
    {
        #region "CONSTRUCTOR"
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="inCnn"></param>
        public MainFormDataAccess(SqlConnection inCnn)
            : base(inCnn)
        {
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="inCnn"></param>
        /// <param name="inTran"></param>
        public MainFormDataAccess(SqlConnection inCnn, SqlTransaction inTran)
            : base(inCnn, inTran)
        {
        }
        #endregion

        #region "METHOD"

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
                 end as bank, bank_prov, bank_city
                from cmb_m_vendor where bank_no like @bank_no; ";

            try
            {
                // DEBUG LOG START
                LogUtil.WriteDebugStartMessage();

                List<SqlParameter> lsParas = new List<SqlParameter>();
                SqlParameter[] paras;
                lsParas.Add(new SqlParameter("@bank_no", bankCd+"%"));
                paras = lsParas.ToArray();

                // 执行前，在LOG中输出SQL文。
                LogUtil.WriteDebugMessage(base.GetLogSQL(sql, paras));

                dt = this.ExecuteWithDataTable(sql, paras);
                //if (dt != null)
                //{
                //    if (dt.Rows.Count > 0)
                //    {
                //        bankName = dt.Rows[0][0].ToString();
                //    }
                //}

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

    }
}
