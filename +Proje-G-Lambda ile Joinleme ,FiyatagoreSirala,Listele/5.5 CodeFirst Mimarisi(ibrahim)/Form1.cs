using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _5._5_CodeFirst_Mimarisi_ibrahim_
{

    using Models;

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
      

        NorthwindContext ctx = new NorthwindContext();
        private void Form1_Load(object sender, EventArgs e)
        {
            //kayıt ıcın comboboxların ıcıne gereklı verılerı attıık
            ProductListele();

            cmbKategori.DataSource = ctx.Categories.ToList();
            cmbKategori.DisplayMember = "CategoryName";
            cmbKategori.ValueMember = "CategoryID";

            cmbTedarikci.DataSource = ctx.Suppliers.ToList();
            cmbTedarikci.DisplayMember = "CompanyName";
            cmbTedarikci.ValueMember = "SupplierID";
        }
        void   ProductListele()
        { //DIREK ctx.Products ı datagride baglasak Catid ve stock ıd ve daha bır cok salak sacma colum gelıyor.
            //amacım o categorı ıd ye karsılık gelen catnameyı ve stock ıd ye karsılı kgelen stock nameyı cekmek
            //o yuzden joın yaptım...
       

               var sonuc = ctx.Products.Join(ctx.Categories,
                pro => pro.CategoryID,
                cat => cat.CategoryID,
                (proo, catt) => new
                {

                    proo.ProductID,
                    proo.ProductName,
                    proo.UnitPrice,
                    proo.UnitsInStock,
                    catt.CategoryName,
                    proo.SupplierID,
                    proo.CategoryID


                }).Join(ctx.Suppliers,
                pvc => pvc.SupplierID,
                sup => sup.SupplierID,
                (all, last) => new
                {
                    all.ProductID,
                    all.ProductName,
                    all.UnitPrice,
                    all.UnitsInStock,
                    all.CategoryName,
                    last.CompanyName,
                    all.SupplierID,
                    all.CategoryID
                }).ToList();
            dataGridView1.DataSource = sonuc;
            dataGridView1.Columns["SupplierID"].Visible = false;
            dataGridView1.Columns["CategoryID"].Visible = false;
           
          
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
           
            Product temp = new Product();
            temp.ProductName = txtUrunAdi.Text;
            temp.UnitPrice = nudFiyat.Value;
            temp.UnitsInStock = Convert.ToInt16(nudStok.Value);
           
            temp.CategoryID =(int) cmbKategori.SelectedValue;
            temp.SupplierID = (int)cmbTedarikci.SelectedValue;
            ctx.Products.Add(temp);
            ctx.SaveChanges();
            ProductListele();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
            dataGridView1.DataSource = ctx.Products.Where(x => x.ProductName.Contains(textBox1.Text)).ToList();

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = ctx.Products.OrderBy(x => x.UnitPrice).ToList();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = ctx.Products.OrderByDescending(x => x.UnitPrice).ToList();
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            ProductListele();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RaporFormu frm = new RaporFormu();
            frm.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //ONEMLI SILME ISLEMINDEYKEN
            // ID YI ALIP O ID LI NESNEYI BULUP YAPIYORUZ @@
            //SINGLEORDEFAULT   OR FIRSTORDEFAULT
            DataGridViewRow row = dataGridView1.CurrentRow;

            int k = Convert.ToInt32(row.Cells[0].Value);

            Product temp = ctx.Products.SingleOrDefault(x => x.ProductID == k);
       ctx.Products.Remove(temp);

            ctx.SaveChanges();
            ProductListele();


        }

        private void silToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
            DialogResult dr = MessageBox.Show("Secili kayıt silinsin mi?", "Kayıt Sil", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
               

                Product silinecek = ctx.Products.FirstOrDefault(x => x.ProductID == id);
                ctx.Products.Remove(silinecek);
                ctx.SaveChanges();
                ProductListele();
            }
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = dataGridView1.CurrentRow;
            int k = row.Index;
            txtUrunAdi.Tag = row.Cells[0].Value;
            txtUrunAdi.Text = row.Cells["ProductName"].Value.ToString();
            nudFiyat.Value = Convert.ToDecimal(row.Cells["UnitPrice"].Value);
            nudStok.Value = Convert.ToDecimal(row.Cells[3].Value);
            string vav = row.Cells[4].Value.ToString();
            cmbKategori.SelectedValue =row.Cells["CategoryID"].Value;
            cmbTedarikci.SelectedValue = Convert.ToInt32(row.Cells["SupplierID"].Value);


        }

        private void button3_Click(object sender, EventArgs e)
        {
            Product temp = ctx.Products.SingleOrDefault(x => x.ProductID == (int)txtUrunAdi.Tag);
            temp.ProductName = txtUrunAdi.Text;
            temp.UnitPrice = (int)nudFiyat.Value;
            temp.UnitsInStock = (short)nudStok.Value;
            temp.CategoryID =(int) cmbKategori.SelectedValue;
            temp.SupplierID = (int)cmbTedarikci.SelectedValue;
                
            ctx.SaveChanges();
            ProductListele();


        }
    }
}
