using System;
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
            Application.Run(new Toppy { StartPosition = FormStartPosition.CenterScreen });
        }
    }
}
