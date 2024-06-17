using Contract.Dtos;

namespace PiggyBankAuthenApi.Db
{
    public interface IDbContext
    {
        public IDbConnectionBuilder GetConnectionBuilder();

        public Task<PiggyBankUserEntity> Insert(UserRequestDto dto);

        public Task<UserResponseDto> QueryByNameAndPassword(string userName, string password);
    }
}
