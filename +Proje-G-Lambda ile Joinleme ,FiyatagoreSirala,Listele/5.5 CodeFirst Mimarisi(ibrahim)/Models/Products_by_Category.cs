using System;
using System.Collections.Generic;

namespace _5._5_CodeFirst_Mimarisi_ibrahim_.Models
{
    public partial class Products_by_Category
    {
        public string CategoryName { get; set; }
        public string ProductName { get; set; }
        public string QuantityPerUnit { get; set; }
        public Nullable<short> UnitsInStock { get; set; }
        public bool Discontinued { get; set; }
    }
}
