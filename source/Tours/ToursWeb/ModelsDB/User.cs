using System;
using System.Collections.Generic;

#nullable disable

namespace ToursWeb
{
    public partial class User
    {
        public int Userid { get; set; }
        public int[] Toursid { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public int? Accesslevel { get; set; }
    }
}
