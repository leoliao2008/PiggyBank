using Contract.Dtos;

namespace PiggyBankAuthenApi.Db
{
    public interface IDbContext
    {
        public IDbConnectionBuilder GetConnectionBuilder();

        public Task<PiggyBankUserEntity> Insert(UserRegisterRequestDto dto);

        public Task<UserRegisterResponseDto> QueryByNameAndPassword(string userName, string password);
    }
}
