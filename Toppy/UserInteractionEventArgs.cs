using System;

namespace ToppyMcTopface
{
    public class UserInteractionEventArgs : EventArgs
    {
        public UserInteractionEventArgs(UserInteractionType type)
        {
            Type = type;
        }

        public UserInteractionType Type { get; }
    }
}