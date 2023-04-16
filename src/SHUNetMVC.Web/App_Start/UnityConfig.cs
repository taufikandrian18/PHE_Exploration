using ASPNetMVC.Abstraction.Model.Entities;
using Serilog;
using SHUNetMVC.Abstraction.Repositories;
using SHUNetMVC.Abstraction.Services;
using SHUNetMVC.Infrastructure.EntityFramework.Repositories;
using SHUNetMVC.Infrastructure.Services;
using SHUNetMVC.Web.Providers;
using System.Runtime.Caching;
using System.Web;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;

namespace ASPNetMVC.Web
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();

            container.RegisterInstance(new MemoryCache("system"));
            container.RegisterType<DB_PHE_ExplorationEntities, DbContextMapperExploration>();
            container.RegisterType<DB_PHE_HRIS_DEVEntities,DbContextMapperHRIS>();
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

            container.RegisterType<ILGExplorationStructureRepository, LGExplorationStructureRepository>();
            container.RegisterType<ILGExplorationStructureService, LGExplorationStructureService>();

            container.RegisterType<IMDExplorationStructureESDCRepository, MDExplorationStructureESDCRepository>();
            container.RegisterType<IMDExplorationStructureESDCService, MDExplorationStructureESDCService>();

            container.RegisterType<IMDExplorationAssetRepository, MDExplorationAssetRepository>();
            container.RegisterType<IMDExplorationAssetService, MDExplorationAssetService>();

            container.RegisterType<IMDExplorationBlockRepository, MDExplorationBlockRepository>();
            container.RegisterType<IMDExplorationBlockService, MDExplorationBlockService>();

            container.RegisterType<IMDExplorationBasinRepository, MDExplorationBasinRepository>();
            container.RegisterType<IMDExplorationBasinService, MDExplorationBasinService>();

            container.RegisterType<ITXProsResourcesTargetRepository, TXProsResourcesTargetRepository>();
            container.RegisterType<ITXProsResourcesTargetService, TXProsResourcesTargetService>();

            container.RegisterType<ILGProsResourcesTargetRepository, LGProsResourcesTargetRepository>();
            container.RegisterType<ILGProsResourcesTargetService, LGProsResourcesTargetService>();

            container.RegisterType<ITXProsResourcesRepository, TXProsResourcesRepository>();
            container.RegisterType<ITXProsResourcesService, TXProsResourcesService>();

            container.RegisterType<ILGProsResourcesRepository, LGProsResourcesRepository>();
            container.RegisterType<ILGProsResourcesService, LGProsResourcesService>();

            container.RegisterType<ITXContingenResourcesRepository, TXContingenResourcesRepository>();
            container.RegisterType<ITXContingenResourcesService, TXContingenResourcesService>();

            container.RegisterType<ILGContingenResourcesRepository, LGContingenResourcesRepository>();
            container.RegisterType<ILGContingenResourcesService, LGContingenResourcesService>();

            container.RegisterType<ITXDrillingRepository, TXDrillingRepository>();
            container.RegisterType<ITXDrillingService, TXDrillingService>();

            container.RegisterType<ILGDrillingRepository, LGDrillingRepository>();
            container.RegisterType<ILGDrillingService, LGDrillingService>();

            container.RegisterType<ITXESDCForecastRepository, TXESDCForecastRepository>();
            container.RegisterType<ITXESDCForecastService, TXESDCForecastService>();

            container.RegisterType<ITXEconomicRepository, TXEconomicRepository>();
            container.RegisterType<ITXEconomicService, TXEconomicService>();

            container.RegisterType<ILGEconomicRepository, LGEconomicRepository>();
            container.RegisterType<ILGEconomicService, LGEconomicService>();

            container.RegisterType<ILGActivityRepository, LGActivityRepository>();
            container.RegisterType<ILGActivityService, LGActivityService>();

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

            container.RegisterType<ITXAttachmentRepository, TXAttachmentRepository>();
            container.RegisterType<ITXAttachmentService, TXAttachmentService>();

            container.RegisterType<ILookupRepository, LookupRepository>();
            container.RegisterType<ILookupService, LookupService>();

            container.RegisterType<ITXESDCVolumetricRepository, TXESDCVolumetricRepository>();
            container.RegisterType<ITXESDCVolumetricService, TXESDCVolumetricService>();

            container.RegisterType<ITXESDCDiscrepancyRepository, TXESDCDiscrepancyRepository>();
            container.RegisterType<ITXESDCDiscrepancyService, TXESDCDiscrepancyService>();

            container.RegisterType<ITXESDCInPlaceRepository, TXESDCInPlaceRepository>();
            container.RegisterType<ITXESDCInPlaceService, TXESDCInPlaceService>();

            container.RegisterType<IVWDIMEntityRepository, VWDIMEntityRepository>();
            container.RegisterType<IVWDIMEntityService, VWDIMEntityService>();

            container.RegisterType<ITXESDCRepository, TXESDCRepository>();
            container.RegisterType<ITXESDCService, TXESDCService>();

            container.RegisterType<ITXESDCProductionRepository, TXESDCProductionRepository>();
            container.RegisterType<ITXESDCProductionService, TXESDCProductionService>();

            container.RegisterType<IVWCountryRepository, VWCountryRepository>();
            container.RegisterType<IVWCountryService, VWCountryService>();


            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}