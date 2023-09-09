using CompanyTask.Domain.Entities;
using CompanyTask.Domain.Repositories;
using CompanyTask.Infrastructure.DbContext;

namespace CompanyTask.Infrastructure.Repositories
{
    public class DistrictRepository : BaseRepository<District>, IDistrictRepository
    {
        public DistrictRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
