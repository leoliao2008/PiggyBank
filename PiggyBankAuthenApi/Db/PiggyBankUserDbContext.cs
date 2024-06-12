using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace PiggyBankAuthenApi.Db
{
    public class PiggyBankUserDbContext:IdentityDbContext<PiggyBankUserEntity>
    {
        public PiggyBankUserDbContext(DbContextOptions<PiggyBankUserDbContext> opt):base(opt) {
        }

        

        
    }
}
