﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ASPNetMVC.Infrastructure.HttpUtils
{
    public class FileServices
    {
        public static string GetFileUrl(string fileName)
        {
            var request = HttpContext.Current.Request;
            var appUrl = HttpRuntime.AppDomainAppVirtualPath;

            if (appUrl != "/")
                appUrl = "/" + appUrl;

            var baseUrl = string.Format("{0}://{1}{2}", request.Url.Scheme, request.Url.Authority, appUrl);
            //var baseDevUrl = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory);
            if (!baseUrl.EndsWith("/"))
            {
                baseUrl += "/";
            }
            //if (!baseDevUrl.EndsWith("/"))
            //{
            //    baseDevUrl += "/";
            //}
            return baseUrl + "Uploads/" + fileName;
            //return baseDevUrl + "Uploads/" + fileName;
        }
    }
}
