using CompanyTask.Domain.Entities;
using CompanyTask.Domain.Repositories;
using CompanyTask.Infrastructure.DbContext;

namespace CompanyTask.Infrastructure.Repositories
{
    public class CompanySectorRepository : BaseRepository<CompanySector>, ICompanySectorRepository
    {
        public CompanySectorRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
