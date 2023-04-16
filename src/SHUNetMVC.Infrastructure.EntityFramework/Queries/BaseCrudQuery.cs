namespace SHUNetMVC.Infrastructure.EntityFramework.Queries
{
    public abstract class BaseCrudQuery
    {
        public abstract string SelectPagedQuery { get; }
        public abstract string PagedRoles { get; }
        public abstract string PagedReport { get; }
        public abstract string CountQuery { get; }
        public abstract string LookupTextQuery { get; }
        public abstract string LookupListTextQuery { get; }
        public abstract string GenerateID { get; }
        public abstract string ExcelExportQuery { get; }
    }
}
