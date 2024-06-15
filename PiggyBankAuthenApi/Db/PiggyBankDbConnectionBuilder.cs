using Microsoft.Data.SqlClient;
using System.Data.Common;

namespace PiggyBankAuthenApi.Db
{
    public class PiggyBankDbConnectionBuilder : IDbConnectionBuilder
    {
        private string _conStr = string.Empty;

        public string GetConnectionString()
        {
            return _conStr;
        }

        public SqlConnection GetDbConnection()
        {
            if (GetConnectionString() == string.Empty) { throw new Exception("Connection string is empty"); }
            return new SqlConnection(_conStr);
        }

        public void SetConnectionString(string conStr)
        {
            _conStr = conStr;
        }

    
    }
}
