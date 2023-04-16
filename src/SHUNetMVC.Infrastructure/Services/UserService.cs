using ASPNetMVC.Abstraction.Model.Entities;
using SHUNetMVC.Abstraction.Model.Response;
using SHUNetMVC.Abstraction.Services;
using SHUNetMVC.Infrastructure.Constant;
using SHUNetMVC.Infrastructure.HttpUtils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Runtime.Caching;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;

namespace SHUNetMVC.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly HttpContextBase _httpContext;
        private readonly MemoryCache _cache;
        private readonly DB_PHE_ExplorationEntities _explorationContext;
        private readonly IMDParameterListService _parameterService;

        public UserService(HttpContextBase httpContext, MemoryCache cache, IMDParameterListService parameterService, DB_PHE_ExplorationEntities explorationContext)
        {
            _httpContext = httpContext;
            _cache = cache;
            _parameterService = parameterService;
            _explorationContext = explorationContext;
        }

        public static string GetFileUrl(string fileName)
        {
            var request = HttpContext.Current.Request;
            var appUrl = HttpRuntime.AppDomainAppVirtualPath;

            if (appUrl != "/")
                appUrl = "/" + appUrl;

            var baseUrl = string.Format("{0}://{1}{2}", request.Url.Scheme, request.Url.Authority, appUrl);
            if (!baseUrl.EndsWith("/"))
            {
                baseUrl += "/";
            }
            return baseUrl + "Upload/" + fileName;
        }

        public string GetCurrentUserName()
        {
            string userName;
            if (IsSSOEnabled())
            {
                userName = (_httpContext.User.Identity as ClaimsIdentity)?.Claims
                    .FirstOrDefault(c => c.Type == "preferred_username")?.Value;
            }
            else
            {
                userName = _httpContext.User.Identity.Name;
            }
            return userName;
        }

        public async Task<User> GetCurrentUserInfo()
        {
            string userName = GetCurrentUserName();

            if (string.IsNullOrEmpty(userName))
                return null;

            User userInfo = _cache.Get(userName) as User;
            if (userInfo != null)
                return userInfo;

            userInfo = await GetUserInfo(userName);
            if(userInfo != null)
                _cache.Set(userName, userInfo, null);
            return userInfo;
        }

        private bool IsSSOEnabled()
        {
            AuthenticationSection authenticationSection = (AuthenticationSection)ConfigurationManager.GetSection("system.web/authentication");
            return authenticationSection.Mode == AuthenticationMode.None;
        }

        public async Task<User> GetUserInfo(string userName)
        {
            if (string.IsNullOrEmpty(userName))
                return null;

            var masterUserData = await GetMasterData(userName);
            if(masterUserData == null)
            {
                return null;
            }

            User ret = new User
            {
                EmpId = masterUserData.EmpID,
                EmpAccount = masterUserData.EmpAccount,
                Name = masterUserData.EmpName,
                Email = masterUserData.EmpEmail,
                OrgUnitID = masterUserData.OrgUnitID
            };

            var userDataDto = await GetUserData(userName);
            if (userDataDto != null)
            {
                ret.Roles = userDataDto.Roles;
                ret.UserMenu = await GetUserMenu(userDataDto.AuthUserApp);
            }
            return ret;
        }

        private async Task<List<UserMenu>> GetUserMenu(string authUserApp)
        {
            try
            {
                //string xPropUserMenu = _explorationContext.MD_ParamaterList.Where(o => o.ParamListID == "XPropUserMenu").Select(o => o.ParamListDesc).FirstOrDefault();
                string xPropUserMenu = await _parameterService.GetParamDescByParamListID("XPropUserMenu");
                UriBuilder uriBuilder = new UriBuilder(AimanConstant.Uri)
                {
                    Path = AimanConstant.UserMenu
                };
                var query = HttpUtility.ParseQueryString(uriBuilder.Query);
                query["userAuthAppFK"] = authUserApp;
                uriBuilder.Query = query.ToString() ?? string.Empty;
                var data = await HttpRequester.Http.GetAsJson<HttpResponse<List<UserMenu>>>(uriBuilder.Uri, (req) => AddHeaders(req, xPropUserMenu));
                //var data = await HttpRequester.Http.GetAsJson<HttpResponse<List<UserMenu>>>(uriBuilder.Uri, (req) => AddHeaders(req, AimanConstant.XPropUserMenu));
                if (data.IsSuccessful)
                {
                    return data.Value.Object;
                }
            }
            catch
            {
            }
            return new List<UserMenu>();
        }

        private async Task<UserDataDto> GetUserData(string userName)
        {
            try
            {
                //string appFK = _explorationContext.MD_ParamaterList.Where(o => o.ParamListID == "AppFK").Select(o => o.ParamListDesc).FirstOrDefault();
                string appFK = await _parameterService.GetParamDescByParamListID("AppFK");
                //string xPropUserData = _explorationContext.MD_ParamaterList.Where(o => o.ParamListID == "XPropUserData").Select(o => o.ParamListDesc).FirstOrDefault();
                string xPropUserData = await _parameterService.GetParamDescByParamListID("XPropUserData");
                UriBuilder uriBuilder = new UriBuilder(AimanConstant.Uri)
                {
                    Path = AimanConstant.UserData
                };
                var query = HttpUtility.ParseQueryString(uriBuilder.Query);
                //query["appFk"] = AimanConstant.AppFK;
                query["appFk"] = appFK;
                query["username"] = userName;
                uriBuilder.Query = query.ToString() ?? string.Empty;
                var data = await HttpRequester.Http.GetAsJson<HttpResponse<List<UserDataDto>>>(uriBuilder.Uri, (req) => AddHeaders(req, xPropUserData));
                //var data = await HttpRequester.Http.GetAsJson<HttpResponse<List<UserDataDto>>>(uriBuilder.Uri, (req) => AddHeaders(req, AimanConstant.XPropUserData));
                if (data.IsSuccessful)
                {
                    return data.Value.Object.FirstOrDefault();
                }
            }
            catch
            {
                return null;
            }
            return null;
        }

        private async Task<MasterUserData> GetMasterData(string username)
        {
            try
            {
                //string xPropMasterData = _explorationContext.MD_ParamaterList.Where(o => o.ParamListID == "XPropMasterData").Select(o => o.ParamListDesc).FirstOrDefault();
                string xPropMasterData = await _parameterService.GetParamDescByParamListID("XPropMasterData");
                UriBuilder uriBuilder = new UriBuilder(AimanConstant.Uri)
                {
                    Path = AimanConstant.MasterUserData
                };
                var query = HttpUtility.ParseQueryString(uriBuilder.Query);
                query["WhereCondition"] = $"EmpAccount='{username}'";
                uriBuilder.Query = query.ToString() ?? string.Empty;
                var data = await HttpRequester.Http.GetAsJson<HttpResponse<List<MasterUserData>>>(uriBuilder.Uri, (req) => AddHeaders(req, xPropMasterData));
                //var data = await HttpRequester.Http.GetAsJson<HttpResponse<List<MasterUserData>>>(uriBuilder.Uri, (req) => AddHeaders(req, AimanConstant.XPropMasterData));
                if (data.IsSuccessful)
                {
                    return data.Value.Object.FirstOrDefault();
                }
                return new MasterUserData();
            }
            catch
            {
                return null;
            }
        }

        private void AddHeaders(HttpRequestMessage request, string prop)
        {
            //string xAuth = _explorationContext.MD_ParamaterList.Where(o => o.ParamListID == "XAuth").Select(o => o.ParamListDesc).FirstOrDefault();
            string xAuth = Task.Run(() => _parameterService.GetParamDescByParamListID("XAuth")).Result;
            request.Headers.Add("X-User-Prop", prop);
            //request.Headers.Add("X-User-Auth", AimanConstant.XAuth);
            request.Headers.Add("X-User-Auth", xAuth);
        }
    }
}
