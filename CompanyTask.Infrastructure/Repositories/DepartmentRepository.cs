using CompanyTask.Domain.Entities;
using CompanyTask.Domain.Repositories;
using CompanyTask.Infrastructure.DbContext;

namespace CompanyTask.Infrastructure.Repositories
{
    public class DepartmentRepository : BaseRepository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
