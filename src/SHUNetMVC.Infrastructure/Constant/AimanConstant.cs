using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHUNetMVC.Infrastructure.Constant
{
    public class AimanConstant
    {
        public static string AppFK = ConfigurationManager.AppSettings["aiman:AppFK"];
        public static string XAuth = ConfigurationManager.AppSettings["aiman:XAuth"];
        public static string Uri = ConfigurationManager.AppSettings["aiman:Uri"];
        public static string MasterUserData = ConfigurationManager.AppSettings["aiman:MasterUserData"];
        public static string UserData = ConfigurationManager.AppSettings["aiman:UserData"];
        public static string UserMenu = ConfigurationManager.AppSettings["aiman:UserMenu"];
        public static string TransactionWorkflow = ConfigurationManager.AppSettings["aiman:TransactionWorkflow"];
        public static string XPropMasterData = ConfigurationManager.AppSettings["aiman:XPropMasterData"];
        public static string XPropUserData = ConfigurationManager.AppSettings["aiman:XPropUserData"];
        public static string XPropUserMenu = ConfigurationManager.AppSettings["aiman:XPropUserMenu"];
        public static string Environment = ConfigurationManager.AppSettings["oidc:RedirectUri"];
        public static string ConKey = ConfigurationManager.AppSettings["Con:Key"];
    }
}
