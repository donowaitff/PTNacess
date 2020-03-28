using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using System.IO;

namespace PTNReadFiber
{
   
        
    

    public partial class PTN : Form
    {
        public PTN()
        {
            InitializeComponent();
        }

        public static class DataSetT
        {
            //定义静态变量
            public static DataSet DS = new DataSet();
            public static  StringBuilder sbDs = new StringBuilder();
            public static StringBuilder sbFirstNe = new StringBuilder();
        }
        public enum Board3979
        {
            EX2,
            EX4,
            EG16,
            EFG2,
            EG8,
            EFG4,
            EX8,
            EX10,
            EG24
        }
        public static string AGGRNE= "";
        private void btn_ReadExcel_Click(object sender, EventArgs e)
        {
                //DataSetT.DS = new DataSet();
            //读取网管导出的二层链路数据
                string strPath = System.IO.Directory.GetCurrentDirectory();
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.InitialDirectory = strPath;
                openFileDialog1.Filter = "端口对应关系 (*.xls)|*.xls";
                openFileDialog1.FilterIndex = 1;
                openFileDialog1.RestoreDirectory = true;

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                     using (ExcelHelper excelHelper = new ExcelHelper(openFileDialog1.FileName))
                    {
                        // 创建datatable，将链路信息读取入表
                        DataTable dt_Lujing = excelHelper.ExcelToDataTable("Sheet1", true,0);
                    dt_Lujing.TableName = "dt_Lujing";
                    DataSetT.DS.Tables.Add(dt_Lujing);
                        //创建新表，存放需需查找内容
                    DataTable dtHuanluTongJi = dt_Lujing;
                    int number = dt_Lujing.Rows.Count;
                    DataSetT.DS.Tables["dt_Lujing"].Columns.Add("监视列Flag", Type.GetType("System.String"));
                    MessageBox.Show("已完成输入Excel " + number + "  条数据");
                    }
                }
        }
        public static bool Is_Huiju(DataSet ds,string NeName)
        {
            bool IsHuiJu = false;
            //数据表中查找网元属性
            DataRow[] dataRows = ds.Tables["dtNeXinXi"].Select("网元名称= '" + NeName + "'");
            if (dataRows.Length > 0)
            {
               string  Nep = dataRows[0]["网元类型"].ToString();
                if (Nep.Contains("OptiX 10G(Metro 5000)") || Nep.Contains("OptiX 2500+(Metro 3000)") || Nep.Contains("OptiX OSN 3500") || Nep.Contains("OptiX OSN 7500"))
                {
                    IsHuiJu = true;
                }
            }
            else
            {
                //MessageBox.Show(NeName+"网元不存在，请核实网元信息报表是否正常");
            }
            return IsHuiJu;

        }
        /// <summary>
        /// 提取流量分析报表中接入环第一个网元（环首，用来确定环路连接）
        /// </summary>
        /// <param name="dtLiuLiang"></param>
        /// <returns></returns>
        public static bool FindFirstNe(DataSet DS)
        {
            bool findFirstNe = false;
            //定义查找到首网元表
            //DataTable dtFindFirstNe = new DataTable();
            //dtFindFirstNe = DS.Tables["LiuLiang"];
            //增加监视列，监视是否完成内容查找
            //dtFindFirstNe.Columns.Add("首网元", Type.GetType("System.String"));
            //遍历表，查找设置首网元
            for (int i = 0; i < DS.Tables["LiuLiang"].Rows.Count; i++)
            {
                string strAllNe = DS.Tables["LiuLiang"].Rows[i]["网元信息"].ToString();
                //将环路网元分割，确定环首网元
                string[] strNeList = strAllNe.Split(',');
                for (int j = 0; j < strNeList.Length; j++)
                {
                    //网元不为3979时，进行查询
                    if (!Is_Huiju(DataSetT.DS, strNeList[j]))
                    {
                        try
                        {
                            DataRow[] dataRows = DS.Tables["Fiber"].Select("源网元= '" + strNeList[j] + "'");
                            if (dataRows.Length > 0)
                            {
                                for (int m = 0; m < dataRows.Length; m++)
                                {
                                    string strDstNeName = dataRows[m]["宿网元"].ToString();
                                    if (Is_Huiju(DataSetT.DS, strDstNeName))
                                    {
                                        DS.Tables["LiuLiang"].Rows[i]["首网元"] = strNeList[j];
                                    }
                                }
                            }

                        }
                        catch (Exception)
                        {

                            //throw;
                        }
                        //try
                        //{
                        //    DataRow[] dataRows = DS.Tables["Fiber"].Select("宿网元= '" + strNeList[j] + "'");
                        //    if (dataRows.Length > 0)
                        //    {
                        //        for (int m = 0; m < dataRows.Length; m++)
                        //        {
                        //            string strDstNeName = dataRows[m]["源网元"].ToString();
                        //            if (Find3979(DataSetT.DS, strDstNeName))
                        //            {
                        //                DS.Tables["LiuLiang"].Rows[i]["首网元"] = strNeList[j];
                        //            }
                        //        }
                        //    }

                        //}
                        //catch (Exception)
                        //{

                        //    throw;
                        //}
                    }
                    
                }
            }
            return findFirstNe;
        }
        /// <summary>
        /// 查找下一个网元，下一个网元不为汇聚网元
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="srcNeName"></param>
        /// <returns></returns>
        public static StringBuilder FindNextNe(DataSet DS,string srcNeName,int NumNe)
        {
            StringBuilder sbTemp = new StringBuilder();
            #region MyRegion
            //try
            //{
            //            DataRow[] dataRows = DS.Tables["Fiber"].Select("源网元= '" + srcNeName + "'");
            //            if (dataRows.Length > 0)
            //            {
            //                for (int m = 0; m < dataRows.Length; m++)
            //                {
            //                    string strDstNeName = dataRows[m]["宿网元"].ToString();
            //                    //查找到宿网元不为汇聚网元
            //                    if (!Find3979(DataSetT.DS, strDstNeName))
            //                    {
            //                    NumNe = NumNe + 1;
            //                    DataRow dr2 = DS.Tables["dtHuanluTongJi"].NewRow();
            //                    dr2[0] = NumNe; //通过索引赋值
            //                    dr2[1] = strDstNeName;
            //                    dr2["Time"] = DateTime.Now;//通过名称赋值
            //                    DS.Tables["dtHuanluTongJi"].Rows.Add(dr2);
            //                    FindNextNe(DS, strDstNeName,NumNe);

            //                    }   
            //                }
            //            }

            //        }
            //        catch (Exception)
            //        {

            //            throw;
            //        }
            #endregion
            //首节点增加到数据表
            if (NumNe==0)
            {
                DataRow drm = DataSetT.DS.Tables["dtHuanluTongJi"].NewRow();
                drm[0] = NumNe;
                drm[1] = srcNeName;
                DataSetT.DS.Tables["dtHuanluTongJi"].Rows.Add(drm);
            }
            for (int i = 0; i < DataSetT.DS.Tables["Fiber"].Rows.Count; i++)
            {
                string srcN = DataSetT.DS.Tables["Fiber"].Rows[i]["源网元"].ToString();
                string IsOk = DataSetT.DS.Tables["Fiber"].Rows[i]["监视列Flag"].ToString();
                string dstN = DataSetT.DS.Tables["Fiber"].Rows[i]["宿网元"].ToString();
                // bool isFirst = false;
                //查询到网元尾纤连接、宿网元不为汇聚网元、监视列不为IsOk
                if (srcNeName.Contains(srcN)&& !Is_Huiju(DataSetT.DS, dstN) &&!IsOk.Contains("IsOk"))
                {
                    DataSetT.DS.Tables["Fiber"].Rows[i]["监视列Flag"] = "IsOk";
                    sbTemp = sbTemp.Append(","+ dstN);
                    DataRow dr = DataSetT.DS.Tables["dtHuanluTongJi"].NewRow();
                    NumNe = NumNe + 1;
                    dr[0]= NumNe;
                    dr[1] = dstN;
                    DataSetT.DS.Tables["dtHuanluTongJi"].Rows.Add(dr);
                    Console.Write(dstN+"行号"+i+"\r\n");
                    FindNextNe(DataSetT.DS, dstN, NumNe);
                }
               
            }
            return sbTemp;
        }

        /// <summary>
        /// 传递电路数据表、需存的表，网元名称，此方法存在问题
        /// </summary>
        /// <param name="dt_Lianlu"></param>
        /// <param name="dt_Baocun"></param>
        /// <param name="DstNeNameChuandi"></param>
        /// <returns></returns>
        public static int findAllNe(DataTable dt_Lianlu,DataTable dt_Baocun,string DstNeNameChuandi)
        {
            int Num = 0;
            //传递下一个网元名称，第一次为空
            string NeNameNext = DstNeNameChuandi;
            if (NeNameNext.Contains("Null"))
            {
                for (int i = 0; i < dt_Lianlu.Rows.Count; i++)
                {
                    string srcNeName = dt_Lianlu.Rows[i]["源网元"].ToString();
                    string srcPort = dt_Lianlu.Rows[i]["源端口"].ToString();
                    string dstNeName = dt_Lianlu.Rows[i]["宿网元"].ToString();
                    string dstPort = dt_Lianlu.Rows[i]["宿端口"].ToString();
                    if (Is39Or79(srcPort))
                    {
                        if (!Is39Or79(dstPort))
                        {
                            AGGRNE = srcNeName;
                            if (!dt_Lianlu.Rows[i]["核心Flag"].ToString().Contains("OK"))
                            {
                                //标注为已完成查找
                                dt_Lianlu.Rows[i]["核心Flag"] = "OK";
                                DataRow newRow;
                                newRow = dt_Baocun.NewRow();
                                newRow["核心网元"] = AGGRNE;
                                newRow["接入网元"] = dstNeName;
                                dt_Baocun.Rows.Add(newRow);
                                findAllNe(dt_Lianlu, dt_Baocun, dstNeName);
                            }
                        }
                        else
                        {
                            dt_Lianlu.Rows[i]["核心Flag"] = "OK";
                        }
                    }
                        if (Is39Or79(dstPort))
                        {
                            if (!Is39Or79(srcPort))
                            {
                                AGGRNE = dstNeName;
                                if (!dt_Lianlu.Rows[i]["核心Flag"].ToString().Contains("OK"))
                                {
                                    //标注为已完成查找
                                    dt_Lianlu.Rows[i]["核心Flag"] = "OK";
                                    DataRow newRow;
                                    newRow = dt_Baocun.NewRow();
                                    newRow["核心网元"] = AGGRNE;
                                    newRow["接入网元"] = srcNeName;
                                    dt_Baocun.Rows.Add(newRow);
                                    findAllNe(dt_Lianlu, dt_Baocun, srcNeName);
                                }
                            }
                        }
                    else
                    {
                        dt_Lianlu.Rows[i]["核心Flag"] = "OK";
                    }
                }
            }
            else
            {
                for (int j = 0; j < dt_Lianlu.Rows.Count; j++)
                {
                    string srcNeName = dt_Lianlu.Rows[j]["源网元"].ToString();
                    string srcPort = dt_Lianlu.Rows[j]["源端口"].ToString();
                    string dstNeName = dt_Lianlu.Rows[j]["宿网元"].ToString();
                    string dstPort = dt_Lianlu.Rows[j]["宿端口"].ToString();
                    //源网元为查找网元，且接入标志为False
                    if (srcNeName.Contains(NeNameNext) & !dt_Lianlu.Rows[j]["核心Flag"].ToString().Contains("OK") & !Is39Or79(dstPort))
                        {
                               dt_Lianlu.Rows[j]["接入Flag"] = "OK";
                               DataRow newRow;
                               newRow = dt_Baocun.NewRow();
                               newRow["核心网元"] = AGGRNE;
                               newRow["接入网元"] = dstNeName;
                               dt_Baocun.Rows.Add(newRow);
                               findAllNe(dt_Lianlu, dt_Baocun, dstNeName);
                        }
                    if (dstNeName.Contains(NeNameNext) & !dt_Lianlu.Rows[j]["核心Flag"].ToString().Contains("OK")&!Is39Or79(srcPort))
                    {
                        dt_Lianlu.Rows[j]["接入Flag"] = "OK";
                        DataRow newRow;
                        newRow = dt_Baocun.NewRow();
                        newRow["核心网元"] = AGGRNE;
                        newRow["接入网元"] = srcNeName;
                        dt_Baocun.Rows.Add(newRow);
                        findAllNe(dt_Lianlu, dt_Baocun, srcNeName);
                    }
                }
                //AGGRNE = "Null";
            }
                    return Num;
        }
        public static string FindNextNe(string NeName)
        {
            string NextNeName = "";

            return NextNeName;
        }
        /// <summary>
        /// 判断是否为39或79设备单板端口
        /// </summary>
        /// <param name="PortName">单板端口名称</param>
        /// <returns></returns>
        public static bool Is39Or79(string PortName)
        {
            bool Is39Or79 = false;
            foreach (string item in Enum.GetNames(typeof(Board3979)))
            {
                if (PortName.Contains(item))
                {
                    Is39Or79 = true;
                    break;
                }
            }
            return Is39Or79;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //将尾纤链接表宿网元复制至源网元
            DataTable dtFiber = DataSetT.DS.Tables["Fiber"].Copy();
            int NumFiber = dtFiber.Rows.Count;
            for (int i = 0; i < NumFiber; i++)
            {
                //DataRow dr = dtFiber.NewRow();
                string srcNeName = dtFiber.Rows[i]["宿网元"].ToString();
                string srcNePort = dtFiber.Rows[i]["宿端口"].ToString();
                dtFiber.Rows[i]["宿网元"] = dtFiber.Rows[i]["源网元"];
                dtFiber.Rows[i]["宿端口"] = dtFiber.Rows[i]["源端口"];
                dtFiber.Rows[i]["源网元"] = srcNeName;
                dtFiber.Rows[i]["源端口"] = srcNePort;
                Console.Write(i);
                DataRow drtemp = dtFiber.Rows[i];
                DataSetT.DS.Tables["Fiber"].Rows.Add(drtemp.ItemArray);
            }
            //增加临时数据表，存放每个查找出来的网元

            FindFirstNe(DataSetT.DS);
            DataTable dtHuanluTongJi = new DataTable();
            dtHuanluTongJi.Columns.Add("序号", Type.GetType("System.String"));
            dtHuanluTongJi.Columns.Add("网元名称", Type.GetType("System.String"));
            dtHuanluTongJi.TableName = "dtHuanluTongJi";
            //int NumNe = 0;
            DataSetT.DS.Tables.Add(dtHuanluTongJi);
            for (int i = 0; i < DataSetT.DS.Tables["LiuLiang"].Rows.Count; i++)
            {
                DataSetT.DS.Tables["dtHuanluTongJi"].Clear();
                string strAllNe = DataSetT.DS.Tables["LiuLiang"].Rows[i]["首网元"].ToString();
                FindNextNe(DataSetT.DS,strAllNe,0);
                Console.WriteLine("流量报表"+i+"\r\n");
                string strNeList =null;
                DataTable dtHuanluTongJiCopy = DataSetT.DS.Tables["dtHuanluTongJi"].Copy();
                DataView dv = new DataView(dtHuanluTongJiCopy);
                dtHuanluTongJiCopy = dv.ToTable(true, "网元名称");
                //DataTable DistTable = dv.ToTable("Dist", true, filedNames);
                for (int t = 0; t < dtHuanluTongJiCopy.Rows.Count; t++)
                {
                    int ttt = dtHuanluTongJiCopy.Rows.Count;
                    //Console.Write("dtHuanluTongJi行数"+ttt+"\r\n");
                    strNeList =strNeList+","+ dtHuanluTongJiCopy.Rows[t]["网元名称"].ToString();
                }
                DataSetT.DS.Tables["LiuLiang"].Rows[i]["网元顺序列表"] = strNeList;
            }
                ExcelHelper.DataSetToExcel(DataSetT.DS, "环路流量信息-" + DateTime.Now.ToString("yyyyMMddHHmmss"));
            MessageBox.Show("aaaaa");

            DataSetT.DS = new DataSet();
            //读取网管导出的二层链路数据
            string strPath = System.IO.Directory.GetCurrentDirectory();
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = strPath;
            openFileDialog1.Filter = "端口对应关系 (*.xls)|*.xls";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                using (ExcelHelper excelHelper = new ExcelHelper(openFileDialog1.FileName))
                {
                    // 创建datatable，将链路信息读取入表
                    DataTable dt = excelHelper.ExcelToDataTable("Sheet1", true, 7);
                    //创建新表，存放需需查找内容
                    //DataTable dtHuanluTongJi = dt;
                    DataView dvNew = new DataView(dt);
                    DataTable dtLineAll = dvNew.ToTable(true, "源网元", "源端口", "宿网元", "宿端口");
                    int num = dtLineAll.Rows.Count;
                    //将宿网元复制至源网元列
                    for (int i = 0; i < num; i++)
                    {
                        DataRow newRow;
                        newRow = dtLineAll.NewRow();
                        newRow[0] = dtLineAll.Rows[2];
                        newRow[1] = dtLineAll.Rows[3];
                        newRow[2] = dtLineAll.Rows[0];
                        newRow[3] = dtLineAll.Rows[1];
                        dtLineAll.Rows.Add(newRow);
                    }
                    //需生成的datatable
                    DataTable dtBaoCun = new DataTable();
                    //增加监视列，监视是否完成内容查找
                    dtLineAll.Columns.Add("核心Flag", Type.GetType("System.String"));
                    StringBuilder sbNextAll = new StringBuilder();
                    //
                    #region MyRegion
                    for (int i = 0; i < dtLineAll.Rows.Count; i++)
                    {
                        string srcNeName = dtLineAll.Rows[i]["源网元"].ToString();
                        string srcPort = dtLineAll.Rows[i]["源端口"].ToString();
                        string dstNeName = dtLineAll.Rows[i]["宿网元"].ToString();
                        string dstPort = dtLineAll.Rows[i]["宿端口"].ToString();
                        if (Is39Or79(srcPort))
                        {
                            if (!Is39Or79(dstPort))
                            {
                                AGGRNE = srcNeName;
                                if (!dtLineAll.Rows[i]["核心Flag"].ToString().Contains("OK"))
                                {
                                    //标注为已完成查找
                                    dtLineAll.Rows[i]["核心Flag"] = "OK";
                                    DataSetT.sbDs = DataSetT.sbDs.Append(srcNeName);
                                    DataSetT.sbDs = DataSetT.sbDs.Append("\r\n");
                                    DataSetT.sbDs = DataSetT.sbDs.Append(dstNeName);
                                    DataSetT.sbDs = DataSetT.sbDs.Append("\r\n");
                                    //StringBuilder sbSrc = new StringBuilder();
                                    //sbSrc = sbSrc.Append(dstNeName);
                                    FindNextNe(dtLineAll, dtBaoCun, dstNeName);
                                }
                                //输出所查询的stringbuild到所要保存的表中
                                string[] strNeName = DataSetT.sbDs.ToString().Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                              
                                //保存表格的列小于返回的网元数量，增加相应列数
                                if (dtBaoCun.Columns.Count < strNeName.Length)
                                {
                                    for (int a = 0; a <=(strNeName.Length-dtBaoCun.Columns.Count); a++)
                                    {
                                        //dtBaoCun.Columns.Add("NextNeName", Type.GetType("System.String"));
                                        dtBaoCun.Columns.Add();
                                    }
                                   
                                }
                                //将返回网元写入需保存的数据表中
                                DataRow newRow;
                                newRow = dtBaoCun.NewRow();
                                for (int j = 0; j < strNeName.Length; j++)
                                {
                                    newRow[j] = strNeName[j].ToString();
                                }
                                dtBaoCun.Rows.Add(newRow);
                                //清空所查询的stringbuild到所要保存的表
                                DataSetT.sbDs.Clear();
                            }
                            else
                            {
                                //dtLineAll.Rows[i]["核心Flag"] = "OK";
                            }
                        }
                    }

                    #endregion
                    //dtHuanluTongJi.Columns.Add("接入Flag", Type.GetType("System.String"));
                  
                    //dtHuiju.Columns.Add("核心网元", Type.GetType("System.String"));
                    //dtHuiju.Columns.Add("网元名称", Type.GetType("System.String"));
                    //findAllNe(dtLineAll, dtHuiju, "Null");
                    ExcelHelper ExcelWrite = new ExcelHelper("Write.xlsx");
                    int number = ExcelWrite.DataTableToExcel(dtBaoCun, "Sheet3", true);
                    MessageBox.Show("已完成输入Excel " + number + "  条数据");
                }
            }
        }

        /// <summary>
        /// //写递归循环，通过 源网元查找宿网元
        /// </summary>
        /// <param name="dt_Lianlu"></param>
        /// <param name="dt_Baocun"></param>
        /// <param name="DstNeNameChuandi"></param>
        /// <returns></returns>
        public static string FindNextNe(DataTable dt_Lianlu, DataTable dt_Baocun, string DstNeNameChuandi)
        {
            string sbNextNe="";
            string NeNameNext = DstNeNameChuandi;
            for (int j = 0; j < dt_Lianlu.Rows.Count; j++)
            {
                string srcNeName = dt_Lianlu.Rows[j]["源网元"].ToString();
                string srcPort = dt_Lianlu.Rows[j]["源端口"].ToString();
                string dstNeName = dt_Lianlu.Rows[j]["宿网元"].ToString();
                string dstPort = dt_Lianlu.Rows[j]["宿端口"].ToString();
                //源网元为查找网元，且接入标志为False
                if (srcNeName.Contains(NeNameNext) & !dt_Lianlu.Rows[j]["核心Flag"].ToString().Contains("OK") & !Is39Or79(dstPort))
                {
                    dt_Lianlu.Rows[j]["核心Flag"] = "OK";
                    DataSetT.sbDs = DataSetT.sbDs.Append(dstNeName);
                    DataSetT.sbDs = DataSetT.sbDs.Append("\r\n");
                    FindNextNe(dt_Lianlu, dt_Baocun, dstNeName);
                }
                if (srcNeName.Contains(NeNameNext) & !dt_Lianlu.Rows[j]["核心Flag"].ToString().Contains("OK") & Is39Or79(dstPort))
                {
                    DataSetT.sbDs = DataSetT.sbDs.Append(dstNeName);
                    DataSetT.sbDs = DataSetT.sbDs.Append("\r\n");
                }
                
            }
            return sbNextNe;
        }
        /// <summary>
        /// 读取网元信息，已测试
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_ReadNe_Click(object sender, EventArgs e)
        {
            //读取网管导出的二层链路数据
            string strPath = System.IO.Directory.GetCurrentDirectory();
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = strPath;
            openFileDialog1.Filter = "端口对应关系 (*.xls)|*.xls";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                using (ExcelHelper excelHelper = new ExcelHelper(openFileDialog1.FileName))
                {
                    // 创建datatable，将链路信息读取入表
                    DataTable dtNeXinXi = excelHelper.ExcelToDataTable("Sheet1", true, 0);
                    dtNeXinXi.TableName = "dtNeXinXi";
                    DataSetT.DS.Tables.Add(dtNeXinXi);
                    int number = dtNeXinXi.Rows.Count;
                    MessageBox.Show("已完成输入Excel " + number + "  条数据");
                }
            }
        }

        private void PTN_Load(object sender, EventArgs e)
        {
            DataSetT.DS = new DataSet();
        }

        private void bt_ReadLiuLiang_Click(object sender, EventArgs e)
        {
            //读取网管导出的二层链路数据
            string strPath = System.IO.Directory.GetCurrentDirectory();
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = strPath;
            openFileDialog1.Filter = "端口对应关系 (*.xls)|*.xls";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                using (ExcelHelper excelHelper = new ExcelHelper(openFileDialog1.FileName))
                {
                    // 创建datatable，将链路信息读取入表
                    DataTable LiuLiang = excelHelper.ExcelToDataTable("Sheet0", true, 0);
                    LiuLiang.TableName = "LiuLiang";
                    DataSetT.DS.Tables.Add(LiuLiang);
                    int number = LiuLiang.Rows.Count;
                    DataSetT.DS.Tables["LiuLiang"].Columns.Add("首网元", Type.GetType("System.String"));
                    DataSetT.DS.Tables["LiuLiang"].Columns.Add("网元顺序列表", Type.GetType("System.String"));
                    MessageBox.Show("已完成输入Excel " + number + "  条数据");
                }
            }
        }

        private void bt_ReadFiber_Click(object sender, EventArgs e)
        {
            //读取网管导出的二层链路数据
            string strPath = System.IO.Directory.GetCurrentDirectory();
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = strPath;
            openFileDialog1.Filter = "端口对应关系 (*.xls)|*.xls";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                using (ExcelHelper excelHelper = new ExcelHelper(openFileDialog1.FileName))
                {
                    // 创建datatable，将链路信息读取入表
                    DataTable Fiber = excelHelper.ExcelToDataTable("Sheet0", true, 0);
                    Fiber.TableName = "Fiber";
                    DataSetT.DS.Tables.Add(Fiber);
                    int number = Fiber.Rows.Count;
                    //增加监视列，监视是否完成内容查找
                    DataSetT.DS.Tables["Fiber"].Columns.Add("监视列Flag", Type.GetType("System.String"));
                    MessageBox.Show("已完成输入Excel " + number + "  条数据");
                }
            }
        }
        /// <summary>
        /// 查找环路并生成数据表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_CreatROle_Click(object sender, EventArgs e)
        {
            //将尾纤链接表宿网元复制至源网元
            DataTable dtFiber = DataSetT.DS.Tables["Fiber"].Copy();
            int NumFiber = dtFiber.Rows.Count;
            for (int i = 0; i < NumFiber; i++)
            {
                //DataRow dr = dtFiber.NewRow();
                string srcNeName = dtFiber.Rows[i]["宿网元"].ToString();
                string srcNePort = dtFiber.Rows[i]["宿端口"].ToString();
                dtFiber.Rows[i]["宿网元"] = dtFiber.Rows[i]["源网元"];
                dtFiber.Rows[i]["宿端口"] = dtFiber.Rows[i]["源端口"];
                dtFiber.Rows[i]["源网元"] = srcNeName;
                dtFiber.Rows[i]["源端口"] = srcNePort;
                //Console.Write(i);
                DataRow drtemp = dtFiber.Rows[i];
                DataSetT.DS.Tables["Fiber"].Rows.Add(drtemp.ItemArray);
            }
            //增加临时数据表，存放每个查找出来的网元

            //FindFirstNe(DataSetT.DS);
            //网元环路信息，网元命名为县区+第一个连接网元非汇聚设备
            DataTable dtHuanluTongJi = new DataTable();
            //dtHuanluTongJi.Columns.Add("序号", Type.GetType("System.String"));
            dtHuanluTongJi.Columns.Add("环路名称", Type.GetType("System.String"));
            dtHuanluTongJi.Columns.Add("网元名称", Type.GetType("System.String"));
            dtHuanluTongJi.Columns.Add("业务数量", Type.GetType("System.Int16"));
            dtHuanluTongJi.Columns.Add("所属子网", Type.GetType("System.String"));
            dtHuanluTongJi.TableName = "dtHuanluTongJi";
            //int NumNe = 0;
            DataSetT.DS.Tables.Add(dtHuanluTongJi);
           

            //通过汇聚设备，查询接入层设备
            for (int i = 0; i < DataSetT.DS.Tables["dtNeXinXi"].Rows.Count; i++)
            {
                string str_Huiju_Name = DataSetT.DS.Tables["dtNeXinXi"].Rows[i]["网元名称"].ToString();
                //判断网元类型是否为汇聚设备,接入设备包含1000
                if (IsHuiju(str_Huiju_Name))
                {
                    //查询汇聚网元相连的接入层网元
                    string strHuanlumingcheng = "";
                    find_Next(strHuanlumingcheng, str_Huiju_Name, DataSetT.DS);
                   // Console.WriteLine("网元信息表查询进度："+ str_Huiju_Name +"--数量---"+ i);
                }
            }
           // Console.WriteLine("完成"+ DataSetT.DS.Tables["dtHuanluTongJi"].Rows.Count);
            //查询业务信息
            #region //查询生成路径业务信息
            DataTable dt_Temp = DataSetT.DS.Tables["dtHuanluTongJi"];
            for (int i = 0; i < dt_Temp.Rows.Count; i++)
            {
                string NeName = dt_Temp.Rows[i]["网元名称"].ToString();
                DataRow[] drLuJingXinXi = DataSetT.DS.Tables["dt_Lujing"].Select("源端 like '" + NeName + "%' or 宿端 like '" + NeName + "%'");
                //Console.WriteLine(drLuJingXinXi.Length);
                dt_Temp.Rows[i]["业务数量"] = drLuJingXinXi.Length;
                //查询所属子网
                DataRow[] drDstType = DataSetT.DS.Tables["dtNeXinXi"].Select("网元名称= '" + NeName + "'");
                dt_Temp.Rows[i]["所属子网"] = drDstType[0]["所属子网"];
            }
            #endregion //查询生成路径业务信息

            #region //生成按照所属子网，环路名称生成路径信息文件
            //配置文件名称，需要按环路筛选后查询
            DataView view = new DataView(dt_Temp);
            DataTable DisOnly = view.ToTable(true, "环路名称");//得到目标
            //获取到环路名称唯一，获取所属子网
            string FileNameCreateTinm = "环路路径信息" + DateTime.Now.ToString("yyyyMMddHHmmss");
            for (int i = 0; i < DisOnly.Rows.Count; i++)
            {
                DataTable dt_HuanluTemp = DataSetT.DS.Tables["dt_Lujing"].Clone();
                string strHuanLuMingCheng = DisOnly.Rows[i]["环路名称"].ToString();
                //获取环路名称包含的网元名称
                DataRow[] drDstType = DataSetT.DS.Tables["dtHuanluTongJi"].Select("环路名称= '" + strHuanLuMingCheng + "'");
                for (int d = 0; d < drDstType.Length; d++)
                {
                    string strNename = drDstType[d]["网元名称"].ToString();
                    //获取该网元路径信息
                    DataRow[] drLuJingXinXi = DataSetT.DS.Tables["dt_Lujing"].Select("源端 like '" + strNename + "%' or 宿端 like '" + strNename + "%'");
                    for (int o = 0; o < drLuJingXinXi.Length; o++)
                    {
                        dt_HuanluTemp.Rows.Add(drLuJingXinXi[o].ItemArray);
                    }
                }
                //根据环路名称，获取所属子网

                //完成所有路径添加，生成Excel表格
                string dirDaoChu = FileNameCreateTinm + "/" + drDstType[0]["所属子网"].ToString();
                if (!Directory.Exists(dirDaoChu))//如果不存在就创建 dir 文件夹  
                {
                    Directory.CreateDirectory(dirDaoChu);
                }
                string FileName = dirDaoChu + "/" + strHuanLuMingCheng + ".xls";
                using (ExcelHelper excelHelper = new ExcelHelper(FileName))
                {
                    //FileStream fs = new FileStream(FileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                    //DataTable dt_TempLuJing = drLuJingXinXi.CopyToDataTable();
                    excelHelper.DataTableToExcel(dt_HuanluTemp, "业务信息", true);
                }
            }

            #endregion  //配置文件名称，需要按环路筛选后查询
            //修改数据表名称
            #region //汇总环路网元、业务信息汇总
            #region //环路数据汇总表
            DataTable dtHuanluHuiZong = new DataTable();
            //dtHuanluTongJi.Columns.Add("序号", Type.GetType("System.String"));
            dtHuanluHuiZong.Columns.Add("环路名称", Type.GetType("System.String"));
            dtHuanluHuiZong.Columns.Add("网元数量", Type.GetType("System.Int16"));
            dtHuanluHuiZong.Columns.Add("业务数量", Type.GetType("System.Int16"));
            dtHuanluHuiZong.Columns.Add("所属子网", Type.GetType("System.String"));
            dtHuanluHuiZong.TableName = "dtHuanluHuiZong";
            //int NumNe = 0;
            DataSetT.DS.Tables.Add(dtHuanluHuiZong);
            #endregion //环路数据汇总表
            for (int i = 0; i < DisOnly.Rows.Count; i++)
            {
                //获取环路名称
                string strHuanLuMingCheng = DisOnly.Rows[i]["环路名称"].ToString();
                //获取环路名称包含的网元名称
                DataRow[] drDstType = DataSetT.DS.Tables["dtHuanluTongJi"].Select("环路名称= '" + strHuanLuMingCheng + "'");
                //环路数据大于0
                if (drDstType.Length>0)
                {
                    //增加该环路统计信息至环路汇总表
                    DataRow drHuiZong = dtHuanluHuiZong.NewRow();
                    drHuiZong["环路名称"] = strHuanLuMingCheng;
                    drHuiZong["网元数量"] = drDstType.Length;
                    int CountLuJing = 0;
                    for (int t = 0; t < drDstType.Length; t++)
                    {
                        CountLuJing = CountLuJing + Convert.ToInt32(drDstType[t]["业务数量"].ToString());
                    }
                    drHuiZong["业务数量"] = CountLuJing;
                    drHuiZong["所属子网"] = drDstType[0]["所属子网"].ToString();
                    dtHuanluHuiZong.Rows.Add(drHuiZong);
                }
            }
            //DataSetT.DS.Tables.Add(dtHuanluHuiZong);
            DataSetT.DS.Tables["dtHuanluHuiZong"].TableName = "环路网元业务汇总表";
            #endregion
            DataSetT.DS.Tables["dtNeXinXi"].TableName ="网元信息报表";
            DataSetT.DS.Tables["Fiber"].TableName ="尾纤连接信息";
            DataSetT.DS.Tables["dt_Lujing"].TableName ="全网路径信息";
            DataSetT.DS.Tables["dtHuanluTongJi"].TableName ="环路统计信息";
            ExcelHelper.DataSetToExcel(DataSetT.DS, FileNameCreateTinm+"/环路信息汇总");

            MessageBox.Show("已完成SDH网络数据分析，请查看");
            //弹出生成脚本所在文件夹
            string strPath = System.IO.Directory.GetCurrentDirectory()+ @"\"+FileNameCreateTinm;

            System.Diagnostics.Process.Start("explorer.exe", strPath);
        }
        public static bool IsHuiju(string NeName)
        {
            bool IsHuiju = false;
            DataRow[] drDstType = DataSetT.DS.Tables["dtNeXinXi"].Select("网元名称= '" + NeName + "'");
            if (!drDstType[0]["网元类型"].ToString().Contains("1000"))
            {
                IsHuiju = true;
            }
            return IsHuiju;
        }
        public static DataRow[]  find_Next(string strHuanLuMingCheng,string strNeName,DataSet ds)
        {
            
            //添加第一个接入源网元
            DataRow[] drF = ds.Tables["Fiber"].Select("源网元= '" + strNeName + "'"); 
            if (drF.Length>0)
            {
                for (int e = 0; e < drF.Length; e++)
                {
                    string dstName = drF[e]["宿网元"].ToString();
                    //源网元为汇聚网元，宿网元不为汇聚网元，设置环路名称
                    if (IsHuiju(strNeName))
                    {
                        if (!IsHuiju(dstName))
                        {
                            strHuanLuMingCheng = "环路" + dstName;
                        }
                    }
                    
                    //宿网元不为汇聚网元，并且未被查询过------在临时数据表中添加该网元信息
                    //宿网元类型为接入层网元
                    if (!IsHuiju(dstName))
                    {
                        //尾纤连接未被标记
                        if (!drF[e]["监视列Flag"].ToString().Contains("OK"))
                        {
                            //设置标记列 OK
                            for (int k = 0; k < ds.Tables["Fiber"].Rows.Count; k++)
                            {
                                if (ds.Tables["Fiber"].Rows[k]["源网元"].ToString().Contains(strNeName))
                                {
                                    if (ds.Tables["Fiber"].Rows[k]["宿网元"].ToString().Contains(dstName))
                                    {
                                        ds.Tables["Fiber"].Rows[k]["监视列Flag"] = "OK";
                                    }
                                }
                            }

                            //查询是否添加，未添加过，则进行添加
                            DataRow[] drAAA = ds.Tables["dtHuanluTongJi"].Select("网元名称= '" + dstName + "'");
                            if (!(drAAA.Length > 0))
                            {
                                DataRow drm = DataSetT.DS.Tables["dtHuanluTongJi"].NewRow();
                                drm["环路名称"] = strHuanLuMingCheng;
                                drm["网元名称"] = dstName;
                                DataSetT.DS.Tables["dtHuanluTongJi"].Rows.Add(drm);
                                //Console.WriteLine("链路入库进度：" + dstName + "---数量--e-" + e);
                            }

                            find_Next(strHuanLuMingCheng, dstName, ds);
                        } 
                    }
                    //设置数据表dtHuanluTongJi 网元名称及环路名称
                }
            }
            return drF;
        }

    }
}
