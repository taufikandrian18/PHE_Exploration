using SHUNetMVC.Abstraction.Enum;
using SHUNetMVC.Abstraction.Model.View;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace SHUNetMVC.Infrastructure.EntityFramework.Queries
{
    public class FilterBuilder
    {
        public static string BuildFilter(List<ColumnDefinition> columns, List<List<FilterItem>> param, string HRISObj)
        {
            StringBuilder sb = new StringBuilder();
            if (columns != null)
            {
                if (columns.Count() >= 8)
                {
                    if (columns[8].Name == "ESDC Status")
                        sb.Append(" AND (es.ZonaID = '" + HRISObj + "' or es.RegionalID = '" + HRISObj + "' or es.SubholdingID = '" + HRISObj + "')");
                    else
                        sb.Append(" AND (s.ZonaID = '" + HRISObj + "' or s.RegionalID = '" + HRISObj + "' or s.SubholdingID = '" + HRISObj + "')");
                }
            }
            sb.Append(" AND (");

            var filterCount = 0;
            for (int i = 0; i < param.Count; i++)
            {
                List<FilterItem> paramItem = param[i];
                sb.Append(i == 0 ? "" : " AND ");
                for (int j = 0; j < paramItem.Count; j++)
                {
                    FilterItem filter = paramItem[j];
                    var column = columns.FirstOrDefault(o => o.Id == filter.Name);
                    var columnName = filter.Name;

                    if (column != null)
                    {
                        columnName = column.GetColumnName();
                    }
                  

                    if (columnName == "AnyField")
                    {
                        var queryAnyField = BuildAnyFieldContain(columns, filter.Value);
                        sb.Append(queryAnyField);
                        filterCount++;
                        break;
                    }

                    sb.Append(j == 0 ? "(" : " OR ");

                    switch (filter.FilterType)
                    {
                        case FilterType.Equal:
                            sb.Append($"{columnName} = '{filter.Value}'");
                            break;
                        case FilterType.NotEqual:
                            sb.Append($"{columnName} != '{filter.Value}'");
                            break;
                        case FilterType.BeginWith:
                            sb.Append($"LOWER({columnName}) LIKE LOWER('{filter.Value}%')");
                            break;
                        case FilterType.NotBeginWith:
                            sb.Append($"LOWER({columnName}) NOT LIKE LOWER('{filter.Value}%')");
                            break;
                        case FilterType.Contains:
                            sb.Append($"LOWER({columnName}) LIKE LOWER('%{filter.Value}%')");
                            break;
                        case FilterType.NotContains:
                            sb.Append($"LOWER({columnName}) NOT LIKE LOWER('%{filter.Value}%')");
                            break;
                        case FilterType.EndWith:
                            sb.Append($"LOWER({columnName}) LIKE LOWER('%{filter.Value}')");
                            break;
                        case FilterType.NotEndWith:
                            sb.Append($"LOWER({columnName}) NOT LIKE LOWER('%{filter.Value}')");
                            break;
                        case FilterType.LessThan:
                            sb.Append($"{columnName} < '{filter.Value}'");
                            break;
                        case FilterType.GreaterThan:
                            sb.Append($"{columnName} > '{filter.Value}'");
                            break;
                        case FilterType.LessThanOrEqual:
                            sb.Append($"{columnName} <= '{filter.Value}'");
                            break;
                        case FilterType.GreaterThanOrEqual:
                            sb.Append($"{columnName} >= '{filter.Value}'");
                            break;
                        case FilterType.NotEmpty:
                            sb.Append($"{columnName} <> ''");
                            break;
                        case FilterType.Empty:
                            sb.Append($"ISNULL({columnName},'') = ''");
                            break;
                        case FilterType.Includes:
                            if (string.IsNullOrWhiteSpace(filter.Value))
                            {
                                if (filter.Value != null)
                                {
                                    sb.Append($"{columnName} = '{filter.Value}'");
                                    sb.Append($" OR {columnName} IS NULL");
                                }
                                else
                                {
                                    sb.Append($"{columnName} = '{filter.Value}'");
                                }
                            }
                            else
                            {
                                sb.Append($"{columnName} = '{filter.Value}'");
                            }
                            break;
                        default:
                        case FilterType.Undefined:
                            throw new System.Exception($"Cannot build {filter.FilterType}, of {filter.Name} : {filter.Value}");
                    }
                    filterCount++;
                    if (j == paramItem.Count - 1)
                    {
                        sb.Append(')');
                    }
                }
            }

            if (filterCount == 0)
            {
                if (columns != null)
                {
                    if (columns.Count() >= 8)
                    {
                        if (columns[8].Name == "ESDC Status")
                        {
                            return " AND (es.ZonaID = '" + HRISObj + "' or es.RegionalID = '" + HRISObj + "' or es.SubholdingID = '" + HRISObj + "') ";
                        }
                        else
                        {
                            return " AND (s.ZonaID = '" + HRISObj + "' or s.RegionalID = '" + HRISObj + "' or s.SubholdingID = '" + HRISObj + "') ";
                        }
                    }
                    else
                    {
                        return "";
                    }
                }
                else
                {
                    return "";
                }
            }


            sb.Append(')');
            return sb.ToString();
        }

        public static string BuildFilterProsResource(List<ColumnDefinition> columns, List<List<FilterItem>> param, string HRISObj)
        {
            StringBuilder sb = new StringBuilder();
            if (columns != null)
            {
                if (columns.Count() >= 8)
                {
                    if (columns[8].Name == "ESDC Status")
                        sb.Append(" AND (es.ZonaID = '" + HRISObj + "' or es.RegionalID = '" + HRISObj + "' or es.SubholdingID = '" + HRISObj + "')");
                    else
                        sb.Append(" AND (es.ZonaID = '" + HRISObj + "' or es.RegionalID = '" + HRISObj + "' or es.SubholdingID = '" + HRISObj + "')");
                }
            }
            sb.Append(" AND (");

            var filterCount = 0;
            for (int i = 0; i < param.Count; i++)
            {
                List<FilterItem> paramItem = param[i];
                sb.Append(i == 0 ? "" : " AND ");
                for (int j = 0; j < paramItem.Count; j++)
                {
                    FilterItem filter = paramItem[j];
                    var column = columns.FirstOrDefault(o => o.Id == filter.Name);
                    var columnName = "";
                    if(filter.Name == "xStructureID")
                    {
                        columnName = "es." + filter.Name;
                    }
                    else if(filter.Name == "CountriesID" || filter.Name == "CreatedDate" || filter.Name == "UpdatedDate")
                    {
                        columnName = "es." + filter.Name;
                    }
                    else if(filter.Name == "ZonaName")
                    {
                        columnName = "(select dim.EntityName from dbo.vw_DIM_Entity dim where dim.EntityID = es.ZonaID)";
                    }
                    else if(filter.Name == "RegionalName")
                    {
                        columnName = "(select dim.EntityName from dbo.vw_DIM_Entity dim where dim.EntityID = es.RegionalID)";
                    }
                    else if(filter.Name == "UpdatedBy")
                    {
                        columnName = "COALESCE(es.UpdatedBy,'')";
                    }
                    else
                    {
                        columnName = filter.Name;
                    }

                    if (column != null)
                    {
                        columnName = column.GetColumnName();
                        if (columnName == "xStructureID")
                        {
                            columnName = "es." + columnName;
                        }
                        else if (columnName == "CountriesID" || columnName == "CreatedDate" || columnName == "UpdatedDate")
                        {
                            columnName = "es." + columnName;
                        }
                        else if (columnName == "ZonaName")
                        {
                            columnName = "(select dim.EntityName from dbo.vw_DIM_Entity dim where dim.EntityID = es.ZonaID)";
                        }
                        else if (columnName == "RegionalName")
                        {
                            columnName = "(select dim.EntityName from dbo.vw_DIM_Entity dim where dim.EntityID = es.RegionalID)";
                        }
                        else if (columnName == "CreatedBy")
                        {
                            columnName = "COALESCE(es.CreatedBy,'')";
                        }
                        else if (columnName == "UpdatedBy")
                        {
                            columnName = "COALESCE(es.UpdatedBy,'')";
                        }
                        //else
                        //{
                        //    columnName = columnName;
                        //}
                    }


                    if (columnName == "AnyField")
                    {
                        var queryAnyField = BuildAnyFieldContain(columns, filter.Value);
                        sb.Append(queryAnyField);
                        filterCount++;
                        break;
                    }

                    sb.Append(j == 0 ? "(" : " OR ");

                    switch (filter.FilterType)
                    {
                        case FilterType.Equal:
                            sb.Append($"{columnName} = '{filter.Value}'");
                            break;
                        case FilterType.NotEqual:
                            sb.Append($"{columnName} != '{filter.Value}'");
                            break;
                        case FilterType.BeginWith:
                            sb.Append($"LOWER({columnName}) LIKE LOWER('{filter.Value}%')");
                            break;
                        case FilterType.NotBeginWith:
                            sb.Append($"LOWER({columnName}) NOT LIKE LOWER('{filter.Value}%')");
                            break;
                        case FilterType.Contains:
                            sb.Append($"LOWER({columnName}) LIKE LOWER('%{filter.Value}%')");
                            break;
                        case FilterType.NotContains:
                            sb.Append($"LOWER({columnName}) NOT LIKE LOWER('%{filter.Value}%')");
                            break;
                        case FilterType.EndWith:
                            sb.Append($"LOWER({columnName}) LIKE LOWER('%{filter.Value}')");
                            break;
                        case FilterType.NotEndWith:
                            sb.Append($"LOWER({columnName}) NOT LIKE LOWER('%{filter.Value}')");
                            break;
                        case FilterType.LessThan:
                            sb.Append($"{columnName} < '{filter.Value}'");
                            break;
                        case FilterType.GreaterThan:
                            sb.Append($"{columnName} > '{filter.Value}'");
                            break;
                        case FilterType.LessThanOrEqual:
                            sb.Append($"{columnName} <= '{filter.Value}'");
                            break;
                        case FilterType.GreaterThanOrEqual:
                            sb.Append($"{columnName} >= '{filter.Value}'");
                            break;
                        case FilterType.NotEmpty:
                            sb.Append($"{columnName} <> ''");
                            break;
                        case FilterType.Empty:
                            sb.Append($"ISNULL({columnName},'') = ''");
                            break;
                        case FilterType.Includes:
                            if (string.IsNullOrWhiteSpace(filter.Value))
                            {
                                if (filter.Value != null)
                                {
                                    sb.Append($"{columnName} = '{filter.Value}'");
                                    sb.Append($" OR {columnName} IS NULL");
                                }
                                else
                                {
                                    sb.Append($"{columnName} = '{filter.Value}'");
                                }
                            }
                            else
                            {
                                sb.Append($"{columnName} = '{filter.Value}'");
                            }
                            break;
                        default:
                        case FilterType.Undefined:
                            throw new System.Exception($"Cannot build {filter.FilterType}, of {filter.Name} : {filter.Value}");
                    }
                    filterCount++;
                    if (j == paramItem.Count - 1)
                    {
                        sb.Append(')');
                    }
                }
            }

            if (filterCount == 0)
            {
                if (columns != null)
                {
                    if (columns.Count() >= 8)
                    {
                        if (columns[8].Name == "ESDC Status")
                        {
                            return " AND (es.ZonaID = '" + HRISObj + "' or es.RegionalID = '" + HRISObj + "' or es.SubholdingID = '" + HRISObj + "') ";
                        }
                        else
                        {
                            return " AND (es.ZonaID = '" + HRISObj + "' or es.RegionalID = '" + HRISObj + "' or es.SubholdingID = '" + HRISObj + "') ";
                        }
                    }
                    else
                    {
                        return "";
                    }
                }
                else
                {
                    return "";
                }
            }


            sb.Append(')');
            return sb.ToString();
        }

        public static string BuildFilterESDC(List<ColumnDefinition> columns, List<List<FilterItem>> param, string HRISObj)
        {
            StringBuilder sb = new StringBuilder();
            if (columns != null)
            {
                if (columns.Count() >= 8)
                {
                    if (columns[8].Name == "ESDC Status")
                        sb.Append(" AND (es.ZonaID = '" + HRISObj + "' or es.RegionalID = '" + HRISObj + "' or es.SubholdingID = '" + HRISObj + "')");
                    else
                        sb.Append(" AND (s.ZonaID = '" + HRISObj + "' or s.RegionalID = '" + HRISObj + "' or s.SubholdingID = '" + HRISObj + "')");
                }
            }
            sb.Append(" AND (");

            var filterCount = 0;
            for (int i = 0; i < param.Count; i++)
            {
                List<FilterItem> paramItem = param[i];
                sb.Append(i == 0 ? "" : " AND ");
                for (int j = 0; j < paramItem.Count; j++)
                {
                    FilterItem filter = paramItem[j];
                    var column = columns.FirstOrDefault(o => o.Id == filter.Name);
                    var columnName = "";
                    if (filter.Name == "xStructureID")
                    {
                        columnName = "esdc." + filter.Name;
                    }
                    else if (filter.Name == "CountriesID" || filter.Name == "UpdatedDate" || filter.Name == "CreatedDate")
                    {
                        columnName = "esdc." + filter.Name;
                    }
                    else if (filter.Name == "ZonaName")
                    {
                        columnName = "(select dim.EntityName from dbo.vw_DIM_Entity dim where dim.EntityID = es.ZonaID)";
                    }
                    else if(filter.Name == "RKAPFiscalYear")
                    {
                        columnName = "COALESCE((select DISTINCT dr.RKAPFiscalYear from xplore.TX_Drilling dr where dr.xStructureID = esdc.xStructureID),0)";
                    }
                    else if (filter.Name == "RegionalName")
                    {
                        columnName = "(select dim.EntityName from dbo.vw_DIM_Entity dim where dim.EntityID = es.RegionalID)";
                    }
                    else if (filter.Name == "UpdatedBy")
                    {
                        columnName = "COALESCE(esdc.UpdatedBy,'')";
                    }
                    else if (filter.Name == "CreatedBy")
                    {
                        columnName = "COALESCE(esdc.CreatedBy,'')";
                    }
                    else
                    {
                        columnName = filter.Name;
                    }

                    if (column != null)
                    {
                        columnName = column.GetColumnName();
                        if (columnName == "xStructureID")
                        {
                            columnName = "es." + columnName;
                        }
                        else if (filter.Name == "CountriesID")
                        {
                            columnName = "es." + columnName;
                        }
                        //else
                        //{
                        //    columnName = columnName;
                        //}
                    }


                    if (columnName == "AnyField")
                    {
                        var queryAnyField = BuildAnyFieldContain(columns, filter.Value);
                        sb.Append(queryAnyField);
                        filterCount++;
                        break;
                    }

                    sb.Append(j == 0 ? "(" : " OR ");

                    switch (filter.FilterType)
                    {
                        case FilterType.Equal:
                            sb.Append($"{columnName} = '{filter.Value}'");
                            break;
                        case FilterType.NotEqual:
                            sb.Append($"{columnName} != '{filter.Value}'");
                            break;
                        case FilterType.BeginWith:
                            sb.Append($"LOWER({columnName}) LIKE LOWER('{filter.Value}%')");
                            break;
                        case FilterType.NotBeginWith:
                            sb.Append($"LOWER({columnName}) NOT LIKE LOWER('{filter.Value}%')");
                            break;
                        case FilterType.Contains:
                            sb.Append($"LOWER({columnName}) LIKE LOWER('%{filter.Value}%')");
                            break;
                        case FilterType.NotContains:
                            sb.Append($"LOWER({columnName}) NOT LIKE LOWER('%{filter.Value}%')");
                            break;
                        case FilterType.EndWith:
                            sb.Append($"LOWER({columnName}) LIKE LOWER('%{filter.Value}')");
                            break;
                        case FilterType.NotEndWith:
                            sb.Append($"LOWER({columnName}) NOT LIKE LOWER('%{filter.Value}')");
                            break;
                        case FilterType.LessThan:
                            sb.Append($"{columnName} < '{filter.Value}'");
                            break;
                        case FilterType.GreaterThan:
                            sb.Append($"{columnName} > '{filter.Value}'");
                            break;
                        case FilterType.LessThanOrEqual:
                            sb.Append($"{columnName} <= '{filter.Value}'");
                            break;
                        case FilterType.GreaterThanOrEqual:
                            sb.Append($"{columnName} >= '{filter.Value}'");
                            break;
                        case FilterType.NotEmpty:
                            sb.Append($"{columnName} <> ''");
                            break;
                        case FilterType.Empty:
                            sb.Append($"ISNULL({columnName},'') = ''");
                            break;
                        case FilterType.Includes:
                            if (string.IsNullOrWhiteSpace(filter.Value))
                            {
                                if (filter.Value != null)
                                {
                                    sb.Append($"{columnName} = '{filter.Value}'");
                                    sb.Append($" OR {columnName} IS NULL");
                                }
                                else
                                {
                                    sb.Append($"{columnName} = '{filter.Value}'");
                                }
                            }
                            else
                            {
                                sb.Append($"{columnName} = '{filter.Value}'");
                            }
                            break;
                        default:
                        case FilterType.Undefined:
                            throw new System.Exception($"Cannot build {filter.FilterType}, of {filter.Name} : {filter.Value}");
                    }
                    filterCount++;
                    if (j == paramItem.Count - 1)
                    {
                        sb.Append(')');
                    }
                }
            }

            if (filterCount == 0)
            {
                if (columns != null)
                {
                    if (columns.Count() >= 8)
                    {
                        if (columns[8].Name == "ESDC Status")
                        {
                            return " AND (es.ZonaID = '" + HRISObj + "' or es.RegionalID = '" + HRISObj + "' or es.SubholdingID = '" + HRISObj + "') ";
                        }
                        else
                        {
                            return " AND (s.ZonaID = '" + HRISObj + "' or s.RegionalID = '" + HRISObj + "' or s.SubholdingID = '" + HRISObj + "') ";
                        }
                    }
                    else
                    {
                        return "";
                    }
                }
                else
                {
                    return "";
                }
            }


            sb.Append(')');
            return sb.ToString();
        }

        private static string BuildAnyFieldContain(List<ColumnDefinition> columns, string keywords)
        {
            var listFilter = new List<string>();
            var listKeywords = keywords.Trim().Split(' ');

            foreach (var column in columns)
            {
                foreach (var keyword in listKeywords)
                {
                    listFilter.Add($"LOWER({column.GetColumnName()}) LIKE LOWER('%{keyword}%')\n");
                }
            }
            return  $"({string.Join(" OR ", listFilter)})";
        }
    }
}
