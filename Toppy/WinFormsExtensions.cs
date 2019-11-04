using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace ToppyMcTopface
{
    internal static class WinFormsExtensions
    {
        public static void EnableClickThrough(this CreateParams cp)
        {
            cp.ExStyle |= Native.WS_EX_LAYERED | Native.WS_EX_TRANSPARENT;
        }

        public static void MoveForm(this Form target, Control sender)
        {
            sender.Capture = false;
            target.SendMessage(Native.WM_NCLBUTTONDOWN, Native.HT_CAPTION, null);
        }

        public static IntPtr SendMessage(this Control control, int msg, IntPtr wParam, StringBuilder lParam)
        {
            if (!control.IsHandleCreated || control.IsDisposed) return default;
            return Native.SendMessage(control.Handle, msg, wParam, lParam);
        }

        public static IntPtr SendMessage(this Control control, int msg, IntPtr wParam, [MarshalAs(UnmanagedType.LPWStr)] string lParam)
        {
            if (!control.IsHandleCreated || control.IsDisposed) return default;
            return Native.SendMessage(control.Handle, msg, wParam, lParam);
        }

        public static IntPtr SendMessage(this Control control, int msg, int wParam, [MarshalAs(UnmanagedType.LPWStr)] string lParam)
        {
            if (!control.IsHandleCreated || control.IsDisposed) return default;
            return Native.SendMessage(control.Handle, msg, wParam, lParam);
        }

        public static IntPtr SendMessage(this Control control, int msg, int wParam, ref IntPtr lParam)
        {
            if (!control.IsHandleCreated || control.IsDisposed) return default;
            return Native.SendMessage(control.Handle, msg, wParam, lParam);
        }

        public static IntPtr SendMessage(this Control control, int msg, int wParam, IntPtr lParam)
        {
            if (!control.IsHandleCreated || control.IsDisposed) return default;
            return Native.SendMessage(control.Handle, msg, wParam, lParam);
        }
    }
}
