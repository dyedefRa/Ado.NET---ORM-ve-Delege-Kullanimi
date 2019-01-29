using System;
using System.Collections.Generic;

namespace _5._5_CodeFirst_Mimarisi_ibrahim_.Models
{
    public partial class Region
    {
        public Region()
        {
            this.Territories = new List<Territory>();
        }

        public int RegionID { get; set; }
        public string RegionDescription { get; set; }
        public virtual ICollection<Territory> Territories { get; set; }
    }
}
