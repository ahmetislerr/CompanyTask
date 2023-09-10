using CompanyTask.Domain.Entities;
using CompanyTask.Domain.Repositories;
using CompanyTask.Infrastructure.DbContext;

namespace CompanyTask.Infrastructure.Repositories
{
    public class WorkShiftRepository : BaseRepository<WorkShift>, IWorkShiftRepository
    {
        public WorkShiftRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
