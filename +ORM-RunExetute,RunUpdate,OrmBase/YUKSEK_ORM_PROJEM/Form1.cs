using ENTITY;
using ORM;
using System;
using System.Data;
using System.Windows.Forms;

namespace YUKSEK_ORM_PROJEM
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            kuls temp = new kuls();
            dataGridView1.DataSource = temp.RunSELECT();
           
        }
        private void Temizle1()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
        }
        private void Temizle2()
        {
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            kuls orm = new kuls();
            kul temp = new kul();
            temp.kulAdi = textBox1.Text;
            temp.kulSoyadi = textBox2.Text;
            temp.kulMaas = Convert.ToInt32(textBox3.Text);

            if (orm.RunINSERT(temp))
            {
                dataGridView1.DataSource = orm.RunSELECT();
                MessageBox.Show("Kayıt başarıyla eklendi...");
                Temizle1();
            }
            else
            {
                MessageBox.Show("Hata oluştu");
            }
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = dataGridView1.CurrentRow;
            int k = row.Index;

            textBox7.Text = row.Cells[0].Value.ToString();
            textBox6.Text = row.Cells[1].Value.ToString();
            textBox5.Text = row.Cells[2].Value.ToString();
            textBox4.Text = row.Cells[3].Value.ToString();

        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {

            try
            {
                DataGridViewRow row = dataGridView1.CurrentRow;
                int k = row.Index;

                textBox7.Text = row.Cells[0].Value.ToString();
                textBox6.Text = row.Cells[1].Value.ToString();
                textBox5.Text = row.Cells[2].Value.ToString();
                textBox4.Text = row.Cells[3].Value.ToString();

            }
            catch 
            {

                MessageBox.Show("Lütfen alan dışına çıkmayınız");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                int silinecekid = Convert.ToInt32(textBox7.Text);
                kuls ss = new kuls();
                if (ss.RunDELETE(silinecekid))
                {

                    MessageBox.Show("Silme işlemi başarıyla tamamlandı...");
                   dataGridView1.DataSource= ss.RunSELECT();
                    Temizle2();
                }
               

            }
            catch 
            {

                MessageBox.Show("HATA OLUŞTU | DENEMEDE SORUNVAR");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                kuls ss = new kuls();
                kul temp = new kul();
                temp.ID = Convert.ToInt32(textBox7.Text);
                temp.kulAdi = textBox6.Text;
                temp.kulSoyadi = textBox5.Text;
                temp.kulMaas = Convert.ToInt32(textBox4.Text);
                if (ss.RunUPDATE(temp))
                {
                   dataGridView1.DataSource= ss.RunSELECT();
                    MessageBox.Show("Güncelleme işlemi başarıyla tamamlandı...");
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Hata oluştu");
            }
        }

        kuls gg;
        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            if (gg==null)
            {
                gg = new kuls();
            }

            DataView dv = gg.RunSELECT().DefaultView;
            dv.RowFilter = $"kulAdi like '%{textBox8.Text}%'";
            dataGridView1.DataSource = dv;
        }
    }
}
