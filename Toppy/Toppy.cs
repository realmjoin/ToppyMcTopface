using System.Windows.Forms;
using Utilities;

namespace ToppyMcTopface
{
    public partial class Toppy : Form
    {
        private readonly GlobalKeyboardHook gkh = new GlobalKeyboardHook { HookedKeys = { Keys.LControlKey, Keys.RControlKey } };

        private bool clickThrough = true;

        public Toppy()
        {
            InitializeComponent();

            gkh.KeyDown += (sender, e) =>
            {
                body.TickerEnabled = false;
                ClickThrough = false;
            };

            gkh.KeyUp += (sender, e) =>
            {
                body.TickerEnabled = true;
                ClickThrough = true;
            };

            ContextMenu = new ContextMenu
            {
                MenuItems = {
                    { "Close", delegate { Close(); } },
                }
            };
        }

        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;

                if (ClickThrough)
                {
                    cp.EnableClickThrough();
                }

                return cp;
            }
        }

        public bool ClickThrough
        {
            get => clickThrough;
            set
            {
                clickThrough = value;
                UpdateStyles();
            }
        }

        private void MoveForm(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && ModifierKeys.HasFlag(Keys.Control))
            {
                this.MoveForm((Control)sender);
            }
        }
    }
}