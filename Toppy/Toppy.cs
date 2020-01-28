using Gma.System.MouseKeyHook;
using Mwfga;
using System;
using System.ComponentModel;
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
        private readonly GreatScaling scaling;

        private Point initialMousePosition;
        private bool clickThrough = true;
        private bool disableClose = true;
        private int interacting = 0;

        public Toppy()
        {
            InitializeComponent();

            scaling = new GreatScaling(this);
            scaling.PrepareFontScaling(header);
            scaling.PrepareFontScaling(footer);
            scaling.PrepareFontScaling(close);

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

            ResizeToMinimumHeight();
        }

        public Label Header => header;
        public Label Body => body;
        public Label Footer => footer;

        public bool EnableClickThrough { get; set; } = true;
        public bool EnableFormMove { get; set; } = true;
        public bool EnableUserClose { get; set; } = true;
        public double OpacityWhenInteracting { get; set; } = 1.0;
        public double OpacityWhenNotInteracting { get; set; } = 0.8;

        public event EventHandler<UserClosingEventArgs> UserClosing;
        public event EventHandler UserClosed;

        public void ResizeToMinimumHeight()
        {
            ClientSize = new Size(ClientSize.Width, header.Height + body.Height + footer.Height);
        }

        public void ForceClose()
        {
            disableClose = false;
            Close();
        }

        protected virtual void OnUserClosing()
        {
            var e = new UserClosingEventArgs();
            UserClosing?.Invoke(this, e);

            if (!e.Cancel)
            {
                ForceClose();
                OnUserClosed();
            }
        }

        protected virtual void OnUserClosed()
        {
            UserClosed?.Invoke(this, EventArgs.Empty);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (disableClose)
                e.Cancel = true;
        }

        protected override void OnDpiChanged(DpiChangedEventArgs e)
        {
            base.OnDpiChanged(e);
            scaling.ScaleFontAfterDpiChange();
            scaling.ScaleFormSizeConstraintsAfterDpiChange();
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
                Opacity = OpacityWhenInteracting;
                clickThrough = false;
                UpdateStyles();
            }
        }

        private void DisableInteraction(int type)
        {
            if (Interlocked.CompareExchange(ref interacting, 0, type) == type)
            {
                Opacity = OpacityWhenNotInteracting;
                clickThrough = EnableClickThrough;
                UpdateStyles();
            }
        }

        protected override bool ShowWithoutActivation => true;

        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;

                cp.SetTopMost(true);

                if (clickThrough && EnableClickThrough)
                {
                    cp.EnableClickThrough();
                }

                // Need to duplicate the designer properties here:
                cp.SetMaximizeBox(false);
                cp.SetMinimizeBox(false);

                return cp;
            }
        }

        private void ToppyLoad(object sender, EventArgs e)
        {
            initialMousePosition = MousePosition;
            DpiUtils.InitPerMonitorDpi(this);

            Opacity = OpacityWhenNotInteracting;
            close.Enabled = EnableUserClose;
            close.Visible = EnableUserClose;
            ResizeToMinimumHeight();
        }

        private void MoveForm(object sender, MouseEventArgs e)
        {
            if (EnableFormMove && e.Button == MouseButtons.Left && ModifierKeys.HasFlag(Keys.Control))
            {
                ((Control)sender).MoveFormFromChild();
            }
        }

        private void CloseClick(object sender, EventArgs e)
        {
            if (EnableUserClose)
                OnUserClosing();
        }
    }
}