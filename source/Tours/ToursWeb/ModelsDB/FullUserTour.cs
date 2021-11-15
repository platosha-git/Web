using Microsoft.EntityFrameworkCore;
using System;

namespace ToursWeb.ModelsDB
{
    [Keyless]
    public class FullUserTour
    {
        public int tourid { get; set; }
        public string city { get; set; }
	    public string name { get; set; }
        public string type { get; set; }
        public string category { get; set; }
        public int transfer { get; set; }
        public int cost { get; set; }
        public DateTime datebegin { get; set; }
        public DateTime dateend { get; set; }
    }
}
