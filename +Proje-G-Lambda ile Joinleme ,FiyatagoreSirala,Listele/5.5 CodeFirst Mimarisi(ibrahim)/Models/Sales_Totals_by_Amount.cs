using System;
using System.Collections.Generic;

namespace _5._5_CodeFirst_Mimarisi_ibrahim_.Models
{
    public partial class Sales_Totals_by_Amount
    {
        public Nullable<decimal> SaleAmount { get; set; }
        public int OrderID { get; set; }
        public string CompanyName { get; set; }
        public Nullable<System.DateTime> ShippedDate { get; set; }
    }
}
