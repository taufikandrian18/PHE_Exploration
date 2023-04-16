using SHUNetMVC.Abstraction.Repositories;
using System;
using System.Configuration;
using System.Data.Entity.Core.EntityClient;

namespace SHUNetMVC.Web.Providers
{
    public class ConnectionStringProvider : IConnectionProvider
    {
        public string GetConnectionString()
        {
            try
            {
                string connectionStringRaw = System.Configuration.ConfigurationManager.ConnectionStrings["DB_PHE_ExplorationEntities"].ConnectionString;
                string providerConnectionString = new EntityConnectionStringBuilder(connectionStringRaw).ProviderConnectionString;

                return providerConnectionString;
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                throw e;
            }
            //string connectionString = ConfigurationManager.ConnectionStrings[1].ConnectionString;
            //int pFrom = connectionString.IndexOf("\"") + 1;
            //int pTo = connectionString.LastIndexOf("\"");

            ////string result = (pTo >= 0) ? connectionString.Substring(pFrom, pTo - pFrom) : connectionString;
            //string result = connectionString.Substring(pFrom, pTo - pFrom);
            //return result;
        }

        public string GetConnectionStringHRIS()
        {
            string connectionString = ConfigurationManager.ConnectionStrings[2].ConnectionString;
            int pFrom = connectionString.IndexOf("\"") + 1;
            int pTo = connectionString.LastIndexOf("\"");

            string result = connectionString.Substring(pFrom, pTo - pFrom);
            return result;
        }
    }
}