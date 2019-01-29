using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LINQ.Lamda
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

          //Predicate<string> charmı = I => (I.Length == 1) ? true : false;
          //  string kk = "k";
          //  MessageBox.Show(charmı(kk).ToString());


        private void button1_Click(object sender, EventArgs e)
        {
            List<int> sayilarım = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
            int toplam = sayilarım.Sum();
            int Functoplam = sayilarım.Sum(new Func<int, int>(CiftSayilarınToplamı));
            int delegatetoplam = sayilarım.Sum(CiftSayilarınToplamı);
            int lamdatoplam = sayilarım.Sum(I => I % 2 == 0 ? I : 0);

            var SAS = from item in sayilarım where item % 2 == 0 select item;
            foreach (int çiftsayılar in SAS)
            {
                listBox1.Items.Add(çiftsayılar);
            }
        }
        static int CiftSayilarınToplamı(int I)
        {
            int toplamm = 0;
            if (I % 2 == 0)
            {
                toplamm += I;
            }
            return toplamm;
        }
    }
}
