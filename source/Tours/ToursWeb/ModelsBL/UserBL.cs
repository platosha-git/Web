using System.Collections.Generic;
using System.Linq;
using ToursWeb.ModelsDB;

namespace ToursWeb.ModelsBL
{
    public class UserBL
    {
        public int Userid { get; set; }
        public List<int> Toursid { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public int? Accesslevel { get; set; }

        public UserBL() { }

        public UserBL(User user)
        {
            Userid = user.Userid;
            Toursid = user.Toursid.ToList();
            Login = user.Login;
            Password = user.Password;
            Accesslevel = user.Accesslevel;
        }

        public User GetUser()
        {
            User user = new User()
            {
                Userid = Userid,
                Toursid = Toursid.ToArray(),
                Login = Login,
                Password = Password,
                Accesslevel = Accesslevel
            };

            return user;
        }
    }
}