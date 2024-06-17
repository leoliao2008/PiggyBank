
namespace PiggyBankAuthenApi.Db
{
    public sealed class PiggyBankUserEntity
    {
        public int Id, PairedId, Gender;
        public string? UserName, Email, Password, Role, Claims, AvatarUrl, PhoneNumber;
        public DateTime? CreateDate, LastUpdateTime;
        public bool IsDeleted;

    }
}
