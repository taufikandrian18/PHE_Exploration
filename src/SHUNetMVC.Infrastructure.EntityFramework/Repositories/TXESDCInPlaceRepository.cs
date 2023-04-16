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
    public class TXESDCInPlaceRepository : BaseCrudRepository<TX_ESDCInPlace, TXESDCInPlaceDto, TXESDCInPlaceDto, TXESDCInPlaceQuery>, ITXESDCInPlaceRepository
    {
        public TXESDCInPlaceRepository(DB_PHE_ExplorationEntities explorationContext, IConnectionProvider connection, DB_PHE_HRIS_DEVEntities hrContext)
                : base(explorationContext, connection, new TXESDCInPlaceQuery(), hrContext)
        {

        }

        public override async Task Create(TXESDCInPlaceDto model)
        {
            var entity = model.ToEntity();
            try
            {
                _explorationContext.TX_ESDCInPlace.Add(entity);
                await _explorationContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public async Task<List<TXESDCInPlaceDto>> GetListTXESDCInPlaceByStructureID(string structureID)
        {
            var getResult = await GetLookupListText(structureID);
            return getResult.ToList();
        }
        public async Task Destroy(string structureID, string uncertaintyLevel)
        {
            try
            {
                var item = await _explorationContext.Set<TX_ESDCInPlace>().FindAsync(new object[] { structureID, uncertaintyLevel });
                _explorationContext.Set<TX_ESDCInPlace>().Remove(item);
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
        public async Task<TX_ESDCInPlace> GetInPlaceTargetByStructureID(string structureID)
        {
            try
            {
                var getResult = await _explorationContext.TX_ESDCInPlace.Where(x => x.xStructureID.Trim() == structureID.Trim()).FirstOrDefaultAsync();
                return getResult;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<TXESDCInPlaceDto> GetAll()
        {
            List<TX_ESDCInPlace> result = _explorationContext.Set<TX_ESDCInPlace>().ToList();
            List<TXESDCInPlaceDto> dto = (List<TXESDCInPlaceDto>)Activator.CreateInstance(typeof(List<TXESDCInPlaceDto>), result);
            return dto;
        }
    }
}
