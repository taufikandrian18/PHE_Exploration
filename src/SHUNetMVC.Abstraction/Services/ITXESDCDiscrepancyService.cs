using ASPNetMVC.Abstraction.Model.Entities;
using SHUNetMVC.Abstraction.Model.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHUNetMVC.Abstraction.Services
{
    public interface ITXESDCDiscrepancyService : ICrudService<TXESDCDiscrepancyDto, TXESDCDiscrepancyDto>
    {
        List<TXESDCDiscrepancyDto> GetAll();
        Task Destroy(string structureID, string uncertaintyLevel);
        Task<string> GenerateNewID();
        Task<TX_ESDCDiscrepancy> GetDiscrepancyTargetByStructureID(string structureID);
        Task<List<TXESDCDiscrepancyDto>> GetListTXESDCDiscrepancyByStructureID(string structureID);
    }
}
