using Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace CMBPayment
{
    public class MainController : BaseController
    {
        protected UserInfoBean LoginUser
        {
            get
            {
                if (System.Web.HttpContext.Current.Session[Constant.SessionKey.LOGIN_USER_INFO] != null)
                {
                    return (UserInfoBean)System.Web.HttpContext.Current.Session[Constant.SessionKey.LOGIN_USER_INFO];
                }
                return new UserInfoBean();
            }
        }

        #region "METHOD"

        #region "Get batch info"
        public DataTable GetBatchList(BatchBean infoBean)
        {
            DataTable returnDt = new DataTable();

            try
            {
                // DEBUG LOG START
                LogUtil.WriteDebugStartMessage();

                SqlConnection cnn = this.GetConnection();
                MainDataAccess dac = new MainDataAccess(cnn);
                this.OpenConnection();

                returnDt = dac.GetBatchList(infoBean);

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

            return returnDt;
        }

        public DataTable GetBatchInfo(string batchId)
        {
            DataTable returnDt = new DataTable();

            try
            {
                // DEBUG LOG START
                LogUtil.WriteDebugStartMessage();

                SqlConnection cnn = this.GetConnection();
                MainDataAccess dac = new MainDataAccess(cnn);
                this.OpenConnection();

                returnDt = dac.GetBatchInfo(batchId);

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

            return returnDt;
        }

        public DataTable GetSucBatchList(BatchBean infoBean)
        {
            DataTable returnDt = new DataTable();

            try
            {
                // DEBUG LOG START
                LogUtil.WriteDebugStartMessage();

                SqlConnection cnn = this.GetConnection();
                MainDataAccess dac = new MainDataAccess(cnn);
                this.OpenConnection();

                returnDt = dac.GetSucBatchList(infoBean);

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

            return returnDt;
        }

        public DataTable GetFailBatchList(BatchBean infoBean)
        {
            DataTable returnDt = new DataTable();

            try
            {
                // DEBUG LOG START
                LogUtil.WriteDebugStartMessage();

                SqlConnection cnn = this.GetConnection();
                MainDataAccess dac = new MainDataAccess(cnn);
                this.OpenConnection();

                returnDt = dac.GetFailBatchList(infoBean);

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

            return returnDt;
        }
        #endregion

        #region "Get payment info"
        public DataTable GetPaymentList(PaymentBean infoBean)
        {
            DataTable returnDt = new DataTable();

            try
            {
                // DEBUG LOG START
                LogUtil.WriteDebugStartMessage();

                SqlConnection cnn = this.GetConnection();
                MainDataAccess dac = new MainDataAccess(cnn);
                this.OpenConnection();

                returnDt = dac.GetPaymentList(infoBean);

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

            return returnDt;
        }

        public List<PaymentReqt> GetPaymentsByBatchId(string batchId)
        {
            List<PaymentReqt> payList = new List<PaymentReqt>();

            try
            {
                // DEBUG LOG START
                LogUtil.WriteDebugStartMessage();

                SqlConnection cnn = this.GetConnection();
                MainDataAccess dac = new MainDataAccess(cnn);
                this.OpenConnection();

                payList = dac.GetPaymentsByBatchId(batchId);

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

            return payList;
        }
        #endregion

        #region "Insert or update info"

        public bool UpdateBatchInfo(BatchItemBean itemBean)
        {
            bool isSucess = true;

            try
            {
                // DEBUG LOG START
                LogUtil.WriteDebugStartMessage();

                SqlConnection cnn = this.GetConnection();
                MainDataAccess dac = new MainDataAccess(cnn);
                this.OpenConnection();

                int effectCount = dac.UpdateBatchInfo(itemBean);
                if (effectCount == 0)
                {
                    isSucess = false;
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

            return isSucess;
        }

        public void InsertBatchAndPaymentInfo(List<PaymentReqt> payList, string statusId)
        {
            try
            {
                // DEBUG LOG START
                LogUtil.WriteDebugStartMessage();

                SqlConnection cnn = this.GetConnection();
                this.OpenConnection();
                base.BeginTrans();

                MainDataAccess dac = new MainDataAccess(cnn, this.Trans);

                // 最新ID取得
                string batchId = dac.GetNewBatchId();
                double ttlAmt = 0;
                // 詳細追加
                foreach (PaymentReqt item in payList)
                {
                    ttlAmt = ttlAmt + ComUtil.ConvertToDouble(item.TRSAMT, 0);
                    item.YURREF = dac.GetNewPaymentId();
                    item.batch_id = batchId;
                    item.status_id = statusId;
                    item.user_id = LoginUser.UserId;
                    dac.InsertPaymentInfo(item);
                }
                BatchItemBean batchItem = new BatchItemBean();
                batchItem.BatchId = batchId;
                batchItem.StatusId = statusId;
                batchItem.TtlAmt = ttlAmt.ToString();
                batchItem.PayCnt = payList.Count.ToString();
                batchItem.UpdateUserId = LoginUser.UserId;
                dac.InsertBatchInfo(batchItem);

                base.CommitTrans();

                // DEBUG LOG END
                LogUtil.WriteDebugEndMessage();

            }
            catch (DataAccessException daex)
            {
                base.RollbackTrans();
                throw daex;
            }
            catch (Exception ex)
            {
                base.RollbackTrans();
                LogicException le = new LogicException(ex);
                throw le;
            }
            finally
            {
                this.CloseConnection();
            }

        }

        public void UpdateBatchAndPaymentStatus(List<PaymentReqt> payList, string batchId, string batchStatusId)
        {
            try
            {
                // DEBUG LOG START
                LogUtil.WriteDebugStartMessage();

                SqlConnection cnn = this.GetConnection();
                this.OpenConnection();
                base.BeginTrans();

                MainDataAccess dac = new MainDataAccess(cnn, this.Trans);

                // 最新ID取得
                // 詳細追加
                foreach (PaymentReqt item in payList)
                {
                    item.user_id = LoginUser.UserId;
                    dac.UpdatePaymentInfo(item);
                }
                BatchItemBean batchItem = new BatchItemBean();
                batchItem.BatchId = batchId;
                batchItem.StatusId = batchStatusId;
                batchItem.UpdateUserId = LoginUser.UserId;
                dac.UpdateBatchInfo(batchItem);

                base.CommitTrans();

                // DEBUG LOG END
                LogUtil.WriteDebugEndMessage();

            }
            catch (DataAccessException daex)
            {
                base.RollbackTrans();
                throw daex;
            }
            catch (Exception ex)
            {
                base.RollbackTrans();
                LogicException le = new LogicException(ex);
                throw le;
            }
            finally
            {
                this.CloseConnection();
            }

        }

        public void InsertXmlLog(string xml, string type)
        {
            try
            {
                // DEBUG LOG START
                LogUtil.WriteDebugStartMessage();

                SqlConnection cnn = this.GetConnection();
                this.OpenConnection();
                base.BeginTrans();

                MainDataAccess dac = new MainDataAccess(cnn, this.Trans);
                dac.InsertXmlLog(xml, type);

                base.CommitTrans();

                // DEBUG LOG END
                LogUtil.WriteDebugEndMessage();
            }
            catch (DataAccessException daex)
            {
                base.RollbackTrans();
                throw daex;
            }
            catch (Exception ex)
            {
                base.RollbackTrans();
                LogicException le = new LogicException(ex);
                throw le;
            }
            finally
            {
                this.CloseConnection();
            }

        }
        #endregion

        #region "Get other info"
        public DataTable GetBankName(string bankCd)
        {
            DataTable dt = new DataTable();

            try
            {
                // DEBUG LOG START
                LogUtil.WriteDebugStartMessage();

                SqlConnection cnn = this.GetConnection();
                MainDataAccess dac = new MainDataAccess(cnn);
                this.OpenConnection();

                dt = dac.GetBankName(bankCd);

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

            return dt;
        }

        #endregion

        #endregion
    }
}
