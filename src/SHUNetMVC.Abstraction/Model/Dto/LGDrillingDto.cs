﻿using ASPNetMVC.Abstraction.Model.Entities;
using SHUNetMVC.Abstraction.Model.Response;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SHUNetMVC.Abstraction.Model.Dto
{
    public class LGDrillingDto : BaseDtoAutoMapper<LG_Drilling>
    {
        public string StructureHistoryID { get; set; }
        public string xStructureID { get; set; }
        public string xWellID { get; set; }
        public Nullable<int> RKAPFiscalYear { get; set; }
        public Nullable<bool> PlayOpener { get; set; }
        public Nullable<decimal> DrillingCompletionPeriod { get; set; }
        public string Location { get; set; }
        public Nullable<decimal> WaterDepthFeet { get; set; }
        public Nullable<int> WaterDepthMeter { get; set; }
        public Nullable<decimal> TotalDepthFeet { get; set; }
        public Nullable<int> TotalDepthMeter { get; set; }
        public Nullable<decimal> SurfaceLocationLatitude { get; set; }
        public Nullable<decimal> SurfaceLocationLongitude { get; set; }
        public Nullable<bool> CommitmentWell { get; set; }
        public string OperationalContextParId { get; set; }
        public Nullable<bool> PotentialDelay { get; set; }
        public Nullable<decimal> NetRevenueInterest { get; set; }
        public Nullable<decimal> DrillingCostDHB { get; set; }
        public string DrillingCostDHBCurr { get; set; }
        public Nullable<decimal> DrillingCost { get; set; }
        public string DrillingCostCurr { get; set; }
        public Nullable<System.DateTime> ExpectedDrillingDate { get; set; }
        public Nullable<decimal> P90ResourceOil { get; set; }
        public string P90ResourceOilUoM { get; set; }
        public Nullable<decimal> P50ResourceOil { get; set; }
        public string P50ResourceOilUoM { get; set; }
        public Nullable<decimal> P10ResourceOil { get; set; }
        public string P10ResourceOilUoM { get; set; }
        public Nullable<decimal> P90ResourceGas { get; set; }
        public string P90ResourceGasUoM { get; set; }
        public Nullable<decimal> P50ResourceGas { get; set; }
        public string P50ResourceGasUoM { get; set; }
        public Nullable<decimal> P10ResourceGas { get; set; }
        public string P10ResourceGasUoM { get; set; }
        public Nullable<decimal> CurrentPG { get; set; }
        public Nullable<decimal> ExpectedPG { get; set; }
        public Nullable<decimal> ChanceComponentSource { get; set; }
        public Nullable<decimal> ChanceComponentTiming { get; set; }
        public Nullable<decimal> ChanceComponentReservoir { get; set; }
        public Nullable<decimal> ChanceComponentClosure { get; set; }
        public Nullable<decimal> ChanceComponentContainment { get; set; }
        public Nullable<decimal> P90NPVProfitabilityOil { get; set; }
        public string P90NPVProfitabilityOilCurr { get; set; }
        public Nullable<decimal> P50NPVProfitabilityOil { get; set; }
        public string P50NPVProfitabilityOilCurr { get; set; }
        public Nullable<decimal> P10NPVProfitabilityOil { get; set; }
        public string P10NPVProfitabilityOilCurr { get; set; }
        public Nullable<decimal> P90NPVProfitabilityGas { get; set; }
        public string P90NPVProfitabilityGasCurr { get; set; }
        public Nullable<decimal> P50NPVProfitabilityGas { get; set; }
        public string P50NPVProfitabilityGasCurr { get; set; }
        public Nullable<decimal> P10NPVProfitabilityGas { get; set; }
        public string P10NPVProfitabilityGasCurr { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public virtual LG_ExplorationStructure LG_ExplorationStructure { get; set; }
    }
}
