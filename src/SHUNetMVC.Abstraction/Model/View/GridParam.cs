using ASPNetMVC.Abstraction.Model.Entities;
using System.Collections.Generic;

namespace SHUNetMVC.Abstraction.Model.View
{
    public class GridParam
    {
        public string GridId { get; set; }

        public string xStructureID { get; set; }
        public ExportExcelExploration Data { get; set; }

        public FormDefinition ItemList { get; set; }

        public string GridJsVar => $"adsGrid['{GridId}']";

        public List<ColumnDefinition> ColumnDefinitions { get; set; }
        public FilterList FilterList { get; set; }
        public string HrisRegObj { get; set; }
        public string UsernameSession { get; set; }

    }
    public class ExportExcelExploration
    {
        public IEnumerable<MDExplorationStructureExcelDto> ExplorationStructure { get; set; }
        public IEnumerable<TXProsResourcesTargetExcelDto> ProsResourcesTarget { get; set; }
        public IEnumerable<TXProsResourcesExcelDto> ProsResources { get; set; }
        public IEnumerable<TXContResourcesExcelDto> ContResources { get; set; }
        public IEnumerable<TXDrillingExcelDto> Drilling { get; set; }
        public IEnumerable<TXEconomicExcelDto> Economic { get; set; }
    }

    public class ExportExcelESDC
    {
        public IEnumerable<ESDCExcelDto> TXESDC { get; set; }
        public IEnumerable<ESDCProdExcelDto> TXESDCProduction { get; set; }
        public IEnumerable<ESDCVolumetricExcelDto> TXESDCVolumetric { get; set; }
        public IEnumerable<ESDCForecastExcelDto> TXESDCForecast { get; set; }
        public IEnumerable<ESDCDiscrepancyExcelDto> TXESDCDiscrepancy { get; set; }
        public IEnumerable<ESDCInPlaceExcelDto> TXESDCInPlace { get; set; }
    }
}
