using ASPNetMVC.Abstraction.Model.Entities;
using SHUNetMVC.Abstraction.Model.Dto;
using SHUNetMVC.Abstraction.Model.Response;
using SHUNetMVC.Abstraction.Model.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHUNetMVC.Abstraction.Services
{
    public interface ILGProsResourcesTargetService : ICrudService<LGProsResourcesTargetDto, TXPropectiveResourceTargetWthFields>
    {
        List<LGProsResourcesTargetDto> GetAll();
        Task Destroy(string targetID);
        void DestroyTarget(string targetID);
        Task<string> GenerateNewID();
    }
}
