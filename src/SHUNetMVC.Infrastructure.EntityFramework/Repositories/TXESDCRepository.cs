using ASPNetMVC.Abstraction.Model.Entities;
using Dapper;
using SHUNetMVC.Abstraction.Model.Dto;
using SHUNetMVC.Abstraction.Model.View;
using SHUNetMVC.Abstraction.Repositories;
using SHUNetMVC.Infrastructure.EntityFramework.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHUNetMVC.Infrastructure.EntityFramework.Repositories
{
    public class TXESDCRepository : BaseCrudRepository<TX_ESDC, TXESDCDto, TXESDCDto, TXESDCQuery>, ITXESDCRepository
    {
        public TXESDCRepository(DB_PHE_ExplorationEntities explorationContext, IConnectionProvider connection, DB_PHE_HRIS_DEVEntities hrContext)
                : base(explorationContext, connection, new TXESDCQuery(), hrContext)
        {

        }

        public override async Task Create(TXESDCDto model)
        {
            var entity = model.ToEntity();
            try
            {
                _explorationContext.TX_ESDC.Add(entity);
                await _explorationContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public override async Task<LookupList> GetAdaptiveFilterList(string columnId, string usernameSession)
        {
            var result = new LookupList
            {
                ColumnId = columnId
            };

            using (var connection = OpenConnection())
            {
                var items = await connection.QueryAsync<string>($"SELECT DISTINCT {columnId} FROM dbo.MD_Parameter ORDER BY {columnId}");

                result.Items = items.Select(item => new LookupItem
                {
                    Text = item,
                    Value = item
                }).ToList();
            }


            result.Items = result.Items.GroupBy(o => o.Text).Select(o => o.FirstOrDefault()).ToList();

            return result;
        }
        public override Task<IEnumerable<TXESDCDto>> GetLookupListText(string id, string zonaId)
        {
            return base.GetLookupListText(id, zonaId);
        }
    }
}
