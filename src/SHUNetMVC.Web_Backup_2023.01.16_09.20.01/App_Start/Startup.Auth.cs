using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using OpenAthens.Owin.Security.OpenIdConnect;
using Owin;
using System.Configuration;

namespace SHUNetMVC.Web
{
    public partial class Startup
    {
        public void ConfigureAuth(IAppBuilder app)
        {
            app.SetDefaultSignInAsAuthenticationType(CookieAuthenticationDefaults.AuthenticationType);
            app.UseCookieAuthentication(new CookieAuthenticationOptions());
            app.UseOpenIdConnectAuthentication(
                new OpenIdConnectAuthenticationOptions
                {
                    SignInAsAuthenticationType = CookieAuthenticationDefaults.AuthenticationType,
                    Authority = ConfigurationManager.AppSettings["oidc:ServerRealm"],
                    ClientId = ConfigurationManager.AppSettings["oidc:ClientId"],
                    ClientSecret = ConfigurationManager.AppSettings["oidc:ClientSecret"],
                    MetadataAddress = ConfigurationManager.AppSettings["oidc:Metadata"],
                    RedirectUri = ConfigurationManager.AppSettings["oidc:RedirectUri"],
                    PostLogoutRedirectUri = ConfigurationManager.AppSettings["oidc:RedirectUri"],
                    Scope = OpenIdConnectScope.OpenIdProfile,
                    GetClaimsFromUserInfoEndpoint = true,
                    ResponseType = OpenIdConnectResponseType.Code
                }
            );
        }
    }
}
