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
    public class MDParameterListRepository : BaseCrudRepository<MD_ParamaterList, MDParameterListDto, MDParameterListDto, MDParameterListQuery>, IMDParameterListRepository
    {
        public MDParameterListRepository(DB_PHE_ExplorationEntities explorationContext, IConnectionProvider connection, DB_PHE_HRIS_DEVEntities hrContext)
                : base(explorationContext, connection, new MDParameterListQuery(), hrContext)
        {

        }
        public override async Task<LookupList> GetAdaptiveFilterList(string columnId, string usernameSession)
        {
            var result = new LookupList
            {
                ColumnId = columnId
            };

            using (var connection = OpenConnection())
            {
                var items = await connection.QueryAsync<string>($"SELECT DISTINCT {columnId} FROM dbo.MD_ParameterList ORDER BY {columnId}");

                result.Items = items.Select(item => new LookupItem
                {
                    Text = item,
                    Value = item
                }).ToList();
            }


            result.Items = result.Items.GroupBy(o => o.Text).Select(o => o.FirstOrDefault()).ToList();

            return result;
        }

        public async Task<string> GetParamDescByParamListID(string paramListID)
        {
            try
            {
                var getParamListID = await GetLookupText(paramListID);
                return getParamListID;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<string> GetParamListIDByTwoParam(string paramID, string paramValueText)
        {
            try
            {
                var getParamListID = await GetLookupListTextWithParam(paramID, paramValueText);
                return getParamListID;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
