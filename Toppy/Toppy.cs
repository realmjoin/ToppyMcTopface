using Gma.System.MouseKeyHook;
using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace ToppyMcTopface
{
    public partial class Toppy : Form
    {
        private const int InteractionTypeKeyboard = 1;
        private const int InteractionTypeMouse = 2;

        private readonly IKeyboardMouseEvents gkh = Hook.GlobalEvents();

        private Point initialMousePosition;
        private bool clickThrough = true;
        private int interacting = 0;

        public Toppy()
        {
            InitializeComponent();

            gkh.KeyDown += (sender, e) =>
            {
                if (e.KeyCode == Keys.LControlKey || e.KeyCode == Keys.RControlKey)
                {
                    EnableInteraction(InteractionTypeKeyboard);
                }
            };

            gkh.KeyUp += (sender, e) =>
            {
                if (e.KeyCode == Keys.LControlKey || e.KeyCode == Keys.RControlKey)
                {
                    initialMousePosition = default;
                    DisableInteraction(InteractionTypeKeyboard);
                }
            };

            gkh.MouseMove += (sender, e) =>
            {
                var target = new Rectangle(close.PointToScreen(Point.Empty), close.Size);
                var hit = target.Contains(e.Location);

                if (hit)
                {
                    if (!target.Contains(initialMousePosition))
                    {
                        EnableInteraction(InteractionTypeMouse);
                    }
                }
                else
                {
                    initialMousePosition = default;
                    DisableInteraction(InteractionTypeMouse);
                }
            };
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                gkh?.Dispose();
                components?.Dispose();
            }

            base.Dispose(disposing);
        }

        private void EnableInteraction(int type)
        {
            if (Interlocked.CompareExchange(ref interacting, type, 0) == 0)
            {
                body.TickerEnabled = false;
                Opacity = 1;
                clickThrough = false;
                UpdateStyles();
            }
        }

        private void DisableInteraction(int type)
        {
            if (Interlocked.CompareExchange(ref interacting, 0, type) == type)
            {
                body.TickerEnabled = true;
                Opacity = 0.85;
                clickThrough = true;
                UpdateStyles();
            }
        }

        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;

                if (clickThrough)
                {
                    cp.EnableClickThrough();
                }

                return cp;
            }
        }

        private void ToppyLoad(object sender, EventArgs e)
        {
            initialMousePosition = MousePosition;
        }

        private void MoveForm(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && ModifierKeys.HasFlag(Keys.Control))
            {
                this.MoveForm((Control)sender);
            }
        }

        private void CloseClick(object sender, EventArgs e)
        {
            Close();
        }
    }
}