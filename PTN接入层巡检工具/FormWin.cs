using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace PTNAccessOp
{
    public partial class FormWin : Form
    {
        public FormWin()
        {
            InitializeComponent();
            
        }
        public static class common
        {
            //添加dataset
            public static DataSet DataSet;
            //输出dataset
            public static DataSet DS;
        }
        /// <summary>
        /// 读取光功率csv文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReadFile_Click(object sender, EventArgs e)
        {
            string strPath = System.IO.Directory.GetCurrentDirectory();
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            //190617新增打开窗口替换关系表
            openFileDialog1.InitialDirectory = strPath + "\\20200224导出数据";
            openFileDialog1.Filter = "端口对应关系 (*.csv)|*.csv";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;
            //多选
            openFileDialog1.Multiselect = true;
            DataSet ds_Lte = new DataSet();
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
                        DataTable dt_Csv = CSVFileHelper.OpenCSV(filename);
                        //设置datable名称为CSV文件名（不带扩展）
                        dt_Csv.TableName = System.IO.Path.GetFileNameWithoutExtension(filename);
                        Console.WriteLine(dt_Csv.Rows.Count);
                        //PrintData(dt_Csv);
                        //common.DataSet.Tables.Add(dt_Csv);
                        ds_Lte.Tables.Add(dt_Csv);
                    }
                    //合并datatable
                    for (int i = 1; i < ds_Lte.Tables.Count; i++)
                    {
                        ds_Lte.Tables[0].Merge(ds_Lte.Tables[i]);
                    }
                    //复制datatable
                    common.DataSet.Tables.Add(ds_Lte.Tables[0].Copy());
                }

                catch (Exception ex)
                {
                    Console.WriteLine("Exception: " + ex.Message);
                }

            }
        }

        static void PrintData(DataTable data)
        {
            if (data == null) return;
            for (int i = 0; i < data.Rows.Count; ++i)
            {
                for (int j = 0; j < data.Columns.Count; ++j)
                    Console.Write("{0} ", data.Rows[i][j]);
                Console.Write("\n");
            }
        }
        /// <summary>
        /// 读取Excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_ReadEXcel_Click(object sender, EventArgs e)
        {
            string strPath = System.IO.Directory.GetCurrentDirectory();
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            //190617新增打开窗口替换关系表
            openFileDialog1.InitialDirectory = strPath + "\\20200224导出数据";
            openFileDialog1.Filter = "LTE导出业务 (*.xls,*.xlsx)|*.xls;*.xlsx";
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
                        DataTable dt_Csv = excelHelper.ExcelToDataTable("PWE3 ETH", true, 8);
                        //设置datable名称为CSV文件名（不带扩展）
                        dt_Csv.TableName = System.IO.Path.GetFileNameWithoutExtension(filename);
                        ExcelHelper excelHelper2 = new ExcelHelper("aaa.xlsx");
                        excelHelper2.DataTableToExcel(dt_Csv, "aaa", true);
                        Console.WriteLine(dt_Csv.Rows.Count);
                        //PrintData(dt_Csv);
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception: " + ex.Message);
                }

            }
        }
        /// <summary>
        /// 读取网元信息报表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_ReadNeBase_Click(object sender, EventArgs e)
        {
            string strPath = System.IO.Directory.GetCurrentDirectory();
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            //190617新增打开窗口替换关系表
            openFileDialog1.InitialDirectory = strPath + "\\20200224导出数据";
            openFileDialog1.Filter = "网元信息报表 (*.xls,*.xlsx)|*.xls;*.xlsx";
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
                        DataTable dt_Csv = excelHelper.ExcelToDataTable("Sheet1", true, 8);
                        //设置datable名称为CSV文件名（不带扩展）
                        dt_Csv.TableName = System.IO.Path.GetFileNameWithoutExtension(filename);
                        Console.WriteLine(dt_Csv.Rows.Count);
                        //PrintData(dt_Csv);
                        //添加到全局DataSet中
                        common.DataSet.Tables.Add(dt_Csv);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception: " + ex.Message);
                }

            }
        }
        /// <summary>
        /// 读取尾纤链接
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_ReadFiber_Click(object sender, EventArgs e)
        {
            string strPath = System.IO.Directory.GetCurrentDirectory();
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            //190617新增打开窗口替换关系表
            openFileDialog1.InitialDirectory = strPath + "\\20200224导出数据";
            openFileDialog1.Filter = "纤缆连接关系 (*.xls,*.xlsx)|*.xls;*.xlsx";
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
                        DataTable dt_Csv = excelHelper.ExcelToDataTable("Sheet1", true, 8);
                        //设置datable名称为CSV文件名（不带扩展）
                        dt_Csv.TableName = System.IO.Path.GetFileNameWithoutExtension(filename);
                        Console.WriteLine(dt_Csv.Rows.Count);
                        //PrintData(dt_Csv);
                        //添加到全局DataSet中
                        common.DataSet.Tables.Add(dt_Csv);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception: " + ex.Message);
                }
            }
        }

        private void FormWin_Load(object sender, EventArgs e)
        {
            common.DataSet = new DataSet();
            common.DS = new DataSet();
            #region //读取默认表格

            
            string Path = Directory.GetCurrentDirectory();
            try
            {
                string fileName = Directory.GetCurrentDirectory() + "\\光功率模板.xls";
                if (!File.Exists(fileName))
                {
                    MessageBox.Show("配置文件不存在或被打开占用，请确认文件完整性", "警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    System.Environment.Exit(0);
                }
                else
                {
                    #region  //读取配置文件，添加datatable到dataset
                    try
                    {
                        using (ExcelHelper excelHelper = new ExcelHelper(fileName))
                        {
                            //DataSetG.DS = new DataSet();
                            #region 添加表名及采集命令对应关系，直接读取Excel表生成表名
                            DataTable tblStrCmd = excelHelper.ExcelToDataTableA("光功率模板", true);
                            tblStrCmd.TableName = "光功率模板";
                            common.DS.Tables.Add(tblStrCmd);
                            Console.WriteLine();
                            #endregion
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                        MessageBox.Show("提示： " + "请关闭-光功率模板及入口Excel表后重新运行！");
                        Environment.Exit(0);
                    }
                    #endregion  //读取配置文件，添加datatable到dataset

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Exception: " + ex.Message + "请关闭文件后重新运行");
            }
            #endregion //读取默认表格
        }
        /// <summary>
        /// 生成数据报表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_CreatReport_Click(object sender, EventArgs e)
        {
            DataSet ds = common.DataSet.Copy();
            string dt_Power = null;
            string dt_Fiber = null;
            //读取基础文件
            //查找光功率表名
            for (int i = 0; i < ds.Tables.Count; i++)
            {
                if (ds.Tables[i].TableName.Contains("光功率"))
                {
                    dt_Power = ds.Tables[i].TableName;
                }
                if (ds.Tables[i].TableName.Contains("纤缆"))
                {
                    dt_Fiber = ds.Tables[i].TableName;
                }
            }
            //合并光功率表前四列 网元名称	槽位ID	单板名称	端口
            foreach (DataRow dr in ds.Tables[dt_Power].Rows)
            {
                dr["网元名称"] = dr["网元名称"].ToString() + "-" + dr["槽位ID"].ToString() + "-"+dr["单板名称"].ToString() + "-" + dr["端口"].ToString();
                //去掉-子架1，两个文件匹配
                dr["网元名称"] = dr["网元名称"].ToString().Replace("子架1-","").Replace("(端口-", "(PORT-");
                //Console.WriteLine(dr["网元名称"]);
            }
            //合并光功率表前四列 网元名称	槽位ID	单板名称	端口
            foreach (DataRow dr in ds.Tables[dt_Fiber].Rows)
            {
                dr["源网元"] = dr["源网元"].ToString() + "-" + dr["源端口"].ToString();
                dr["宿网元"] = dr["宿网元"].ToString() + "-" + dr["宿端口"].ToString();
                //去掉-子架1，两个文件匹配
                dr["源网元"] = dr["源网元"].ToString().Replace("子架1-", "");
                //去掉-子架1，两个文件匹配
                dr["宿网元"] = dr["宿网元"].ToString().Replace("子架1-", "");
                // Console.WriteLine(dr["源网元"]);
            }
            //添加列名
            #region //将光缆连接表查询、复制至光功率模板表中
            foreach (DataRow dr in ds.Tables[dt_Fiber].Rows)
            {
                //获取源宿网元名称
                string str_SrcName = dr["源网元"].ToString();
                string str_DstName = dr["宿网元"].ToString();
                //创建光功率模板行
                DataRow row = common.DS.Tables["光功率模板"].NewRow();
                row["源网元"] = str_SrcName;
                row["宿网元"] = str_DstName;
                DataRow[] dr_Src = ds.Tables[dt_Power].Select("网元名称 = '" + str_SrcName + "'");
                //查找到数据
                if (dr_Src.Length>0)
                {
                    //对光功率模板表进行赋值
                    for (int i = 4; i < ds.Tables[dt_Power].Columns.Count; i++)
                    {
                        string str_Column = ds.Tables[dt_Power].Columns[i].Caption;
                        row["源网元_" + str_Column] = dr_Src[0][str_Column];
                    }
                }
                else
                {
                    Console.WriteLine("未查询到该端口光功率，请检查:" + str_SrcName);
                }
                DataRow[] dr_Dst = ds.Tables[dt_Power].Select("网元名称 = '" + str_DstName + "'");
                if (dr_Dst.Length > 0)
                {
                    //对光功率模板表进行赋值
                    for (int i = 4; i < ds.Tables[dt_Power].Columns.Count; i++)
                    {
                        string str_Column = ds.Tables[dt_Power].Columns[i].Caption;
                        row["宿网元_" + str_Column] = dr_Dst[0][str_Column];
                    }
                }
                else
                {
                    Console.WriteLine("未查询到该端口光功率，请检查:"+str_DstName);
                }
                //完成查询赋值
                #region // 计算光缆衰耗注意部分站点光功率未查询出来
                //string转换为int型
                 
                if (row["源网元_输入光功率(dBm)"].ToString().Length>1&& row["宿网元_输出光功率(dBm)"].ToString().Length > 1)
                {
                    //Console.WriteLine(row["宿网元_输出光功率(dBm)"].ToString());
                    //Console.WriteLine(row["源网元_输入光功率(dBm)"].ToString());
                    row["正向光缆衰耗"] = double.Parse(row["宿网元_输出光功率(dBm)"].ToString()) - double.Parse(row["源网元_输入光功率(dBm)"].ToString());
                }
                else
                {
                    row["正向光缆衰耗"] = "未查询到输入输出";
                }
                if (row["宿网元_输入光功率(dBm)"].ToString().Length > 1 && row["源网元_输出光功率(dBm)"].ToString().Length > 1)
                {
                    //Console.WriteLine(row["源网元_输出光功率(dBm)"].ToString());
                    //Console.WriteLine(row["宿网元_输入光功率(dBm)"].ToString());
                    row["反向光缆衰耗"] = double.Parse(row["源网元_输出光功率(dBm)"].ToString()) - double.Parse(row["宿网元_输入光功率(dBm)"].ToString());
                }
                else
                {
                    row["反向光缆衰耗"] = "未查询到输入输出";
                }
                if (row["正向光缆衰耗"].ToString().Length<8&&row["反向光缆衰耗"].ToString().Length <8)
                {
                    row["两芯光缆衰耗差值"] = double.Parse(row["正向光缆衰耗"].ToString()) - double.Parse(row["反向光缆衰耗"].ToString());
                }
                #endregion // 计算光缆衰耗注意部分站点光功率未查询出来
                //添加行
                common.DS.Tables["光功率模板"].Rows.Add(row);
                //Console.WriteLine(common.DS.Tables["光功率模板"].Rows.Count);
               // ExcelHelper excelHelper2 = new ExcelHelper("查询输出.xls");
               
                   
            }
            #endregion
            ExcelHelper.DataSetToExcel(common.DS, "查询输出aaaa");
            ExcelHelper.DataSetToExcel(ds, "查询输出aaaaaa");
            Console.WriteLine("");
            //查询对比数据
            //尾纤连接关系为基础

        }
    }
}