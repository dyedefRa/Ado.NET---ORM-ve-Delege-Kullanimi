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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection baglan = new SqlConnection(@"Data Source=BAKIOZTURK\SQLSERVER;Initial Catalog=silebilirsin;Integrated Security=True");
        private void button1_Click(object sender, EventArgs e)
        {

        }
        DataTable dt;
        private void Form1_Load(object sender, EventArgs e)
       {
            SqlDataAdapter da = new SqlDataAdapter("Select * from bu", baglan);

           dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;

            SqlDataAdapter dat = new SqlDataAdapter("Select * from bu", baglan);
            DataTable ddt = new DataTable();
            dat.Fill(ddt);
            listBox1.DataSource = ddt;
            listBox1.DisplayMember = "KitapYayinEvi";
            listBox1.ValueMember = "KitapNo";
            
        }

        private void listBox1_Click(object sender, EventArgs e)
        {
            int k =(int) listBox1.SelectedValue;
            SqlDataAdapter dd = new SqlDataAdapter("Select KitapAd from bu where KitapNo=@k1", baglan);
            dd.SelectCommand.Parameters.AddWithValue("@k1", k);
            DataTable dtt = new DataTable();
            dd.Fill(dtt);
            dataGridView1.DataSource = dtt;

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //string gelen = listBox1.SelectedItem.ToString();
            //SqlDataAdapter son = new SqlDataAdapter($"select KitapAd from bu where KitapYayinEvi ={gelen}", baglan);


            //DataTable sont = new DataTable();
            //son.Fill(sont);
            //dataGridView1.DataSource = sont;
            
        }
    }
}
