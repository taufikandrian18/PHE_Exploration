using SHUNetMVC.Abstraction.Model.Dto;
using AutoMapper;
using ASPNetMVC.Abstraction.Model.Entities;

namespace SHUNetMVC.Web
{
    public class AutoMapperConfig
    {
        public static void RegisterGlobalMapping()
        {
            Mapper.Initialize(cfg => {
                cfg.CreateMap<MD_ExplorationStructure, MDExplorationStructureDto>().ReverseMap();
                cfg.CreateMap<LG_ExplorationStructure, LGExplorationStructureDto>().ReverseMap();
                cfg.CreateMap<MD_ExplorationStructure, MDExplorationStructureESDCDto>().ReverseMap();
                cfg.CreateMap<TX_ProsResourcesTarget, TXProsResourcesTargetDto>().ReverseMap();
                cfg.CreateMap<LG_ProsResourcesTarget, LGProsResourcesTargetDto>().ReverseMap();
                cfg.CreateMap<TX_ProsResources, TXProsResourceDto>().ReverseMap();
                cfg.CreateMap<LG_ProsResources, LGProsResourceDto>().ReverseMap();
                cfg.CreateMap<TX_ContingentResources, TXContingenResourcesDto>().ReverseMap();
                cfg.CreateMap<LG_ContingentResources, LGContingenResourcesDto>().ReverseMap();
                cfg.CreateMap<TX_Drilling, TXDrillingDto>().ReverseMap();
                cfg.CreateMap<LG_Drilling, LGDrillingDto>().ReverseMap();
                cfg.CreateMap<TX_Economic, TXEconomicDto>().ReverseMap();
                cfg.CreateMap<LG_Economic, LGEconomicDto>().ReverseMap();
                cfg.CreateMap<MD_ExplorationWell, MDExplorationWellDto>().ReverseMap();
                cfg.CreateMap<MD_ExplorationAsset, MDExplorationAssetDto>().ReverseMap();
                cfg.CreateMap<MD_ExplorationBlock, MDExplorationBlockDto>().ReverseMap();
                cfg.CreateMap<MD_ExplorationBasin, MDExplorationBasinDto>().ReverseMap();
                cfg.CreateMap<vw_DIM_Entity, VWDIMEntityDto>().ReverseMap();
                cfg.CreateMap<TX_ESDC, TXESDCDto>().ReverseMap();
                cfg.CreateMap<TX_ESDCVolumetric, TXESDCVolumetricDto>().ReverseMap();
                cfg.CreateMap<TX_ESDCProd, TXESDCProductionDto>().ReverseMap();
                cfg.CreateMap<TX_ESDCForecast, TXESDCForecastDto>().ReverseMap();
                cfg.CreateMap<TX_ESDCDiscrepancy, TXESDCDiscrepancyDto>().ReverseMap();
                cfg.CreateMap<TX_ESDCInPlace, TXESDCInPlaceDto>().ReverseMap();
                cfg.CreateMap<vw_Country, VWCountryDto>().ReverseMap();
            });
        }
    }
}
