using System;
using System.Threading.Tasks;
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
            };

            toppy.Top = 20;
            toppy.Left = (int)((Screen.PrimaryScreen.Bounds.Width - toppy.Width) / 2.0);

            // Sample for updating body content
            Task.Run(async () =>
            {
                await Task.Delay(1000);

                toppy.Invoke((MethodInvoker)delegate
                {
                    toppy.Body.Text = toppy.Body.Text + Environment.NewLine + toppy.Body.Text;
                });

                await Task.Delay(500);

                toppy.Invoke((MethodInvoker)delegate
                {
                    toppy.ResizeToMinimumHeight();
                });
            });

            Application.Run(toppy);
        }
    }
}
