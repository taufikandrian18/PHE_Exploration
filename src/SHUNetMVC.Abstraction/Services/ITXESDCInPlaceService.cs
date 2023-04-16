using ASPNetMVC.Abstraction.Model.Entities;
using SHUNetMVC.Abstraction.Model.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHUNetMVC.Abstraction.Services
{
    public interface ITXESDCInPlaceService : ICrudService<TXESDCInPlaceDto, TXESDCInPlaceDto>
    {
        List<TXESDCInPlaceDto> GetAll();
        Task Destroy(string structureID, string uncertaintyLevel);
        Task<string> GenerateNewID();
        Task<TX_ESDCInPlace> GetInPlaceTargetByStructureID(string structureID);
        Task<List<TXESDCInPlaceDto>> GetListTXESDCInPlaceByStructureID(string structureID);
    }
}
