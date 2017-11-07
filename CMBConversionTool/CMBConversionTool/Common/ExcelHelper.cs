using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;

namespace Common
{
    public class ExcelHelper
    {
        /// <summary>
        /// Convert excel file to datatable 
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns>DataTable</returns>
        public static DataTable ExcelToDataTable(string filePath)
        {
            DataTable dt = new DataTable();
            IWorkbook workbook;
            FileInfo xlsFile = new FileInfo(filePath);

            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                // get excel workbook
                if (xlsFile.Extension.Equals(".xlsx"))
                {
                    // when excel version over 2007~
                    workbook = new XSSFWorkbook(fs);
                }
                else
                {
                    // when excel version is 97~2003
                    workbook = new HSSFWorkbook(fs);
                }

                // get excel first sheet
                ISheet sheet = workbook.GetSheetAt(0);

                // Header   
                IRow header = sheet.GetRow(sheet.FirstRowNum);
                List<int> columns = new List<int>();
                for (int i = 0; i < header.LastCellNum; i++)
                {
                    object obj = GetValueByType(header.GetCell(i));
                    if (obj == null || obj.ToString() == string.Empty)
                    {
                        dt.Columns.Add(new DataColumn("Columns" + i.ToString()));
                    }
                    else
                    {
                        dt.Columns.Add(new DataColumn(obj.ToString()));
                    }
                    columns.Add(i);
                }

                // Data   
                for (int i = sheet.FirstRowNum + 1; i <= sheet.LastRowNum; i++)
                {
                    DataRow dr = dt.NewRow();
                    bool hasValue = false;
                    foreach (int j in columns)
                    {
                        dr[j] = GetValueByType(sheet.GetRow(i).GetCell(j));
                        if (dr[j] != null && dr[j].ToString() != string.Empty)
                        {
                            hasValue = true;
                        }
                    }
                    if (hasValue)
                    {
                        dt.Rows.Add(dr);
                    }
                }
            }
            return dt;
        }

        /// <summary>
        /// Export excel file from datatable
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="dataSource"></param>
        public static void ExportExcelFromDT(string filePath, DataTable dataSource)
        {
            XSSFWorkbook workbook = new XSSFWorkbook();
            //新建sheet工作表
            XSSFSheet sheet = (XSSFSheet)workbook.CreateSheet("Sheet1");

            int rowCount = dataSource.Rows.Count;
            int colCount = dataSource.Columns.Count;

            for (int i = 0; i < rowCount; i++)
            {
                IRow dataRow = sheet.CreateRow(i);
                for (int j = 0; j < colCount; j++)
                {
                    ICell cell = dataRow.CreateCell(j);
                    cell.SetCellValue(dataSource.Rows[i][j].ToString());
                }
            }

            //保存文件
            using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                workbook.Write(fs);
            }
        }

        #region "other methode"
        
        /// <summary>
        /// DataTable convert to List
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static List<T> ConvertToList<T>(DataTable dt)
        {

            List<T> listORM = new List<T>();

            if (dt.Rows.Count == 0)
            {
                return listORM;
            }

            Type typeOfT = typeof(T);

            Dictionary<string, PropertyInfo> dicPI = new Dictionary<string, PropertyInfo>();
            PropertyInfo[] pis = typeOfT.GetProperties();
            foreach (PropertyInfo pi in pis)
            {
                dicPI.Add(pi.Name.ToUpper(), pi);
            }

            for (int rowIndex = 0; rowIndex < dt.Rows.Count; rowIndex++)
            {
                T objT = (T)Activator.CreateInstance(typeOfT);

                for (int colIndex = 0; colIndex < dt.Columns.Count; colIndex++)
                {
                    string strColName = dt.Columns[colIndex].ColumnName.ToUpper();

                    if (!dicPI.ContainsKey(strColName))
                    {
                        continue;
                    }
                    PropertyInfo pi = dicPI[strColName];

                    object value = dt.Rows[rowIndex][colIndex];
                    if (Convert.IsDBNull(value))
                    {
                        continue;
                    }
                    if (dt.Columns[colIndex].DataType != pi.PropertyType)
                    {
                        if (pi.PropertyType == typeof(Nullable<DateTime>))
                        {
                            if (((Nullable<DateTime>)value).HasValue)
                            {
                                value = ((Nullable<DateTime>)value).Value;
                            }
                            else
                            {
                                value = null;
                            }
                        }
                        else if (pi.PropertyType == typeof(Nullable<Int32>))
                        {
                            if (((Nullable<Int32>)value).HasValue)
                            {
                                value = ((Nullable<Int32>)value).Value;
                            }
                            else
                            {
                                value = null;
                            }
                        }
                        else if (pi.PropertyType == typeof(Nullable<Double>))
                        {
                            if (((Nullable<Double>)value).HasValue)
                            {
                                value = ((Nullable<Double>)value).Value;
                            }
                            else
                            {
                                value = null;
                            }
                        }
                        else if (pi.PropertyType == typeof(Nullable<Decimal>))
                        {
                            if (((Nullable<Decimal>)value).HasValue)
                            {
                                value = ((Nullable<Decimal>)value).Value;
                            }
                            else
                            {
                                value = null;
                            }
                        }
                        else
                        {
                            value = Convert.ChangeType(value, pi.PropertyType);
                        }
                    }

                    pi.SetValue(objT, value, null);
                }

                listORM.Add(objT);
            }

            return listORM;
        }


        /// <summary>
        /// Get cell value by data type
        /// </summary>
        /// <param name="cell"></param>
        /// <returns>cell object</returns>
        private static object GetValueByType(ICell cell)
        {
            if (cell == null)
                return null;
            switch (cell.CellType)
            {
                case CellType.Blank: //BLANK:   
                    return null;
                case CellType.Boolean: //BOOLEAN:   
                    return cell.BooleanCellValue;
                case CellType.Numeric: //NUMERIC:  
                    if (DateUtil.IsCellDateFormatted(cell))
                    { // source cell's type is date.
                        return cell.DateCellValue;
                    }
                    else
                    {
                        return cell.NumericCellValue;
                    }
                case CellType.String: //STRING:   
                    return cell.StringCellValue;
                case CellType.Error: //ERROR:   
                    return cell.ErrorCellValue;
                case CellType.Formula: //FORMULA:   
                default:
                    return "=" + cell.CellFormula;
            }
        }

        #endregion

    }
}
