
namespace PiggyBankAuthenApi.Db
{
    public class PiggyBankUserEntity
    {
        
        public int Id { get; set; }

        public int PairedGroupId { get; set; }
        public int Gender { get; set; }

        public string? UserName { get; set; }
        public string? Email { get; set; }

        public string? Password { get; set; }

        public string? Role { get; set; }

        public string? AvatarUrl { get; set; }

        public string? PhoneNumber { get; set; }

        public DateTime CreateDate { get; set; } 

        public DateTime LastUpdateTime { get; set; } 

        public bool IsDeleted { get; set; } = false; 


    }
}
