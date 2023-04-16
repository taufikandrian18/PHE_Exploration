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
    public class VWCountryRepository : BaseCrudRepository<vw_Country, VWCountryDto, VWCountryDto, VWCountryQuery>, IVWCountryRepository
    {
        public VWCountryRepository(DB_PHE_ExplorationEntities explorationContext, IConnectionProvider connection, DB_PHE_HRIS_DEVEntities hrContext)
        : base(explorationContext, connection, new VWCountryQuery(), hrContext)
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
                var items = await connection.QueryAsync<string>($"SELECT DISTINCT {columnId} FROM [DB_PHE_Exploration].[dbo].[vw_Country] ORDER BY {columnId}");

                result.Items = items.Select(item => new LookupItem
                {
                    Text = item,
                    Value = item
                }).ToList();
            }


            result.Items = result.Items.GroupBy(o => o.Text).Select(o => o.FirstOrDefault()).ToList();

            return result;
        }

        public async Task<string> GetCountryByCountryID(string paramListID)
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

        public async Task<string> GetCountryByTwoParam(string paramID, string paramValueText)
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
