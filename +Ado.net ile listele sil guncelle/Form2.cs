using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace _4._2_ADO.NET_DISCONNETED_MIMARI
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        SqlConnection baglan = new SqlConnection(@"Data Source=BAKIOZTURK\SQLSERVER;Initial Catalog=silebilirsin;Integrated Security=True");

        //DATASET DISPLAYMEMBER CMBYE AT,
        private void Form2_Load(object sender, EventArgs e)
        {
            SqlDataAdapter da = new SqlDataAdapter("Select * from bu Select * from bu2", baglan);
            DataSet ds = new DataSet();
            da.Fill(ds);
            comboBox1.DataSource = ds.Tables[0];
            comboBox1.DisplayMember = "KitapAd";
            comboBox2.DataSource = ds.Tables[1];
            comboBox2.DisplayMember = "kitapadi";
            comboBox1.ValueMember = "KitapNo";
            comboBox2.ValueMember = "KitapRenk";

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            MessageBox.Show(comboBox1.SelectedValue.ToString());
        }
    }
}
