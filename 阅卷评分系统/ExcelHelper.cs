using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.HSSF.UserModel;
using System.IO;
using System.Data;

namespace 阅卷评分系统
{
    public class ExcelHelper : IDisposable
    {
        private string fileName = null; //文件名
        private IWorkbook workbook = null;
        private FileStream fs = null;
        private bool disposed;

        public ExcelHelper(string fileName)
        {
            this.fileName = fileName;
            disposed = false;
        }

        /// <summary>
        /// 将DataTable数据导入到excel中
        /// </summary>
        /// <param name="data">要导入的数据</param>
        /// <param name="isColumnWritten">DataTable的列名是否要导入</param>
        /// <param name="sheetName">要导入的excel的sheet的名称</param>
        /// <returns>导入数据行数(包含列名那一行)</returns>
        public int DataTableToExcel(DataTable data, string sheetName, bool isColumnWritten)
        {
            int i = 0;
            int j = 0;
            int count = 0;
            ISheet sheet = null;
            fs = File.OpenWrite(fileName);
            //fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            if (fileName.IndexOf(".xlsx") > 0) // 2007版本
                workbook = new XSSFWorkbook();
            else if (fileName.IndexOf(".xls") > 0) // 2003版本
                workbook = new HSSFWorkbook();

            try
            {
                if (workbook != null)
                {
                    sheet = workbook.CreateSheet(sheetName);
                }
                else
                {
                    return -1;
                }

                if (isColumnWritten == true) //写入DataTable的列名
                {
                    IRow row = sheet.CreateRow(0);
                    for (j = 0; j < data.Columns.Count; ++j)
                    {
                        row.CreateCell(j).SetCellValue(data.Columns[j].ColumnName);
                    }
                    count = 1;
                }
                else
                {
                    count = 0;
                }

                for (i = 0; i < data.Rows.Count; ++i)
                {
                    IRow row = sheet.CreateRow(count);
                    for (j = 0; j < data.Columns.Count; ++j)
                    {
                        row.CreateCell(j).SetCellValue(data.Rows[i][j].ToString());
                    }
                    ++count;
                }
                workbook.Write(fs); //写入到excel
                return count;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                return -1;
            }
        }

        /// <summary>
        /// 将excel中的数据导入到DataTable中
        /// </summary>
        /// <param name="sheetName">excel工作薄sheet的名称</param>
        /// <param name="isFirstRowColumn">第一行是否是DataTable的列名</param>
        /// <param name="titleNum">需求数据的行数---第一行是否是DataTable的列名</param>
        /// <returns>返回的DataTable</returns>
        public DataTable ExcelToDataTable(string sheetName, bool isFirstRowColumn,int titleNum)
        {
            ISheet sheet = null;
            DataTable data = new DataTable();
            int startRow = titleNum;
            try
            {
                fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                if (fileName.IndexOf(".xlsx") > 0) // 2007版本
                    workbook = new XSSFWorkbook(fs);
                else if (fileName.IndexOf(".xls") > 0) // 2003版本
                    workbook = new HSSFWorkbook(fs);

                if (sheetName != null)
                {
                    sheet = workbook.GetSheet(sheetName);
                    if (sheet == null) //如果没有找到指定的sheetName对应的sheet，则尝试获取第一个sheet
                    {
                        sheet = workbook.GetSheetAt(0);
                    }
                }
                else
                {
                    sheet = workbook.GetSheetAt(0);
                }
                if (sheet != null)
                {
                    IRow firstRow = sheet.GetRow(titleNum);
                    int cellCount = firstRow.LastCellNum; //一行最后一个cell的编号 即总的列数

                    if (isFirstRowColumn)
                    {
                        for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
                        {
                            ICell cell = firstRow.GetCell(i);
                            if (cell != null)
                            {
                                string cellValue = cell.StringCellValue;
                                cellValue = cellValue.Trim();
                                if (cellValue != null)
                                {
                                    //不带列号数字
                                    DataColumn column = new DataColumn(cellValue);
                                    //带列号数字
                                    // DataColumn column = new DataColumn(cellValue+i.ToString());
                                    data.Columns.Add(column);
                                }
                            }
                        }
                        //startRow = sheet.FirstRowNum + 1;
                        startRow = startRow + 1;
                    }
                    else
                    {
                        startRow = sheet.FirstRowNum;
                    }

                    //最后一列的标号
                    int rowCount = sheet.LastRowNum;
                    for (int i = startRow; i <= rowCount; ++i)
                    {
                        IRow row = sheet.GetRow(i);
                        if (row == null) continue; //没有数据的行默认是null　　　　　　　

                        DataRow dataRow = data.NewRow();
                        for (int j = row.FirstCellNum; j < cellCount; ++j)
                        {
                            if (row.GetCell(j) != null) //同理，没有数据的单元格都默认是null
                                dataRow[j] = row.GetCell(j).ToString().Trim();
                        }
                        data.Rows.Add(dataRow);
                    }
                }

                return data;
            }
            catch (Exception ex)
            {
               Console.WriteLine("Exception: " + ex.Message);
                return null;
            }
        }

        public static void DataSetToExcel(DataSet ds, string filename)
        {
            #region 将DATASET中文件，生成Excel代码

            //导出Excel代码
            //创建Workbook
            IWorkbook wk = new HSSFWorkbook();
            //创建Sheet
            foreach (DataTable dtl in ds.Tables)
            {

                //设置Excel第一行名称
                ISheet sheet = wk.CreateSheet(dtl.TableName);
                IRow row0 = sheet.CreateRow(0);
                for (int i = 0; i < dtl.Columns.Count; i++)
                {
                    row0.CreateCell(i).SetCellValue(dtl.Columns[i].ColumnName);
                }
                int rowIndex = 1;
                foreach (DataRow dtdr in dtl.Rows)
                {
                    IRow row = sheet.CreateRow(rowIndex);
                    foreach (DataColumn dtdc in dtl.Columns)
                    {
                        for (int i = 0; i < dtl.Columns.Count; i++)
                        {
                            row.CreateCell(i).SetCellValue(dtdr[i].ToString());
                        }
                    }
                    rowIndex++;
                }

                ////第一行自动筛选
                //CellRangeAddress c = new CellRangeAddress(1, 65535, 0,9); 
                //sheet.SetAutoFilter(c);

                //冻结窗口
                sheet.CreateFreezePane(0, 1);
                // 把数据写入到磁盘上
                using (FileStream fs = File.OpenWrite(filename + ".xls"))
                {
                    wk.Write(fs);
                }
            }

            //MessageBox.Show("PTN配置数据已成功导出至程序目录下：" + filename + "中，请查看。");
            #endregion
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    if (fs != null)
                        fs.Close();
                }

                fs = null;
                disposed = true;
            }
        }
    }
}