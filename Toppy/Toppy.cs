﻿using Gma.System.MouseKeyHook;
using Mwfga;
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
        private readonly GreatScaling scaling;

        private Point initialMousePosition;
        private bool clickThrough = true;
        private int interacting = 0;

        public Toppy()
        {
            InitializeComponent();

            scaling = new GreatScaling(this);
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
        }

        public Label Header => header;
        public Label Body => body;

        public bool EnableBodyTicker { get; set; } = true;
        public bool EnableClickThrough { get; set; } = true;
        public bool EnableFormMove { get; set; } = true;
        public double OpacityWhenInteracting { get; set; } = 1.0;
        public double OpacityWhenNotInteracting { get; set; } = 0.9;

        public event EventHandler<UserClosingEventArgs> UserClosing;
        public event EventHandler UserClosed;

        protected virtual void OnUserClosing()
        {
            var e = new UserClosingEventArgs();
            UserClosing?.Invoke(this, e);

            if (!e.Cancel)
            {
                Close();
                OnUserClosed();
            }
        }

        protected virtual void OnUserClosed()
        {
            UserClosed?.Invoke(this, EventArgs.Empty);
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
                body.TickerEnabled = false;
                Opacity = OpacityWhenInteracting;
                clickThrough = false;
                UpdateStyles();
            }
        }

        private void DisableInteraction(int type)
        {
            if (Interlocked.CompareExchange(ref interacting, 0, type) == type)
            {
                body.TickerEnabled = EnableBodyTicker;
                Opacity = OpacityWhenNotInteracting;
                clickThrough = EnableClickThrough;
                UpdateStyles();
            }
        }

        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;

                if (clickThrough && EnableClickThrough)
                {
                    cp.EnableClickThrough();
                }

                return cp;
            }
        }

        private void ToppyLoad(object sender, EventArgs e)
        {
            initialMousePosition = MousePosition;
            DpiUtils.InitPerMonitorDpi(this);

            body.TickerEnabled = EnableBodyTicker;
            Opacity = OpacityWhenNotInteracting;
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
            OnUserClosing();
        }
    }
}