namespace SHUNetMVC.Abstraction.Repositories
{
    public interface IConnectionProvider
    {
        string GetConnectionString();
        string GetConnectionStringHRIS();
    }
}
