using Microsoft.Extensions.Options;
using PiggyBankAuthenApi.Options;

namespace PiggyBankAuthenApi.Services
{
    public class DapperConnectionProvider(IOptionsSnapshot<DapperConnectionOptions> options)
    {
        private string _connectionString = options.Value.ConnectionString!;

    }
}
