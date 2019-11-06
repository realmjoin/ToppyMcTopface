using System;
using System.Drawing;
using System.Windows.Forms;

namespace ToppyMcTopface
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Console.WriteLine("ToppyMcTopface demo app");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var toppy = new Toppy
            {
                StartPosition = FormStartPosition.CenterScreen,
                Header = { Text = "ToppyMcTopface demo app" },
                Body = { BackColor = Color.Sienna }
            };

            Application.Run(toppy);
        }
    }
}
