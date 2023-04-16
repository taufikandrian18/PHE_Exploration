using SHUNetMVC.Abstraction.Model.Dto;
using SHUNetMVC.Abstraction.Model.Response;
using SHUNetMVC.Abstraction.Model.View;
using SHUNetMVC.Abstraction.Repositories;
using SHUNetMVC.Infrastructure.EntityFramework.Queries;
using Dapper;
using System;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;
using ASPNetMVC.Abstraction.Model.Entities;
using System.Collections.Generic;

namespace SHUNetMVC.Infrastructure.EntityFramework.Repositories
{
    public abstract class BaseCrudRepository<TEntity,TDto,TGridModel,TCrudQuery> : BaseRepository  
        where TCrudQuery : BaseCrudQuery
        where TEntity : class
        where TDto :  BaseDtoAutoMapper<TEntity>

    {
        private TCrudQuery _crudQuery;

        public BaseCrudRepository(DB_PHE_ExplorationEntities explorationContext, IConnectionProvider connection, TCrudQuery tCrudQuery, DB_PHE_HRIS_DEVEntities hrisContext)
            : base(explorationContext, connection, hrisContext)
        {
            _crudQuery = tCrudQuery;
        }

        public virtual async Task Create(TDto dto)
        {
            try
            {
                var entity = dto.ToEntity();
                _explorationContext.Set<TEntity>().Add(entity);
                _explorationContext.Set<TEntity>().Attach(entity);
                await _explorationContext.SaveChangesAsync();
            }
            catch (Exception e)
            {

                throw e;
            }
          
        }

        public virtual async Task<TDto> GetOne(string id)
        {
            TEntity result = await _explorationContext.Set<TEntity>().FindAsync(id);
            TDto dto = (TDto)Activator.CreateInstance(typeof(TDto), result);
            return dto;
        }

        public virtual async Task Delete(int id)
        {
            var item = await _explorationContext.Set<TEntity>().FindAsync(id);
            _explorationContext.Set<TEntity>().Remove(item);
          await _explorationContext.SaveChangesAsync();

        }

        public virtual Task<LookupList> GetAdaptiveFilterList(string columnId, string usernameSession)
        {
            throw new System.NotImplementedException();
        }

     

        public virtual async Task<Paged<TGridModel>> GetPaged(GridParam param)
        {
            return await GetPaged<TGridModel>(
               _crudQuery.SelectPagedQuery,
               _crudQuery.CountQuery,

               param.ColumnDefinitions,
               param.FilterList.FilterItems,
               param.FilterList.Page,
               param.FilterList.Size,
               param.FilterList.OrderBy,
               param.HrisRegObj);
        }

        public virtual async Task<Paged<ProsResourceExcelDto>> GetPagedProsResource(GridParam param)
        {
            ExportExcelExplorationQuery prosResourceQuery = new ExportExcelExplorationQuery();
            return await GetPagedProsResource<ProsResourceExcelDto>(
               prosResourceQuery.ExploreProsResourceExcel,
               prosResourceQuery.ExploreProsResourceExcelCount,

               param.ColumnDefinitions,
               param.FilterList.FilterItems,
               param.FilterList.Page,
               param.FilterList.Size,
               "es.[xStructureID] DESC",
               param.HrisRegObj);
        }

        public virtual async Task<Paged<ExploreRJPPExcelDto>> GetPagedRJPP(GridParam param)
        {
            ExportExcelExplorationQuery prosResourceQuery = new ExportExcelExplorationQuery();
            return await GetPagedRJPP<ExploreRJPPExcelDto>(
               prosResourceQuery.ExploreRJPPExcel,
               prosResourceQuery.ExploreRJPPExcelCount,

               param.ColumnDefinitions,
               param.FilterList.FilterItems,
               param.FilterList.Page,
               param.FilterList.Size,
               "es.[xStructureID] DESC",
               param.HrisRegObj);
        }

        public virtual async Task<Paged<TGridModel>> GetPagedAll()
        {
            return await GetPagedAll<TGridModel>(
               _crudQuery.SelectPagedQuery,
               _crudQuery.CountQuery);
        }

        public virtual async Task<Paged<TGridModel>> GetPagedRoles(GridParam param)
        {
            MDExplorationStructureQuery structureQuery = new MDExplorationStructureQuery();
            return await GetPagedRoles<TGridModel>(
               _crudQuery.PagedRoles,
               structureQuery.CountQueryRoles,

               param.ColumnDefinitions,
               param.FilterList.FilterItems,
               param.FilterList.Page,
               param.FilterList.Size,
               param.FilterList.OrderBy,
               param.HrisRegObj);
        }

        public virtual async Task<Paged<TGridModel>> GetPagedReport(GridParam param)
        {
            MDExplorationStructureQuery structureQuery = new MDExplorationStructureQuery();
            return await GetPaged<TGridModel>(
               _crudQuery.PagedReport,
               structureQuery.CountQueryReport,

               param.ColumnDefinitions,
               param.FilterList.FilterItems,
               param.FilterList.Page,
               param.FilterList.Size,
               param.FilterList.OrderBy,
               param.HrisRegObj);
        }

        public virtual async Task Update(TDto dto)
        {
            TEntity model = dto.ToEntity();
            _explorationContext.Set<TEntity>().AddOrUpdate(model);
            await _explorationContext.SaveChangesAsync();
        }


        public virtual async Task<string> GetLookupText(string id)
        {
            using (var connection = OpenConnection())
            {
                var querySQL = string.Format(_crudQuery.LookupTextQuery,id);
                var lookupText = await connection.QueryFirstOrDefaultAsync<string>(querySQL);
                if (lookupText == null)
                {
                    lookupText = "";
                }
                return lookupText;
            }
        }

        public virtual async Task<string> GetCountDataTable()
        {
            using (var connection = OpenConnection())
            {
                var querySQL = string.Format(_crudQuery.GenerateID);
                var lookupText = await connection.QueryFirstOrDefaultAsync<string>(querySQL);
                if (lookupText == null)
                {
                    lookupText = "";
                }
                return lookupText;
            }
        }

        public virtual async Task<IEnumerable<TDto>> GetLookupListText(string id)
        {
            using (var connection = OpenConnection())
            {
                var querySQL = string.Format(_crudQuery.LookupListTextQuery, id);
                var lookupText = await connection.QueryAsync<TDto>(querySQL);
                return lookupText;
            }
        }

        public virtual async Task<IEnumerable<TDto>> GetExportExcelText(string id)
        {
            using (var connection = OpenConnection())
            {
                var querySQL = string.Format(_crudQuery.ExcelExportQuery, id);
                var lookupText = await connection.QueryAsync<TDto>(querySQL);
                return lookupText;
            }
        }

        public virtual async Task<IEnumerable<TDto>> GetLookupListText(string id, string secondId)
        {
            using (var connection = OpenConnection())
            {
                var querySQL = string.Format(_crudQuery.LookupListTextQuery, id, secondId);
                var lookupText = await connection.QueryAsync<TDto>(querySQL);
                return lookupText;
            }
        }

        public virtual async Task<string> GetLookupListTextWithParam(string id, string secondId)
        {
            using (var connection = OpenConnection())
            {
                var querySQL = string.Format(_crudQuery.SelectPagedQuery, id, secondId);
                var lookupText = await connection.QueryFirstOrDefaultAsync<string>(querySQL);
                return lookupText;
            }
        }
        public virtual async Task<ExportExcelESDC> GetPagedExcelESDC(GridParam param)
        {
            List<Paged<TGridModel>> ItemReturn = new List<Paged<TGridModel>>();
            ExportExcelESDCQuery queryESDC = new ExportExcelESDCQuery();
            var esdcItem = await GetPagedESDC<ESDCExcelDto>(
               queryESDC.ESDCGeneralInfoExport,
               queryESDC.ESDCGeneralInfoExportCount,

               param.ColumnDefinitions,
               param.FilterList.FilterItems,
               param.FilterList.Page,
               param.FilterList.Size,
               param.FilterList.OrderBy,
               param.HrisRegObj);

            var esdcProd = await GetPagedESDC<ESDCProdExcelDto>(
               queryESDC.ESDCProdExport,
               queryESDC.ESDCProdExportCount,

               param.ColumnDefinitions,
               param.FilterList.FilterItems,
               param.FilterList.Page,
               param.FilterList.Size,
               param.FilterList.OrderBy,
               param.HrisRegObj);

            var esdcVol = await GetPagedESDC<ESDCVolumetricExcelDto>(
               queryESDC.ESDCVolumetricExport,
               queryESDC.ESDCVolumetricExportCount,

               param.ColumnDefinitions,
               param.FilterList.FilterItems,
               param.FilterList.Page,
               param.FilterList.Size,
               "es.xStructureID,CASE WHEN UncertaintyLevel like '%Low%' THEN 1 WHEN UncertaintyLevel like '%Middle%' THEN 2 ELSE 3 END asc",
               param.HrisRegObj);

            var esdcFor = await GetPagedESDC<ESDCForecastExcelDto>(
               queryESDC.ESDCForecastExport,
               queryESDC.ESDCForecastExportCount,

               param.ColumnDefinitions,
               param.FilterList.FilterItems,
               param.FilterList.Page,
               param.FilterList.Size,
               param.FilterList.OrderBy,
               param.HrisRegObj);

            var esdcDis = await GetPagedESDC<ESDCDiscrepancyExcelDto>(
               queryESDC.ESDCDiscrepancyExport,
               queryESDC.ESDCDiscrepancyExportCount,

               param.ColumnDefinitions,
               param.FilterList.FilterItems,
               param.FilterList.Page,
               param.FilterList.Size,
               "es.xStructureID,CASE WHEN UncertaintyLevel like '%Low%' THEN 1 WHEN UncertaintyLevel like '%Middle%' THEN 2 ELSE 3 END asc",
               param.HrisRegObj);

            var esdcIn = await GetPagedESDC<ESDCInPlaceExcelDto>(
               queryESDC.ESDCInPlaceExport,
               queryESDC.ESDCInPlaceExportCount,

               param.ColumnDefinitions,
               param.FilterList.FilterItems,
               param.FilterList.Page,
               param.FilterList.Size,
               "es.xStructureID,CASE WHEN UncertaintyLevel like '%Low%' THEN 1 WHEN UncertaintyLevel like '%Middle%' THEN 2 ELSE 3 END asc",
               param.HrisRegObj);

            return new ExportExcelESDC
            {
                TXESDC = esdcItem.Items,
                TXESDCProduction = esdcProd.Items,
                TXESDCVolumetric = esdcVol.Items,
                TXESDCForecast = esdcFor.Items,
                TXESDCDiscrepancy = esdcDis.Items,
                TXESDCInPlace = esdcIn.Items
            };
        }
    }
}
