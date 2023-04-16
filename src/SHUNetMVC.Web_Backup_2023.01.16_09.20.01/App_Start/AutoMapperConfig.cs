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
                cfg.CreateMap<TX_ProsResourcesTarget, TXProsResourcesTargetDto>().ReverseMap();
                cfg.CreateMap<TX_ProsResources, TXProsResourceDto>().ReverseMap();
                cfg.CreateMap<TX_ContingentResources, TXContingenResourcesDto>().ReverseMap();
                cfg.CreateMap<TX_Drilling, TXDrillingDto>().ReverseMap();
                cfg.CreateMap<TX_Economic, TXEconomicDto>().ReverseMap();
                cfg.CreateMap<MD_ExplorationWell, MDExplorationWellDto>().ReverseMap();
                cfg.CreateMap<MD_ExplorationAsset, MDExplorationAssetDto>().ReverseMap();
                cfg.CreateMap<MD_ExplorationBlock, MDExplorationBlockDto>().ReverseMap();
                cfg.CreateMap<MD_ExplorationBasin, MDExplorationBasinDto>().ReverseMap();
            });
        }
    }
}
