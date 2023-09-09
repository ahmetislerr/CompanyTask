using CompanyTask.Application.Models.Vms.PersonelVMs;
using CompanyTask.Domain.Entities;
using CompanyTask.Domain.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CompanyTask.Application.Services.PersonelService
{
    public class PersonelService : IPersonelService
    {
        private readonly IAppUserRepository _userRepository;
        private readonly UserManager<AppUser> _userManager;

        public PersonelService(IAppUserRepository userRepository, UserManager<AppUser> userManager)
        {
            _userRepository = userRepository;
            _userManager = userManager;
        }

        public async Task<List<CompanyEmployeesVM>> GetCompanyEmployees(int companyId, string searchString)
        {
            List<CompanyEmployeesVM> companyEmployees = await _userRepository.GetFilteredList(
                select: x => new CompanyEmployeesVM()
                {
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    PhoneNumber = x.PhoneNumber,
                    Email = x.Email,
                    Title = x.Title.Name,
                    Department = x.Department.Name,
                    ImagePath = x.ImagePath
                },
                where: x => x.CompanyId == companyId && ((x.FirstName + " " + x.LastName).Contains(searchString)),
                orderby: x => x.OrderBy(x => x.FirstName),
                include: x => x.Include(x => x.Title).Include(x => x.Department)
                );

            return companyEmployees;
        }

        public async Task<PersonelVM> GetPersonel(string userName)
        {
            var personel = await _userRepository.GetFilteredFirstOrDefault(
               select: x => new PersonelVM()
               {
                   FirstName = x.FirstName,
                   LastName = x.LastName,
                   Image = x.ImagePath == null ? "/media/images/noImage.png" : x.ImagePath,
                   FullName = x.FirstName + " " + x.LastName,
                   CompanyId = x.CompanyId,
                   CompanyName = x.Company.CompanyName,
                   CompanyLogo = x.Company.ImagePath
               },
               where: x => x.UserName == userName,
               orderby: null,
               include: x => x.Include(x => x.Department).Include(x => x.Company)
               );

            return personel;
        }

        public async Task<Guid> GetPersonelId(string name)
        {
            AppUser user = await _userManager.FindByNameAsync(name);
            return user.Id;
        }
    }
}
