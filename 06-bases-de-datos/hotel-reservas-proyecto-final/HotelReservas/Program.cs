using System;
using System.Windows.Forms;
using HotelReservas.Assets;

namespace HotelReservas
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            // Ensure Assets/logo.png and Assets/logo.ico exist next to the executable
            AppLogo.EnsureAssets();
            Application.Run(new LoginForm());
        }
    }
}