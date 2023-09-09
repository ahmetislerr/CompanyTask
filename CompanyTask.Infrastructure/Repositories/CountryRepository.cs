using CompanyTask.Domain.Entities;
using CompanyTask.Domain.Repositories;
using CompanyTask.Infrastructure.DbContext;

namespace CompanyTask.Infrastructure.Repositories
{
    public class CountryRepository : BaseRepository<Country>, ICountryRepository
    {
        public CountryRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
