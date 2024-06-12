using Microsoft.AspNetCore.Identity;

namespace PiggyBankAuthenApi.Db
{
    public class PiggyBankUserEntity:IdentityUser
    {
        public string PairedID { get; set; } = string.Empty;
        public DateTime CreateTime { get; set; }
    }
}
