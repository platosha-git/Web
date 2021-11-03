using System;
using System.Collections.Generic;

#nullable disable

namespace ToursWeb
{
    public partial class Food
    {
        public Food()
        {
            Tours = new HashSet<Tour>();
        }

        public int Foodid { get; set; }
        public string Category { get; set; }
        public bool? Vegmenu { get; set; }
        public bool? Childrenmenu { get; set; }
        public bool? Bar { get; set; }
        public int Cost { get; set; }

        public virtual ICollection<Tour> Tours { get; set; }
    }
}
