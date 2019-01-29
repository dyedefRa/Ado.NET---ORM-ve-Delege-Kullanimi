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
    //MODELS DOSYASININ GELMESINI ISTIYORUM O YUZDEN NAME SPACENIN ALTINDA YAZIZYORUM BUNU.
    public partial class RaporFormu : Form
    {
        public RaporFormu()
        {
            InitializeComponent();
        }



        NorthwindContext ctx = new NorthwindContext();


        private void button1_Click(object sender, EventArgs e)
        {
            /*--Sorguda hangi kelimesi geçerse group by yapıyoruz..

            --Hangi üründen toplamda kaç dolarlık ve kaç adet satış yapılmıştır?

            SQLKODU

            Select p.ProductName,Sum(od.Quantity*od.UnitPrice*(1-od.Discount)) as SatisTutari,
            Sum(od.Quantity) as ToplamSatisAdeti  from Products p 
            left join [Order Details] od
            on p.ProductID=od.ProductID
            group by p.ProductName

       Amacım bu sorguyu Code First ile yapmak.
        Yani group by lar sum lar felan..
       */

            dataGridView1.DataSource = ctx.Products.Join(ctx.Order_Details,
                pro => pro.ProductID,
                ord => ord.ProductID,
                (proo, ordd) => new
                {
                    proo.ProductName,
                    ordd.Quantity,
                    ordd.UnitPrice
                }).GroupBy(x => x.ProductName).Select(x => new
                {
                    x.Key,
                    ToplamDolar = x.Sum(y => y.Quantity * y.UnitPrice),
                    ToplamSatis = x.Sum(y => y.Quantity)

                }).ToList();


        }

        private void button2_Click(object sender, EventArgs e)
        {
            /*--Hangi tedarikçiden , hangi kategoriden toplamda kaç dolarlık ve toplamda kaç adet satıs yapılmıstır.
           --CompanyName gozukcek , CategoryName gozukcek, ve order detailsten gerekli verileri cekıp ıslem yapılacak
           -- Companyname supplierda ,categoryname catogoride, dıgerlerı order detail de
           -- Product ıle dıgerlerı gecıs yaptık yoksa ıdlerı eıstleyemeyız..

              SQLKODU
           Select s.CompanyName,c.CategoryName,Sum(od.Quantity*od.UnitPrice),Sum(od.Quantity) 
           from Suppliers s left join Products p  on 
           s.SupplierID=p.SupplierID
            left join Categories c on
             p.CategoryID= c.CategoryID 
             left join [Order Details] od on
             p.ProductID=od.ProductID GROUP BY s.CompanyName,c.CategoryName
             */

            dataGridView2.DataSource = ctx.Products.Join(ctx.Order_Details,
                pro => pro.ProductID,
                ord => ord.ProductID,
                (proo, ordd) => new
                {
                    proo.CategoryID,
                    proo.SupplierID,
                    ordd.Quantity,
                    ordd.UnitPrice

                }).Join(ctx.Categories,
                pvo => pvo.CategoryID,
                cat => cat.CategoryID,
                (pvoo, catt) => new
                {
                    pvoo.Quantity,
                    pvoo.UnitPrice,
                    pvoo.SupplierID,
                    catt.CategoryName
                }
                ).Join(ctx.Suppliers,
                all => all.SupplierID,
                sup => sup.SupplierID,
                (al, supp) => new
                {
                    al.CategoryName,
                    supp.CompanyName,
                    al.UnitPrice,
                    al.Quantity
                }).GroupBy(x => new
                {
                    x.CategoryName,
                    x.CompanyName
                }).Select(x => new
                {
                    x.Key.CategoryName,
                    x.Key.CompanyName,
                    ToplamKazanc = x.Sum(y => y.Quantity * y.UnitPrice),
                    SatılanAdet = x.Sum(y => y.Quantity)
                }).ToList();























            dataGridView1.DataSource = ctx.Suppliers.Join(ctx.Products,
                sup => sup.SupplierID,
                pro => pro.SupplierID,
                (supp, proo) => new
                {
                    supp.CompanyName,
                    proo.CategoryID,
                    proo.ProductID
                }
                ).Join(ctx.Categories,
                svp => svp.CategoryID,
                cat => cat.CategoryID,
                (svpp, catt) => new
                {
                    svpp.CompanyName,
                    catt.CategoryName,
                    svpp.ProductID
                }).Join(ctx.Order_Details,
                alayi => alayi.ProductID,
                ord => ord.ProductID,
                (hepsi, ordd) => new
                {
                    hepsi.CategoryName,
                    hepsi.CompanyName,
                    ordd.Quantity,
                    ordd.UnitPrice
                }).GroupBy(x => new { x.CompanyName, x.CategoryName }).Select(x => new
                {
                    x.Key.CategoryName,
                    x.Key.CompanyName,
                   
                    ToplamKazanc = x.Sum(y => y.Quantity * y.UnitPrice),
                    ToplamSayi = x.Sum(y => y.Quantity),
                }).ToList();

            dataGridView1.Columns["ToplamKazanc"].HeaderText = "Toplam Kazanc";

            //-----------------------

            dataGridView2.DataSource = ctx.Products.Join(ctx.Order_Details, pro => pro.ProductID,
                ord => ord.ProductID,
                (proo, ordd) => new
                {
                    proo.Category,
                    proo.Supplier,
                    ordd.UnitPrice,
                    ordd.Quantity

                })


            .GroupBy(x => new { x.Category.CategoryName, x.Supplier.CompanyName }).Select(x => new
            {
                x.Key.CategoryName,
                x.Key.CompanyName,
                ToplamKazanc = x.Sum(y => y.Quantity * y.UnitPrice),
                ToplamSatıs = x.Sum(y => y.Quantity)
            }).ToList();


        }
    }
}
