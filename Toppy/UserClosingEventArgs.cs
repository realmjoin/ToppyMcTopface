using System;

namespace ToppyMcTopface
{
    public class UserClosingEventArgs : EventArgs
    {
        public bool Cancel { get; set; }
    }
}