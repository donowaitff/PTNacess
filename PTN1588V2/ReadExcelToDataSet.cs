using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data;

namespace PTN1588V2
{
    public static class ReadExcelToDataSet
    {
        public static string[] ReadOk(string FileDir,int FirstClomun)
        {
            string[] strFileName = null;
            string strPath = System.IO.Directory.GetCurrentDirectory();
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            //190617新增打开窗口替换关系表
            openFileDialog1.InitialDirectory = strPath + "\\"+FileDir;
            openFileDialog1.Filter = "网元信息报表 (*.xls,*.xlsx)|*.xls;*.xlsx";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;
            //多选
            openFileDialog1.Multiselect = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //获取到多选的文件名称
                Console.WriteLine(openFileDialog1.FileNames);

                //try
                {
                    //遍历多文件名称
                    foreach (var filename in openFileDialog1.FileNames)
                    {
                        //将每个CSV文件转换为datatable
                        ExcelHelper excelHelper = new ExcelHelper(filename);
                        //表名错误时，默认为第一个表
                        DataTable dt_Csv = excelHelper.ExcelToDataTable("Sheet1", true, FirstClomun);
                        //设置datable名称为CSV文件名（不带扩展）
                        dt_Csv.TableName = System.IO.Path.GetFileNameWithoutExtension(filename);
                        Console.WriteLine(dt_Csv.Rows.Count);
                        //PrintData(dt_Csv);
                        //添加到全局DataSet中
                        common.DataSet.Tables.Add(dt_Csv);
                    }
                }
                //catch (Exception ex)
                //{
                //    Console.WriteLine("Exception: " + ex.Message);
                //}
                strFileName = openFileDialog1.FileNames;
            }
            return strFileName;

        }
    }
}
