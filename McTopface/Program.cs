using System;
using System.Threading;
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

            for (int i = 0; i < 3; i++)
            {
                var toppy = new Toppy(GlobalKeyboardMouseHookBroker.Instance)
                {
                    Text = $"Toppy {i}",
                    StartPosition = FormStartPosition.Manual,
                    Header = { Text = $"ToppyMcTopface demo {i}" },
                    Body = { Text = "Omnis quis laboriosam atque est. Explicabo omnis et dolor totam accusamus est vol uptatem. Sint et possimus repellat dolorem." },
                };

                toppy.Body.LinkClicked += Body_LinkClicked;

                toppy.Body.Links.Add(6, 4, "quis");
                toppy.Body.Links.Add(22, 5, "atque");

                toppy.Top = 20 + 200 * i;
                toppy.Left = (int)((Screen.PrimaryScreen.Bounds.Width - toppy.Width) / 2.0);

                var thread = new Thread(() => Application.Run(toppy));
                thread.Start();
            }

            Application.Run();
        }

        private static void Body_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (e.Link.LinkData is string value)
            {
                MessageBox.Show(value);
            }
        }
    }
}
