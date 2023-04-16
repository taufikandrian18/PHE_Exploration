using SHUNetMVC.Abstraction.Model.View;

namespace SHUNetMVC.Abstraction.Services
{
    public interface ILookupService
    {
        LookupList GetExplorationAssets();
        LookupList GetExplorationBlocks();
        LookupList GetExplorationBasins();
    }
}
