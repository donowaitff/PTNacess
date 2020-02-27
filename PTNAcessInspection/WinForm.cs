using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;
namespace PTNAcessInspection
{
    public partial class WinForm : Form
    {
        public WinForm()
        {
            InitializeComponent();
        }

        private void btn_readMysql_Click(object sender, EventArgs e)
        {
            dgv.DataSource = MySqlHelper.GetDataSet(MySqlHelper.Conn, CommandType.Text, "SELECT * FROM weixin_nebase", null).Tables[0].DefaultView;
        }
    }
}
