using SHUNetMVC.Abstraction.Model.View;
using ClosedXML.Excel;
using System.Linq;
using System.Reflection;
using System;
using System.Collections.Generic;
using SHUNetMVC.Abstraction.Model.Response;
using SHUNetMVC.Abstraction.Model.Dto;

namespace SHUNetMVC.Infrastructure.Helpers
{
    public static class SpreadsheetGenerator
    {
        public static XLWorkbook Generate(string title,
            ExportExcelESDC items,
            List<ColumnDefinition> columnDefinitions,
            FilterList filter)
        {
            try
            {
                var columnDefinitionESDC = new List<ColumnDefinition>
                {
                    new ColumnDefinition("StructureID", nameof(ESDCExcelDto.xStructureID), ColumnType.String,"esdc.xStructureID"),
                    new ColumnDefinition("Project ID", nameof(ESDCExcelDto.ESDCProjectID), ColumnType.String, "esdc.ESDCProjectID"),
                    new ColumnDefinition("Project Name", nameof(ESDCExcelDto.ESDCProjectName), ColumnType.String,"esdc.ESDCProjectName"),
                    new ColumnDefinition("Field ID", nameof(ESDCExcelDto.ESDCFieldID), ColumnType.String,"esdc.ESDCFieldID"),
                    new ColumnDefinition("Region", nameof(ESDCExcelDto.RegionalID), ColumnType.String,"(select plm.ParamValue1Text from dbo.MD_ParamaterList plm where plm.ParamListID = 'Expcel-1') + ' ' +  es.RegionalID"),
                    new ColumnDefinition("Field Name", nameof(ESDCExcelDto.xStructureName), ColumnType.String, "es.xStructureName"),
                    new ColumnDefinition("Project Level", nameof(ESDCExcelDto.ProjectLevel), ColumnType.String)
                };

                var columnDefinitionESDCProd = new List<ColumnDefinition>
                {
                    new ColumnDefinition("StructureID", nameof(ESDCProdExcelDto.xStructureID), ColumnType.String,"esdc.xStructureID"),
                    new ColumnDefinition("Project ID", nameof(ESDCProdExcelDto.ESDCProjectID), ColumnType.String, "esdc.ESDCProjectID"),
                    new ColumnDefinition("Asset", nameof(ESDCProdExcelDto.xAssetID), ColumnType.String,"es.xAssetID"),
                    new ColumnDefinition("Project Name", nameof(ESDCProdExcelDto.ESDCProjectName), ColumnType.String,"esdc.ESDCProjectName"),
                    new ColumnDefinition("Gross Cumulative Production Previous Report (Oil MSTB)", nameof(ESDCProdExcelDto.GCPPrevOil), ColumnType.Number),
                    new ColumnDefinition("Gross Cumulative Production Previous Report (Condensate MSTB)", nameof(ESDCProdExcelDto.GCPPrevCondensate), ColumnType.Number),
                    new ColumnDefinition("Gross Cumulative Production Previous Report (Associated Gas BSCF)", nameof(ESDCProdExcelDto.GCPPrevAssociated), ColumnType.Number),
                    new ColumnDefinition("Gross Cumulative Production Previous Report (Non Associated Gas BSCF)", nameof(ESDCProdExcelDto.GCPPrevNonAssociated), ColumnType.Number),
                    new ColumnDefinition("Sales Cumulative Production Previous Report (Oil MSTB)", nameof(ESDCProdExcelDto.SCPPrevOil), ColumnType.Number),
                    new ColumnDefinition("Sales Cumulative Production Previous Report (Condensate MSTB)", nameof(ESDCProdExcelDto.SCPPrevCondensate), ColumnType.Number),
                    new ColumnDefinition("Sales Cumulative Production Previous Report (Associated Gas BSCF)", nameof(ESDCProdExcelDto.SCPPrevAssociated), ColumnType.Number),
                    new ColumnDefinition("Sales Cumulative Production Previous Report (Non Associated Gas BSCF)", nameof(ESDCProdExcelDto.SCPPrevNonAssociated), ColumnType.Number),
                    new ColumnDefinition("GCP (Oil MSTB)", nameof(ESDCProdExcelDto.GCPOil), ColumnType.Number),
                    new ColumnDefinition("GCP (Condensate MSTB)", nameof(ESDCProdExcelDto.GCPCondensate), ColumnType.Number),
                    new ColumnDefinition("GCP (Associated Gas BSCF)", nameof(ESDCProdExcelDto.GCPAssociated), ColumnType.Number),
                    new ColumnDefinition("GCP (Non Associated Gas BSCF)", nameof(ESDCProdExcelDto.GCPNonAssociated), ColumnType.Number),
                    new ColumnDefinition("SCP (Oil MSTB)", nameof(ESDCProdExcelDto.SCPOil), ColumnType.Number),
                    new ColumnDefinition("SCP (Condensate MSTB)", nameof(ESDCProdExcelDto.SCPCondensate), ColumnType.Number),
                    new ColumnDefinition("SCP (Associated Gas BSCF)", nameof(ESDCProdExcelDto.SCPAssociated), ColumnType.Number),
                    new ColumnDefinition("SCP (Non Associated Gas BSCF)", nameof(ESDCProdExcelDto.SCPNonAssociated), ColumnType.Number)
                };

                var columnDefinitionESDCVolumetric = new List<ColumnDefinition>
                {
                    new ColumnDefinition("StructureID", nameof(ESDCVolumetricExcelDto.xStructureID), ColumnType.String,"esdc.xStructureID"),
                    new ColumnDefinition("Project ID", nameof(ESDCVolumetricExcelDto.ESDCProjectID), ColumnType.String, "esdc.ESDCProjectID"),
                    new ColumnDefinition("Asset", nameof(ESDCVolumetricExcelDto.xAssetID), ColumnType.String,"es.xAssetID"),
                    new ColumnDefinition("Project Name", nameof(ESDCVolumetricExcelDto.ESDCProjectName), ColumnType.String,"esdc.ESDCProjectName"),
                    new ColumnDefinition("Uncertainty Level", nameof(ESDCVolumetricExcelDto.UncertaintyLevel), ColumnType.String),
                    new ColumnDefinition("GRR / CR / PR Previous (Oil MSTB)", nameof(ESDCVolumetricExcelDto.GRRPrevOil), ColumnType.Number),
                    new ColumnDefinition("GRR / CR / PR Previous (Condensate MSTB)", nameof(ESDCVolumetricExcelDto.GRRPrevCondensate), ColumnType.Number),
                    new ColumnDefinition("GRR / CR / PR Previous (Associated Gas BSCF)", nameof(ESDCVolumetricExcelDto.GRRPrevAssociated), ColumnType.Number),
                    new ColumnDefinition("GRR / CR / PR Previous (Non Associated Gas BSCF)", nameof(ESDCVolumetricExcelDto.GRRPrevNonAssociated), ColumnType.Number),
                    new ColumnDefinition("Reserves Previous (Oil MSTB)", nameof(ESDCVolumetricExcelDto.ReservesPrevOil), ColumnType.Number),
                    new ColumnDefinition("Reserves Previous (Condensate MSTB)", nameof(ESDCVolumetricExcelDto.ReservesPrevCondensate), ColumnType.Number),
                    new ColumnDefinition("Reserves Previous (Associated Gas BSCF)", nameof(ESDCVolumetricExcelDto.ReservesPrevAssociated), ColumnType.Number),
                    new ColumnDefinition("Reserves Previous (Non Associated Gas BSCF)", nameof(ESDCVolumetricExcelDto.ReservesPrevNonAssociated), ColumnType.Number),
                    new ColumnDefinition("GOI Recoverable Resources / Contingent Resources / Prospective Resources (Oil MSTB)", nameof(ESDCVolumetricExcelDto.GOIOil), ColumnType.Number),
                    new ColumnDefinition("GOI Recoverable Resources / Contingent Resources / Prospective Resources (Condensate MSTB)", nameof(ESDCVolumetricExcelDto.GOICondensate), ColumnType.Number),
                    new ColumnDefinition("GOI Recoverable Resources / Contingent Resources / Prospective Resources (Associated Gas BSCF)", nameof(ESDCVolumetricExcelDto.GOIAssociated), ColumnType.Number),
                    new ColumnDefinition("GOI Recoverable Resources / Contingent Resources / Prospective Resources (Non Associated Gas BSCF)", nameof(ESDCVolumetricExcelDto.GOINonAssociated), ColumnType.Number),
                    new ColumnDefinition("RSV (Oil MSTB)", nameof(ESDCVolumetricExcelDto.ReservesOil), ColumnType.Number),
                    new ColumnDefinition("RSV (Condensate MSTB)", nameof(ESDCVolumetricExcelDto.ReservesCondensate), ColumnType.Number),
                    new ColumnDefinition("RSV (Associated Gas BSCF)", nameof(ESDCVolumetricExcelDto.ReservesAssociated), ColumnType.Number),
                    new ColumnDefinition("RSV (Non Associated Gas BSCF)", nameof(ESDCVolumetricExcelDto.ReservesNonAssociated), ColumnType.Number),
                    new ColumnDefinition("Remark", nameof(ESDCVolumetricExcelDto.Remarks), ColumnType.String),
                };

                var columnDefinitionESDCForecast = new List<ColumnDefinition>
                {
                    new ColumnDefinition("StructureID", nameof(ESDCForecastExcelDto.xStructureID), ColumnType.String,"esdc.xStructureID"),
                    new ColumnDefinition("Project ID", nameof(ESDCForecastExcelDto.ESDCProjectID), ColumnType.String, "esdc.ESDCProjectID"),
                    new ColumnDefinition("Asset", nameof(ESDCForecastExcelDto.xAssetID), ColumnType.String,"es.xAssetID"),
                    new ColumnDefinition("Project Name", nameof(ESDCForecastExcelDto.ESDCProjectName), ColumnType.String,"esdc.ESDCProjectName"),
                    new ColumnDefinition("Year", nameof(ESDCForecastExcelDto.Year), ColumnType.Number),
                    new ColumnDefinition("Total Potential Forecast (2R/2C/2U) (Oil MSTB)", nameof(ESDCForecastExcelDto.TPFOil), ColumnType.Number),
                    new ColumnDefinition("Total Potential Forecast (2R/2C/2U) (Condensate MSTB)", nameof(ESDCForecastExcelDto.TPFCondensate), ColumnType.Number),
                    new ColumnDefinition("Total Potential Forecast (2R/2C/2U) (Associated Gas BSCF)", nameof(ESDCForecastExcelDto.TPFAssociated), ColumnType.Number),
                    new ColumnDefinition("Total Potential Forecast (2R/2C/2U) (Non Associated Gas BSCF)", nameof(ESDCForecastExcelDto.TPFNonAssociated), ColumnType.Number),
                    new ColumnDefinition("Sales Forecast (2P) (Oil MSTB)", nameof(ESDCForecastExcelDto.SFOil), ColumnType.Number),
                    new ColumnDefinition("Sales Forecast (2P) (Condensate MSTB)", nameof(ESDCForecastExcelDto.SFCondensate), ColumnType.Number),
                    new ColumnDefinition("Sales Forecast (2P) (Associated Gas BSCF)", nameof(ESDCForecastExcelDto.SFAssociated), ColumnType.Number),
                    new ColumnDefinition("Sales Forecast (2P) (Non Associated Gas BSCF)", nameof(ESDCForecastExcelDto.SFNonAssociated), ColumnType.Number),
                    new ColumnDefinition("Consumed in Operations (Oil MSTB)", nameof(ESDCForecastExcelDto.CIOOil), ColumnType.Number),
                    new ColumnDefinition("Consumed in Operations (Condensate MSTB)", nameof(ESDCForecastExcelDto.CIOCondensate), ColumnType.Number),
                    new ColumnDefinition("Consumed in Operations (Associated Gas BSCF)", nameof(ESDCForecastExcelDto.CIOAssociated), ColumnType.Number),
                    new ColumnDefinition("Consumed in Operations (Non Associated Gas BSCF)", nameof(ESDCForecastExcelDto.CIONonAssociated), ColumnType.Number),
                    new ColumnDefinition("Loss Production (Oil MSTB)", nameof(ESDCForecastExcelDto.LPOil), ColumnType.Number),
                    new ColumnDefinition("Loss Production (Condensate MSTB)", nameof(ESDCForecastExcelDto.LPCondensate), ColumnType.Number),
                    new ColumnDefinition("Loss Production (Associated Gas BSCF)", nameof(ESDCForecastExcelDto.LPAssociated), ColumnType.Number),
                    new ColumnDefinition("Loss Production (Non Associated Gas BSCF)", nameof(ESDCForecastExcelDto.LPNonAssociated), ColumnType.Number),
                    new ColumnDefinition("Average Gross Heat Value (BBTU/MMSCF)", nameof(ESDCForecastExcelDto.AverageGrossHeat), ColumnType.Number),
                    new ColumnDefinition("Remark", nameof(ESDCForecastExcelDto.Remarks), ColumnType.String),
                };

                var columnDefinitionESDCDiscrepancy = new List<ColumnDefinition>
                {
                    new ColumnDefinition("StructureID", nameof(ESDCDiscrepancyExcelDto.xStructureID), ColumnType.String,"esdc.xStructureID"),
                    new ColumnDefinition("Project ID", nameof(ESDCDiscrepancyExcelDto.ESDCProjectID), ColumnType.String, "esdc.ESDCProjectID"),
                    new ColumnDefinition("Asset", nameof(ESDCDiscrepancyExcelDto.xAssetID), ColumnType.String,"es.xAssetID"),
                    new ColumnDefinition("Project Name", nameof(ESDCDiscrepancyExcelDto.ESDCProjectName), ColumnType.String,"esdc.ESDCProjectName"),
                    new ColumnDefinition("Uncertainty Level", nameof(ESDCDiscrepancyExcelDto.UncertaintyLevel), ColumnType.String),
                    new ColumnDefinition("Change from Update Model (Oil MSTB)", nameof(ESDCDiscrepancyExcelDto.CFUMOil), ColumnType.Number),
                    new ColumnDefinition("Change from Update Model (Condensate MSTB)", nameof(ESDCDiscrepancyExcelDto.CFUMCondensate), ColumnType.Number),
                    new ColumnDefinition("Change from Update Model (Associated Gas BSCF)", nameof(ESDCDiscrepancyExcelDto.CFUMAssociated), ColumnType.Number),
                    new ColumnDefinition("Change from Update Model (Non Associated Gas BSCF)", nameof(ESDCDiscrepancyExcelDto.CFUMNonAssociated), ColumnType.Number),
                    new ColumnDefinition("Change from Production Performance Analysis (Oil MSTB)", nameof(ESDCDiscrepancyExcelDto.CFPPAOil), ColumnType.Number),
                    new ColumnDefinition("Change from Production Performance Analysis (Condensate MSTB)", nameof(ESDCDiscrepancyExcelDto.CFPPACondensate), ColumnType.Number),
                    new ColumnDefinition("Change from Production Performance Analysis (Associated Gas BSCF)", nameof(ESDCDiscrepancyExcelDto.CFPPAAssociated), ColumnType.Number),
                    new ColumnDefinition("Change from Production Performance Analysis (Non Associated Gas BSCF)", nameof(ESDCDiscrepancyExcelDto.CFPPANonAssociated), ColumnType.Number),
                    new ColumnDefinition("Change from Well Intervention (Oil MSTB)", nameof(ESDCDiscrepancyExcelDto.CFWIOil), ColumnType.Number),
                    new ColumnDefinition("Change from Well Intervention (Condensate MSTB)", nameof(ESDCDiscrepancyExcelDto.CFWICondensate), ColumnType.Number),
                    new ColumnDefinition("Change from Well Intervention (Associated Gas BSCF)", nameof(ESDCDiscrepancyExcelDto.CFWIAssociated), ColumnType.Number),
                    new ColumnDefinition("Change from Well Intervention (Non Associated Gas BSCF)", nameof(ESDCDiscrepancyExcelDto.CFWINonAssociated), ColumnType.Number),
                    new ColumnDefinition("Change from Commerciality (Oil MSTB)", nameof(ESDCDiscrepancyExcelDto.CFCOil), ColumnType.Number),
                    new ColumnDefinition("Change from Commerciality (Condensate MSTB)", nameof(ESDCDiscrepancyExcelDto.CFCCondensate), ColumnType.Number),
                    new ColumnDefinition("Change from Commerciality (Associated Gas BSCF)", nameof(ESDCDiscrepancyExcelDto.CFCAssociated), ColumnType.Number),
                    new ColumnDefinition("Change from Commerciality (Non Associated Gas BSCF)", nameof(ESDCDiscrepancyExcelDto.CFCNonAssociated), ColumnType.Number),
                    new ColumnDefinition("Unaccounted Changes (Oil MSTB)", nameof(ESDCDiscrepancyExcelDto.UCOil), ColumnType.Number),
                    new ColumnDefinition("Unaccounted Changes (Condensate MSTB)", nameof(ESDCDiscrepancyExcelDto.UCCondensate), ColumnType.Number),
                    new ColumnDefinition("Unaccounted Changes (Associated Gas BSCF)", nameof(ESDCDiscrepancyExcelDto.UCAssociated), ColumnType.Number),
                    new ColumnDefinition("Unaccounted Changes (Non Associated Gas BSCF)", nameof(ESDCDiscrepancyExcelDto.UCNonAssociated), ColumnType.Number),
                    new ColumnDefinition("Counsumed in Operations (Oil MSTB)", nameof(ESDCDiscrepancyExcelDto.CIOOil), ColumnType.Number),
                    new ColumnDefinition("Counsumed in Operations (Condensate MSTB)", nameof(ESDCDiscrepancyExcelDto.CIOCondensate), ColumnType.Number),
                    new ColumnDefinition("Counsumed in Operations (Associated Gas BSCF)", nameof(ESDCDiscrepancyExcelDto.CIOAssociated), ColumnType.Number),
                    new ColumnDefinition("Counsumed in Operations (Non Associated Gas BSCF)", nameof(ESDCDiscrepancyExcelDto.CIONonAssociated), ColumnType.Number)
                };

                var columnDefinitionESDCInPlace = new List<ColumnDefinition>
                {
                    new ColumnDefinition("StructureID", nameof(ESDCInPlaceExcelDto.xStructureID), ColumnType.String,"esdc.xStructureID"),
                    new ColumnDefinition("Field ID", nameof(ESDCInPlaceExcelDto.ESDCFieldID), ColumnType.String, "esdc.ESDCFieldID"),
                    new ColumnDefinition("Asset", nameof(ESDCInPlaceExcelDto.xAssetID), ColumnType.String,"es.xAssetID"),
                    new ColumnDefinition("Field Name", nameof(ESDCInPlaceExcelDto.xStructureName), ColumnType.String, "es.xStructureName"),
                    new ColumnDefinition("Uncertainty Level", nameof(ESDCInPlaceExcelDto.UncertaintyLevel), ColumnType.String),
                    new ColumnDefinition("IOIP", nameof(ESDCInPlaceExcelDto.P90IOIP), ColumnType.Number),
                    new ColumnDefinition("IGIP", nameof(ESDCInPlaceExcelDto.P90IGIP), ColumnType.Number)
                };

                XLWorkbook wb = new ClosedXML.Excel.XLWorkbook();
                for (int i = 0; i < 6; i++)
                {
                    if (i == 0)
                    {
                        IXLWorksheet ws = wb.AddWorksheet("Project");

                        // header
                        var col = 1;
                        foreach (var columnDefinition in columnDefinitionESDC)
                        {
                            // set column name with format text
                            ws.Cell(2, col).SetValue(columnDefinition.Name);
                            ws.Cell(2, col).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                            ws.Cell(2, col).Style.Font.SetBold();
                            ws.Cell(2, col).Style.Fill.SetBackgroundColor(XLColor.OrangePeel);
                            col++;
                        }

                        // title report
                        ws.FirstCell().SetValue("Project");
                        ws.FirstCell().Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                        ws.FirstCell().Style.Font.SetBold();
                        ws.FirstCell().SetDataType(XLDataType.Text);
                        ws.Range(ws.Cell(1, 1), ws.Cell(1, columnDefinitionESDC.Count())).Merge();

                        // rows mulai dari row 3
                        var row = 3;
                        foreach (var item in items.TXESDC)
                        {
                            Type t = item.GetType();
                            PropertyInfo[] props = t.GetProperties();

                            // tiap col
                            col = 1;
                            foreach (var columnDefinition in columnDefinitionESDC)
                            {
                                var prop = props.FirstOrDefault(o => o.Name == columnDefinition.Id);
                                var val = prop.GetValue(item);

                                if (val != null)
                                {
                                    // set column format
                                    switch (columnDefinition.Type)
                                    {
                                        case ColumnType.Number:
                                            if (prop.PropertyType == typeof(Int32))
                                            {
                                                ws.Cell(row, col).Style.NumberFormat.SetNumberFormatId((int)XLPredefinedFormat.Number.Integer);
                                            }
                                            else
                                            {
                                                ws.Cell(row, col).Style.NumberFormat.SetNumberFormatId((int)XLPredefinedFormat.Number.General);
                                            }
                                            break;
                                        case ColumnType.DateTime:
                                            if (prop.PropertyType == typeof(DateTime?))
                                            {
                                                var valDateTime = (DateTime?)prop.GetValue(item);
                                                val = valDateTime.Value.Date;
                                            }
                                            ws.Cell(row, col).Style.NumberFormat.SetNumberFormatId((int)XLPredefinedFormat.DateTime.MonthDayYear4WithDashesHour24Minutes);
                                            break;
                                        case ColumnType.Date:
                                            if (prop.PropertyType == typeof(DateTime?))
                                            {
                                                var valDateTime = (DateTime?)prop.GetValue(item);
                                                val = valDateTime.Value.Date;
                                            }
                                            ws.Cell(row, col).Style.NumberFormat.SetNumberFormatId((int)XLPredefinedFormat.DateTime.DayMonthYear4WithSlashes);
                                            break;
                                    }
                                    ws.Cell(row, col).SetValue(val);
                                }
                                col++;
                            }
                            row++;
                        }

                        //auto fit column
                        ws.Columns(1, columnDefinitionESDC.Count()).AdjustToContents();
                    }
                    if (i == 1)
                    {
                        IXLWorksheet ws = wb.AddWorksheet("Production");

                        // header
                        var col = 1;
                        foreach (var columnDefinition in columnDefinitionESDCProd)
                        {
                            // set column name with format text
                            if(columnDefinition.Name.Contains("Gross Cumulative Production Previous"))
                            {
                                ws.Cell(2, 5).SetValue("Gross Cumulative Production Previous Report");
                                ws.Cell(2, 5).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                                ws.Cell(2, 5).Style.Font.SetBold();
                                ws.Cell(2, 5).Style.Fill.SetBackgroundColor(XLColor.OrangePeel);
                                ws.Range(ws.Cell(2, 5), ws.Cell(2, 8)).Merge();
                                ws.Range(ws.Cell(2, 5), ws.Cell(2, 8)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                                ws.Range(ws.Cell(2, 5), ws.Cell(2, 8)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                                ws.Range(ws.Cell(2, 5), ws.Cell(2, 8)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                                ws.Range(ws.Cell(2, 5), ws.Cell(2, 8)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                                if (columnDefinition.Name.Contains("Oil MSTB"))
                                {
                                    ws.Cell(3, col).SetValue("Oil MSTB");
                                    ws.Cell(3, col).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                                    ws.Cell(3, col).Style.Font.SetBold();
                                    ws.Cell(3, col).Style.Fill.SetBackgroundColor(XLColor.OrangePeel);
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                                }
                                if (columnDefinition.Name.Contains("Condensate MSTB"))
                                {
                                    ws.Cell(3, col).SetValue("Condensate MSTB");
                                    ws.Cell(3, col).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                                    ws.Cell(3, col).Style.Font.SetBold();
                                    ws.Cell(3, col).Style.Fill.SetBackgroundColor(XLColor.OrangePeel);
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                                }
                                if (columnDefinition.Name.Contains("Associated Gas BSCF"))
                                {
                                    ws.Cell(3, col).SetValue("Associated Gas BSCF");
                                    ws.Cell(3, col).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                                    ws.Cell(3, col).Style.Font.SetBold();
                                    ws.Cell(3, col).Style.Fill.SetBackgroundColor(XLColor.OrangePeel);
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                                }
                                if (columnDefinition.Name.Contains("Non Associated Gas BSCF"))
                                {
                                    ws.Cell(3, col).SetValue("Non Associated Gas BSCF");
                                    ws.Cell(3, col).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                                    ws.Cell(3, col).Style.Font.SetBold();
                                    ws.Cell(3, col).Style.Fill.SetBackgroundColor(XLColor.OrangePeel);
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                                }
                            }
                            else if(columnDefinition.Name.Contains("Sales Cumulative Production Previous"))
                            {
                                ws.Cell(2, 9).SetValue("Sales Cumulative Production Previous");
                                ws.Cell(2, 9).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                                ws.Cell(2, 9).Style.Font.SetBold();
                                ws.Cell(2, 9).Style.Fill.SetBackgroundColor(XLColor.OrangePeel);
                                ws.Range(ws.Cell(2, 9), ws.Cell(2, 12)).Merge();
                                ws.Range(ws.Cell(2, 9), ws.Cell(2, 12)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                                ws.Range(ws.Cell(2, 9), ws.Cell(2, 12)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                                ws.Range(ws.Cell(2, 9), ws.Cell(2, 12)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                                ws.Range(ws.Cell(2, 9), ws.Cell(2, 12)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                                if (columnDefinition.Name.Contains("Oil MSTB"))
                                {
                                    ws.Cell(3, col).SetValue("Oil MSTB");
                                    ws.Cell(3, col).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                                    ws.Cell(3, col).Style.Font.SetBold();
                                    ws.Cell(3, col).Style.Fill.SetBackgroundColor(XLColor.OrangePeel);
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                                }
                                if (columnDefinition.Name.Contains("Condensate MSTB"))
                                {
                                    ws.Cell(3, col).SetValue("Condensate MSTB");
                                    ws.Cell(3, col).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                                    ws.Cell(3, col).Style.Font.SetBold();
                                    ws.Cell(3, col).Style.Fill.SetBackgroundColor(XLColor.OrangePeel);
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                                }
                                if (columnDefinition.Name.Contains("Associated Gas BSCF"))
                                {
                                    ws.Cell(3, col).SetValue("Associated Gas BSCF");
                                    ws.Cell(3, col).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                                    ws.Cell(3, col).Style.Font.SetBold();
                                    ws.Cell(3, col).Style.Fill.SetBackgroundColor(XLColor.OrangePeel);
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                                }
                                if (columnDefinition.Name.Contains("Non Associated Gas BSCF"))
                                {
                                    ws.Cell(3, col).SetValue("Non Associated Gas BSCF");
                                    ws.Cell(3, col).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                                    ws.Cell(3, col).Style.Font.SetBold();
                                    ws.Cell(3, col).Style.Fill.SetBackgroundColor(XLColor.OrangePeel);
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                                }
                            }
                            else if (columnDefinition.Name.Contains("GCP"))
                            {
                                ws.Cell(2, 13).SetValue("Gross Cumulative Production");
                                ws.Cell(2, 13).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                                ws.Cell(2, 13).Style.Font.SetBold();
                                ws.Cell(2, 13).Style.Fill.SetBackgroundColor(XLColor.OrangePeel);
                                ws.Range(ws.Cell(2, 13), ws.Cell(2, 16)).Merge();
                                ws.Range(ws.Cell(2, 13), ws.Cell(2, 16)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                                ws.Range(ws.Cell(2, 13), ws.Cell(2, 16)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                                ws.Range(ws.Cell(2, 13), ws.Cell(2, 16)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                                ws.Range(ws.Cell(2, 13), ws.Cell(2, 16)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                                if (columnDefinition.Name.Contains("Oil MSTB"))
                                {
                                    ws.Cell(3, col).SetValue("Oil MSTB");
                                    ws.Cell(3, col).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                                    ws.Cell(3, col).Style.Font.SetBold();
                                    ws.Cell(3, col).Style.Fill.SetBackgroundColor(XLColor.OrangePeel);
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                                }
                                if (columnDefinition.Name.Contains("Condensate MSTB"))
                                {
                                    ws.Cell(3, col).SetValue("Condensate MSTB");
                                    ws.Cell(3, col).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                                    ws.Cell(3, col).Style.Font.SetBold();
                                    ws.Cell(3, col).Style.Fill.SetBackgroundColor(XLColor.OrangePeel);
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                                }
                                if (columnDefinition.Name.Contains("Associated Gas BSCF"))
                                {
                                    ws.Cell(3, col).SetValue("Associated Gas BSCF");
                                    ws.Cell(3, col).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                                    ws.Cell(3, col).Style.Font.SetBold();
                                    ws.Cell(3, col).Style.Fill.SetBackgroundColor(XLColor.OrangePeel);
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                                }
                                if (columnDefinition.Name.Contains("Non Associated Gas BSCF"))
                                {
                                    ws.Cell(3, col).SetValue("Non Associated Gas BSCF");
                                    ws.Cell(3, col).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                                    ws.Cell(3, col).Style.Font.SetBold();
                                    ws.Cell(3, col).Style.Fill.SetBackgroundColor(XLColor.OrangePeel);
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                                }
                            }
                            else if (columnDefinition.Name.Contains("SCP"))
                            {
                                ws.Cell(2, 17).SetValue("Sales Cumulative Production");
                                ws.Cell(2, 17).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                                ws.Cell(2, 17).Style.Font.SetBold();
                                ws.Cell(2, 17).Style.Fill.SetBackgroundColor(XLColor.OrangePeel);
                                ws.Range(ws.Cell(2, 17), ws.Cell(2, 20)).Merge();
                                ws.Range(ws.Cell(2, 17), ws.Cell(2, 20)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                                ws.Range(ws.Cell(2, 17), ws.Cell(2, 20)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                                ws.Range(ws.Cell(2, 17), ws.Cell(2, 20)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                                ws.Range(ws.Cell(2, 17), ws.Cell(2, 20)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                                if (columnDefinition.Name.Contains("Oil MSTB"))
                                {
                                    ws.Cell(3, col).SetValue("Oil MSTB");
                                    ws.Cell(3, col).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                                    ws.Cell(3, col).Style.Font.SetBold();
                                    ws.Cell(3, col).Style.Fill.SetBackgroundColor(XLColor.OrangePeel);
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                                }
                                if (columnDefinition.Name.Contains("Condensate MSTB"))
                                {
                                    ws.Cell(3, col).SetValue("Condensate MSTB");
                                    ws.Cell(3, col).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                                    ws.Cell(3, col).Style.Font.SetBold();
                                    ws.Cell(3, col).Style.Fill.SetBackgroundColor(XLColor.OrangePeel);
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                                }
                                if (columnDefinition.Name.Contains("Associated Gas BSCF"))
                                {
                                    ws.Cell(3, col).SetValue("Associated Gas BSCF");
                                    ws.Cell(3, col).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                                    ws.Cell(3, col).Style.Font.SetBold();
                                    ws.Cell(3, col).Style.Fill.SetBackgroundColor(XLColor.OrangePeel);
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                                }
                                if (columnDefinition.Name.Contains("Non Associated Gas BSCF"))
                                {
                                    ws.Cell(3, col).SetValue("Non Associated Gas BSCF");
                                    ws.Cell(3, col).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                                    ws.Cell(3, col).Style.Font.SetBold();
                                    ws.Cell(3, col).Style.Fill.SetBackgroundColor(XLColor.OrangePeel);
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                                }
                            }
                            else
                            {
                                ws.Cell(2, col).SetValue(columnDefinition.Name);
                                ws.Cell(2, col).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                                ws.Cell(2, col).Style.Font.SetBold();
                                ws.Cell(2, col).Style.Fill.SetBackgroundColor(XLColor.OrangePeel);
                                ws.Range(ws.Cell(2, col), ws.Cell(3, col)).Merge();
                                ws.Range(ws.Cell(2, col), ws.Cell(3, col)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                                ws.Range(ws.Cell(2, col), ws.Cell(3, col)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                                ws.Range(ws.Cell(2, col), ws.Cell(3, col)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                                ws.Range(ws.Cell(2, col), ws.Cell(3, col)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                                ws.Cell(2, col).SetValue(columnDefinition.Name);
                            }
                            col++;
                        }

                        // title report
                        ws.FirstCell().SetValue("Production");
                        ws.FirstCell().Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                        ws.FirstCell().Style.Font.SetBold();
                        ws.FirstCell().SetDataType(XLDataType.Text);
                        ws.Range(ws.Cell(1, 1), ws.Cell(1, columnDefinitionESDCProd.Count())).Merge();

                        // rows mulai dari row 3
                        var row = 4;
                        foreach (var item in items.TXESDCProduction)
                        {
                            Type t = item.GetType();
                            PropertyInfo[] props = t.GetProperties();

                            // tiap col
                            col = 1;
                            foreach (var columnDefinition in columnDefinitionESDCProd)
                            {
                                var prop = props.FirstOrDefault(o => o.Name == columnDefinition.Id);
                                var val = prop.GetValue(item);

                                if (val != null)
                                {
                                    // set column format
                                    switch (columnDefinition.Type)
                                    {
                                        case ColumnType.Number:
                                            if (prop.PropertyType == typeof(Int32))
                                            {
                                                ws.Cell(row, col).Style.NumberFormat.SetNumberFormatId((int)XLPredefinedFormat.Number.Integer);
                                            }
                                            else
                                            {
                                                ws.Cell(row, col).Style.NumberFormat.SetNumberFormatId((int)XLPredefinedFormat.Number.General);
                                            }
                                            break;
                                        case ColumnType.DateTime:
                                            ws.Cell(row, col).Style.NumberFormat.SetNumberFormatId((int)XLPredefinedFormat.DateTime.MonthDayYear4WithDashesHour24Minutes);
                                            break;
                                        case ColumnType.Date:
                                            if (prop.PropertyType == typeof(DateTime?))
                                            {
                                                var valDateTime = (DateTime?)prop.GetValue(item);
                                                val = valDateTime.Value.Date;
                                            }
                                            ws.Cell(row, col).Style.NumberFormat.SetNumberFormatId((int)XLPredefinedFormat.DateTime.DayMonthYear4WithSlashes);
                                            break;
                                    }
                                    ws.Cell(row, col).SetValue(val);
                                }
                                col++;
                            }
                            row++;
                        }

                        //auto fit column
                        ws.Columns(1, columnDefinitionESDCProd.Count()).AdjustToContents();
                    }
                    if (i == 2)
                    {
                        IXLWorksheet ws = wb.AddWorksheet("Volumetric");

                        // header
                        var col = 1;
                        foreach (var columnDefinition in columnDefinitionESDCVolumetric)
                        {
                            // set column name with format text
                            if (columnDefinition.Name.Contains("GRR / CR / PR Previous"))
                            {
                                ws.Cell(2, 6).SetValue("GRR / CR / PR Previous");
                                ws.Cell(2, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                                ws.Cell(2, 6).Style.Font.SetBold();
                                ws.Cell(2, 6).Style.Fill.SetBackgroundColor(XLColor.OrangePeel);
                                ws.Range(ws.Cell(2, 6), ws.Cell(2, 9)).Merge();
                                ws.Range(ws.Cell(2, 6), ws.Cell(2, 9)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                                ws.Range(ws.Cell(2, 6), ws.Cell(2, 9)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                                ws.Range(ws.Cell(2, 6), ws.Cell(2, 9)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                                ws.Range(ws.Cell(2, 6), ws.Cell(2, 9)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                                if (columnDefinition.Name.Contains("Oil MSTB"))
                                {
                                    ws.Cell(3, col).SetValue("Oil MSTB");
                                    ws.Cell(3, col).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                                    ws.Cell(3, col).Style.Font.SetBold();
                                    ws.Cell(3, col).Style.Fill.SetBackgroundColor(XLColor.OrangePeel);
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                                }
                                if (columnDefinition.Name.Contains("Condensate MSTB"))
                                {
                                    ws.Cell(3, col).SetValue("Condensate MSTB");
                                    ws.Cell(3, col).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                                    ws.Cell(3, col).Style.Font.SetBold();
                                    ws.Cell(3, col).Style.Fill.SetBackgroundColor(XLColor.OrangePeel);
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                                }
                                if (columnDefinition.Name.Contains("Associated Gas BSCF"))
                                {
                                    ws.Cell(3, col).SetValue("Associated Gas BSCF");
                                    ws.Cell(3, col).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                                    ws.Cell(3, col).Style.Font.SetBold();
                                    ws.Cell(3, col).Style.Fill.SetBackgroundColor(XLColor.OrangePeel);
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                                }
                                if (columnDefinition.Name.Contains("Non Associated Gas BSCF"))
                                {
                                    ws.Cell(3, col).SetValue("Non Associated Gas BSCF");
                                    ws.Cell(3, col).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                                    ws.Cell(3, col).Style.Font.SetBold();
                                    ws.Cell(3, col).Style.Fill.SetBackgroundColor(XLColor.OrangePeel);
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                                }
                            }
                            else if (columnDefinition.Name.Contains("Reserves Previous"))
                            {
                                ws.Cell(2, 10).SetValue("Reserves Previous");
                                ws.Cell(2, 10).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                                ws.Cell(2, 10).Style.Font.SetBold();
                                ws.Cell(2, 10).Style.Fill.SetBackgroundColor(XLColor.OrangePeel);
                                ws.Range(ws.Cell(2, 10), ws.Cell(2, 13)).Merge();
                                ws.Range(ws.Cell(2, 10), ws.Cell(2, 13)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                                ws.Range(ws.Cell(2, 10), ws.Cell(2, 13)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                                ws.Range(ws.Cell(2, 10), ws.Cell(2, 13)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                                ws.Range(ws.Cell(2, 10), ws.Cell(2, 13)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                                if (columnDefinition.Name.Contains("Oil MSTB"))
                                {
                                    ws.Cell(3, col).SetValue("Oil MSTB");
                                    ws.Cell(3, col).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                                    ws.Cell(3, col).Style.Font.SetBold();
                                    ws.Cell(3, col).Style.Fill.SetBackgroundColor(XLColor.OrangePeel);
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                                }
                                if (columnDefinition.Name.Contains("Condensate MSTB"))
                                {
                                    ws.Cell(3, col).SetValue("Condensate MSTB");
                                    ws.Cell(3, col).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                                    ws.Cell(3, col).Style.Font.SetBold();
                                    ws.Cell(3, col).Style.Fill.SetBackgroundColor(XLColor.OrangePeel);
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                                }
                                if (columnDefinition.Name.Contains("Associated Gas BSCF"))
                                {
                                    ws.Cell(3, col).SetValue("Associated Gas BSCF");
                                    ws.Cell(3, col).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                                    ws.Cell(3, col).Style.Font.SetBold();
                                    ws.Cell(3, col).Style.Fill.SetBackgroundColor(XLColor.OrangePeel);
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                                }
                                if (columnDefinition.Name.Contains("Non Associated Gas BSCF"))
                                {
                                    ws.Cell(3, col).SetValue("Non Associated Gas BSCF");
                                    ws.Cell(3, col).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                                    ws.Cell(3, col).Style.Font.SetBold();
                                    ws.Cell(3, col).Style.Fill.SetBackgroundColor(XLColor.OrangePeel);
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                                }
                            }
                            else if (columnDefinition.Name.Contains("GOI Recoverable Resources / Contingent Resources / Prospective Resources"))
                            {
                                ws.Cell(2, 14).SetValue("GOI Recoverable Resources / Contingent Resources / Prospective Resources");
                                ws.Cell(2, 14).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                                ws.Cell(2, 14).Style.Font.SetBold();
                                ws.Cell(2, 14).Style.Fill.SetBackgroundColor(XLColor.OrangePeel);
                                ws.Range(ws.Cell(2, 14), ws.Cell(2, 17)).Merge();
                                ws.Range(ws.Cell(2, 14), ws.Cell(2, 17)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                                ws.Range(ws.Cell(2, 14), ws.Cell(2, 17)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                                ws.Range(ws.Cell(2, 14), ws.Cell(2, 17)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                                ws.Range(ws.Cell(2, 14), ws.Cell(2, 17)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                                if (columnDefinition.Name.Contains("Oil MSTB"))
                                {
                                    ws.Cell(3, col).SetValue("Oil MSTB");
                                    ws.Cell(3, col).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                                    ws.Cell(3, col).Style.Font.SetBold();
                                    ws.Cell(3, col).Style.Fill.SetBackgroundColor(XLColor.OrangePeel);
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                                }
                                if (columnDefinition.Name.Contains("Condensate MSTB"))
                                {
                                    ws.Cell(3, col).SetValue("Condensate MSTB");
                                    ws.Cell(3, col).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                                    ws.Cell(3, col).Style.Font.SetBold();
                                    ws.Cell(3, col).Style.Fill.SetBackgroundColor(XLColor.OrangePeel);
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                                }
                                if (columnDefinition.Name.Contains("Associated Gas BSCF"))
                                {
                                    ws.Cell(3, col).SetValue("Associated Gas BSCF");
                                    ws.Cell(3, col).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                                    ws.Cell(3, col).Style.Font.SetBold();
                                    ws.Cell(3, col).Style.Fill.SetBackgroundColor(XLColor.OrangePeel);
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                                }
                                if (columnDefinition.Name.Contains("Non Associated Gas BSCF"))
                                {
                                    ws.Cell(3, col).SetValue("Non Associated Gas BSCF");
                                    ws.Cell(3, col).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                                    ws.Cell(3, col).Style.Font.SetBold();
                                    ws.Cell(3, col).Style.Fill.SetBackgroundColor(XLColor.OrangePeel);
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                                }
                            }
                            else if (columnDefinition.Name.Contains("RSV"))
                            {
                                ws.Cell(2, 18).SetValue("Sales Cumulative Production");
                                ws.Cell(2, 18).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                                ws.Cell(2, 18).Style.Font.SetBold();
                                ws.Cell(2, 18).Style.Fill.SetBackgroundColor(XLColor.OrangePeel);
                                ws.Range(ws.Cell(2, 18), ws.Cell(2, 21)).Merge();
                                ws.Range(ws.Cell(2, 18), ws.Cell(2, 21)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                                ws.Range(ws.Cell(2, 18), ws.Cell(2, 21)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                                ws.Range(ws.Cell(2, 18), ws.Cell(2, 21)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                                ws.Range(ws.Cell(2, 18), ws.Cell(2, 21)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                                if (columnDefinition.Name.Contains("Oil MSTB"))
                                {
                                    ws.Cell(3, col).SetValue("Oil MSTB");
                                    ws.Cell(3, col).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                                    ws.Cell(3, col).Style.Font.SetBold();
                                    ws.Cell(3, col).Style.Fill.SetBackgroundColor(XLColor.OrangePeel);
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                                }
                                if (columnDefinition.Name.Contains("Condensate MSTB"))
                                {
                                    ws.Cell(3, col).SetValue("Condensate MSTB");
                                    ws.Cell(3, col).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                                    ws.Cell(3, col).Style.Font.SetBold();
                                    ws.Cell(3, col).Style.Fill.SetBackgroundColor(XLColor.OrangePeel);
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                                }
                                if (columnDefinition.Name.Contains("Associated Gas BSCF"))
                                {
                                    ws.Cell(3, col).SetValue("Associated Gas BSCF");
                                    ws.Cell(3, col).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                                    ws.Cell(3, col).Style.Font.SetBold();
                                    ws.Cell(3, col).Style.Fill.SetBackgroundColor(XLColor.OrangePeel);
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                                }
                                if (columnDefinition.Name.Contains("Non Associated Gas BSCF"))
                                {
                                    ws.Cell(3, col).SetValue("Non Associated Gas BSCF");
                                    ws.Cell(3, col).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                                    ws.Cell(3, col).Style.Font.SetBold();
                                    ws.Cell(3, col).Style.Fill.SetBackgroundColor(XLColor.OrangePeel);
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                                }
                            }
                            else
                            {
                                ws.Cell(2, col).SetValue(columnDefinition.Name);
                                ws.Cell(2, col).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                                ws.Cell(2, col).Style.Font.SetBold();
                                ws.Cell(2, col).Style.Fill.SetBackgroundColor(XLColor.OrangePeel);
                                ws.Range(ws.Cell(2, col), ws.Cell(3, col)).Merge();
                                ws.Range(ws.Cell(2, col), ws.Cell(3, col)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                                ws.Range(ws.Cell(2, col), ws.Cell(3, col)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                                ws.Range(ws.Cell(2, col), ws.Cell(3, col)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                                ws.Range(ws.Cell(2, col), ws.Cell(3, col)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                                ws.Cell(2, col).SetValue(columnDefinition.Name);
                            }
                            col++;
                        }

                        // title report
                        ws.FirstCell().SetValue("Volumetric");
                        ws.FirstCell().Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                        ws.FirstCell().Style.Font.SetBold();
                        ws.FirstCell().SetDataType(XLDataType.Text);
                        ws.Range(ws.Cell(1, 1), ws.Cell(1, columnDefinitionESDCVolumetric.Count())).Merge();

                        // rows mulai dari row 3
                        var row = 4;
                        foreach (var item in items.TXESDCVolumetric)
                        {
                            Type t = item.GetType();
                            PropertyInfo[] props = t.GetProperties();

                            // tiap col
                            col = 1;
                            foreach (var columnDefinition in columnDefinitionESDCVolumetric)
                            {
                                var prop = props.FirstOrDefault(o => o.Name == columnDefinition.Id);
                                var val = prop.GetValue(item);

                                if (val != null)
                                {
                                    // set column format
                                    switch (columnDefinition.Type)
                                    {
                                        case ColumnType.Number:
                                            if (prop.PropertyType == typeof(Int32))
                                            {
                                                ws.Cell(row, col).Style.NumberFormat.SetNumberFormatId((int)XLPredefinedFormat.Number.Integer);
                                            }
                                            else
                                            {
                                                ws.Cell(row, col).Style.NumberFormat.SetNumberFormatId((int)XLPredefinedFormat.Number.General);
                                            }
                                            break;
                                        case ColumnType.DateTime:
                                            ws.Cell(row, col).Style.NumberFormat.SetNumberFormatId((int)XLPredefinedFormat.DateTime.MonthDayYear4WithDashesHour24Minutes);
                                            break;
                                        case ColumnType.Date:
                                            if (prop.PropertyType == typeof(DateTime?))
                                            {
                                                var valDateTime = (DateTime?)prop.GetValue(item);
                                                val = valDateTime.Value.Date;
                                            }
                                            ws.Cell(row, col).Style.NumberFormat.SetNumberFormatId((int)XLPredefinedFormat.DateTime.DayMonthYear4WithSlashes);
                                            break;
                                    }
                                    ws.Cell(row, col).SetValue(val);
                                }
                                col++;
                            }
                            row++;
                        }

                        //auto fit column
                        ws.Columns(1, columnDefinitionESDCVolumetric.Count()).AdjustToContents();
                    }
                    if (i == 3)
                    {
                        IXLWorksheet ws = wb.AddWorksheet("Forecast");

                        // header
                        var col = 1;
                        foreach (var columnDefinition in columnDefinitionESDCForecast)
                        {
                            // set column name with format text
                            if (columnDefinition.Name.Contains("Total Potential Forecast (2R/2C/2U)"))
                            {
                                ws.Cell(2, 6).SetValue("Total Potential Forecast (2R/2C/2U)");
                                ws.Cell(2, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                                ws.Cell(2, 6).Style.Font.SetBold();
                                ws.Cell(2, 6).Style.Fill.SetBackgroundColor(XLColor.OrangePeel);
                                ws.Range(ws.Cell(2, 6), ws.Cell(2, 9)).Merge();
                                ws.Range(ws.Cell(2, 6), ws.Cell(2, 9)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                                ws.Range(ws.Cell(2, 6), ws.Cell(2, 9)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                                ws.Range(ws.Cell(2, 6), ws.Cell(2, 9)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                                ws.Range(ws.Cell(2, 6), ws.Cell(2, 9)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                                if (columnDefinition.Name.Contains("Oil MSTB"))
                                {
                                    ws.Cell(3, col).SetValue("Oil MSTB");
                                    ws.Cell(3, col).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                                    ws.Cell(3, col).Style.Font.SetBold();
                                    ws.Cell(3, col).Style.Fill.SetBackgroundColor(XLColor.OrangePeel);
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                                }
                                if (columnDefinition.Name.Contains("Condensate MSTB"))
                                {
                                    ws.Cell(3, col).SetValue("Condensate MSTB");
                                    ws.Cell(3, col).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                                    ws.Cell(3, col).Style.Font.SetBold();
                                    ws.Cell(3, col).Style.Fill.SetBackgroundColor(XLColor.OrangePeel);
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                                }
                                if (columnDefinition.Name.Contains("Associated Gas BSCF"))
                                {
                                    ws.Cell(3, col).SetValue("Associated Gas BSCF");
                                    ws.Cell(3, col).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                                    ws.Cell(3, col).Style.Font.SetBold();
                                    ws.Cell(3, col).Style.Fill.SetBackgroundColor(XLColor.OrangePeel);
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                                }
                                if (columnDefinition.Name.Contains("Non Associated Gas BSCF"))
                                {
                                    ws.Cell(3, col).SetValue("Non Associated Gas BSCF");
                                    ws.Cell(3, col).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                                    ws.Cell(3, col).Style.Font.SetBold();
                                    ws.Cell(3, col).Style.Fill.SetBackgroundColor(XLColor.OrangePeel);
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                                }
                            }
                            else if (columnDefinition.Name.Contains("Sales Forecast (2P)"))
                            {
                                ws.Cell(2, 10).SetValue("Sales Forecast (2P)");
                                ws.Cell(2, 10).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                                ws.Cell(2, 10).Style.Font.SetBold();
                                ws.Cell(2, 10).Style.Fill.SetBackgroundColor(XLColor.OrangePeel);
                                ws.Range(ws.Cell(2, 10), ws.Cell(2, 13)).Merge();
                                ws.Range(ws.Cell(2, 10), ws.Cell(2, 13)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                                ws.Range(ws.Cell(2, 10), ws.Cell(2, 13)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                                ws.Range(ws.Cell(2, 10), ws.Cell(2, 13)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                                ws.Range(ws.Cell(2, 10), ws.Cell(2, 13)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                                if (columnDefinition.Name.Contains("Oil MSTB"))
                                {
                                    ws.Cell(3, col).SetValue("Oil MSTB");
                                    ws.Cell(3, col).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                                    ws.Cell(3, col).Style.Font.SetBold();
                                    ws.Cell(3, col).Style.Fill.SetBackgroundColor(XLColor.OrangePeel);
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                                }
                                if (columnDefinition.Name.Contains("Condensate MSTB"))
                                {
                                    ws.Cell(3, col).SetValue("Condensate MSTB");
                                    ws.Cell(3, col).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                                    ws.Cell(3, col).Style.Font.SetBold();
                                    ws.Cell(3, col).Style.Fill.SetBackgroundColor(XLColor.OrangePeel);
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                                }
                                if (columnDefinition.Name.Contains("Associated Gas BSCF"))
                                {
                                    ws.Cell(3, col).SetValue("Associated Gas BSCF");
                                    ws.Cell(3, col).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                                    ws.Cell(3, col).Style.Font.SetBold();
                                    ws.Cell(3, col).Style.Fill.SetBackgroundColor(XLColor.OrangePeel);
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                                }
                                if (columnDefinition.Name.Contains("Non Associated Gas BSCF"))
                                {
                                    ws.Cell(3, col).SetValue("Non Associated Gas BSCF");
                                    ws.Cell(3, col).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                                    ws.Cell(3, col).Style.Font.SetBold();
                                    ws.Cell(3, col).Style.Fill.SetBackgroundColor(XLColor.OrangePeel);
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                                }
                            }
                            else if (columnDefinition.Name.Contains("Consumed in Operations"))
                            {
                                ws.Cell(2, 14).SetValue("Consumed in Operations");
                                ws.Cell(2, 14).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                                ws.Cell(2, 14).Style.Font.SetBold();
                                ws.Cell(2, 14).Style.Fill.SetBackgroundColor(XLColor.OrangePeel);
                                ws.Range(ws.Cell(2, 14), ws.Cell(2, 17)).Merge();
                                ws.Range(ws.Cell(2, 14), ws.Cell(2, 17)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                                ws.Range(ws.Cell(2, 14), ws.Cell(2, 17)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                                ws.Range(ws.Cell(2, 14), ws.Cell(2, 17)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                                ws.Range(ws.Cell(2, 14), ws.Cell(2, 17)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                                if (columnDefinition.Name.Contains("Oil MSTB"))
                                {
                                    ws.Cell(3, col).SetValue("Oil MSTB");
                                    ws.Cell(3, col).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                                    ws.Cell(3, col).Style.Font.SetBold();
                                    ws.Cell(3, col).Style.Fill.SetBackgroundColor(XLColor.OrangePeel);
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                                }
                                if (columnDefinition.Name.Contains("Condensate MSTB"))
                                {
                                    ws.Cell(3, col).SetValue("Condensate MSTB");
                                    ws.Cell(3, col).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                                    ws.Cell(3, col).Style.Font.SetBold();
                                    ws.Cell(3, col).Style.Fill.SetBackgroundColor(XLColor.OrangePeel);
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                                }
                                if (columnDefinition.Name.Contains("Associated Gas BSCF"))
                                {
                                    ws.Cell(3, col).SetValue("Associated Gas BSCF");
                                    ws.Cell(3, col).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                                    ws.Cell(3, col).Style.Font.SetBold();
                                    ws.Cell(3, col).Style.Fill.SetBackgroundColor(XLColor.OrangePeel);
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                                }
                                if (columnDefinition.Name.Contains("Non Associated Gas BSCF"))
                                {
                                    ws.Cell(3, col).SetValue("Non Associated Gas BSCF");
                                    ws.Cell(3, col).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                                    ws.Cell(3, col).Style.Font.SetBold();
                                    ws.Cell(3, col).Style.Fill.SetBackgroundColor(XLColor.OrangePeel);
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                                }
                            }
                            else if (columnDefinition.Name.Contains("Loss Production"))
                            {
                                ws.Cell(2, 18).SetValue("Loss Production");
                                ws.Cell(2, 18).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                                ws.Cell(2, 18).Style.Font.SetBold();
                                ws.Cell(2, 18).Style.Fill.SetBackgroundColor(XLColor.OrangePeel);
                                ws.Range(ws.Cell(2, 18), ws.Cell(2, 21)).Merge();
                                ws.Range(ws.Cell(2, 18), ws.Cell(2, 21)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                                ws.Range(ws.Cell(2, 18), ws.Cell(2, 21)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                                ws.Range(ws.Cell(2, 18), ws.Cell(2, 21)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                                ws.Range(ws.Cell(2, 18), ws.Cell(2, 21)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                                if (columnDefinition.Name.Contains("Oil MSTB"))
                                {
                                    ws.Cell(3, col).SetValue("Oil MSTB");
                                    ws.Cell(3, col).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                                    ws.Cell(3, col).Style.Font.SetBold();
                                    ws.Cell(3, col).Style.Fill.SetBackgroundColor(XLColor.OrangePeel);
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                                }
                                if (columnDefinition.Name.Contains("Condensate MSTB"))
                                {
                                    ws.Cell(3, col).SetValue("Condensate MSTB");
                                    ws.Cell(3, col).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                                    ws.Cell(3, col).Style.Font.SetBold();
                                    ws.Cell(3, col).Style.Fill.SetBackgroundColor(XLColor.OrangePeel);
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                                }
                                if (columnDefinition.Name.Contains("Associated Gas BSCF"))
                                {
                                    ws.Cell(3, col).SetValue("Associated Gas BSCF");
                                    ws.Cell(3, col).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                                    ws.Cell(3, col).Style.Font.SetBold();
                                    ws.Cell(3, col).Style.Fill.SetBackgroundColor(XLColor.OrangePeel);
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                                }
                                if (columnDefinition.Name.Contains("Non Associated Gas BSCF"))
                                {
                                    ws.Cell(3, col).SetValue("Non Associated Gas BSCF");
                                    ws.Cell(3, col).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                                    ws.Cell(3, col).Style.Font.SetBold();
                                    ws.Cell(3, col).Style.Fill.SetBackgroundColor(XLColor.OrangePeel);
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                                }
                            }
                            else
                            {
                                ws.Cell(2, col).SetValue(columnDefinition.Name);
                                ws.Cell(2, col).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                                ws.Cell(2, col).Style.Font.SetBold();
                                ws.Cell(2, col).Style.Fill.SetBackgroundColor(XLColor.OrangePeel);
                                ws.Range(ws.Cell(2, col), ws.Cell(3, col)).Merge();
                                ws.Range(ws.Cell(2, col), ws.Cell(3, col)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                                ws.Range(ws.Cell(2, col), ws.Cell(3, col)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                                ws.Range(ws.Cell(2, col), ws.Cell(3, col)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                                ws.Range(ws.Cell(2, col), ws.Cell(3, col)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                                ws.Cell(2, col).SetValue(columnDefinition.Name);
                            }
                            col++;
                        }

                        // title report
                        ws.FirstCell().SetValue("Forecast");
                        ws.FirstCell().Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                        ws.FirstCell().Style.Font.SetBold();
                        ws.FirstCell().SetDataType(XLDataType.Text);
                        ws.Range(ws.Cell(1, 1), ws.Cell(1, columnDefinitionESDCForecast.Count())).Merge();

                        // rows mulai dari row 3
                        var row = 4;
                        foreach (var item in items.TXESDCForecast)
                        {
                            Type t = item.GetType();
                            PropertyInfo[] props = t.GetProperties();

                            // tiap col
                            col = 1;
                            foreach (var columnDefinition in columnDefinitionESDCForecast)
                            {
                                var prop = props.FirstOrDefault(o => o.Name == columnDefinition.Id);
                                var val = prop.GetValue(item);

                                if (val != null)
                                {
                                    // set column format
                                    switch (columnDefinition.Type)
                                    {
                                        case ColumnType.Number:
                                            if (prop.PropertyType == typeof(Int32))
                                            {
                                                ws.Cell(row, col).Style.NumberFormat.SetNumberFormatId((int)XLPredefinedFormat.Number.Integer);
                                            }
                                            else
                                            {
                                                ws.Cell(row, col).Style.NumberFormat.SetNumberFormatId((int)XLPredefinedFormat.Number.General);
                                            }
                                            break;
                                        case ColumnType.DateTime:
                                            ws.Cell(row, col).Style.NumberFormat.SetNumberFormatId((int)XLPredefinedFormat.DateTime.MonthDayYear4WithDashesHour24Minutes);
                                            break;
                                        case ColumnType.Date:
                                            if (prop.PropertyType == typeof(DateTime?))
                                            {
                                                var valDateTime = (DateTime?)prop.GetValue(item);
                                                val = valDateTime.Value.Date;
                                            }
                                            ws.Cell(row, col).Style.NumberFormat.SetNumberFormatId((int)XLPredefinedFormat.DateTime.DayMonthYear4WithSlashes);
                                            break;
                                    }
                                    ws.Cell(row, col).SetValue(val);
                                }
                                col++;
                            }
                            row++;
                        }

                        //auto fit column
                        ws.Columns(1, columnDefinitionESDCForecast.Count()).AdjustToContents();
                    }
                    if (i == 4)
                    {
                        IXLWorksheet ws = wb.AddWorksheet("Discrepancy");

                        // header
                        var col = 1;
                        foreach (var columnDefinition in columnDefinitionESDCDiscrepancy)
                        {
                            // set column name with format text
                            if (columnDefinition.Name.Contains("Change from Update Model"))
                            {
                                ws.Cell(2, 6).SetValue("Change from Update Model");
                                ws.Cell(2, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                                ws.Cell(2, 6).Style.Font.SetBold();
                                ws.Cell(2, 6).Style.Fill.SetBackgroundColor(XLColor.OrangePeel);
                                ws.Range(ws.Cell(2, 6), ws.Cell(2, 9)).Merge();
                                ws.Range(ws.Cell(2, 6), ws.Cell(2, 9)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                                ws.Range(ws.Cell(2, 6), ws.Cell(2, 9)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                                ws.Range(ws.Cell(2, 6), ws.Cell(2, 9)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                                ws.Range(ws.Cell(2, 6), ws.Cell(2, 9)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                                if (columnDefinition.Name.Contains("Oil MSTB"))
                                {
                                    ws.Cell(3, col).SetValue("Oil MSTB");
                                    ws.Cell(3, col).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                                    ws.Cell(3, col).Style.Font.SetBold();
                                    ws.Cell(3, col).Style.Fill.SetBackgroundColor(XLColor.OrangePeel);
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                                }
                                if (columnDefinition.Name.Contains("Condensate MSTB"))
                                {
                                    ws.Cell(3, col).SetValue("Condensate MSTB");
                                    ws.Cell(3, col).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                                    ws.Cell(3, col).Style.Font.SetBold();
                                    ws.Cell(3, col).Style.Fill.SetBackgroundColor(XLColor.OrangePeel);
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                                }
                                if (columnDefinition.Name.Contains("Associated Gas BSCF"))
                                {
                                    ws.Cell(3, col).SetValue("Associated Gas BSCF");
                                    ws.Cell(3, col).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                                    ws.Cell(3, col).Style.Font.SetBold();
                                    ws.Cell(3, col).Style.Fill.SetBackgroundColor(XLColor.OrangePeel);
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                                }
                                if (columnDefinition.Name.Contains("Non Associated Gas BSCF"))
                                {
                                    ws.Cell(3, col).SetValue("Non Associated Gas BSCF");
                                    ws.Cell(3, col).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                                    ws.Cell(3, col).Style.Font.SetBold();
                                    ws.Cell(3, col).Style.Fill.SetBackgroundColor(XLColor.OrangePeel);
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                                }
                            }
                            else if (columnDefinition.Name.Contains("Change from Production Performance Analysis"))
                            {
                                ws.Cell(2, 10).SetValue("Change from Production Performance Analysis");
                                ws.Cell(2, 10).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                                ws.Cell(2, 10).Style.Font.SetBold();
                                ws.Cell(2, 10).Style.Fill.SetBackgroundColor(XLColor.OrangePeel);
                                ws.Range(ws.Cell(2, 10), ws.Cell(2, 13)).Merge();
                                ws.Range(ws.Cell(2, 10), ws.Cell(2, 13)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                                ws.Range(ws.Cell(2, 10), ws.Cell(2, 13)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                                ws.Range(ws.Cell(2, 10), ws.Cell(2, 13)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                                ws.Range(ws.Cell(2, 10), ws.Cell(2, 13)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                                if (columnDefinition.Name.Contains("Oil MSTB"))
                                {
                                    ws.Cell(3, col).SetValue("Oil MSTB");
                                    ws.Cell(3, col).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                                    ws.Cell(3, col).Style.Font.SetBold();
                                    ws.Cell(3, col).Style.Fill.SetBackgroundColor(XLColor.OrangePeel);
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                                }
                                if (columnDefinition.Name.Contains("Condensate MSTB"))
                                {
                                    ws.Cell(3, col).SetValue("Condensate MSTB");
                                    ws.Cell(3, col).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                                    ws.Cell(3, col).Style.Font.SetBold();
                                    ws.Cell(3, col).Style.Fill.SetBackgroundColor(XLColor.OrangePeel);
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                                }
                                if (columnDefinition.Name.Contains("Associated Gas BSCF"))
                                {
                                    ws.Cell(3, col).SetValue("Associated Gas BSCF");
                                    ws.Cell(3, col).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                                    ws.Cell(3, col).Style.Font.SetBold();
                                    ws.Cell(3, col).Style.Fill.SetBackgroundColor(XLColor.OrangePeel);
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                                }
                                if (columnDefinition.Name.Contains("Non Associated Gas BSCF"))
                                {
                                    ws.Cell(3, col).SetValue("Non Associated Gas BSCF");
                                    ws.Cell(3, col).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                                    ws.Cell(3, col).Style.Font.SetBold();
                                    ws.Cell(3, col).Style.Fill.SetBackgroundColor(XLColor.OrangePeel);
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                                }
                            }
                            else if (columnDefinition.Name.Contains("Change from Well Intervention"))
                            {
                                ws.Cell(2, 14).SetValue("Change from Well Intervention");
                                ws.Cell(2, 14).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                                ws.Cell(2, 14).Style.Font.SetBold();
                                ws.Cell(2, 14).Style.Fill.SetBackgroundColor(XLColor.OrangePeel);
                                ws.Range(ws.Cell(2, 14), ws.Cell(2, 17)).Merge();
                                ws.Range(ws.Cell(2, 14), ws.Cell(2, 17)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                                ws.Range(ws.Cell(2, 14), ws.Cell(2, 17)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                                ws.Range(ws.Cell(2, 14), ws.Cell(2, 17)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                                ws.Range(ws.Cell(2, 14), ws.Cell(2, 17)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                                if (columnDefinition.Name.Contains("Oil MSTB"))
                                {
                                    ws.Cell(3, col).SetValue("Oil MSTB");
                                    ws.Cell(3, col).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                                    ws.Cell(3, col).Style.Font.SetBold();
                                    ws.Cell(3, col).Style.Fill.SetBackgroundColor(XLColor.OrangePeel);
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                                }
                                if (columnDefinition.Name.Contains("Condensate MSTB"))
                                {
                                    ws.Cell(3, col).SetValue("Condensate MSTB");
                                    ws.Cell(3, col).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                                    ws.Cell(3, col).Style.Font.SetBold();
                                    ws.Cell(3, col).Style.Fill.SetBackgroundColor(XLColor.OrangePeel);
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                                }
                                if (columnDefinition.Name.Contains("Associated Gas BSCF"))
                                {
                                    ws.Cell(3, col).SetValue("Associated Gas BSCF");
                                    ws.Cell(3, col).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                                    ws.Cell(3, col).Style.Font.SetBold();
                                    ws.Cell(3, col).Style.Fill.SetBackgroundColor(XLColor.OrangePeel);
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                                }
                                if (columnDefinition.Name.Contains("Non Associated Gas BSCF"))
                                {
                                    ws.Cell(3, col).SetValue("Non Associated Gas BSCF");
                                    ws.Cell(3, col).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                                    ws.Cell(3, col).Style.Font.SetBold();
                                    ws.Cell(3, col).Style.Fill.SetBackgroundColor(XLColor.OrangePeel);
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                                }
                            }
                            else if (columnDefinition.Name.Contains("Change from Commerciality"))
                            {
                                ws.Cell(2, 18).SetValue("Change from Commerciality");
                                ws.Cell(2, 18).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                                ws.Cell(2, 18).Style.Font.SetBold();
                                ws.Cell(2, 18).Style.Fill.SetBackgroundColor(XLColor.OrangePeel);
                                ws.Range(ws.Cell(2, 18), ws.Cell(2, 21)).Merge();
                                ws.Range(ws.Cell(2, 18), ws.Cell(2, 21)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                                ws.Range(ws.Cell(2, 18), ws.Cell(2, 21)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                                ws.Range(ws.Cell(2, 18), ws.Cell(2, 21)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                                ws.Range(ws.Cell(2, 18), ws.Cell(2, 21)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                                if (columnDefinition.Name.Contains("Oil MSTB"))
                                {
                                    ws.Cell(3, col).SetValue("Oil MSTB");
                                    ws.Cell(3, col).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                                    ws.Cell(3, col).Style.Font.SetBold();
                                    ws.Cell(3, col).Style.Fill.SetBackgroundColor(XLColor.OrangePeel);
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                                }
                                if (columnDefinition.Name.Contains("Condensate MSTB"))
                                {
                                    ws.Cell(3, col).SetValue("Condensate MSTB");
                                    ws.Cell(3, col).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                                    ws.Cell(3, col).Style.Font.SetBold();
                                    ws.Cell(3, col).Style.Fill.SetBackgroundColor(XLColor.OrangePeel);
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                                }
                                if (columnDefinition.Name.Contains("Associated Gas BSCF"))
                                {
                                    ws.Cell(3, col).SetValue("Associated Gas BSCF");
                                    ws.Cell(3, col).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                                    ws.Cell(3, col).Style.Font.SetBold();
                                    ws.Cell(3, col).Style.Fill.SetBackgroundColor(XLColor.OrangePeel);
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                                }
                                if (columnDefinition.Name.Contains("Non Associated Gas BSCF"))
                                {
                                    ws.Cell(3, col).SetValue("Non Associated Gas BSCF");
                                    ws.Cell(3, col).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                                    ws.Cell(3, col).Style.Font.SetBold();
                                    ws.Cell(3, col).Style.Fill.SetBackgroundColor(XLColor.OrangePeel);
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                                }
                            }
                            else if (columnDefinition.Name.Contains("Unaccounted Changes"))
                            {
                                ws.Cell(2, 22).SetValue("Unaccounted Changes");
                                ws.Cell(2, 22).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                                ws.Cell(2, 22).Style.Font.SetBold();
                                ws.Cell(2, 22).Style.Fill.SetBackgroundColor(XLColor.OrangePeel);
                                ws.Range(ws.Cell(2, 22), ws.Cell(2, 25)).Merge();
                                ws.Range(ws.Cell(2, 22), ws.Cell(2, 25)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                                ws.Range(ws.Cell(2, 22), ws.Cell(2, 25)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                                ws.Range(ws.Cell(2, 22), ws.Cell(2, 25)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                                ws.Range(ws.Cell(2, 22), ws.Cell(2, 25)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                                if (columnDefinition.Name.Contains("Oil MSTB"))
                                {
                                    ws.Cell(3, col).SetValue("Oil MSTB");
                                    ws.Cell(3, col).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                                    ws.Cell(3, col).Style.Font.SetBold();
                                    ws.Cell(3, col).Style.Fill.SetBackgroundColor(XLColor.OrangePeel);
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                                }
                                if (columnDefinition.Name.Contains("Condensate MSTB"))
                                {
                                    ws.Cell(3, col).SetValue("Condensate MSTB");
                                    ws.Cell(3, col).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                                    ws.Cell(3, col).Style.Font.SetBold();
                                    ws.Cell(3, col).Style.Fill.SetBackgroundColor(XLColor.OrangePeel);
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                                }
                                if (columnDefinition.Name.Contains("Associated Gas BSCF"))
                                {
                                    ws.Cell(3, col).SetValue("Associated Gas BSCF");
                                    ws.Cell(3, col).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                                    ws.Cell(3, col).Style.Font.SetBold();
                                    ws.Cell(3, col).Style.Fill.SetBackgroundColor(XLColor.OrangePeel);
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                                }
                                if (columnDefinition.Name.Contains("Non Associated Gas BSCF"))
                                {
                                    ws.Cell(3, col).SetValue("Non Associated Gas BSCF");
                                    ws.Cell(3, col).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                                    ws.Cell(3, col).Style.Font.SetBold();
                                    ws.Cell(3, col).Style.Fill.SetBackgroundColor(XLColor.OrangePeel);
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                                }
                            }
                            else if (columnDefinition.Name.Contains("Counsumed in Operations"))
                            {
                                ws.Cell(2, 26).SetValue("Counsumed in Operations");
                                ws.Cell(2, 26).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                                ws.Cell(2, 26).Style.Font.SetBold();
                                ws.Cell(2, 26).Style.Fill.SetBackgroundColor(XLColor.OrangePeel);
                                ws.Range(ws.Cell(2, 26), ws.Cell(2, 29)).Merge();
                                ws.Range(ws.Cell(2, 26), ws.Cell(2, 29)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                                ws.Range(ws.Cell(2, 26), ws.Cell(2, 29)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                                ws.Range(ws.Cell(2, 26), ws.Cell(2, 29)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                                ws.Range(ws.Cell(2, 26), ws.Cell(2, 29)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                                if (columnDefinition.Name.Contains("Oil MSTB"))
                                {
                                    ws.Cell(3, col).SetValue("Oil MSTB");
                                    ws.Cell(3, col).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                                    ws.Cell(3, col).Style.Font.SetBold();
                                    ws.Cell(3, col).Style.Fill.SetBackgroundColor(XLColor.OrangePeel);
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                                }
                                if (columnDefinition.Name.Contains("Condensate MSTB"))
                                {
                                    ws.Cell(3, col).SetValue("Condensate MSTB");
                                    ws.Cell(3, col).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                                    ws.Cell(3, col).Style.Font.SetBold();
                                    ws.Cell(3, col).Style.Fill.SetBackgroundColor(XLColor.OrangePeel);
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                                }
                                if (columnDefinition.Name.Contains("Associated Gas BSCF"))
                                {
                                    ws.Cell(3, col).SetValue("Associated Gas BSCF");
                                    ws.Cell(3, col).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                                    ws.Cell(3, col).Style.Font.SetBold();
                                    ws.Cell(3, col).Style.Fill.SetBackgroundColor(XLColor.OrangePeel);
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                                }
                                if (columnDefinition.Name.Contains("Non Associated Gas BSCF"))
                                {
                                    ws.Cell(3, col).SetValue("Non Associated Gas BSCF");
                                    ws.Cell(3, col).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                                    ws.Cell(3, col).Style.Font.SetBold();
                                    ws.Cell(3, col).Style.Fill.SetBackgroundColor(XLColor.OrangePeel);
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                                    ws.Range(ws.Cell(3, col), ws.Cell(3, col)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                                }
                            }
                            else
                            {
                                ws.Cell(2, col).SetValue(columnDefinition.Name);
                                ws.Cell(2, col).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                                ws.Cell(2, col).Style.Font.SetBold();
                                ws.Cell(2, col).Style.Fill.SetBackgroundColor(XLColor.OrangePeel);
                                ws.Range(ws.Cell(2, col), ws.Cell(3, col)).Merge();
                                ws.Range(ws.Cell(2, col), ws.Cell(3, col)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                                ws.Range(ws.Cell(2, col), ws.Cell(3, col)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                                ws.Range(ws.Cell(2, col), ws.Cell(3, col)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                                ws.Range(ws.Cell(2, col), ws.Cell(3, col)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                                ws.Cell(2, col).SetValue(columnDefinition.Name);
                            }
                            col++;
                        }

                        // title report
                        ws.FirstCell().SetValue("Discrepancy");
                        ws.FirstCell().Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                        ws.FirstCell().Style.Font.SetBold();
                        ws.FirstCell().SetDataType(XLDataType.Text);
                        ws.Range(ws.Cell(1, 1), ws.Cell(1, columnDefinitionESDCDiscrepancy.Count())).Merge();

                        // rows mulai dari row 3
                        var row = 4;
                        foreach (var item in items.TXESDCDiscrepancy)
                        {
                            Type t = item.GetType();
                            PropertyInfo[] props = t.GetProperties();

                            // tiap col
                            col = 1;
                            foreach (var columnDefinition in columnDefinitionESDCDiscrepancy)
                            {
                                var prop = props.FirstOrDefault(o => o.Name == columnDefinition.Id);
                                var val = prop.GetValue(item);

                                if (val != null)
                                {
                                    // set column format
                                    switch (columnDefinition.Type)
                                    {
                                        case ColumnType.Number:
                                            if (prop.PropertyType == typeof(Int32))
                                            {
                                                ws.Cell(row, col).Style.NumberFormat.SetNumberFormatId((int)XLPredefinedFormat.Number.Integer);
                                            }
                                            else
                                            {
                                                ws.Cell(row, col).Style.NumberFormat.SetNumberFormatId((int)XLPredefinedFormat.Number.General);
                                            }
                                            break;
                                        case ColumnType.DateTime:
                                            ws.Cell(row, col).Style.NumberFormat.SetNumberFormatId((int)XLPredefinedFormat.DateTime.MonthDayYear4WithDashesHour24Minutes);
                                            break;
                                        case ColumnType.Date:
                                            if (prop.PropertyType == typeof(DateTime?))
                                            {
                                                var valDateTime = (DateTime?)prop.GetValue(item);
                                                val = valDateTime.Value.Date;
                                            }
                                            ws.Cell(row, col).Style.NumberFormat.SetNumberFormatId((int)XLPredefinedFormat.DateTime.DayMonthYear4WithSlashes);
                                            break;
                                    }
                                    ws.Cell(row, col).SetValue(val);
                                }
                                col++;
                            }
                            row++;
                        }

                        //auto fit column
                        ws.Columns(1, columnDefinitionESDCDiscrepancy.Count()).AdjustToContents();

                    }
                    if (i == 5)
                    {
                        IXLWorksheet ws = wb.AddWorksheet("In Place");

                        // header
                        var col = 1;
                        foreach (var columnDefinition in columnDefinitionESDCInPlace)
                        {
                            // set column name with format text
                            ws.Cell(2, col).SetValue(columnDefinition.Name);
                            ws.Cell(2, col).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                            ws.Cell(2, col).Style.Font.SetBold();
                            ws.Cell(2, col).Style.Fill.SetBackgroundColor(XLColor.OrangePeel);
                            col++;
                        }

                        // title report
                        ws.FirstCell().SetValue("In Place");
                        ws.FirstCell().Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                        ws.FirstCell().Style.Font.SetBold();
                        ws.FirstCell().SetDataType(XLDataType.Text);
                        ws.Range(ws.Cell(1, 1), ws.Cell(1, columnDefinitionESDCInPlace.Count())).Merge();

                        // rows mulai dari row 3
                        var row = 3;
                        foreach (var item in items.TXESDCInPlace)
                        {
                            Type t = item.GetType();
                            PropertyInfo[] props = t.GetProperties();

                            // tiap col
                            col = 1;
                            foreach (var columnDefinition in columnDefinitionESDCInPlace)
                            {
                                var prop = props.FirstOrDefault(o => o.Name == columnDefinition.Id);
                                var val = prop.GetValue(item);

                                if (val != null)
                                {
                                    // set column format
                                    switch (columnDefinition.Type)
                                    {
                                        case ColumnType.Number:
                                            if (prop.PropertyType == typeof(Int32))
                                            {
                                                ws.Cell(row, col).Style.NumberFormat.SetNumberFormatId((int)XLPredefinedFormat.Number.Integer);
                                            }
                                            else
                                            {
                                                ws.Cell(row, col).Style.NumberFormat.SetNumberFormatId((int)XLPredefinedFormat.Number.General);
                                            }
                                            break;
                                        case ColumnType.DateTime:
                                            ws.Cell(row, col).Style.NumberFormat.SetNumberFormatId((int)XLPredefinedFormat.DateTime.MonthDayYear4WithDashesHour24Minutes);
                                            break;
                                        case ColumnType.Date:
                                            if (prop.PropertyType == typeof(DateTime?))
                                            {
                                                var valDateTime = (DateTime?)prop.GetValue(item);
                                                val = valDateTime.Value.Date;
                                            }
                                            ws.Cell(row, col).Style.NumberFormat.SetNumberFormatId((int)XLPredefinedFormat.DateTime.DayMonthYear4WithSlashes);
                                            break;
                                    }
                                    ws.Cell(row, col).SetValue(val);
                                }
                                col++;
                            }
                            row++;
                        }

                        //auto fit column
                        ws.Columns(1, columnDefinitionESDCInPlace.Count()).AdjustToContents();
                    }
                }
                return wb;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public static XLWorkbook GenerateExploration<T>(string title,
            IEnumerable<T> items,
            List<ColumnDefinition> columnDefinitions,
            FilterList filter)
        {
            try
            {
                XLWorkbook wb = new ClosedXML.Excel.XLWorkbook();
                IXLWorksheet ws = wb.AddWorksheet(title);

                // header
                var col = 1;
                foreach (var columnDefinition in columnDefinitions)
                {
                    // set column name with format text
                    if (columnDefinition.Name.Contains("Net Pertamina Interest"))
                    {
                        ws.Cell(3, 33).SetValue("Net Pertamina Interest");
                        ws.Cell(3, 33).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                        ws.Cell(3, 33).Style.Font.SetBold();
                        ws.Cell(3, 33).Style.Fill.SetBackgroundColor(XLColor.BabyBlue);
                        ws.Range(ws.Cell(3, 33), ws.Cell(3, 50)).Merge();
                        ws.Range(ws.Cell(3, 33), ws.Cell(3, 50)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        ws.Range(ws.Cell(3, 33), ws.Cell(3, 50)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                        ws.Range(ws.Cell(3, 33), ws.Cell(3, 50)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        ws.Range(ws.Cell(3, 33), ws.Cell(3, 50)).Style.Border.TopBorder = XLBorderStyleValues.Thin;

                        ws.Cell(4, 33).SetValue("Initial in Place MSTB");
                        ws.Cell(4, 33).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                        ws.Cell(4, 33).Style.Font.SetBold();
                        ws.Cell(4, 33).Style.Fill.SetBackgroundColor(XLColor.LightGray);
                        ws.Range(ws.Cell(4, 33), ws.Cell(4, 36)).Merge();
                        ws.Range(ws.Cell(4, 33), ws.Cell(4, 36)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        ws.Range(ws.Cell(4, 33), ws.Cell(4, 36)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                        ws.Range(ws.Cell(4, 33), ws.Cell(4, 36)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        ws.Range(ws.Cell(4, 33), ws.Cell(4, 36)).Style.Border.TopBorder = XLBorderStyleValues.Thin;

                        ws.Cell(4, 37).SetValue("RF");
                        ws.Cell(4, 37).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                        ws.Cell(4, 37).Style.Font.SetBold();
                        ws.Cell(4, 37).Style.Fill.SetBackgroundColor(XLColor.LightGray);
                        ws.Range(ws.Cell(4, 37), ws.Cell(5, 37)).Merge();
                        ws.Range(ws.Cell(4, 37), ws.Cell(5, 37)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        ws.Range(ws.Cell(4, 37), ws.Cell(5, 37)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                        ws.Range(ws.Cell(4, 37), ws.Cell(5, 37)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        ws.Range(ws.Cell(4, 37), ws.Cell(5, 37)).Style.Border.TopBorder = XLBorderStyleValues.Thin;

                        ws.Cell(4, 38).SetValue("Prospective Resources MSTB");
                        ws.Cell(4, 38).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                        ws.Cell(4, 38).Style.Font.SetBold();
                        ws.Cell(4, 38).Style.Fill.SetBackgroundColor(XLColor.LightGray);
                        ws.Range(ws.Cell(4, 38), ws.Cell(4, 41)).Merge();
                        ws.Range(ws.Cell(4, 38), ws.Cell(4, 41)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        ws.Range(ws.Cell(4, 38), ws.Cell(4, 41)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                        ws.Range(ws.Cell(4, 38), ws.Cell(4, 41)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        ws.Range(ws.Cell(4, 38), ws.Cell(4, 41)).Style.Border.TopBorder = XLBorderStyleValues.Thin;

                        ws.Cell(4, 42).SetValue("Initial in Place MMSCF");
                        ws.Cell(4, 42).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                        ws.Cell(4, 42).Style.Font.SetBold();
                        ws.Cell(4, 42).Style.Fill.SetBackgroundColor(XLColor.LightGray);
                        ws.Range(ws.Cell(4, 42), ws.Cell(4, 45)).Merge();
                        ws.Range(ws.Cell(4, 42), ws.Cell(4, 45)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        ws.Range(ws.Cell(4, 42), ws.Cell(4, 45)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                        ws.Range(ws.Cell(4, 42), ws.Cell(4, 45)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        ws.Range(ws.Cell(4, 42), ws.Cell(4, 45)).Style.Border.TopBorder = XLBorderStyleValues.Thin;

                        ws.Cell(4, 46).SetValue("RF");
                        ws.Cell(4, 46).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                        ws.Cell(4, 46).Style.Font.SetBold();
                        ws.Cell(4, 46).Style.Fill.SetBackgroundColor(XLColor.LightGray);
                        ws.Range(ws.Cell(4, 46), ws.Cell(5, 46)).Merge();
                        ws.Range(ws.Cell(4, 46), ws.Cell(5, 46)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        ws.Range(ws.Cell(4, 46), ws.Cell(5, 46)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                        ws.Range(ws.Cell(4, 46), ws.Cell(5, 46)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        ws.Range(ws.Cell(4, 46), ws.Cell(5, 46)).Style.Border.TopBorder = XLBorderStyleValues.Thin;

                        ws.Cell(4, 47).SetValue("Prospective Resources MMSCF");
                        ws.Cell(4, 47).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                        ws.Cell(4, 47).Style.Font.SetBold();
                        ws.Cell(4, 47).Style.Fill.SetBackgroundColor(XLColor.LightGray);
                        ws.Range(ws.Cell(4, 47), ws.Cell(4, 50)).Merge();
                        ws.Range(ws.Cell(4, 47), ws.Cell(4, 50)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        ws.Range(ws.Cell(4, 47), ws.Cell(4, 50)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                        ws.Range(ws.Cell(4, 47), ws.Cell(4, 50)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        ws.Range(ws.Cell(4, 47), ws.Cell(4, 50)).Style.Border.TopBorder = XLBorderStyleValues.Thin;

                        if (columnDefinition.Name != "Net Pertamina Interest - RF MSTB" || columnDefinition.Name != "Net Pertamina Interest - RF MMSCF")
                        {
                            if(columnDefinition.Name.Contains("Low"))
                            {
                                ws.Cell(5, col).SetValue("Low");
                                ws.Cell(5, col).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                                ws.Cell(5, col).Style.Font.SetBold();
                                ws.Cell(5, col).Style.Fill.SetBackgroundColor(XLColor.Cream);
                                ws.Range(ws.Cell(5, col), ws.Cell(5, col)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                                ws.Range(ws.Cell(5, col), ws.Cell(5, col)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                                ws.Range(ws.Cell(5, col), ws.Cell(5, col)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                                ws.Range(ws.Cell(5, col), ws.Cell(5, col)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                            }
                            if (columnDefinition.Name.Contains("Best"))
                            {
                                ws.Cell(5, col).SetValue("Best");
                                ws.Cell(5, col).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                                ws.Cell(5, col).Style.Font.SetBold();
                                ws.Cell(5, col).Style.Fill.SetBackgroundColor(XLColor.Cream);
                                ws.Range(ws.Cell(5, col), ws.Cell(5, col)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                                ws.Range(ws.Cell(5, col), ws.Cell(5, col)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                                ws.Range(ws.Cell(5, col), ws.Cell(5, col)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                                ws.Range(ws.Cell(5, col), ws.Cell(5, col)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                            }
                            if (columnDefinition.Name.Contains("Mean"))
                            {
                                ws.Cell(5, col).SetValue("Mean");
                                ws.Cell(5, col).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                                ws.Cell(5, col).Style.Font.SetBold();
                                ws.Cell(5, col).Style.Fill.SetBackgroundColor(XLColor.Cream);
                                ws.Range(ws.Cell(5, col), ws.Cell(5, col)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                                ws.Range(ws.Cell(5, col), ws.Cell(5, col)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                                ws.Range(ws.Cell(5, col), ws.Cell(5, col)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                                ws.Range(ws.Cell(5, col), ws.Cell(5, col)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                            }
                            if (columnDefinition.Name.Contains("High"))
                            {
                                ws.Cell(5, col).SetValue("High");
                                ws.Cell(5, col).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                                ws.Cell(5, col).Style.Font.SetBold();
                                ws.Cell(5, col).Style.Fill.SetBackgroundColor(XLColor.Cream);
                                ws.Range(ws.Cell(5, col), ws.Cell(5, col)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                                ws.Range(ws.Cell(5, col), ws.Cell(5, col)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                                ws.Range(ws.Cell(5, col), ws.Cell(5, col)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                                ws.Range(ws.Cell(5, col), ws.Cell(5, col)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                            }
                            if (columnDefinition.Name.Contains("1U"))
                            {
                                ws.Cell(5, col).SetValue("1U");
                                ws.Cell(5, col).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                                ws.Cell(5, col).Style.Font.SetBold();
                                ws.Cell(5, col).Style.Fill.SetBackgroundColor(XLColor.Cream);
                                ws.Range(ws.Cell(5, col), ws.Cell(5, col)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                                ws.Range(ws.Cell(5, col), ws.Cell(5, col)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                                ws.Range(ws.Cell(5, col), ws.Cell(5, col)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                                ws.Range(ws.Cell(5, col), ws.Cell(5, col)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                            }
                            if (columnDefinition.Name.Contains("2U"))
                            {
                                ws.Cell(5, col).SetValue("2U");
                                ws.Cell(5, col).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                                ws.Cell(5, col).Style.Font.SetBold();
                                ws.Cell(5, col).Style.Fill.SetBackgroundColor(XLColor.Cream);
                                ws.Range(ws.Cell(5, col), ws.Cell(5, col)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                                ws.Range(ws.Cell(5, col), ws.Cell(5, col)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                                ws.Range(ws.Cell(5, col), ws.Cell(5, col)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                                ws.Range(ws.Cell(5, col), ws.Cell(5, col)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                            }
                            if (columnDefinition.Name.Contains("3U"))
                            {
                                ws.Cell(5, col).SetValue("3U");
                                ws.Cell(5, col).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                                ws.Cell(5, col).Style.Font.SetBold();
                                ws.Cell(5, col).Style.Fill.SetBackgroundColor(XLColor.Cream);
                                ws.Range(ws.Cell(5, col), ws.Cell(5, col)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                                ws.Range(ws.Cell(5, col), ws.Cell(5, col)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                                ws.Range(ws.Cell(5, col), ws.Cell(5, col)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                                ws.Range(ws.Cell(5, col), ws.Cell(5, col)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                            }
                        }
                    }
                    else if(columnDefinition.Name.Contains("PI 100%"))
                    {
                        ws.Cell(3, 15).SetValue("PI 100%");
                        ws.Cell(3, 15).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                        ws.Cell(3, 15).Style.Font.SetBold();
                        ws.Cell(3, 15).Style.Fill.SetBackgroundColor(XLColor.BabyBlue);
                        ws.Range(ws.Cell(3, 15), ws.Cell(3, 32)).Merge();
                        ws.Range(ws.Cell(3, 15), ws.Cell(3, 32)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        ws.Range(ws.Cell(3, 15), ws.Cell(3, 32)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                        ws.Range(ws.Cell(3, 15), ws.Cell(3, 32)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        ws.Range(ws.Cell(3, 15), ws.Cell(3, 32)).Style.Border.TopBorder = XLBorderStyleValues.Thin;

                        ws.Cell(4, 15).SetValue("Initial in Place MSTB");
                        ws.Cell(4, 15).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                        ws.Cell(4, 15).Style.Font.SetBold();
                        ws.Cell(4, 15).Style.Fill.SetBackgroundColor(XLColor.LightGray);
                        ws.Range(ws.Cell(4, 15), ws.Cell(4, 18)).Merge();
                        ws.Range(ws.Cell(4, 15), ws.Cell(4, 18)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        ws.Range(ws.Cell(4, 15), ws.Cell(4, 18)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                        ws.Range(ws.Cell(4, 15), ws.Cell(4, 18)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        ws.Range(ws.Cell(4, 15), ws.Cell(4, 18)).Style.Border.TopBorder = XLBorderStyleValues.Thin;

                        ws.Cell(4, 19).SetValue("RF");
                        ws.Cell(4, 19).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                        ws.Cell(4, 19).Style.Font.SetBold();
                        ws.Cell(4, 19).Style.Fill.SetBackgroundColor(XLColor.LightGray);
                        ws.Range(ws.Cell(4, 19), ws.Cell(5, 19)).Merge();
                        ws.Range(ws.Cell(4, 19), ws.Cell(5, 19)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        ws.Range(ws.Cell(4, 19), ws.Cell(5, 19)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                        ws.Range(ws.Cell(4, 19), ws.Cell(5, 19)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        ws.Range(ws.Cell(4, 19), ws.Cell(5, 19)).Style.Border.TopBorder = XLBorderStyleValues.Thin;

                        ws.Cell(4, 20).SetValue("Prospective Resources MSTB");
                        ws.Cell(4, 20).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                        ws.Cell(4, 20).Style.Font.SetBold();
                        ws.Cell(4, 20).Style.Fill.SetBackgroundColor(XLColor.LightGray);
                        ws.Range(ws.Cell(4, 20), ws.Cell(4, 23)).Merge();
                        ws.Range(ws.Cell(4, 20), ws.Cell(4, 23)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        ws.Range(ws.Cell(4, 20), ws.Cell(4, 23)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                        ws.Range(ws.Cell(4, 20), ws.Cell(4, 23)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        ws.Range(ws.Cell(4, 20), ws.Cell(4, 23)).Style.Border.TopBorder = XLBorderStyleValues.Thin;

                        ws.Cell(4, 24).SetValue("Initial in Place MMSCF");
                        ws.Cell(4, 24).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                        ws.Cell(4, 24).Style.Font.SetBold();
                        ws.Cell(4, 24).Style.Fill.SetBackgroundColor(XLColor.LightGray);
                        ws.Range(ws.Cell(4, 24), ws.Cell(4, 27)).Merge();
                        ws.Range(ws.Cell(4, 24), ws.Cell(4, 27)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        ws.Range(ws.Cell(4, 24), ws.Cell(4, 27)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                        ws.Range(ws.Cell(4, 24), ws.Cell(4, 27)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        ws.Range(ws.Cell(4, 24), ws.Cell(4, 27)).Style.Border.TopBorder = XLBorderStyleValues.Thin;

                        ws.Cell(4, 28).SetValue("RF");
                        ws.Cell(4, 28).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                        ws.Cell(4, 28).Style.Font.SetBold();
                        ws.Cell(4, 28).Style.Fill.SetBackgroundColor(XLColor.LightGray);
                        ws.Range(ws.Cell(4, 28), ws.Cell(5, 28)).Merge();
                        ws.Range(ws.Cell(4, 28), ws.Cell(5, 28)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        ws.Range(ws.Cell(4, 28), ws.Cell(5, 28)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                        ws.Range(ws.Cell(4, 28), ws.Cell(5, 28)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        ws.Range(ws.Cell(4, 28), ws.Cell(5, 28)).Style.Border.TopBorder = XLBorderStyleValues.Thin;

                        ws.Cell(4, 29).SetValue("Prospective Resources MMSCF");
                        ws.Cell(4, 29).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                        ws.Cell(4, 29).Style.Font.SetBold();
                        ws.Cell(4, 29).Style.Fill.SetBackgroundColor(XLColor.LightGray);
                        ws.Range(ws.Cell(4, 29), ws.Cell(4, 32)).Merge();
                        ws.Range(ws.Cell(4, 29), ws.Cell(4, 32)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        ws.Range(ws.Cell(4, 29), ws.Cell(4, 32)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                        ws.Range(ws.Cell(4, 29), ws.Cell(4, 32)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        ws.Range(ws.Cell(4, 29), ws.Cell(4, 32)).Style.Border.TopBorder = XLBorderStyleValues.Thin;

                        if (columnDefinition.Name != "PI 100% - RF MSTB" || columnDefinition.Name != "PI 100% - RF MMSCF")
                        {
                            if (columnDefinition.Name.Contains("Low"))
                            {
                                ws.Cell(5, col).SetValue("Low");
                                ws.Cell(5, col).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                                ws.Cell(5, col).Style.Font.SetBold();
                                ws.Cell(5, col).Style.Fill.SetBackgroundColor(XLColor.Cream);
                                ws.Range(ws.Cell(5, col), ws.Cell(5, col)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                                ws.Range(ws.Cell(5, col), ws.Cell(5, col)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                                ws.Range(ws.Cell(5, col), ws.Cell(5, col)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                                ws.Range(ws.Cell(5, col), ws.Cell(5, col)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                            }
                            if (columnDefinition.Name.Contains("Best"))
                            {
                                ws.Cell(5, col).SetValue("Best");
                                ws.Cell(5, col).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                                ws.Cell(5, col).Style.Font.SetBold();
                                ws.Cell(5, col).Style.Fill.SetBackgroundColor(XLColor.Cream);
                                ws.Range(ws.Cell(5, col), ws.Cell(5, col)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                                ws.Range(ws.Cell(5, col), ws.Cell(5, col)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                                ws.Range(ws.Cell(5, col), ws.Cell(5, col)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                                ws.Range(ws.Cell(5, col), ws.Cell(5, col)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                            }
                            if (columnDefinition.Name.Contains("Mean"))
                            {
                                ws.Cell(5, col).SetValue("Mean");
                                ws.Cell(5, col).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                                ws.Cell(5, col).Style.Font.SetBold();
                                ws.Cell(5, col).Style.Fill.SetBackgroundColor(XLColor.Cream);
                                ws.Range(ws.Cell(5, col), ws.Cell(5, col)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                                ws.Range(ws.Cell(5, col), ws.Cell(5, col)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                                ws.Range(ws.Cell(5, col), ws.Cell(5, col)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                                ws.Range(ws.Cell(5, col), ws.Cell(5, col)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                            }
                            if (columnDefinition.Name.Contains("High"))
                            {
                                ws.Cell(5, col).SetValue("High");
                                ws.Cell(5, col).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                                ws.Cell(5, col).Style.Font.SetBold();
                                ws.Cell(5, col).Style.Fill.SetBackgroundColor(XLColor.Cream);
                                ws.Range(ws.Cell(5, col), ws.Cell(5, col)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                                ws.Range(ws.Cell(5, col), ws.Cell(5, col)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                                ws.Range(ws.Cell(5, col), ws.Cell(5, col)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                                ws.Range(ws.Cell(5, col), ws.Cell(5, col)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                            }
                            if (columnDefinition.Name.Contains("1U"))
                            {
                                ws.Cell(5, col).SetValue("1U");
                                ws.Cell(5, col).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                                ws.Cell(5, col).Style.Font.SetBold();
                                ws.Cell(5, col).Style.Fill.SetBackgroundColor(XLColor.Cream);
                                ws.Range(ws.Cell(5, col), ws.Cell(5, col)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                                ws.Range(ws.Cell(5, col), ws.Cell(5, col)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                                ws.Range(ws.Cell(5, col), ws.Cell(5, col)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                                ws.Range(ws.Cell(5, col), ws.Cell(5, col)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                            }
                            if (columnDefinition.Name.Contains("2U"))
                            {
                                ws.Cell(5, col).SetValue("2U");
                                ws.Cell(5, col).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                                ws.Cell(5, col).Style.Font.SetBold();
                                ws.Cell(5, col).Style.Fill.SetBackgroundColor(XLColor.Cream);
                                ws.Range(ws.Cell(5, col), ws.Cell(5, col)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                                ws.Range(ws.Cell(5, col), ws.Cell(5, col)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                                ws.Range(ws.Cell(5, col), ws.Cell(5, col)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                                ws.Range(ws.Cell(5, col), ws.Cell(5, col)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                            }
                            if (columnDefinition.Name.Contains("3U"))
                            {
                                ws.Cell(5, col).SetValue("3U");
                                ws.Cell(5, col).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                                ws.Cell(5, col).Style.Font.SetBold();
                                ws.Cell(5, col).Style.Fill.SetBackgroundColor(XLColor.Cream);
                                ws.Range(ws.Cell(5, col), ws.Cell(5, col)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                                ws.Range(ws.Cell(5, col), ws.Cell(5, col)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                                ws.Range(ws.Cell(5, col), ws.Cell(5, col)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                                ws.Range(ws.Cell(5, col), ws.Cell(5, col)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                            }
                        }
                    }
                    else
                    {
                        if (columnDefinition.Name.Trim() == "StructureID")
                        {
                            ws.Cell(3, col).SetValue(columnDefinition.Name);
                            ws.Cell(3, col).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                            ws.Cell(3, col).Style.Font.SetBold();
                            ws.Cell(3, col).Style.Fill.SetBackgroundColor(XLColor.Green);
                            ws.Range(ws.Cell(3, col), ws.Cell(5, col)).Merge();
                            ws.Range(ws.Cell(3, col), ws.Cell(5, col)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                            ws.Range(ws.Cell(3, col), ws.Cell(5, col)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                            ws.Range(ws.Cell(3, col), ws.Cell(5, col)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                            ws.Range(ws.Cell(3, col), ws.Cell(5, col)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                        }
                        else
                        {
                            ws.Cell(3, col).SetValue(columnDefinition.Name);
                            ws.Cell(3, col).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                            ws.Cell(3, col).Style.Font.SetBold();
                            ws.Cell(3, col).Style.Fill.SetBackgroundColor(XLColor.BabyBlue);
                            ws.Range(ws.Cell(3, col), ws.Cell(5, col)).Merge();
                            ws.Range(ws.Cell(3, col), ws.Cell(5, col)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                            ws.Range(ws.Cell(3, col), ws.Cell(5, col)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                            ws.Range(ws.Cell(3, col), ws.Cell(5, col)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                            ws.Range(ws.Cell(3, col), ws.Cell(5, col)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                        }
                    }
                    col++;
                }

                // title report
                ws.FirstCell().SetValue("PROSPECTIVE RESOURCES SUMMARY PER STRUKTUR PT. PERTAMINA HULU ENERGI");
                ws.FirstCell().Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                ws.FirstCell().Style.Font.SetBold();
                ws.FirstCell().Style.Fill.SetBackgroundColor(XLColor.Green);
                ws.FirstCell().SetDataType(XLDataType.Text);
                ws.Range(ws.Cell(1, 1), ws.Cell(1, columnDefinitions.Count())).Merge();

                ws.Cell(2, 1).SetValue("AS OF " + DateTime.Now.Date.ToString("dddd, dd MMMM yyyy"));
                ws.Cell(2, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                ws.Cell(2, 1).Style.Font.SetBold();
                ws.Cell(2, 1).Style.Fill.SetBackgroundColor(XLColor.Green);
                ws.Cell(2, 1).SetDataType(XLDataType.Text);
                ws.Range(ws.Cell(2, 1), ws.Cell(2, columnDefinitions.Count())).Merge();

                // rows mulai dari row 3
                var totProsResource = items.Count();
                var totPSB = 0;
                var totProspek = 0;
                var totalLead = 0;
                var row = 6;
                var totRow = 0;
                foreach (var item in items)
                {
                    Type t = item.GetType();
                    PropertyInfo[] props = t.GetProperties();

                    // tiap col
                    col = 1;
                    foreach (var columnDefinition in columnDefinitions)
                    {
                        var prop = props.FirstOrDefault(o => o.Name == columnDefinition.Id);
                        var val = prop.GetValue(item);

                        if (val != null)
                        {
                            // set column format
                            switch (columnDefinition.Type)
                            {
                                case ColumnType.Number:
                                    if (prop.PropertyType == typeof(Int32))
                                    {
                                        ws.Cell(row, col).Style.NumberFormat.SetNumberFormatId((int)XLPredefinedFormat.Number.Integer);
                                    }
                                    else
                                    {
                                        ws.Cell(row, col).Style.NumberFormat.SetNumberFormatId((int)XLPredefinedFormat.Number.General);
                                    }
                                    break;
                                case ColumnType.DateTime:
                                    var datetimeMin = (DateTime?)prop.GetValue(item);
                                    if (datetimeMin == DateTime.MinValue)
                                    {
                                        val = "";
                                    }
                                    ws.Cell(row, col).Style.NumberFormat.SetNumberFormatId((int)XLPredefinedFormat.DateTime.MonthDayYear4WithDashesHour24Minutes);
                                    break;
                                case ColumnType.Date:
                                    if (prop.PropertyType == typeof(DateTime?))
                                    {
                                        var valDateTime = (DateTime?)prop.GetValue(item);
                                        val = valDateTime.Value.Date;
                                    }
                                    ws.Cell(row, col).Style.NumberFormat.SetNumberFormatId((int)XLPredefinedFormat.DateTime.DayMonthYear4WithSlashes);
                                    break;
                            }
                            ws.Cell(row, col).SetValue(val);
                        }
                        if(columnDefinition.Name == "TRR/Prospect/Lead")
                        {
                            if(val.ToString() == "Lead")
                            {
                                totalLead++;
                            }
                            else if(val.ToString() == "Prospek (Prospect)")
                            {
                                totProspek++;
                            }
                            else if(val.ToString() == "Prospek Siap Bor (Drillable Prospect)")
                            {
                                totPSB++;
                            }
                        }
                        col++;
                    }
                    row++;
                    totRow = row;
                }

                //ws.Cell(totRow + 2, 3).SetValue("Jumlah PSB / Prospect / Lead");
                //ws.Cell(totRow + 2, 3).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                //ws.Cell(totRow + 2, 3).Style.Font.SetBold();
                //ws.Cell(totRow + 3, 2).SetValue("Total Prospective Resources");
                //ws.Cell(totRow + 3, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                //ws.Cell(totRow + 3, 2).Style.Font.SetBold();
                //ws.Cell(totRow + 4, 2).SetValue("Total Prospect Siap Bor (PSB)");
                //ws.Cell(totRow + 4, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                //ws.Cell(totRow + 4, 2).Style.Font.SetBold();
                //ws.Cell(totRow + 5, 2).SetValue("Total Prospect (P)");
                //ws.Cell(totRow + 5, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                //ws.Cell(totRow + 5, 2).Style.Font.SetBold();
                //ws.Cell(totRow + 6, 2).SetValue("Total Lead (L)");
                //ws.Cell(totRow + 6, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                //ws.Cell(totRow + 6, 2).Style.Font.SetBold();

                //// isi total
                //ws.Cell(totRow + 3, 3).SetValue(totProsResource);
                //ws.Cell(totRow + 4, 3).SetValue(totPSB);
                //ws.Cell(totRow + 5, 3).SetValue(totProspek);
                //ws.Cell(totRow + 6, 3).SetValue(totalLead);


                //auto fit column
                ws.Columns(1, columnDefinitions.Count()).AdjustToContents();

                return wb;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static XLWorkbook GenerateExplorationRJPP<T>(string title,
            IEnumerable<T> items,
            List<ColumnDefinition> columnDefinitions,
            FilterList filter)
        {
            try
            {
                XLWorkbook wb = new ClosedXML.Excel.XLWorkbook();
                IXLWorksheet ws = wb.AddWorksheet(title);

                // header
                var col = 1;
                foreach (var columnDefinition in columnDefinitions)
                {
                    if(columnDefinition.Name.Contains("Resources"))
                    {
                        ws.Cell(2, 23).SetValue("Resources");
                        ws.Cell(2, 23).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                        ws.Cell(2, 23).Style.Font.SetBold();
                        ws.Cell(2, 23).Style.Fill.SetBackgroundColor(XLColor.BabyBlue);
                        ws.Range(ws.Cell(2, 23), ws.Cell(2, 28)).Merge();
                        ws.Range(ws.Cell(2, 23), ws.Cell(2, 28)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        ws.Range(ws.Cell(2, 23), ws.Cell(2, 28)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                        ws.Range(ws.Cell(2, 23), ws.Cell(2, 28)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        ws.Range(ws.Cell(2, 23), ws.Cell(2, 28)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                        ws.Cell(3, 23).SetValue("Oil MMBo");
                        ws.Cell(3, 23).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                        ws.Cell(3, 23).Style.Font.SetBold();
                        ws.Cell(3, 23).Style.Fill.SetBackgroundColor(XLColor.LightGray);
                        ws.Range(ws.Cell(3, 23), ws.Cell(3, 25)).Merge();
                        ws.Range(ws.Cell(3, 23), ws.Cell(3, 25)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        ws.Range(ws.Cell(3, 23), ws.Cell(3, 25)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                        ws.Range(ws.Cell(3, 23), ws.Cell(3, 25)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        ws.Range(ws.Cell(3, 23), ws.Cell(3, 25)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                        ws.Cell(3, 26).SetValue("Gas Bcf");
                        ws.Cell(3, 26).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                        ws.Cell(3, 26).Style.Font.SetBold();
                        ws.Cell(3, 26).Style.Fill.SetBackgroundColor(XLColor.LightGray);
                        ws.Range(ws.Cell(3, 26), ws.Cell(3, 28)).Merge();
                        ws.Range(ws.Cell(3, 26), ws.Cell(3, 28)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        ws.Range(ws.Cell(3, 26), ws.Cell(3, 28)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                        ws.Range(ws.Cell(3, 26), ws.Cell(3, 28)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        ws.Range(ws.Cell(3, 26), ws.Cell(3, 28)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                        if(columnDefinition.Name.Contains("P90"))
                        {
                            ws.Cell(4, col).SetValue("P90");
                            ws.Cell(4, col).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                            ws.Cell(4, col).Style.Font.SetBold();
                            ws.Cell(4, col).Style.Fill.SetBackgroundColor(XLColor.Cream);
                            ws.Range(ws.Cell(4, col), ws.Cell(4, col)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                            ws.Range(ws.Cell(4, col), ws.Cell(4, col)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                            ws.Range(ws.Cell(4, col), ws.Cell(4, col)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                            ws.Range(ws.Cell(4, col), ws.Cell(4, col)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                        }
                        if (columnDefinition.Name.Contains("P50"))
                        {
                            ws.Cell(4, col).SetValue("P50");
                            ws.Cell(4, col).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                            ws.Cell(4, col).Style.Font.SetBold();
                            ws.Cell(4, col).Style.Fill.SetBackgroundColor(XLColor.Cream);
                            ws.Range(ws.Cell(4, col), ws.Cell(4, col)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                            ws.Range(ws.Cell(4, col), ws.Cell(4, col)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                            ws.Range(ws.Cell(4, col), ws.Cell(4, col)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                            ws.Range(ws.Cell(4, col), ws.Cell(4, col)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                        }
                        if (columnDefinition.Name.Contains("P10"))
                        {
                            ws.Cell(4, col).SetValue("P10");
                            ws.Cell(4, col).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                            ws.Cell(4, col).Style.Font.SetBold();
                            ws.Cell(4, col).Style.Fill.SetBackgroundColor(XLColor.Cream);
                            ws.Range(ws.Cell(4, col), ws.Cell(4, col)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                            ws.Range(ws.Cell(4, col), ws.Cell(4, col)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                            ws.Range(ws.Cell(4, col), ws.Cell(4, col)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                            ws.Range(ws.Cell(4, col), ws.Cell(4, col)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                        }
                    }
                    else if(columnDefinition.Name.Contains("Chance"))
                    {
                        ws.Cell(2, 31).SetValue("Chance Components");
                        ws.Cell(2, 31).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                        ws.Cell(2, 31).Style.Font.SetBold();
                        ws.Cell(2, 31).Style.Fill.SetBackgroundColor(XLColor.BabyBlue);
                        ws.Range(ws.Cell(2, 31), ws.Cell(3, 35)).Merge();
                        ws.Range(ws.Cell(2, 31), ws.Cell(3, 35)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        ws.Range(ws.Cell(2, 31), ws.Cell(3, 35)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                        ws.Range(ws.Cell(2, 31), ws.Cell(3, 35)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        ws.Range(ws.Cell(2, 31), ws.Cell(3, 35)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                        if(columnDefinition.Name.Contains("Source"))
                        {
                            ws.Cell(4, col).SetValue("Source");
                            ws.Cell(4, col).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                            ws.Cell(4, col).Style.Font.SetBold();
                            ws.Cell(4, col).Style.Fill.SetBackgroundColor(XLColor.Cream);
                            ws.Range(ws.Cell(4, col), ws.Cell(4, col)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                            ws.Range(ws.Cell(4, col), ws.Cell(4, col)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                            ws.Range(ws.Cell(4, col), ws.Cell(4, col)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                            ws.Range(ws.Cell(4, col), ws.Cell(4, col)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                        }
                        if (columnDefinition.Name.Contains("Timing/Migration"))
                        {
                            ws.Cell(4, col).SetValue("Timing/Migration");
                            ws.Cell(4, col).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                            ws.Cell(4, col).Style.Font.SetBold();
                            ws.Cell(4, col).Style.Fill.SetBackgroundColor(XLColor.Cream);
                            ws.Range(ws.Cell(4, col), ws.Cell(4, col)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                            ws.Range(ws.Cell(4, col), ws.Cell(4, col)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                            ws.Range(ws.Cell(4, col), ws.Cell(4, col)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                            ws.Range(ws.Cell(4, col), ws.Cell(4, col)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                        }
                        if (columnDefinition.Name.Contains("Reservoir"))
                        {
                            ws.Cell(4, col).SetValue("Reservoir");
                            ws.Cell(4, col).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                            ws.Cell(4, col).Style.Font.SetBold();
                            ws.Cell(4, col).Style.Fill.SetBackgroundColor(XLColor.Cream);
                            ws.Range(ws.Cell(4, col), ws.Cell(4, col)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                            ws.Range(ws.Cell(4, col), ws.Cell(4, col)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                            ws.Range(ws.Cell(4, col), ws.Cell(4, col)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                            ws.Range(ws.Cell(4, col), ws.Cell(4, col)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                        }
                        if (columnDefinition.Name.Contains("Closure"))
                        {
                            ws.Cell(4, col).SetValue("Closure");
                            ws.Cell(4, col).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                            ws.Cell(4, col).Style.Font.SetBold();
                            ws.Cell(4, col).Style.Fill.SetBackgroundColor(XLColor.Cream);
                            ws.Range(ws.Cell(4, col), ws.Cell(4, col)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                            ws.Range(ws.Cell(4, col), ws.Cell(4, col)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                            ws.Range(ws.Cell(4, col), ws.Cell(4, col)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                            ws.Range(ws.Cell(4, col), ws.Cell(4, col)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                        }
                        if (columnDefinition.Name.Contains("Containment"))
                        {
                            ws.Cell(4, col).SetValue("Containment");
                            ws.Cell(4, col).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                            ws.Cell(4, col).Style.Font.SetBold();
                            ws.Cell(4, col).Style.Fill.SetBackgroundColor(XLColor.Cream);
                            ws.Range(ws.Cell(4, col), ws.Cell(4, col)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                            ws.Range(ws.Cell(4, col), ws.Cell(4, col)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                            ws.Range(ws.Cell(4, col), ws.Cell(4, col)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                            ws.Range(ws.Cell(4, col), ws.Cell(4, col)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                        }
                    }
                    else if(columnDefinition.Name.Contains("Well Cost"))
                    {
                        ws.Cell(2, 36).SetValue("Well Cost (MMUSD)");
                        ws.Cell(2, 36).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                        ws.Cell(2, 36).Style.Font.SetBold();
                        ws.Cell(2, 36).Style.Fill.SetBackgroundColor(XLColor.BabyBlue);
                        ws.Range(ws.Cell(2, 36), ws.Cell(3, 37)).Merge();
                        ws.Range(ws.Cell(2, 36), ws.Cell(3, 37)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        ws.Range(ws.Cell(2, 36), ws.Cell(3, 37)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                        ws.Range(ws.Cell(2, 36), ws.Cell(3, 37)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        ws.Range(ws.Cell(2, 36), ws.Cell(3, 37)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                        if(columnDefinition.Name.Contains("DHB"))
                        {
                            ws.Cell(4, col).SetValue("DHB");
                            ws.Cell(4, col).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                            ws.Cell(4, col).Style.Font.SetBold();
                            ws.Cell(4, col).Style.Fill.SetBackgroundColor(XLColor.Cream);
                            ws.Range(ws.Cell(4, col), ws.Cell(4, col)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                            ws.Range(ws.Cell(4, col), ws.Cell(4, col)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                            ws.Range(ws.Cell(4, col), ws.Cell(4, col)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                            ws.Range(ws.Cell(4, col), ws.Cell(4, col)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                        }
                        if (columnDefinition.Name.Contains("CHB"))
                        {
                            ws.Cell(4, col).SetValue("CHB");
                            ws.Cell(4, col).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                            ws.Cell(4, col).Style.Font.SetBold();
                            ws.Cell(4, col).Style.Fill.SetBackgroundColor(XLColor.Cream);
                            ws.Range(ws.Cell(4, col), ws.Cell(4, col)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                            ws.Range(ws.Cell(4, col), ws.Cell(4, col)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                            ws.Range(ws.Cell(4, col), ws.Cell(4, col)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                            ws.Range(ws.Cell(4, col), ws.Cell(4, col)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                        }
                    }
                    else if(columnDefinition.Name.Contains("NPV"))
                    {
                        ws.Cell(2, 38).SetValue("NPV Profitability");
                        ws.Cell(2, 38).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                        ws.Cell(2, 38).Style.Font.SetBold();
                        ws.Cell(2, 38).Style.Fill.SetBackgroundColor(XLColor.BabyBlue);
                        ws.Range(ws.Cell(2, 38), ws.Cell(2, 43)).Merge();
                        ws.Range(ws.Cell(2, 38), ws.Cell(2, 43)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        ws.Range(ws.Cell(2, 38), ws.Cell(2, 43)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                        ws.Range(ws.Cell(2, 38), ws.Cell(2, 43)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        ws.Range(ws.Cell(2, 38), ws.Cell(2, 43)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                        ws.Cell(3, 38).SetValue("Oil (USD/Bbl)");
                        ws.Cell(3, 38).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                        ws.Cell(3, 38).Style.Font.SetBold();
                        ws.Cell(3, 38).Style.Fill.SetBackgroundColor(XLColor.LightGray);
                        ws.Range(ws.Cell(3, 38), ws.Cell(3, 40)).Merge();
                        ws.Range(ws.Cell(3, 38), ws.Cell(3, 40)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        ws.Range(ws.Cell(3, 38), ws.Cell(3, 40)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                        ws.Range(ws.Cell(3, 38), ws.Cell(3, 40)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        ws.Range(ws.Cell(3, 38), ws.Cell(3, 40)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                        ws.Cell(3, 41).SetValue("Gas (USD/Mcf)");
                        ws.Cell(3, 41).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                        ws.Cell(3, 41).Style.Font.SetBold();
                        ws.Cell(3, 41).Style.Fill.SetBackgroundColor(XLColor.LightGray);
                        ws.Range(ws.Cell(3, 41), ws.Cell(3, 43)).Merge();
                        ws.Range(ws.Cell(3, 41), ws.Cell(3, 43)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        ws.Range(ws.Cell(3, 41), ws.Cell(3, 43)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                        ws.Range(ws.Cell(3, 41), ws.Cell(3, 43)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        ws.Range(ws.Cell(3, 41), ws.Cell(3, 43)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                        if (columnDefinition.Name.Contains("P90"))
                        {
                            ws.Cell(4, col).SetValue("P90");
                            ws.Cell(4, col).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                            ws.Cell(4, col).Style.Font.SetBold();
                            ws.Cell(4, col).Style.Fill.SetBackgroundColor(XLColor.Cream);
                            ws.Range(ws.Cell(4, col), ws.Cell(4, col)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                            ws.Range(ws.Cell(4, col), ws.Cell(4, col)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                            ws.Range(ws.Cell(4, col), ws.Cell(4, col)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                            ws.Range(ws.Cell(4, col), ws.Cell(4, col)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                        }
                        if (columnDefinition.Name.Contains("P50"))
                        {
                            ws.Cell(4, col).SetValue("P50");
                            ws.Cell(4, col).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                            ws.Cell(4, col).Style.Font.SetBold();
                            ws.Cell(4, col).Style.Fill.SetBackgroundColor(XLColor.Cream);
                            ws.Range(ws.Cell(4, col), ws.Cell(4, col)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                            ws.Range(ws.Cell(4, col), ws.Cell(4, col)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                            ws.Range(ws.Cell(4, col), ws.Cell(4, col)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                            ws.Range(ws.Cell(4, col), ws.Cell(4, col)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                        }
                        if (columnDefinition.Name.Contains("P10"))
                        {
                            ws.Cell(4, col).SetValue("P10");
                            ws.Cell(4, col).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                            ws.Cell(4, col).Style.Font.SetBold();
                            ws.Cell(4, col).Style.Fill.SetBackgroundColor(XLColor.Cream);
                            ws.Range(ws.Cell(4, col), ws.Cell(4, col)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                            ws.Range(ws.Cell(4, col), ws.Cell(4, col)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                            ws.Range(ws.Cell(4, col), ws.Cell(4, col)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                            ws.Range(ws.Cell(4, col), ws.Cell(4, col)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                        }
                    }
                    else
                    {
                        if(columnDefinition.Name.Trim() == "StructureID")
                        {
                            ws.Cell(2, col).SetValue(columnDefinition.Name);
                            ws.Cell(2, col).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                            ws.Cell(2, col).Style.Font.SetBold();
                            ws.Cell(2, col).Style.Fill.SetBackgroundColor(XLColor.Green);
                            ws.Range(ws.Cell(2, col), ws.Cell(4, col)).Merge();
                            ws.Range(ws.Cell(2, col), ws.Cell(4, col)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                            ws.Range(ws.Cell(2, col), ws.Cell(4, col)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                            ws.Range(ws.Cell(2, col), ws.Cell(4, col)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                            ws.Range(ws.Cell(2, col), ws.Cell(4, col)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                        }
                        else
                        {
                            ws.Cell(2, col).SetValue(columnDefinition.Name);
                            ws.Cell(2, col).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                            ws.Cell(2, col).Style.Font.SetBold();
                            ws.Cell(2, col).Style.Fill.SetBackgroundColor(XLColor.BabyBlue);
                            ws.Range(ws.Cell(2, col), ws.Cell(4, col)).Merge();
                            ws.Range(ws.Cell(2, col), ws.Cell(4, col)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                            ws.Range(ws.Cell(2, col), ws.Cell(4, col)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                            ws.Range(ws.Cell(2, col), ws.Cell(4, col)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                            ws.Range(ws.Cell(2, col), ws.Cell(4, col)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                        }
                    }
                    // set column name with format text
                    col++;
                }

                // title report
                ws.FirstCell().SetValue(title);
                ws.FirstCell().Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                ws.FirstCell().Style.Font.SetBold();
                ws.FirstCell().SetDataType(XLDataType.Text);
                ws.Range(ws.Cell(1, 1), ws.Cell(1, columnDefinitions.Count())).Merge();

                // rows mulai dari row 3
                var row = 5;
                foreach (var item in items)
                {
                    Type t = item.GetType();
                    PropertyInfo[] props = t.GetProperties();

                    // tiap col
                    col = 1;
                    foreach (var columnDefinition in columnDefinitions)
                    {
                        var prop = props.FirstOrDefault(o => o.Name == columnDefinition.Id);
                        var val = prop.GetValue(item);

                        if (val != null)
                        {
                            // set column format
                            switch (columnDefinition.Type)
                            {
                                case ColumnType.Number:
                                    if (prop.PropertyType == typeof(Int32))
                                    {
                                        ws.Cell(row, col).Style.NumberFormat.SetNumberFormatId((int)XLPredefinedFormat.Number.Integer);
                                    }
                                    else
                                    {
                                        ws.Cell(row, col).Style.NumberFormat.SetNumberFormatId((int)XLPredefinedFormat.Number.General);
                                    }
                                    break;
                                case ColumnType.DateTime:
                                    var datetimeMin = (DateTime?)prop.GetValue(item);
                                    if (datetimeMin == DateTime.MinValue)
                                    {
                                        val = "";
                                    }
                                    ws.Cell(row, col).Style.NumberFormat.SetNumberFormatId((int)XLPredefinedFormat.DateTime.MonthDayYear4WithDashesHour24Minutes);
                                    break;
                                case ColumnType.Date:
                                    if (prop.PropertyType == typeof(DateTime?))
                                    {
                                        var valDateTime = (DateTime?)prop.GetValue(item);
                                        val = valDateTime.Value.Date;
                                    }
                                    ws.Cell(row, col).Style.NumberFormat.SetNumberFormatId((int)XLPredefinedFormat.DateTime.DayMonthYear4WithSlashes);
                                    break;
                            }
                            ws.Cell(row, col).SetValue(val);
                        }
                        col++;
                    }
                    row++;
                }

                //auto fit column

                ws.Columns(1, columnDefinitions.Count()).AdjustToContents();

                return wb;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
