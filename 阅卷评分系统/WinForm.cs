using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 阅卷评分系统
{
    public partial class WinForm : Form
    {
        public WinForm()
        {
            InitializeComponent();
        }
        public static class common
        {
            //添加dataset
            public static DataSet DS = new DataSet();
            //输出dataset
            public static DataSet ds_Lte = new DataSet();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            string strPath = System.IO.Directory.GetCurrentDirectory();
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            //190617新增打开窗口替换关系表
            openFileDialog1.InitialDirectory = strPath + "\\";
            openFileDialog1.Filter = "读取考生答案 (*.xls,*.xlsx)|*.xls;*.xlsx";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;
            //多选
            openFileDialog1.Multiselect = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //获取到多选的文件名称
                Console.WriteLine(openFileDialog1.FileNames);
                try
                {
                    //遍历多文件名称
                    foreach (var filename in openFileDialog1.FileNames)
                    {
                        //将每个CSV文件转换为datatable
                        ExcelHelper excelHelper = new ExcelHelper(filename);
                        //表名错误时，默认为第一个表
                        DataTable dt_Csv = excelHelper.ExcelToDataTable("PWE3 ETH", true, 1);
                        //将dt_Csv添加到ds_Lte中
                        common.DS.Tables.Add(dt_Csv);
                        //设置datable名称为CSV文件名（不带扩展）
                        dt_Csv.TableName = System.IO.Path.GetFileNameWithoutExtension(filename);
                        Console.WriteLine(dt_Csv.Rows.Count);
                        //PrintData(dt_Csv);
                    }
                    Console.WriteLine(common.DS.Tables[0].Rows.Count);
                    for (int i = 1; i < common.DS.Tables.Count; i++)
                    {
                        common.DS.Tables[0].Merge(common.DS.Tables[i]);
                    }
                    Console.WriteLine(common.DS.Tables.Count);
                    Console.WriteLine(common.DS.Tables[0].Rows.Count);
                    string fileNameOld = System.IO.Path.GetFileNameWithoutExtension(openFileDialog1.FileNames[0]);
                    string fileName = "LTE合并业务" + fileNameOld + ".xlsx";
                    ExcelHelper excelHelper2 = new ExcelHelper(fileName);
                    excelHelper2.DataTableToExcel(common.DS.Tables[0], "LTE业务合并数据", true);
                    MessageBox.Show("已完成合并LTE数据 " + common.DS.Tables[0].Rows.Count + " 条,请使用！");
                    //ExcelHelper.DataSetToExcel(ds_Lte, fileName + "第一个表合并数据.xlsx");
                }

                catch (Exception ex)
                {
                    Console.WriteLine("Exception: " + ex.Message);
                }

            }
        }

        private void btnReadBiaoZhun_Click(object sender, EventArgs e)
        {
            string strPath = System.IO.Directory.GetCurrentDirectory();
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            //190617新增打开窗口替换关系表
            openFileDialog1.InitialDirectory = strPath + "\\";
            openFileDialog1.Filter = "读取考生答案 (*.xls,*.xlsx)|*.xls;*.xlsx";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;
            //多选
            openFileDialog1.Multiselect = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //获取到多选的文件名称
                Console.WriteLine(openFileDialog1.FileNames);

                try
                {
                    //遍历多文件名称
                    foreach (var filename in openFileDialog1.FileNames)
                    {
                        //将每个CSV文件转换为datatable
                        ExcelHelper excelHelper = new ExcelHelper(filename);
                        //表名错误时，默认为第一个表
                        DataTable dt_Csv = excelHelper.ExcelToDataTable("PWE3 ETH", true, 1);
                        //将dt_Csv添加到ds_Lte中
                        common.DS.Tables.Add(dt_Csv);
                        //设置datable名称为CSV文件名（不带扩展）
                        dt_Csv.TableName = System.IO.Path.GetFileNameWithoutExtension(filename);
                        Console.WriteLine(dt_Csv.Rows.Count);
                        //PrintData(dt_Csv);
                    }
                    Console.WriteLine(common.DS.Tables[0].Rows.Count);
                    for (int i = 1; i < common.DS.Tables.Count; i++)
                    {
                        common.DS.Tables[0].Merge(common.DS.Tables[i]);
                    }
                    Console.WriteLine(common.DS.Tables.Count);
                    Console.WriteLine(common.DS.Tables[0].Rows.Count);
                    string fileNameOld = System.IO.Path.GetFileNameWithoutExtension(openFileDialog1.FileNames[0]);
                    string fileName = "LTE合并业务" + fileNameOld + ".xlsx";
                    ExcelHelper excelHelper2 = new ExcelHelper(fileName);
                    excelHelper2.DataTableToExcel(common.DS.Tables[0], "LTE业务合并数据", true);
                    MessageBox.Show("已完成合并LTE数据 " + common.DS.Tables[0].Rows.Count + " 条,请使用！");
                    //ExcelHelper.DataSetToExcel(ds_Lte, fileName + "第一个表合并数据.xlsx");
                }

                catch (Exception ex)
                {
                    Console.WriteLine("Exception: " + ex.Message);
                }

            }
        }
    }
}
