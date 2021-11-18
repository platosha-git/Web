using System.Collections.Generic;
using System.ComponentModel;

#nullable disable

namespace ToursWeb.ModelsDB
{
    public enum FMenu
    {
        Vegeterian,
        Children,
        Dietary
    };
    
    public partial class Food
    {
        public Food()
        {
            Tours = new HashSet<Tour>();
        }

        public int Foodid { get; set; }
        public string Category { get; set; }
        public string Menu { get; set; }
        public bool? Bar { get; set; }
        public int Cost { get; set; }

        public virtual ICollection<Tour> Tours { get; set; }
    }
}
