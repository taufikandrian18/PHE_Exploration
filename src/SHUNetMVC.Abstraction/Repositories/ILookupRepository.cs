using SHUNetMVC.Abstraction.Model.Request;
using SHUNetMVC.Abstraction.Model.Response;
using SHUNetMVC.Abstraction.Model.View;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SHUNetMVC.Abstraction.Repositories
{
    public interface ILookupRepository
    {
        LookupList GetExplorationAssets();
        LookupList GetExplorationBasins();
        LookupList GetExplorationBlocks();
    }
}
