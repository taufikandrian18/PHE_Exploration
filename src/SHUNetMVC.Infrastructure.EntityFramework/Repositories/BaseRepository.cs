using SHUNetMVC.Abstraction.Model.Response;
using SHUNetMVC.Abstraction.Model.View;
using SHUNetMVC.Abstraction.Repositories;
using SHUNetMVC.Infrastructure.EntityFramework.Queries;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using ASPNetMVC.Abstraction.Model.Entities;

namespace SHUNetMVC.Infrastructure.EntityFramework.Repositories
{
    public class BaseRepository
    {
        protected readonly DB_PHE_ExplorationEntities _explorationContext;
        protected readonly DB_PHE_HRIS_DEVEntities _hrisDevContext;
        private readonly IConnectionProvider _connection;

        public BaseRepository(DB_PHE_ExplorationEntities explorationContext, IConnectionProvider connection, DB_PHE_HRIS_DEVEntities hrisDevContext)
        {
            _explorationContext = explorationContext;
            _connection = connection;
            _hrisDevContext = hrisDevContext;
        }

        protected async Task<Paged<TPaged>> GetPaged<TPaged>(string selectQuery, string countQuery, List<ColumnDefinition> columnDefinitions, List<List<FilterItem>> param, int page = 1, int size = 20, string sort = "id asc", string HRISObj = "SHU")
        {
            using (var connection = OpenConnection())
            {
                var filterSql = FilterBuilder.BuildFilter(columnDefinitions,param, HRISObj);

                var metaData = InsertMetaData(selectQuery, filterSql, page, size, sort);

                var querySQL = BuildQuery(BaseQueries.PagedQuery, metaData);
                var items = await connection.QueryAsync<TPaged>(querySQL);
                metaData["select"] = countQuery;
                querySQL = BuildQuery(BaseQueries.CountQuery, metaData);
                var count = await connection.QueryFirstAsync<int>(querySQL);

                return new Paged<TPaged>()
                {
                    TotalItems = count,
                    Items = items
                };
            }
        }

        protected async Task<Paged<ProsResourceExcelDto>> GetPagedProsResource<ProsResourceExcelDto>(string selectQuery, string countQuery, List<ColumnDefinition> columnDefinitions, List<List<FilterItem>> param, int page = 1, int size = 20, string sort = "id asc", string HRISObj = "SHU")
        {
            using (var connection = OpenConnection())
            {
                var filterSql = FilterBuilder.BuildFilterProsResource(columnDefinitions, param, HRISObj);

                var metaData = InsertMetaData(selectQuery, filterSql, page, size, sort);

                var querySQL = BuildQuery(BaseQueries.PagedQuery, metaData);
                var items = await connection.QueryAsync<ProsResourceExcelDto>(querySQL);
                metaData["select"] = countQuery;
                querySQL = BuildQuery(BaseQueries.CountQuery, metaData);
                var count = await connection.QueryFirstAsync<int>(querySQL);

                return new Paged<ProsResourceExcelDto>()
                {
                    TotalItems = count,
                    Items = items
                };
            }
        }
        protected async Task<Paged<ExploreRJPPExcelDto>> GetPagedRJPP<ExploreRJPPExcelDto>(string selectQuery, string countQuery, List<ColumnDefinition> columnDefinitions, List<List<FilterItem>> param, int page = 1, int size = 20, string sort = "id asc", string HRISObj = "SHU")
        {
            using (var connection = OpenConnection())
            {
                var filterSql = FilterBuilder.BuildFilterProsResource(columnDefinitions, param, HRISObj);

                var metaData = InsertMetaData(selectQuery, filterSql, page, size, sort);

                var querySQL = BuildQuery(BaseQueries.PagedQuery, metaData);
                var items = await connection.QueryAsync<ExploreRJPPExcelDto>(querySQL);
                metaData["select"] = countQuery;
                querySQL = BuildQuery(BaseQueries.CountQuery, metaData);
                var count = await connection.QueryFirstAsync<int>(querySQL);

                return new Paged<ExploreRJPPExcelDto>()
                {
                    TotalItems = count,
                    Items = items
                };
            }
        }

        protected async Task<Paged<ExploreESDCExcelDto>> GetPagedESDC<ExploreESDCExcelDto>(string selectQuery, string countQuery, List<ColumnDefinition> columnDefinitions, List<List<FilterItem>> param, int page = 1, int size = 20, string sort = "id asc", string HRISObj = "SHU")
        {
            using (var connection = OpenConnection())
            {
                var filterSql = FilterBuilder.BuildFilterESDC(columnDefinitions, param, HRISObj);

                var metaData = InsertMetaData(selectQuery, filterSql, page, size, sort);

                var querySQL = BuildQuery(BaseQueries.PagedQuery, metaData);
                var items = await connection.QueryAsync<ExploreESDCExcelDto>(querySQL);
                metaData["select"] = countQuery;
                querySQL = BuildQuery(BaseQueries.CountQuery, metaData);
                var count = await connection.QueryFirstAsync<int>(querySQL);

                return new Paged<ExploreESDCExcelDto>()
                {
                    TotalItems = count,
                    Items = items
                };
            }
        }

        protected async Task<Paged<TPaged>> GetPagedAll<TPaged>(string selectQuery, string countQuery, int page = 1, int size = 100, string sort = "[xStructureID] asc")
        {
            using (var connection = OpenConnection())
            {
                var metaData = InsertMetaData(selectQuery, "", page, size, sort);

                var querySQL = BuildQuery(BaseQueries.PagedQuery, metaData);
                var items = await connection.QueryAsync<TPaged>(querySQL);
                metaData["select"] = countQuery;
                querySQL = BuildQuery(BaseQueries.CountQuery, metaData);
                var count = await connection.QueryFirstAsync<int>(querySQL);

                return new Paged<TPaged>()
                {
                    TotalItems = count,
                    Items = items
                };
            }
        }

        protected async Task<Paged<TPaged>> GetPagedRoles<TPaged>(string selectQuery, string countQuery, List<ColumnDefinition> columnDefinitions, List<List<FilterItem>> param, int page = 1, int size = 20, string sort = "id asc", string HRISObj = "SHU")
        {
            using (var connection = OpenConnection())
            {
                var filterSql = FilterBuilder.BuildFilter(columnDefinitions, param, HRISObj);

                var metaData = InsertMetaData(selectQuery, filterSql, page, size, sort);

                var querySQL = BuildQuery(BaseQueries.PagedQuery, metaData);
                var items = await connection.QueryAsync<TPaged>(querySQL);
                metaData["select"] = countQuery;
                querySQL = BuildQuery(BaseQueries.CountQuery, metaData);
                var count = await connection.QueryFirstAsync<int>(querySQL);

                return new Paged<TPaged>()
                {
                    TotalItems = count,
                    Items = items
                };
            }
        }

        protected async Task<Paged<TPaged>> GetPagedReport<TPaged>(string selectQuery, string countQuery, List<ColumnDefinition> columnDefinitions, List<List<FilterItem>> param, int page = 1, int size = 20, string sort = "id asc", string HRISObj = "SHU")
        {
            using (var connection = OpenConnection())
            {
                var filterSql = FilterBuilder.BuildFilter(columnDefinitions, param, HRISObj);

                var metaData = InsertMetaData(selectQuery, filterSql, page, size, sort);

                var querySQL = BuildQuery(BaseQueries.PagedQuery, metaData);
                var items = await connection.QueryAsync<TPaged>(querySQL);
                metaData["select"] = countQuery;
                querySQL = BuildQuery(BaseQueries.CountQuery, metaData);
                var count = await connection.QueryFirstAsync<int>(querySQL);

                return new Paged<TPaged>()
                {
                    TotalItems = count,
                    Items = items
                };
            }
        }

        protected string BuildQuery(string sql, Dictionary<string, object> values)
        {
            var query = sql;
            foreach (var item in values)
            {
                query = query.Replace($"@{item.Key}", item.Value?.ToString());
            }
            return query.ToString();
        }

        protected virtual Dictionary<string, object> InsertMetaData(string selectQuery, string filterSql, int page, int size, string sort)
        {
            var metaData = new Dictionary<string, object>
            {
                { "select", selectQuery },
                { "sort", sort },
                { "offset", ((page - 1) * size) },
                { "limit", size },
                { "filter", filterSql }
            };

            return metaData;
        }

        protected virtual Dictionary<string, object> InsertMetaDataWithoutFilter(string selectQuery, int page, int size)
        {
            var metaData = new Dictionary<string, object>
            {
                { "select", selectQuery },
                { "offset", ((page - 1) * size) },
                { "limit", size },
            };

            return metaData;
        }

        protected async Task<IEnumerable<TResult>> ExecuteQueryAsync<TResult>(string query)
        {
            using (var connection = OpenConnection())
            {
                return await connection.QueryAsync<TResult>(query);
            }
        }

        protected IDbConnection OpenConnection()
        {
            SqlConnection conn;
            var connectionString = _connection.GetConnectionString();
            conn = new SqlConnection(connectionString);
            conn.Open();
            return conn;
        }

        protected IDbConnection OpenConnectionHRIS()
        {
            SqlConnection conn;
            var connectionString = _connection.GetConnectionStringHRIS();
            conn = new SqlConnection(connectionString);
            conn.Open();
            return conn;
        }
    }
}
