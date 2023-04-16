using System;

namespace SHUNetMVC.Infrastructure.HttpUtils
{
    public class OnErrorEventArgs : EventArgs
    {
        public OnErrorEventArgs(Exception e)
        {
            Exception = e;
        }
        public Exception Exception { get; }
    }
}
