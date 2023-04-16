using SHUNetMVC.Abstraction.Model.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHUNetMVC.Abstraction.Services
{
    public interface ITXAttachmentService : ICrudService<TXAttachmentDto, TXAttachmentDto>
    {
        Task<List<TXAttachmentDto>> GetAttachmentByStructureID(string structureID);
        Task<TXAttachmentDto> GetOneByTwoParameter(string TransactionID, string FileCategoryParID);
        int GenerateNewID();
        Task Destroy(string TransactionID, int FileID);
    }
}
