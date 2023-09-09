using CompanyTask.Domain.Entities;
using CompanyTask.Domain.Repositories;
using CompanyTask.Infrastructure.DbContext;

namespace CompanyTask.Infrastructure.Repositories
{
    public class AddressRepository : BaseRepository<Address>, IAddressRepository
    {
        public AddressRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
