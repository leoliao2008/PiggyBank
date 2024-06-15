using Microsoft.Data.SqlClient;
using System.Data.Common;

namespace PiggyBankAuthenApi.Db
{
    public interface IDbConnectionBuilder
    {
        public void SetConnectionString(string conStr);

        public string GetConnectionString();

        SqlConnection GetDbConnection();
    }
}
