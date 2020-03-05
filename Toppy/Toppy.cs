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

        private readonly GlobalKeyboardMouseHookBroker broker;
        private readonly GreatScaling scaling;

        private Point initialMousePosition;
        private bool clickThrough = true;
        private int interacting = 0;
        private bool enableUserClose = true;

        public Toppy(GlobalKeyboardMouseHookBroker broker)
        {
            this.broker = broker;

            InitializeComponent();

            scaling = new GreatScaling(this);
            scaling.PrepareFontScaling(header);
            scaling.PrepareFontScaling(footer);
            scaling.PrepareFontScaling(close);

            ResizeToMinimumHeight();
        }

        public Label Header => header;
        public Label Body => body;
        public Label Footer => footer;

        public bool EnableClickThrough { get; set; } = true;
        public bool EnableFormMove { get; set; } = true;

        public bool EnableUserClose
        {
            get => enableUserClose;
            set
            {
                enableUserClose = value;

                if (close.InvokeRequired)
                    close.Invoke(new MethodInvoker(UpdateUserCloseUi));
                else
                    UpdateUserCloseUi();
            }
        }

        public double OpacityWhenInteracting { get; set; } = 1.0;
        public double OpacityWhenNotInteracting { get; set; } = 0.8;

        public event EventHandler<UserInteractionEventArgs> UserInteraction;

        public void ResizeToMinimumHeight()
        {
            ClientSize = new Size(ClientSize.Width, header.Height + body.Height + footer.Height);
        }

        protected virtual void OnUserInteraction(UserInteractionType type)
        {
            UserInteraction?.Invoke(this, new UserInteractionEventArgs(type));
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
                components?.Dispose();
            }

            base.Dispose(disposing);
        }

        private void EnableInteraction(int type)
        {
            if (Interlocked.CompareExchange(ref interacting, type, 0) == 0)
            {
                OnUserInteraction(UserInteractionType.EnableInteraction);
                Opacity = OpacityWhenInteracting;
                clickThrough = false;
                UpdateStyles();
            }
        }

        private void DisableInteraction(int type)
        {
            if (Interlocked.CompareExchange(ref interacting, 0, type) == type)
            {
                OnUserInteraction(UserInteractionType.DisableInteraction);
                Opacity = OpacityWhenNotInteracting;
                clickThrough = EnableClickThrough;
                UpdateStyles();
            }
        }

        private void GlobalHookKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (InvokeRequired)
                {
                    Invoke(new KeyEventHandler(GlobalHookKeyDownImpl), sender, e);
                }
                else
                {
                    GlobalHookKeyDownImpl(sender, e);
                }
            }
            catch (ObjectDisposedException) { }
            catch { }
        }

        private void GlobalHookKeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (InvokeRequired)
                {
                    Invoke(new KeyEventHandler(GlobalHookKeyUpImpl), sender, e);
                }
                else
                {
                    GlobalHookKeyUpImpl(sender, e);
                }
            }
            catch (ObjectDisposedException) { }
            catch { }
        }

        private void GlobalHookMouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                if (InvokeRequired)
                {
                    Invoke(new MouseEventHandler(GlobalHookMouseMoveImpl), sender, e);
                }
                else
                {
                    GlobalHookMouseMoveImpl(sender, e);
                }
            }
            catch (ObjectDisposedException) { }
            catch { }
        }

        private void GlobalHookKeyDownImpl(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.LControlKey || e.KeyCode == Keys.RControlKey)
            {
                EnableInteraction(InteractionTypeKeyboard);
            }
        }

        private void GlobalHookKeyUpImpl(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.LControlKey || e.KeyCode == Keys.RControlKey)
            {
                initialMousePosition = default;
                DisableInteraction(InteractionTypeKeyboard);
            }
        }

        private void GlobalHookMouseMoveImpl(object sender, MouseEventArgs e)
        {
            if (!enableUserClose) return;

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

            Register();

            Opacity = OpacityWhenNotInteracting;

            UpdateUserCloseUi();
            ResizeToMinimumHeight();
        }

        private void UpdateUserCloseUi()
        {
            if (enableUserClose)
            {
                close.Enabled = true;
                close.BackColor = Color.FromArgb(200, 0, 0, 0);
                close.Text = "🗙";
            }
            else
            {
                close.Enabled = false;
                close.BackColor = header.BackColor;
                close.Text = "";
                DisableInteraction(InteractionTypeMouse);
            }
        }

        private void MoveForm(object sender, MouseEventArgs e)
        {
            if (EnableFormMove && e.Button == MouseButtons.Left && ModifierKeys.HasFlag(Keys.Control))
            {
                OnUserInteraction(UserInteractionType.Move);
                ((Control)sender).MoveFormFromChild();
            }
        }

        private void CloseClick(object sender, EventArgs e)
        {
            if (enableUserClose)
            {
                OnUserInteraction(UserInteractionType.Close);
                Close();
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            Unregister();
            base.OnClosing(e);
        }

        private void Register()
        {
            broker.KeyDown += GlobalHookKeyDown;
            broker.KeyUp += GlobalHookKeyUp;
            broker.MouseMove += GlobalHookMouseMove;
        }

        private void Unregister()
        {
            broker.KeyDown -= GlobalHookKeyDown;
            broker.KeyUp -= GlobalHookKeyUp;
            broker.MouseMove -= GlobalHookMouseMove;
        }
    }
}