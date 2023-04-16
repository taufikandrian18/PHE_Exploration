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
    public class TXESDCDiscrepancyRepository : BaseCrudRepository<TX_ESDCDiscrepancy, TXESDCDiscrepancyDto, TXESDCDiscrepancyDto, TXESDCDiscrepancyQuery>, ITXESDCDiscrepancyRepository
    {
        public TXESDCDiscrepancyRepository(DB_PHE_ExplorationEntities explorationContext, IConnectionProvider connection, DB_PHE_HRIS_DEVEntities hrContext)
                : base(explorationContext, connection, new TXESDCDiscrepancyQuery(), hrContext)
        {

        }

        public override async Task Create(TXESDCDiscrepancyDto model)
        {
            var entity = model.ToEntity();
            try
            {
                _explorationContext.TX_ESDCDiscrepancy.Add(entity);
                await _explorationContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public async Task<List<TXESDCDiscrepancyDto>> GetListTXESDCDiscrepancyByStructureID(string structureID)
        {
            var getResult = await GetLookupListText(structureID);
            return getResult.ToList();
        }
        public async Task Destroy(string structureID, string uncertaintyLevel)
        {
            try
            {
                var item = await _explorationContext.Set<TX_ESDCDiscrepancy>().FindAsync(new object[] { structureID, uncertaintyLevel });
                _explorationContext.Set<TX_ESDCDiscrepancy>().Remove(item);
                await _explorationContext.SaveChangesAsync();
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
        public async Task<TX_ESDCDiscrepancy> GetDiscrepancyTargetByStructureID(string structureID)
        {
            try
            {
                var getResult = await _explorationContext.TX_ESDCDiscrepancy.Where(x => x.xStructureID.Trim() == structureID.Trim()).FirstOrDefaultAsync();
                return getResult;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<TXESDCDiscrepancyDto> GetAll()
        {
            List<TX_ESDCDiscrepancy> result = _explorationContext.Set<TX_ESDCDiscrepancy>().ToList();
            List<TXESDCDiscrepancyDto> dto = (List<TXESDCDiscrepancyDto>)Activator.CreateInstance(typeof(List<TXESDCDiscrepancyDto>), result);
            return dto;
        }
    }
}
