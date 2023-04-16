using ASPNetMVC.Abstraction.Model.Entities;
using SHUNetMVC.Abstraction.Model.Dto;
using SHUNetMVC.Abstraction.Repositories;
using SHUNetMVC.Infrastructure.EntityFramework.Queries;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SHUNetMVC.Infrastructure.EntityFramework.Repositories
{
    public class TXESDCForecastRepository : BaseCrudRepository<TX_ESDCForecast, TXESDCForecastDto, TXESDCForecastDto, TXESDCForecastQuery>, ITXESDCForecastRepository
    {
        public TXESDCForecastRepository(DB_PHE_ExplorationEntities explorationContext, IConnectionProvider connection, DB_PHE_HRIS_DEVEntities hrContext)
                : base(explorationContext, connection, new TXESDCForecastQuery(), hrContext)
        {

        }

        public override async Task Create(TXESDCForecastDto model)
        {
            var entity = model.ToEntity();
            try
            {
                _explorationContext.TX_ESDCForecast.Add(entity);
                await _explorationContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public async Task Destroy(string structureID, int year)
        {
            try
            {
                var item = await _explorationContext.Set<TX_ESDCForecast>().FindAsync(new object[] { structureID, year });
                _explorationContext.Set<TX_ESDCForecast>().Remove(item);
                await _explorationContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public async Task<List<TXESDCForecastDto>> GetForecastByStructureID(string structureID)
        {
            try
            {
                var getResult = await GetLookupListText(structureID);
                return getResult.ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<string> GenerateNewID()
        {
            try
            {
                //var getCount = await GetCountDataTable();
                //var sequence = int.Parse(getCount);
                ////var sequence = 9090909090;
                //var literal = "xT";
                //var id = literal + (sequence + 1).ToString("0000000");
                //return id;

                var getCount = await GetCountDataTable();
                if (!string.IsNullOrEmpty(getCount))
                {
                    var newId = Regex.Replace(getCount, "\\d+", m => (int.Parse(m.Value) + 1).ToString(new string('0', m.Value.Length)));
                    return newId;
                }
                else
                {
                    var idTmp = "xT0000000";
                    var newId = Regex.Replace(idTmp, "\\d+", m => (int.Parse(m.Value) + 1).ToString(new string('0', m.Value.Length)));
                    return newId;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public async Task<TX_ESDCForecast> GetForecastTargetByStructureID(string structureID)
        {
            try
            {
                var getResult = await _explorationContext.TX_ESDCForecast.Where(x => x.xStructureID.Trim() == structureID.Trim()).FirstOrDefaultAsync();
                return getResult;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<TXESDCForecastDto> GetAll()
        {
            List<TX_ESDCForecast> result = _explorationContext.Set<TX_ESDCForecast>().ToList();
            List<TXESDCForecastDto> dto = (List<TXESDCForecastDto>)Activator.CreateInstance(typeof(List<TXESDCForecastDto>), result);
            return dto;
        }
    }
}
