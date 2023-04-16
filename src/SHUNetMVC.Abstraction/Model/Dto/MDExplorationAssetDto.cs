using ASPNetMVC.Abstraction.Model.Entities;

namespace SHUNetMVC.Abstraction.Model.Dto
{
    public class MDExplorationAssetDto : BaseDtoAutoMapper<MD_ExplorationAsset>
    {
        public string xAssetID { get; set; }
        public string xAssetName { get; set; }
        public MDExplorationAssetDto()
        {

        }
        public MDExplorationAssetDto(MD_ExplorationAsset entity) : base(entity)
        {

        }
    }
}
