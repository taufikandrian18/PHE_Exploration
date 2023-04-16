using ASPNetMVC.Abstraction.Model.Entities;
using Dapper;
using SHUNetMVC.Abstraction.Model.Dto;
using SHUNetMVC.Abstraction.Model.Response;
using SHUNetMVC.Abstraction.Model.View;
using SHUNetMVC.Abstraction.Repositories;
using SHUNetMVC.Infrastructure.EntityFramework.Queries;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SHUNetMVC.Infrastructure.EntityFramework.Repositories
{
    public class MDExplorationStructureESDCRepository : BaseCrudRepository<MD_ExplorationStructure, MDExplorationStructureESDCDto, MDExplorationStructureWithAdditionalFields, MDExplorationStructureESDCQuery>, IMDExplorationStructureESDCRepository
    {
        public MDExplorationStructureESDCRepository(DB_PHE_ExplorationEntities explorationContext, IConnectionProvider connection, DB_PHE_HRIS_DEVEntities hrContext)
            : base(explorationContext, connection, new MDExplorationStructureESDCQuery(), hrContext)
        {
        }

        public override async Task Create(MDExplorationStructureESDCDto model)
        {
            var entity = model.ToEntity();
            try
            {
                _explorationContext.MD_ExplorationStructure.Add(entity);
                await _explorationContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public override async Task Update(MDExplorationStructureESDCDto dto)
        {
            try
            {
                //var existing = _context.Employees.Find(dto.EmpId);

                var data = dto.ToEntity();
                _explorationContext.MD_ExplorationStructure.AddOrUpdate(data);
                await _explorationContext.SaveChangesAsync();
            }
            catch (System.Exception e)
            {

                throw e;
            }

        }

        public override async Task<MDExplorationStructureESDCDto> GetOne(string structureId)
        {
            try
            {
                var entity = await _explorationContext.MD_ExplorationStructure.FindAsync(structureId);
                var dto = new MDExplorationStructureESDCDto(entity);
                return dto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public override async Task<LookupList> GetAdaptiveFilterList(string columnId, string usernameSession)
        {
            var result = new LookupList
            {
                ColumnId = columnId
            };

            if (columnId == "xAssetName")
            {
                using (var connection = OpenConnection())
                {
                    _explorationContext.MD_ExplorationStructure.Select(o => o.MD_ExplorationAsset.xAssetName);
                    var items = await connection.QueryAsync<string>($"SELECT DISTINCT a.{columnId} FROM xplore.TX_ESDC esdc LEFT JOIN dbo.MD_ExplorationStructure s  on esdc.xStructureID = s.xStructureID LEFT JOIN dbo.MD_ParamaterList pl on s.xStructureStatusParID = pl.ParamListID LEFT JOIN dbo.MD_ExplorationAsset a on s.xAssetID = a.xAssetID LEFT JOIN dbo.MD_ExplorationBasin ba on s.BasinID = ba.BasinID LEFT JOIN dbo.MD_ExplorationBlock bl on s.xBlockID = bl.xBlockID WHERE pl.ParamID = 'ExplorationStructureStatus' ORDER BY a.{columnId}");

                    result.Items = items.Select(item => new LookupItem
                    {
                        Text = item,
                        Value = item
                    }).ToList();
                }
            }
            else if (columnId == "BasinName")
            {
                using (var connection = OpenConnection())
                {
                    _explorationContext.MD_ExplorationStructure.Select(o => o.MD_ExplorationAsset.xAssetName);
                    var items = await connection.QueryAsync<string>($"SELECT DISTINCT ba.{columnId} FROM xplore.TX_ESDC esdc LEFT JOIN dbo.MD_ExplorationStructure s  on esdc.xStructureID = s.xStructureID LEFT JOIN dbo.MD_ParamaterList pl on s.xStructureStatusParID = pl.ParamListID LEFT JOIN dbo.MD_ExplorationAsset a on s.xAssetID = a.xAssetID LEFT JOIN dbo.MD_ExplorationBasin ba on s.BasinID = ba.BasinID LEFT JOIN dbo.MD_ExplorationBlock bl on s.xBlockID = bl.xBlockID WHERE pl.ParamID = 'ExplorationStructureStatus' ORDER BY ba.{columnId}");

                    result.Items = items.Select(item => new LookupItem
                    {
                        Text = item,
                        Value = item
                    }).ToList();
                }
            }
            else if (columnId == "xBlockName")
            {
                using (var connection = OpenConnection())
                {
                    _explorationContext.MD_ExplorationStructure.Select(o => o.MD_ExplorationAsset.xAssetName);
                    var items = await connection.QueryAsync<string>($"SELECT DISTINCT bl.{columnId} FROM xplore.TX_ESDC esdc LEFT JOIN dbo.MD_ExplorationStructure s  on esdc.xStructureID = s.xStructureID LEFT JOIN dbo.MD_ParamaterList pl on s.xStructureStatusParID = pl.ParamListID LEFT JOIN dbo.MD_ExplorationAsset a on s.xAssetID = a.xAssetID LEFT JOIN dbo.MD_ExplorationBasin ba on s.BasinID = ba.BasinID LEFT JOIN dbo.MD_ExplorationBlock bl on s.xBlockID = bl.xBlockID WHERE pl.ParamID = 'ExplorationStructureStatus' ORDER BY bl.{columnId}");

                    result.Items = items.Select(item => new LookupItem
                    {
                        Text = item,
                        Value = item
                    }).ToList();
                }
            }
            else if (columnId == "xStructureID")
            {
                using (var connection = OpenConnection())
                {
                    _explorationContext.MD_ExplorationStructure.Select(o => o.MD_ExplorationAsset.xAssetName);
                    var items = await connection.QueryAsync<string>($"SELECT DISTINCT esdc.{columnId} FROM xplore.TX_ESDC esdc LEFT JOIN dbo.MD_ExplorationStructure s  on esdc.xStructureID = s.xStructureID LEFT JOIN dbo.MD_ParamaterList pl on s.xStructureStatusParID = pl.ParamListID LEFT JOIN dbo.MD_ExplorationAsset a on s.xAssetID = a.xAssetID LEFT JOIN dbo.MD_ExplorationBasin ba on s.BasinID = ba.BasinID LEFT JOIN dbo.MD_ExplorationBlock bl on s.xBlockID = bl.xBlockID WHERE pl.ParamID = 'ExplorationStructureStatus' ORDER BY esdc.{columnId}");

                    result.Items = items.Select(item => new LookupItem
                    {
                        Text = item,
                        Value = item
                    }).ToList();
                }
            }
            else if (columnId == "StatusData")
            {
                using (var connection = OpenConnection())
                {
                    _explorationContext.MD_ExplorationStructure.Select(o => o.MD_ExplorationAsset.xAssetName);
                    var items = await connection.QueryAsync<string>($"SELECT DISTINCT esdc.{columnId} FROM xplore.TX_ESDC esdc LEFT JOIN dbo.MD_ExplorationStructure s  on esdc.xStructureID = s.xStructureID LEFT JOIN dbo.MD_ParamaterList pl on s.xStructureStatusParID = pl.ParamListID LEFT JOIN dbo.MD_ExplorationAsset a on s.xAssetID = a.xAssetID LEFT JOIN dbo.MD_ExplorationBasin ba on s.BasinID = ba.BasinID LEFT JOIN dbo.MD_ExplorationBlock bl on s.xBlockID = bl.xBlockID WHERE pl.ParamID = 'ExplorationStructureStatus' ORDER BY esdc.{columnId}");

                    result.Items = items.Select(item => new LookupItem
                    {
                        Text = item,
                        Value = item
                    }).ToList();
                }
            }
            else if (columnId == "ParamValue1Text")
            {
                using (var connection = OpenConnection())
                {
                    _explorationContext.MD_ExplorationStructure.Select(o => o.MD_ExplorationAsset.xAssetName);
                    var items = await connection.QueryAsync<string>($"SELECT DISTINCT pl.{columnId} FROM xplore.TX_ESDC esdc LEFT JOIN dbo.MD_ExplorationStructure s  on esdc.xStructureID = s.xStructureID LEFT JOIN dbo.MD_ParamaterList pl on s.xStructureStatusParID = pl.ParamListID LEFT JOIN dbo.MD_ExplorationAsset a on s.xAssetID = a.xAssetID LEFT JOIN dbo.MD_ExplorationBasin ba on s.BasinID = ba.BasinID LEFT JOIN dbo.MD_ExplorationBlock bl on s.xBlockID = bl.xBlockID WHERE pl.ParamID = 'ExplorationStructureStatus' ORDER BY pl.{columnId}");

                    result.Items = items.Select(item => new LookupItem
                    {
                        Text = item,
                        Value = item
                    }).ToList();
                }
            }
            else
            {
                using (var connection = OpenConnection())
                {
                    _explorationContext.MD_ExplorationStructure.Select(o => o.MD_ExplorationAsset.xAssetName);
                    var items = await connection.QueryAsync<string>($"SELECT DISTINCT s.{columnId} FROM xplore.TX_ESDC esdc LEFT JOIN dbo.MD_ExplorationStructure s  on esdc.xStructureID = s.xStructureID LEFT JOIN dbo.MD_ParamaterList pl on s.xStructureStatusParID = pl.ParamListID LEFT JOIN dbo.MD_ExplorationAsset a on s.xAssetID = a.xAssetID LEFT JOIN dbo.MD_ExplorationBasin ba on s.BasinID = ba.BasinID LEFT JOIN dbo.MD_ExplorationBlock bl on s.xBlockID = bl.xBlockID WHERE pl.ParamID = 'ExplorationStructureStatus' ORDER BY s.{columnId}");

                    result.Items = items.Select(item => new LookupItem
                    {
                        Text = item,
                        Value = item
                    }).ToList();
                }
            }


            result.Items = result.Items.GroupBy(o => o.Text).Select(o => o.FirstOrDefault()).ToList();

            return result;
        }

        public async Task<LookupList> GetAdaptiveFilterListESDC(string columnId, string usernameSession)
        {
            var result = new LookupList
            {
                ColumnId = columnId
            };

            if (columnId == "xAssetName")
            {
                using (var connection = OpenConnection())
                {
                    _explorationContext.MD_ExplorationStructure.Select(o => o.MD_ExplorationAsset.xAssetName);
                    var items = await connection.QueryAsync<string>($"SELECT DISTINCT a.{columnId} FROM xplore.TX_ESDC esdc LEFT JOIN dbo.MD_ExplorationStructure s  on esdc.xStructureID = s.xStructureID LEFT JOIN dbo.MD_ParamaterList pl on s.xStructureStatusParID = pl.ParamListID LEFT JOIN dbo.MD_ExplorationAsset a on s.xAssetID = a.xAssetID LEFT JOIN dbo.MD_ExplorationBasin ba on s.BasinID = ba.BasinID LEFT JOIN dbo.MD_ExplorationBlock bl on s.xBlockID = bl.xBlockID WHERE pl.ParamID = 'ExplorationStructureStatus' AND (s.ZonaID = '{usernameSession}' or s.RegionalID = '{usernameSession}' or s.SubholdingID = '{usernameSession}') ORDER BY a.{columnId}");

                    result.Items = items.Select(item => new LookupItem
                    {
                        Text = item,
                        Value = item
                    }).ToList();
                }
            }
            else if (columnId == "ZonaName")
            {
                using (var connection = OpenConnection())
                {
                    _explorationContext.MD_ExplorationStructure.Select(o => o.MD_ExplorationAsset.xAssetName);
                    var items = await connection.QueryAsync<string>($"SELECT DISTINCT dim.EntityName FROM xplore.TX_ESDC esdc LEFT JOIN dbo.MD_ExplorationStructure es on esdc.xStructureID = es.xStructureID LEFT JOIN dbo.MD_ParamaterList pl on es.xStructureStatusParID = pl.ParamListID LEFT JOIN dbo.MD_ExplorationAsset a on es.xAssetID = a.xAssetID LEFT JOIN dbo.MD_ExplorationBasin ba on es.BasinID = ba.BasinID LEFT JOIN dbo.MD_ExplorationBlock bl on es.xBlockID = bl.xBlockID LEFT JOIN dbo.vw_DIM_Entity dim on es.ZonaID = dim.EntityID WHERE pl.ParamID = 'ExplorationStructureStatus' AND (es.ZonaID = '{usernameSession}' or es.RegionalID = '{usernameSession}' or es.SubholdingID = '{usernameSession}') AND dim.EntityID = es.ZonaID ORDER BY dim.EntityName");

                    result.Items = items.Select(item => new LookupItem
                    {
                        Text = item,
                        Value = item
                    }).ToList();
                }
            }
            else if (columnId == "RegionalName")
            {
                using (var connection = OpenConnection())
                {
                    _explorationContext.MD_ExplorationStructure.Select(o => o.MD_ExplorationAsset.xAssetName);
                    var items = await connection.QueryAsync<string>($"SELECT DISTINCT dim.EntityName FROM xplore.TX_ESDC esdc LEFT JOIN dbo.MD_ExplorationStructure es on esdc.xStructureID = es.xStructureID LEFT JOIN dbo.MD_ParamaterList pl on es.xStructureStatusParID = pl.ParamListID LEFT JOIN dbo.MD_ExplorationAsset a on es.xAssetID = a.xAssetID LEFT JOIN dbo.MD_ExplorationBasin ba on es.BasinID = ba.BasinID LEFT JOIN dbo.MD_ExplorationBlock bl on es.xBlockID = bl.xBlockID LEFT JOIN dbo.vw_DIM_Entity dim on es.RegionalID = dim.EntityID WHERE pl.ParamID = 'ExplorationStructureStatus' AND (es.ZonaID = '{usernameSession}' or es.RegionalID = '{usernameSession}' or es.SubholdingID = '{usernameSession}') AND dim.EntityID = es.RegionalID ORDER BY dim.EntityName");

                    result.Items = items.Select(item => new LookupItem
                    {
                        Text = item,
                        Value = item
                    }).ToList();
                }
            }
            else if (columnId == "BasinName")
            {
                using (var connection = OpenConnection())
                {
                    _explorationContext.MD_ExplorationStructure.Select(o => o.MD_ExplorationAsset.xAssetName);
                    var items = await connection.QueryAsync<string>($"SELECT DISTINCT ba.{columnId} FROM xplore.TX_ESDC esdc LEFT JOIN dbo.MD_ExplorationStructure s  on esdc.xStructureID = s.xStructureID LEFT JOIN dbo.MD_ParamaterList pl on s.xStructureStatusParID = pl.ParamListID LEFT JOIN dbo.MD_ExplorationAsset a on s.xAssetID = a.xAssetID LEFT JOIN dbo.MD_ExplorationBasin ba on s.BasinID = ba.BasinID LEFT JOIN dbo.MD_ExplorationBlock bl on s.xBlockID = bl.xBlockID WHERE pl.ParamID = 'ExplorationStructureStatus' AND (s.ZonaID = '{usernameSession}' or s.RegionalID = '{usernameSession}' or s.SubholdingID = '{usernameSession}') ORDER BY ba.{columnId}");

                    result.Items = items.Select(item => new LookupItem
                    {
                        Text = item,
                        Value = item
                    }).ToList();
                }
            }
            else if (columnId == "xBlockName")
            {
                using (var connection = OpenConnection())
                {
                    _explorationContext.MD_ExplorationStructure.Select(o => o.MD_ExplorationAsset.xAssetName);
                    var items = await connection.QueryAsync<string>($"SELECT DISTINCT bl.{columnId} FROM xplore.TX_ESDC esdc LEFT JOIN dbo.MD_ExplorationStructure s  on esdc.xStructureID = s.xStructureID LEFT JOIN dbo.MD_ParamaterList pl on s.xStructureStatusParID = pl.ParamListID LEFT JOIN dbo.MD_ExplorationAsset a on s.xAssetID = a.xAssetID LEFT JOIN dbo.MD_ExplorationBasin ba on s.BasinID = ba.BasinID LEFT JOIN dbo.MD_ExplorationBlock bl on s.xBlockID = bl.xBlockID WHERE pl.ParamID = 'ExplorationStructureStatus' AND (s.ZonaID = '{usernameSession}' or s.RegionalID = '{usernameSession}' or s.SubholdingID = '{usernameSession}') ORDER BY bl.{columnId}");

                    result.Items = items.Select(item => new LookupItem
                    {
                        Text = item,
                        Value = item
                    }).ToList();
                }
            }
            else if (columnId == "CountriesID")
            {
                using (var connection = OpenConnection())
                {
                    _explorationContext.MD_ExplorationStructure.Select(o => o.MD_ExplorationAsset.xAssetName);
                    var items = await connection.QueryAsync<string>($"SELECT DISTINCT s.{columnId} FROM xplore.TX_ESDC esdc LEFT JOIN dbo.MD_ExplorationStructure s  on esdc.xStructureID = s.xStructureID LEFT JOIN dbo.MD_ParamaterList pl on s.xStructureStatusParID = pl.ParamListID LEFT JOIN dbo.MD_ExplorationAsset a on s.xAssetID = a.xAssetID LEFT JOIN dbo.MD_ExplorationBasin ba on s.BasinID = ba.BasinID LEFT JOIN dbo.MD_ExplorationBlock bl on s.xBlockID = bl.xBlockID WHERE pl.ParamID = 'ExplorationStructureStatus' AND (s.ZonaID = '{usernameSession}' or s.RegionalID = '{usernameSession}' or s.SubholdingID = '{usernameSession}') ORDER BY s.{columnId}");

                    result.Items = items.Select(item => new LookupItem
                    {
                        Text = item,
                        Value = item
                    }).ToList();
                }
            }
            else if(columnId == "ParamValue1Text")
            {
                using (var connection = OpenConnection())
                {
                    _explorationContext.MD_ExplorationStructure.Select(o => o.MD_ExplorationAsset.xAssetName);
                    var items = await connection.QueryAsync<string>($"SELECT DISTINCT pl.{columnId} FROM xplore.TX_ESDC esdc LEFT JOIN dbo.MD_ExplorationStructure s  on esdc.xStructureID = s.xStructureID LEFT JOIN dbo.MD_ParamaterList pl on s.xStructureStatusParID = pl.ParamListID LEFT JOIN dbo.MD_ExplorationAsset a on s.xAssetID = a.xAssetID LEFT JOIN dbo.MD_ExplorationBasin ba on s.BasinID = ba.BasinID LEFT JOIN dbo.MD_ExplorationBlock bl on s.xBlockID = bl.xBlockID WHERE pl.ParamID = 'ExplorationStructureStatus' AND (s.ZonaID = '{usernameSession}' or s.RegionalID = '{usernameSession}' or s.SubholdingID = '{usernameSession}') ORDER BY pl.{columnId}");

                    result.Items = items.Select(item => new LookupItem
                    {
                        Text = item,
                        Value = item
                    }).ToList();
                }
            }
            else if(columnId == "StatusData")
            {
                using (var connection = OpenConnection())
                {
                    _explorationContext.MD_ExplorationStructure.Select(o => o.MD_ExplorationAsset.xAssetName);
                    var items = await connection.QueryAsync<string>($"SELECT DISTINCT esdc.{columnId} FROM xplore.TX_ESDC esdc LEFT JOIN dbo.MD_ExplorationStructure s  on esdc.xStructureID = s.xStructureID LEFT JOIN dbo.MD_ParamaterList pl on s.xStructureStatusParID = pl.ParamListID LEFT JOIN dbo.MD_ExplorationAsset a on s.xAssetID = a.xAssetID LEFT JOIN dbo.MD_ExplorationBasin ba on s.BasinID = ba.BasinID LEFT JOIN dbo.MD_ExplorationBlock bl on s.xBlockID = bl.xBlockID WHERE pl.ParamID = 'ExplorationStructureStatus' AND (s.ZonaID = '{usernameSession}' or s.RegionalID = '{usernameSession}' or s.SubholdingID = '{usernameSession}') ORDER BY esdc.{columnId}");

                    result.Items = items.Select(item => new LookupItem
                    {
                        Text = item,
                        Value = item
                    }).ToList();
                }
            }
            else if(columnId == "RKAPFiscalYear")
            {
                using (var connection = OpenConnection())
                {
                    _explorationContext.MD_ExplorationStructure.Select(o => o.MD_ExplorationAsset.xAssetName);
                    var items = await connection.QueryAsync<string>($"SELECT DISTINCT COALESCE(dr.RKAPFiscalYear,0) FROM xplore.TX_ESDC esdc LEFT JOIN dbo.MD_ExplorationStructure s  on esdc.xStructureID = s.xStructureID LEFT JOIN dbo.MD_ParamaterList pl on s.xStructureStatusParID = pl.ParamListID LEFT JOIN dbo.MD_ExplorationAsset a on s.xAssetID = a.xAssetID LEFT JOIN dbo.MD_ExplorationBasin ba on s.BasinID = ba.BasinID LEFT JOIN dbo.MD_ExplorationBlock bl on s.xBlockID = bl.xBlockID LEFT JOIN xplore.TX_Drilling dr on s.xStructureID = dr.xStructureID WHERE pl.ParamID = 'ExplorationStructureStatus' AND (s.ZonaID = '{usernameSession}' or s.RegionalID = '{usernameSession}' or s.SubholdingID = '{usernameSession}') ORDER BY COALESCE(dr.RKAPFiscalYear,0)");

                    result.Items = items.Select(item => new LookupItem
                    {
                        Text = item,
                        Value = item
                    }).ToList();
                }
            }
            else if (columnId == "CreatedDate")
            {
                using (var connection = OpenConnection())
                {
                    _explorationContext.MD_ExplorationStructure.Select(o => o.MD_ExplorationAsset.xAssetName);
                    var items = await connection.QueryAsync<DateTime>($"SELECT DISTINCT esdc.{columnId} FROM xplore.TX_ESDC esdc LEFT JOIN dbo.MD_ExplorationStructure s  on esdc.xStructureID = s.xStructureID LEFT JOIN dbo.MD_ParamaterList pl on s.xStructureStatusParID = pl.ParamListID LEFT JOIN dbo.MD_ExplorationAsset a on s.xAssetID = a.xAssetID LEFT JOIN dbo.MD_ExplorationBasin ba on s.BasinID = ba.BasinID LEFT JOIN dbo.MD_ExplorationBlock bl on s.xBlockID = bl.xBlockID WHERE pl.ParamID = 'ExplorationStructureStatus' AND (s.ZonaID = '{usernameSession}' or s.RegionalID = '{usernameSession}' or s.SubholdingID = '{usernameSession}') ORDER BY esdc.{columnId}");

                    result.Items = items.Select(item => new LookupItem
                    {
                        Text = item.ToString("yyyy-MM-dd HH:mm:ss.fff"),
                        Value = item.ToString("yyyy-MM-dd HH:mm:ss.fff")
                    }).ToList();
                }
            }
            else if (columnId == "CreatedBy")
            {
                using (var connection = OpenConnection())
                {
                    _explorationContext.MD_ExplorationStructure.Select(o => o.MD_ExplorationAsset.xAssetName);
                    var items = await connection.QueryAsync<string>($"SELECT DISTINCT COALESCE(esdc.CreatedBy,'') FROM xplore.TX_ESDC esdc LEFT JOIN dbo.MD_ExplorationStructure s  on esdc.xStructureID = s.xStructureID LEFT JOIN dbo.MD_ParamaterList pl on s.xStructureStatusParID = pl.ParamListID LEFT JOIN dbo.MD_ExplorationAsset a on s.xAssetID = a.xAssetID LEFT JOIN dbo.MD_ExplorationBasin ba on s.BasinID = ba.BasinID LEFT JOIN dbo.MD_ExplorationBlock bl on s.xBlockID = bl.xBlockID WHERE pl.ParamID = 'ExplorationStructureStatus' AND (s.ZonaID = '{usernameSession}' or s.RegionalID = '{usernameSession}' or s.SubholdingID = '{usernameSession}') ORDER BY COALESCE(esdc.CreatedBy,'')");

                    result.Items = items.Select(item => new LookupItem
                    {
                        Text = item,
                        Value = item
                    }).ToList();
                }
            }
            else if(columnId == "UpdatedDate")
            {
                using (var connection = OpenConnection())
                {
                    _explorationContext.MD_ExplorationStructure.Select(o => o.MD_ExplorationAsset.xAssetName);
                    var items = await connection.QueryAsync<DateTime>($"SELECT DISTINCT esdc.{columnId} FROM xplore.TX_ESDC esdc LEFT JOIN dbo.MD_ExplorationStructure s  on esdc.xStructureID = s.xStructureID LEFT JOIN dbo.MD_ParamaterList pl on s.xStructureStatusParID = pl.ParamListID LEFT JOIN dbo.MD_ExplorationAsset a on s.xAssetID = a.xAssetID LEFT JOIN dbo.MD_ExplorationBasin ba on s.BasinID = ba.BasinID LEFT JOIN dbo.MD_ExplorationBlock bl on s.xBlockID = bl.xBlockID WHERE pl.ParamID = 'ExplorationStructureStatus' AND (s.ZonaID = '{usernameSession}' or s.RegionalID = '{usernameSession}' or s.SubholdingID = '{usernameSession}') ORDER BY esdc.{columnId}");

                    result.Items = items.Select(item => new LookupItem
                    {
                        Text = item.ToString("yyyy-MM-dd HH:mm:ss.fff"),
                        Value = item.ToString("yyyy-MM-dd HH:mm:ss.fff")
                    }).ToList();
                }
            }
            else if(columnId == "UpdatedBy")
            {
                using (var connection = OpenConnection())
                {
                    _explorationContext.MD_ExplorationStructure.Select(o => o.MD_ExplorationAsset.xAssetName);
                    var items = await connection.QueryAsync<string>($"SELECT DISTINCT esdc.{columnId} FROM xplore.TX_ESDC esdc LEFT JOIN dbo.MD_ExplorationStructure s  on esdc.xStructureID = s.xStructureID LEFT JOIN dbo.MD_ParamaterList pl on s.xStructureStatusParID = pl.ParamListID LEFT JOIN dbo.MD_ExplorationAsset a on s.xAssetID = a.xAssetID LEFT JOIN dbo.MD_ExplorationBasin ba on s.BasinID = ba.BasinID LEFT JOIN dbo.MD_ExplorationBlock bl on s.xBlockID = bl.xBlockID WHERE pl.ParamID = 'ExplorationStructureStatus' AND (s.ZonaID = '{usernameSession}' or s.RegionalID = '{usernameSession}' or s.SubholdingID = '{usernameSession}') ORDER BY esdc.{columnId}");

                    result.Items = items.Select(item => new LookupItem
                    {
                        Text = item,
                        Value = item
                    }).ToList();
                }
            }
            else
            {
                using (var connection = OpenConnection())
                {
                    _explorationContext.MD_ExplorationStructure.Select(o => o.MD_ExplorationAsset.xAssetName);
                    var items = await connection.QueryAsync<string>($"SELECT DISTINCT s.{columnId} FROM xplore.TX_ESDC esdc LEFT JOIN dbo.MD_ExplorationStructure s  on esdc.xStructureID = s.xStructureID LEFT JOIN dbo.MD_ParamaterList pl on s.xStructureStatusParID = pl.ParamListID LEFT JOIN dbo.MD_ExplorationAsset a on s.xAssetID = a.xAssetID LEFT JOIN dbo.MD_ExplorationBasin ba on s.BasinID = ba.BasinID LEFT JOIN dbo.MD_ExplorationBlock bl on s.xBlockID = bl.xBlockID WHERE pl.ParamID = 'ExplorationStructureStatus' AND (s.ZonaID = '{usernameSession}' or s.RegionalID = '{usernameSession}' or s.SubholdingID = '{usernameSession}') ORDER BY s.{columnId}");

                    result.Items = items.Select(item => new LookupItem
                    {
                        Text = item,
                        Value = item
                    }).ToList();
                }
            }


            result.Items = result.Items.GroupBy(o => o.Text).Select(o => o.FirstOrDefault()).ToList();

            return result;
        }

        public List<MD_ExplorationStructure> GetByStructureName(string structureName)
        {
            return _explorationContext.MD_ExplorationStructure.Where(o => o.xStructureName == structureName).ToList();
        }

        public async Task<string> GenerateNewID()
        {
            try
            {
                //var getCount = await GetCountDataTable();
                //var idTmp = "xS00000001";
                //var Testid = Regex.Replace(idTmp, "\\d+", m => (int.Parse(m.Value) + 1).ToString(new string('0', m.Value.Length)));
                //var sequence = int.Parse(getCount);
                ////var sequence = 9090909090;
                //var literal = "xS";
                //var id = literal + (sequence + 1).ToString("0000000");
                //return id;

                var getCount = await GetCountDataTable();
                if(!string.IsNullOrEmpty(getCount))
                {
                    var newId = Regex.Replace(getCount, "\\d+", m => (int.Parse(m.Value) + 1).ToString(new string('0', m.Value.Length)));
                    return newId;
                }
                else
                {
                    var idTmp = "xS0000000";
                    var newId = Regex.Replace(idTmp, "\\d+", m => (int.Parse(m.Value) + 1).ToString(new string('0', m.Value.Length)));
                    return newId;
                }

            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
