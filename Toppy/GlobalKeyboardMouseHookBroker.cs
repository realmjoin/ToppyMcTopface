using Gma.System.MouseKeyHook;
using System;
using System.Windows.Forms;

namespace ToppyMcTopface
{
    public class GlobalKeyboardMouseHookBroker : IKeyboardEvents, IMouseEvents
    {
        private readonly IKeyboardMouseEvents hook = Hook.GlobalEvents();

        static GlobalKeyboardMouseHookBroker()
        {
            Instance = new GlobalKeyboardMouseHookBroker();
        }

        private GlobalKeyboardMouseHookBroker()
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

        /// <summary>
        /// Gets the singleton <see cref="GlobalKeyboardMouseHookBroker"/> instance. Make sure to call this on your main-thread the first time, so the hooks can be set up properly!
        /// </summary>
        public static GlobalKeyboardMouseHookBroker Instance { get; set; }

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
