using System;
using System.Net.Http;

namespace SHUNetMVC.Infrastructure.HttpUtils
{
    public class AfterResponseEventArgs : EventArgs
    {
        public HttpResponseMessage Response { get; internal set; }
    }
}
