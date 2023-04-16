using SHUNetMVC.Abstraction;
using SHUNetMVC.Abstraction.Model.Response;
using SHUNetMVC.Abstraction.Repositories;
using SHUNetMVC.Abstraction.Services;
using SHUNetMVC.Infrastructure.EntityFramework.Queries;
using SHUNetMVC.Infrastructure.EntityFramework.Repositories;
using SHUNetMVC.Infrastructure.Services;
using SHUNetMVC.Web.Providers;
using Serilog;
using System.Web;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;
using System.Runtime.Caching;
using ASPNetMVC.Abstraction.Model.Entities;

namespace SHUNetMVC.Web
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            container.RegisterInstance(new MemoryCache("system"));
            container.RegisterType<DB_PHE_ExplorationEntities>();
            container.RegisterType<DB_PHE_HRIS_DEVEntities>();
            container.RegisterType<IConnectionProvider, ConnectionStringProvider>();
            container.RegisterFactory<ILogger>((ctr, type, name) =>
            {
                ILogger log = new LoggerConfiguration()
                    .ReadFrom.AppSettings()
                    .CreateLogger();

                return log;
            });
            container.RegisterFactory<HttpContextBase>((_) =>
            {
                return new HttpContextWrapper(HttpContext.Current);
            });
            container.RegisterType<IUserService, UserService>();

            container.RegisterType<IMDExplorationStructureRepository, MDExplorationStructureRepository>();
            container.RegisterType<IMDExplorationStructureService, MDExplorationStructureService>();

            container.RegisterType<IMDExplorationAssetRepository, MDExplorationAssetRepository>();
            container.RegisterType<IMDExplorationAssetService, MDExplorationAssetService>();

            container.RegisterType<IMDExplorationBlockRepository, MDExplorationBlockRepository>();
            container.RegisterType<IMDExplorationBlockService, MDExplorationBlockService>();

            container.RegisterType<IMDExplorationBasinRepository, MDExplorationBasinRepository>();
            container.RegisterType<IMDExplorationBasinService, MDExplorationBasinService>();

            container.RegisterType<ITXProsResourcesTargetRepository, TXProsResourcesTargetRepository>();
            container.RegisterType<ITXProsResourcesTargetService, TXProsResourcesTargetService>();

            container.RegisterType<ITXProsResourcesRepository, TXProsResourcesRepository>();
            container.RegisterType<ITXProsResourcesService, TXProsResourcesService>();

            container.RegisterType<ITXContingenResourcesRepository, TXContingenResourcesRepository>();
            container.RegisterType<ITXContingenResourcesService, TXContingenResourcesService>();

            container.RegisterType<ITXDrillingRepository, TXDrillingRepository>();
            container.RegisterType<ITXDrillingService, TXDrillingService>();

            container.RegisterType<ITXEconomicRepository, TXEconomicRepository>();
            container.RegisterType<ITXEconomicService, TXEconomicService>();

            container.RegisterType<IMDEntityRepository, MDEntityRepository>();
            container.RegisterType<IMDEntityService, MDEntityService>();

            container.RegisterType<IMDParameterRepository, MDParameterRepository>();
            container.RegisterType<IMDParameterService, MDParameterService>();

            container.RegisterType<IMDParameterListRepository, MDParameterListRepository>();
            container.RegisterType<IMDParameterListService, MDParameterListService>();

            container.RegisterType<IMDExplorationBlockPartnerRepository, MDExplorationBlockPartnerRepository>();
            container.RegisterType<IMDExplorationBlockPartnerService, MDExplorationBlockPartnerService>();

            container.RegisterType<IHRISDevOrgUnitHierarchyRepository, HRISDevOrgUnitHierarchyRepository>();
            container.RegisterType<IHRISDevOrgUnitHierarchyService, HRISDevOrgUnitHierarchyService>();

            container.RegisterType<IMDExplorationAreaRepository, MDExplorationAreaRepository>();
            container.RegisterType<IMDExplorationAreaService, MDExplorationAreaService>();

            container.RegisterType<IMDExplorationWellRepository, MDExplorationWellRepository>();
            container.RegisterType<IMDExplorationWellService, MDExplorationWellService>();

            container.RegisterType<ILookupRepository, LookupRepository>();
            container.RegisterType<ILookupService, LookupService>();


            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}