#nullable disable

namespace ToursWeb.ModelsDB
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
