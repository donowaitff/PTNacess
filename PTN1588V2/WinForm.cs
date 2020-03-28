using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PTN1588V2
{
    
    public partial class WinForm : Form
    {
        public WinForm()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 读取网元基本信息，并入库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_ReadNebase_Click(object sender, EventArgs e)
        {
            string[] strFileName = ReadExcelToDataSet.ReadOk("数据文件",8);
            if (strFileName.Length>0)
            {
                common.strNeBase = System.IO.Path.GetFileNameWithoutExtension(strFileName[0]);
                Console.WriteLine(common.strNeBase);
            }
           
        }

        private void WinForm_Load(object sender, EventArgs e)
        {
            //初始化DataSet数据
            common.DataSet = new DataSet();
            common.DS = new DataSet();
        }

        private void btn_readErCeng_Click(object sender, EventArgs e)
        {
            string[] strFileName = ReadExcelToDataSet.ReadOk("数据文件",8);
            if (strFileName.Length > 0)
            {
                common.strErCengLianLu = System.IO.Path.GetFileNameWithoutExtension(strFileName[0]);
                Console.WriteLine(common.strErCengLianLu);
            }
        }

        private void btn_readIP_Click(object sender, EventArgs e)
        {
            string[] strFileName = ReadExcelToDataSet.ReadOk("数据文件",1);
            if (strFileName.Length > 0)
            {
                common.strLteIp = System.IO.Path.GetFileNameWithoutExtension(strFileName[0]);
                Console.WriteLine(common.strLteIp);
            }
        }

        private void btn_readPw_Click(object sender, EventArgs e)
        {
            string[] strFileName = ReadExcelToDataSet.ReadOk("数据文件",1);
            if (strFileName.Length > 0)
            {
                common.strPw = System.IO.Path.GetFileNameWithoutExtension(strFileName[0]);
                Console.WriteLine(common.strPw);
            }
        }

        private void btn_WriteData_Click(object sender, EventArgs e)
        {
            string FileNameCreateTime = DateTime.Now.ToString("yyyyMMddHHmmss");
            //获取LTE业务需要的列-"网元*21", "端口*22","网元*12", "端口*13", "VLAN ID16"
            DataView dv = common.DataSet.Tables[common.strPw].DefaultView;
            DataTable dtLte = dv.ToTable(true, "网元*21", "端口*22", "VLAN ID16", "网元*12", "端口*13");
            //对dtLte进行处理：
            /**
             *网元*21	端口*22	网元*12	端口*13	VLAN ID16
             5134-潍坊通信三楼L2/L3	Global-VE0.99	427-潍坊高密黄金花园7900	1-TPA1EG24-16:1	682,1682
             * 
             **/
            for (int i = 0; i < dtLte.Rows.Count; i++)
            {
                string[] strL= dtLte.Rows[i]["网元*21"].ToString().Split('-');
                dtLte.Rows[i]["网元*21"] = strL[0];
                string[] strLA = dtLte.Rows[i]["端口*22"].ToString().Split('.');
                dtLte.Rows[i]["端口*22"] = strLA[0];
                string[] strLB = dtLte.Rows[i]["VLAN ID16"].ToString().Split(',');
                dtLte.Rows[i]["VLAN ID16"] = strLB[0];
            }

            //向数据规划表中加入两列网元*12	端口*13
            DataTable dtPw = common.DataSet.Tables[common.strLteIp].Copy();
            dtPw.Columns.Add("网元*12", typeof(string)); //数据类型为 文本
            dtPw.Columns.Add("端口*13", typeof(string)); //数据类型为 文本
            //DataRow[] drDstType = DataSetT.DS.Tables["dtNeXinXi"].Select("网元名称= '" + NeName + "'");
            for (int i = 0; i < dtPw.Rows.Count; i++)
            {
                for (int j = 0; j < dtLte.Rows.Count; j++)
                {
                    string strNe = dtLte.Rows[j]["网元*21"].ToString();
                    string strNePort = dtLte.Rows[j]["端口*22"].ToString();
                    string strNeA = dtLte.Rows[j]["网元*12"].ToString();
                    string strNeAPort = dtLte.Rows[j]["端口*13"].ToString();
                    string strVlan = dtLte.Rows[j]["VLAN ID16"].ToString();
                    DataRow[] drFind = dtLte.Select("''=''");
                    //DataRow[] drFind = dtPw.Select("二转三网元 = '" + strNe + "' AND 二转三接口 ='" + strNePort+ "' AND 业务Vlan ='"+ strVlan+"'");
                    if (dtPw.Rows[i]["二转三网元"].ToString()==strNe&& dtPw.Rows[i]["二转三接口"].ToString() == strNePort&& dtPw.Rows[i]["业务Vlan"].ToString()== strVlan)
                    {
                        dtPw.Rows[i]["网元*12"] = strNeA;
                        dtPw.Rows[i]["端口*13"] = strNeAPort;
                        Console.WriteLine(i+"-"+j);
                        break;
                    }
                }
            }
            ExcelHelper ex = new ExcelHelper("LTE业务输出"+ FileNameCreateTime + ".xlsx");
            ex.DataTableToExcel(dtPw, "LTE业务合并数据", true);
            //ExcelHelper.DataSetToExcel(common.DataSet,"导出数据"+ FileNameCreateTime);
        }

        private void btn_test_Click(object sender, EventArgs e)
        {
            string FileNameCreateTime = DateTime.Now.ToString("yyyyMMddHHmmss");
            //获取LTE业务需要的列-"网元*21", "端口*22","网元*12", "端口*13", "VLAN ID16"
            DataView dv = common.DataSet.Tables[common.strPw].DefaultView;
            DataTable dtLte = dv.ToTable(true, "网元*21", "端口*22", "VLAN ID16", "网元*12", "端口*13");
            //对dtLte进行处理：
            /**
             *网元*21	端口*22	网元*12	端口*13	VLAN ID16
             5134-潍坊通信三楼L2/L3	Global-VE0.99	427-潍坊高密黄金花园7900	1-TPA1EG24-16:1	682,1682
             * 
             **/
            for (int i = 0; i < dtLte.Rows.Count; i++)
            {
                string[] strL = dtLte.Rows[i]["网元*21"].ToString().Split('-');
                dtLte.Rows[i]["网元*21"] = strL[0];
                string[] strLA = dtLte.Rows[i]["端口*22"].ToString().Split('.');
                dtLte.Rows[i]["端口*22"] = strLA[0];
                string[] strLB = dtLte.Rows[i]["VLAN ID16"].ToString().Split(',');
                dtLte.Rows[i]["VLAN ID16"] = strLB[0];
            }

            //向数据规划表中加入两列网元*12	端口*13
            DataTable dtPw = common.DataSet.Tables[common.strLteIp].Copy();
            dtPw.Columns.Add("基站网元", typeof(string)); //数据类型为 文本
            dtPw.Columns.Add("基站端口", typeof(string)); //数据类型为 文本
            //修改dtlte列名
            dtLte.Columns["网元*21"].ColumnName = "二转三网元";
            dtLte.Columns["端口*22"].ColumnName = "二转三接口";
            dtLte.Columns["VLAN ID16"].ColumnName = "业务VLAN";
            dtLte.Columns["端口*13"].ColumnName = "基站端口";
            dtLte.Columns["网元*12"].ColumnName = "基站网元";
            //DataRow[] drDstType = DataSetT.DS.Tables["dtNeXinXi"].Select("网元名称= '" + NeName + "'");
            for (int i = 0; i < dtPw.Rows.Count; i++)
            {
                string strNe = dtPw.Rows[i]["二转三网元"].ToString();
                string strNePort = dtPw.Rows[i]["二转三接口"].ToString();
                string strVlan = dtPw.Rows[i]["业务VLAN"].ToString();
                DataRow[] drFind = dtLte.Select("二转三网元 = '" + strNe+ "' AND 二转三接口 = '" + strNePort + "' AND 业务VLAN = '" + strVlan +"'");
                if (drFind.Length>0)
                {
                    dtPw.Rows[i]["基站网元"] = drFind[0]["基站网元"];
                    dtPw.Rows[i]["基站端口"] = drFind[0]["基站端口"];
                }
                Console.WriteLine(i + "-行");
            }
            ExcelHelper ex = new ExcelHelper("LTE业务输出" + FileNameCreateTime + ".xlsx");
            ex.DataTableToExcel(dtPw, "LTE业务合并数据", true);
            //ExcelHelper.DataSetToExcel(common.DataSet,"导出数据"+ FileNameCreateTime);
        }
    }
    public static class common
    {
        //添加dataset
        public static DataSet DataSet;
        //输出dataset
        public static DataSet DS;
        public static string strNeBase;
        public static string strErCengLianLu;
        public static string strLteIp;
        public static string strPw;
    }
}
