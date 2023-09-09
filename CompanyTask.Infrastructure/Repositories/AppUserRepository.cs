using CompanyTask.Domain.Entities;
using CompanyTask.Domain.Repositories;
using CompanyTask.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CompanyTask.Infrastructure.Repositories
{
    public class AppUserRepository : BaseRepository<AppUser>, IAppUserRepository
    {
        public AppUserRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<AppUser> GetDefault(Expression<Func<AppUser, bool>> expression)
        {
            return await _table.Include(x => x.Address).FirstOrDefaultAsync(expression);
        }
    }
}
