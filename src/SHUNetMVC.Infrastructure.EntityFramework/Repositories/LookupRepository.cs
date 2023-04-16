using ASPNetMVC.Abstraction.Model.Entities;
using SHUNetMVC.Abstraction.Model.View;
using SHUNetMVC.Abstraction.Repositories;
using System.Linq;

namespace SHUNetMVC.Infrastructure.EntityFramework.Repositories
{
    public class LookupRepository : BaseRepository, ILookupRepository
    {
        public LookupRepository(DB_PHE_ExplorationEntities context, IConnectionProvider connection, DB_PHE_HRIS_DEVEntities hrContext)
            : base(context, connection, hrContext)
        {
        }

        public LookupList GetExplorationAssets()
        {
            var list = _explorationContext.MD_ExplorationAsset.AsNoTracking()
                .OrderBy(o=>o.xAssetName)
                .Select(o => new LookupItem
            {
                Text = o.xAssetName,
                Value = o.xAssetID.ToString()
            }).ToList();

            return new LookupList
            {
                Items = list
            };
        }
        public LookupList GetExplorationBasins()
        {
            var list = _explorationContext.MD_ExplorationBasin.AsNoTracking()
                .OrderBy(o => o.BasinName)
                .Select(o => new LookupItem
                {
                    Text = o.BasinName,
                    Value = o.BasinID.ToString()
                }).ToList();

            return new LookupList
            {
                Items = list
            };
        }
        public LookupList GetExplorationBlocks()
        {
            var list = _explorationContext.MD_ExplorationBlock.AsNoTracking()
                .OrderBy(o => o.xBlockName)
                .Select(o => new LookupItem
                {
                    Text = o.xBlockName,
                    Value = o.xBlockID.ToString()
                }).ToList();

            return new LookupList
            {
                Items = list
            };
        }
    }
}
