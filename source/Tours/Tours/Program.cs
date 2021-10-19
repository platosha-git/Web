using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tours
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormAuth());
        }
    }
}

//Scaffold-DbContext "Host=localhost;Port=5432;Database=Tours;Username=postgres;Password=21rfrnec" Npgsql.EntityFrameworkCore.PostgreSQL
