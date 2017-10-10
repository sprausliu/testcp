using Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace CMBConversionTool
{
    public class MainFormController : BaseController
    {

        #region "METHOD"

        public DataTable GetBankName(string bankCd)
        {
            DataTable dt = new DataTable();

            try
            {
                // DEBUG LOG START
                LogUtil.WriteDebugStartMessage();

                SqlConnection cnn = this.GetConnection();
                MainFormDataAccess dac = new MainFormDataAccess(cnn);
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

        public List<Currency> GetCRYList()
        {
            List<Currency> list = new List<Currency>();

            try
            {
                // DEBUG LOG START
                LogUtil.WriteDebugStartMessage();

                SqlConnection cnn = this.GetConnection();
                MainFormDataAccess dac = new MainFormDataAccess(cnn);
                this.OpenConnection();

                list = dac.GetCRYList();

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

            return list;
        }

        public string GetCountryCode(string name)
        {
            string countryCode = "";

            try
            {
                // DEBUG LOG START
                LogUtil.WriteDebugStartMessage();

                SqlConnection cnn = this.GetConnection();
                MainFormDataAccess dac = new MainFormDataAccess(cnn);
                this.OpenConnection();

                countryCode = dac.GetCountryCode(name);

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

            return countryCode;
        }
        #endregion

    }
}
