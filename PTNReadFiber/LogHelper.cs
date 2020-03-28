using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace PTNReadFiber
{
    /// <summary>
    /// 枚举：存储命令返回值错误信息
    /// </summary>
    public enum CmdErrCode
    {
    failed,
    EVE,
    EVT,
    Unregistered,
    Missing,
    }

    public static class LogHelper
    {

        /// <summary>
        /// 判断命令是否执行正确
        /// </summary>
        /// <param name="CmdRtnInfo">命令返回值</param>
        /// <returns></returns>
        public static bool g_CmdErrCode(string CmdRtnInfo)
        {
            bool CmdErr = false;
            foreach (string item in Enum.GetNames(typeof(CmdErrCode)))
            {
                if (CmdRtnInfo.Contains(item))
                {
                    CmdErr = true;
                    break;
                }
            }
            return CmdErr;
        }
        /// <summary>
        /// 判断是否为命令返回值结尾
        /// </summary>
        /// <param name="CmdRtnInfo"></param>
        /// <returns></returns>
        public static bool CmdRtnEnd(string CmdRtnInfo)
        {
            bool End = false;
            if (!g_CmdErrCode(CmdRtnInfo))
            {
                if (CmdRtnInfo.Contains("Total records") || CmdRtnInfo.Contains("#"))
                {
                    End = true;
                }
            }
            //报错时，直接设置为命令结束。
            else
            {
                End = true;
            }
            return End;
        }
        /// <summary>
        /// 返回命令执行结果
        /// </summary>
        /// <param name="sr"></param>
        /// <returns></returns>
        public static StringBuilder CmdRtnInfo(string CmdRtnLine)
        {
            StringBuilder sb = new StringBuilder();
            while (!CmdRtnEnd(CmdRtnLine))
            {
                sb.Append(CmdRtnLine);

            }
            return sb;
        }
        /// <summary>
        /// 判断找到命令
        /// </summary>
        /// <param name="CmdStr"></param>
        /// <param name="line"></param>
        /// <returns></returns>
        public static bool CmdStart(string CmdStr, string line)
        {
            bool Cmdstart = false;
            if (line.Contains(CmdStr))
            {
                Cmdstart = true;
            }
            return Cmdstart;
        }
        /// <summary>
        /// 将命令返回值写入数据库
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="tbl"></param>
        /// <returns></returns>
        public static bool CmdRtnToDataTable(StringBuilder sb, DataTable tbl, string Neid)
        {
            bool IsSucess = true;
            string str = sb.ToString();
            string[] Line = str.Split('\n');
            string[] stringSeparators = new string[] { " " };

            foreach (string item in Line)
            {
                //string[] CmdRtn = item.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
                //string Experssion=@" "+;
                string item1 = item.Trim();
                string[] CmdRtn = System.Text.RegularExpressions.Regex.Split(item1, @" [ ]+");

                string regstr = @"^(\d+-*\d*)";
                if (CmdRtn.Length == (tbl.Columns.Count - 1))
                {
                    if (Regex.IsMatch(CmdRtn[0], regstr))
                    {
                        DataRow row = tbl.NewRow();
                        row[0] = Neid;

                        for (int i = 0; i < CmdRtn.Length; i++)
                        {
                            row[i + 1] = CmdRtn[i];
                        }
                        tbl.Rows.Add(row);
                    }
                }

            }

            return IsSucess;
        }

        /// <summary>
        /// 只处理带有字符文本返回值
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="tbl"></param>
        /// <param name="Neid"></param>
        /// <param name="OK"></param>
        /// <returns></returns>
        public static bool CmdRtnToDataTable(StringBuilder sb, DataTable tbl, string Neid, bool OK)
        {
            bool IsSucess = true;
            string str = sb.ToString();
            string[] Line = str.Split('\n');
            string[] stringSeparators = new string[] { " " };

            foreach (string item in Line)
            {
                string CmdRtn = item.Trim();

                DataRow row = tbl.NewRow();
                row[0] = Neid;
                row[1] = CmdRtn;
                tbl.Rows.Add(row);
            }

            return IsSucess;
        }

/// <summary>
/// 将读取到到的命令返回值，写入到datatable中；可能包含空格列
/// </summary>
/// <param name="sb">读取到的字符</param>
/// <param name="tbl">写入的datatable</param>
/// <param name="s">可能包含空格列</param>
/// <returns></returns>
        public static bool CmdRtnToDataTableSpace(StringBuilder sb, DataTable tbl,int s)
        {
            bool IsSucess = true;
            string str = sb.ToString();
            string[] Line = str.Split('\n');
            string[] stringSeparators = new string[] { " " };

            foreach (string item in Line)
            {
                string[] CmdRtn = item.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
                DataRow row = tbl.NewRow();
                //分割后列数相同，证明无空格
                if (CmdRtn.Length == tbl.Columns.Count)
                {
                    for (int i = 0; i < CmdRtn.Length; i++)
                    {
                        row[i] = CmdRtn[i].ToString().Trim();
                    }
                    tbl.Rows.Add(row);
                }
                //存在空格
                if (CmdRtn.Length > tbl.Columns.Count)
                {
                    for (int i = 0; i <=s; i++)
                    {
                        row[i] = CmdRtn[i];
                    }
                    //row[s] = CmdRtn[s];
                    for (int i = 0; i<(CmdRtn.Length-tbl.Columns.Count); i++)
                    {
                        row[s] = row[s] + " " + CmdRtn[s+1+ i].ToString().Trim();
                    }
                    //定义数据表列数
                    int j = tbl.Columns.Count;
                    for (int i = 1; i < j-s; i++)
                    {
                        row[j-i] = CmdRtn[CmdRtn.Length-i].ToString().Trim();
                    }
                    tbl.Rows.Add(row);
                }                
            }
            return IsSucess;
        }




        /// <summary>
        /// 将无文本返回命令加入到相应Table中
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="tbl"></param>
        /// <returns></returns>
        public static bool CmdRtnToDataTableNoKong(StringBuilder sb, DataTable tbl)
        {
            bool IsSucess = true;
            string str = sb.ToString();
            string[] Line = str.Split('\n');
            string[] stringSeparators = new string[] { " " };
            foreach (string item in Line)
            {
                string[] CmdRtn = item.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
                DataRow row = tbl.NewRow();
                if (CmdRtn.Length == tbl.Columns.Count)
                {
                    for (int i = 0; i < CmdRtn.Length; i++)
                    {
                        row[i] = CmdRtn[i].ToString().Trim();
                    }
                    tbl.Rows.Add(row);
                }
            }
            return IsSucess;
        }

        /// <summary>
        /// 将命令返回值写入到数据表中，对Eline业务中端口独占，需进行加入all
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="tbl"></param>
        /// <returns></returns>
        public static bool CmdRtnToDataTableEine(StringBuilder sb, DataTable tbl)
        {
            bool IsSucess = true;
            string str = sb.ToString();
            string[] Line = str.Split('\n');
            string[] stringSeparators = new string[] { " " };
            foreach (string item in Line)
            {
                string[] CmdRtn = item.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
                DataRow row = tbl.NewRow();
                if (CmdRtn.Length == tbl.Columns.Count)
                {
                    for (int i = 0; i < CmdRtn.Length; i++)
                    {
                        row[i] = CmdRtn[i];
                    }
                    tbl.Rows.Add(row);
                }
                if (CmdRtn.Length +1== tbl.Columns.Count)
                {
                    for (int i = 0; i < CmdRtn.Length; i++)
                    {
                        row[i] = CmdRtn[i];
                    }
                    row[tbl.Columns.Count-1] = "all";
                    tbl.Rows.Add(row);
                }
            }
            return IsSucess;
        }

        /// <summary>
        /// d列不包含Nostril字符串的结果入库
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="tbl"></param>
        /// <param name="Neid"></param>
        /// <param name="d"></param>
        /// <returns></returns>
        public static bool CmdRtnToDataTable(StringBuilder sb, DataTable tbl, string Neid, int d,string Nostr)
        {
            bool IsSucess = true;
            string str = sb.ToString();
            string[] Line = str.Split('\n');
            string[] stringSeparators = new string[] { " " };

            foreach (string item in Line)
            {
                //string[] CmdRtn = item.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
                //
                string item1 = item.Trim();
                string[] CmdRtn = System.Text.RegularExpressions.Regex.Split(item1, @" [ ]+");
                string regstr = @"^(\d+-*\d*)";
                if (CmdRtn.Length == (tbl.Columns.Count - 1))
                {
                    if (Regex.IsMatch(CmdRtn[0], regstr))
                    {
                        DataRow row = tbl.NewRow();
                        row[0] = Neid;
                        if (!CmdRtn[d].ToString().Contains(Nostr))
                        {
                            for (int i = 0; i < CmdRtn.Length; i++)
                            {
                                row[i + 1] = CmdRtn[i];
                            }
                            tbl.Rows.Add(row);
                        }

                    }
                }
            }

            return IsSucess;
        }



        /// <summary>
        /// 将带有16进制ID的转换入库
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="tbl"></param>
        /// <param name="Neid"></param>
        /// <returns></returns>
        public static bool CmdRtnToDataTable(StringBuilder sb, DataTable tbl, string Neid,int x1,int x2)
        {
            bool IsSucess = true;
            string str = sb.ToString();
            string[] Line = str.Split('\n');
            string[] stringSeparators = new string[] { " " };

            foreach (string item in Line)
            {
                //string[] CmdRtn = item.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
                //string Experssion=@" "+;
                string item1 = item.Trim();
                string[] CmdRtn = System.Text.RegularExpressions.Regex.Split(item1, @" [ ]+");

                string regstr = @"^(\d+-*\d*)";
                if (CmdRtn.Length == (tbl.Columns.Count - 1))
                {
                    if (Regex.IsMatch(CmdRtn[0], regstr))
                    {
                        DataRow row = tbl.NewRow();
                        row[0] = Neid;

                        for (int i = 0; i < CmdRtn.Length; i++)
                        {
                            string strl=CmdRtn[i];
                            if (i==x1||i==x2)
                            {
                              //0x000a025e  
                                //string str1 = CmdRtn[i].Substring(3, 5);
                                string str2 = CmdRtn[i].Substring(6, 4);
                                strl = Convert.ToInt32(str2, 16).ToString();

                            }
                            row[i + 1] = strl;
                        }
                        tbl.Rows.Add(row);
                    }
                }

            }

            return IsSucess;
        }







        

    }
}
