using Common;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CMBConversionTool
{
    public partial class MainForm : Form
    {

        #region "PROPERTY"
        public string TaskFilePath { get; set; }
        #endregion

        #region "CONSTRUCTOR"

        public MainForm()
        {
            InitializeComponent();
        }

        #endregion

        #region "EVENT"

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void btnSelFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Text File|*.txt";
            dialog.RestoreDirectory = true;
            dialog.InitialDirectory = ConfigurationManager.AppSettings["EXPORT_FOLDER"];
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                txtFilePath.Text = dialog.FileName;
                this.TaskFilePath = txtFilePath.Text;
            }
        }

        private void btnGenerateFile_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(this.TaskFilePath))
            {
                MessageBox.Show("please select file.");
                return;
            }

            this.OutputLog("Convert file start.");

            ConvertFileController ctrl = new ConvertFileController();
            DataTable dt = ctrl.ConvertDecryptedFile(this.TaskFilePath);

            string tempFile = Path.Combine(Directory.GetCurrentDirectory(), "CMBPaymentFile_template.xls");
            string outputFile = Path.Combine(Directory.GetCurrentDirectory(), string.Format(ConfigurationManager.AppSettings["OUTPUT_FILE"], DateTime.Now.ToString("yyyyMMdd")));
            this.WriteDtToExcelCell(tempFile, outputFile, dt);

            this.OutputLog("Convert file end.");

            MessageBox.Show("Convert file Successful!");
        }
        #endregion


        #region "METHOD"

        private void WriteDtToExcelCell(string tempFilePath, string outFilePath, DataTable dt) 
        {
            IWorkbook workbook;

            using (FileStream rstr = new FileStream(tempFilePath, FileMode.Open, FileAccess.Read))
            {
                FileInfo xlsFile = new FileInfo(tempFilePath);
                if (xlsFile.Extension.Equals(".xlsx"))
                {
                    // when excel version over 2007~
                    workbook = new XSSFWorkbook(rstr);
                }
                else
                {
                    // when excel version is 97~2003
                    workbook = new HSSFWorkbook(rstr);
                }

                ISheet sheet = workbook.GetSheetAt(0);

                using (FileStream wstr = new FileStream(outFilePath, FileMode.Create, FileAccess.Write))
                {
                    int rowCount = dt.Rows.Count;
                    int colCount = dt.Columns.Count;

                    // add row data
                    for (int i = 0; i < rowCount; i++)
                    {
                        int beg = i + 6;

                        IRow dataRow = sheet.CreateRow(beg);
                        for (int j = 0; j < colCount; j++)
                        {
                            ICell cell = dataRow.CreateCell(j);
                            cell.SetCellValue(GetDdData(dt.Rows[i][j].ToString()));
                        }
                    }
                    workbook.Write(wstr);
                }
            }
        }

        private string GetDdData(object obj)
        {
            string retVal = string.Empty;
            if (obj == null || obj == DBNull.Value)
            {
                return retVal;
            }
            else
            {
                retVal = obj.ToString();
            }
            return retVal;
        }

        private void OutputLog(string log)
        {
            string msg = DateTime.Now.ToLongTimeString() + " " + log;
            this.listMsg.Items.Add(msg);
            LogUtil.WriteInfoMessage(msg);
            Application.DoEvents();
        }
        #endregion

    }
}
