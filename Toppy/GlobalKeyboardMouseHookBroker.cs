﻿using Gma.System.MouseKeyHook;
using System;
using System.Threading;
using System.Windows.Forms;

namespace ToppyMcTopface
{
    public class GlobalKeyboardMouseHookBroker : IKeyboardEvents, IMouseEvents
    {
        private const int MaxWaitMs = 3000;

        private readonly Thread thread;
        private readonly ManualResetEventSlim hooked = new ManualResetEventSlim();

        private Exception ex;
        private IKeyboardMouseEvents hook;

        private GlobalKeyboardMouseHookBroker()
        {
            thread = new Thread(MyThread) { IsBackground = true, Priority = ThreadPriority.Highest };
        }

        private static GlobalKeyboardMouseHookBroker Factory()
        {
            var self = new GlobalKeyboardMouseHookBroker();

            self.thread.Start();

            if (!self.hooked.Wait(MaxWaitMs))
                throw new TimeoutException($"Hook did not succeed after {MaxWaitMs} ms elapsed.");

            if (self.ex != null)
                throw self.ex;

            return self;
        }

        private void MyThread()
        {
            hook = Hook.GlobalEvents();

            try
            {
                hook.KeyDown += (sender, e) => KeyDown?.Invoke(sender, e);
                hook.KeyPress += (sender, e) => KeyPress?.Invoke(sender, e);
                hook.KeyUp += (sender, e) => KeyUp?.Invoke(sender, e);

                hook.MouseMove += (sender, e) => MouseMove?.Invoke(sender, e);
                hook.MouseMoveExt += (sender, e) => MouseMoveExt?.Invoke(sender, e);
                hook.MouseClick += (sender, e) => MouseClick?.Invoke(sender, e);
                hook.MouseDown += (sender, e) => MouseDown?.Invoke(sender, e);
                hook.MouseDownExt += (sender, e) => MouseDownExt?.Invoke(sender, e);
                hook.MouseUp += (sender, e) => MouseUp?.Invoke(sender, e);
                hook.MouseUpExt += (sender, e) => MouseUpExt?.Invoke(sender, e);
                hook.MouseWheel += (sender, e) => MouseWheel?.Invoke(sender, e);
                hook.MouseWheelExt += (sender, e) => MouseWheelExt?.Invoke(sender, e);
                hook.MouseDoubleClick += (sender, e) => MouseDoubleClick?.Invoke(sender, e);
                hook.MouseDragStarted += (sender, e) => MouseDragStarted?.Invoke(sender, e);
                hook.MouseDragStartedExt += (sender, e) => MouseDragStartedExt?.Invoke(sender, e);
                hook.MouseDragFinished += (sender, e) => MouseDragFinished?.Invoke(sender, e);
                hook.MouseDragFinishedExt += (sender, e) => MouseDragFinishedExt?.Invoke(sender, e);
            }
            catch (Exception e)
            {
                ex = e;
            }

            hooked.Set();

            if (ex != null)
                return;

            Application.Run();
        }

        /// <summary>
        /// Gets the singleton <see cref="GlobalKeyboardMouseHookBroker"/> instance.
        /// </summary>
        public static Lazy<GlobalKeyboardMouseHookBroker> Instance { get; set; } = new Lazy<GlobalKeyboardMouseHookBroker>(Factory, LazyThreadSafetyMode.ExecutionAndPublication);

        public event KeyEventHandler KeyDown;
        public event KeyPressEventHandler KeyPress;
        public event KeyEventHandler KeyUp;

        public event MouseEventHandler MouseMove;
        public event EventHandler<MouseEventExtArgs> MouseMoveExt;
        public event MouseEventHandler MouseClick;
        public event MouseEventHandler MouseDown;
        public event EventHandler<MouseEventExtArgs> MouseDownExt;
        public event MouseEventHandler MouseUp;
        public event EventHandler<MouseEventExtArgs> MouseUpExt;
        public event MouseEventHandler MouseWheel;
        public event EventHandler<MouseEventExtArgs> MouseWheelExt;
        public event MouseEventHandler MouseDoubleClick;
        public event MouseEventHandler MouseDragStarted;
        public event EventHandler<MouseEventExtArgs> MouseDragStartedExt;
        public event MouseEventHandler MouseDragFinished;
        public event EventHandler<MouseEventExtArgs> MouseDragFinishedExt;
    }
}
