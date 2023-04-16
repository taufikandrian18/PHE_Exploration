using Serilog;
using SHUNetMVC.Abstraction.Model.Dto;
using SHUNetMVC.Abstraction.Repositories;
using SHUNetMVC.Abstraction.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHUNetMVC.Infrastructure.Services
{
    public class TXAttachmentService : BaseCrudService<TXAttachmentDto, TXAttachmentDto>, ITXAttachmentService
    {
        private readonly ITXAttachmentRepository _repo;
        public TXAttachmentService(ITXAttachmentRepository repo, ILogger logger) : base(repo, logger)
        {
            _repo = repo;
        }

        public Task Destroy(string TransactionID, int FileID)
        {
            return _repo.Destroy(TransactionID, FileID);
        }

        public int GenerateNewID()
        {
            return _repo.GenerateNewID();
        }

        public async Task<List<TXAttachmentDto>> GetAttachmentByStructureID(string structureID)
        {
            return await _repo.GetAttachmentByStructureID(structureID);
        }

        public async Task<TXAttachmentDto> GetOneByTwoParameter(string TransactionID, string FileCategoryParID)
        {
            return await _repo.GetOneByTwoParameter(TransactionID, FileCategoryParID);
        }
    }
}
