using Cureser.Forms;
using System;
using System.Windows.Forms;

namespace Cureser
{
    internal static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ExchangeProvider ex = new ExchangeProvider();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            UserContext userContext= new UserContext();
            Application.Run(new Login(userContext));
        }
    }
}
