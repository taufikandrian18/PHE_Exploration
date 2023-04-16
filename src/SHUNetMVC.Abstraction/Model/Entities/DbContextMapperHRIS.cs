using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Core.EntityClient;
using Wmo.Extension;

namespace ASPNetMVC.Abstraction.Model.Entities
{
    public class DbContextMapperHRIS : DB_PHE_HRIS_DEVEntities
    {
        public DbContextMapperHRIS() : base(GetConnectionString("DB_PHE_HRIS_DEVEntities"))
        {

        }

        public static string GetConnectionString(string connectionName)
        {
            try
            {
                string connectionStringRaw = System.Configuration.ConfigurationManager.ConnectionStrings[connectionName].ConnectionString;
                string encrypted = new EntityConnectionStringBuilder(connectionStringRaw).ProviderConnectionString;
                string decrypted = decConn(encrypted);

                connectionStringRaw = connectionStringRaw.Replace(encrypted, decrypted);

                return connectionStringRaw;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

        private static string decConn(string con)
        {
            Encryption Enc = Encryption.GetInstance;
            return Enc.Decrypt(con, Enc.Decrypt(System.Configuration.ConfigurationManager.AppSettings["Con:Key"].ToString()));
        }
    }
}
