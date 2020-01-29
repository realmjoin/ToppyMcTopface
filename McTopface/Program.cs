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

            var toppy = new Toppy
            {
                StartPosition = FormStartPosition.Manual,
                Header = { Text = "ToppyMcTopface demo app" },
                Body = { Text = "Omnis quis laboriosam atque est. Explicabo omnis et dolor totam accusamus est vol uptatem. Sint et possimus repellat dolorem." },
            };

            toppy.Top = 20;
            toppy.Left = (int)((Screen.PrimaryScreen.Bounds.Width - toppy.Width) / 2.0);

            Application.Run(toppy);
        }
    }
}
