using NAF.DOMAIN.DomainObjects.Account.User;
using NAF.INFRASTRUCTURE.DataConnect;

namespace NAF.INFRASTRUCTURE
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(AppDbContext db) : base(db)
        {
        }
    }
}