using System;
using System.Drawing;
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
                Body = { Text = "Omnis quis laboriosam atque est. Explicabo omnis et dolor totam accusamus est vol uptatem. Sint et possimus repellat dolorem." },
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
