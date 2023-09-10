using AutoMapper;
using CompanyTask.Application.Models.DTOs.CompanyDTO;
using CompanyTask.Application.Models.DTOs.CompanyManagerDTO;
using CompanyTask.Application.Models.Vms.CompanyManagerVMs;
using CompanyTask.Application.Models.Vms.PersonelVMs;
using CompanyTask.Domain.Entities;
using CompanyTask.Domain.Enums;
using CompanyTask.Domain.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace CompanyTask.Application.Services.CompanyManagerService
{
    public class CompanyManagerService : ICompanyManagerService
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly ITitleRepository _titleRepository;
        private readonly IAppUserRepository _appUserRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly ICompanyRepository _companyRepository;
        private readonly IAddressRepository _addressRepository;
        
        public CompanyManagerService(IDepartmentRepository departmentRepository, ITitleRepository titleRepository, IMapper mapper, UserManager<AppUser> userManager, IAppUserRepository appUserRepository, ICompanyRepository companyRepository, IAddressRepository addressRepository)
        {
            _departmentRepository = departmentRepository;
            _titleRepository = titleRepository;
            _mapper = mapper;
            _userManager = userManager;
            _appUserRepository = appUserRepository;
            _companyRepository = companyRepository;
            _addressRepository = addressRepository;
        }
        public async Task<CreateEmployeeVM> CreateEmployee(CreateEmployeeDTO model)
        {
            var userEmail = await _userManager.FindByEmailAsync(model.Email);
            var userUserName = await _userManager.FindByNameAsync(model.UserName);
            CreateEmployeeVM createEmployee = new CreateEmployeeVM();
            if (userEmail == null && userUserName == null)
            {
                AppUser newEmployee = _mapper.Map<AppUser>(model);
                if (model.Image != null)
                {
                    using var image = Image.Load(model.Image.OpenReadStream());

                    Guid guid = Guid.NewGuid();
                    image.Save($"wwwroot/media/images/{guid}.jpg");

                    newEmployee.ImagePath = $"/media/images/{guid}.jpg";
                }
                else
                    newEmployee.ImagePath = model.ImagePath;

                newEmployee.Address = new Address()
                {
                    CreatedDate = DateTime.Now,
                    Description = model.AddressDescription,
                    DistrictId = model.DistrictId,
                };

                var password = model.Password;

                await _userManager.CreateAsync(newEmployee, password);
                IdentityResult result = await _userManager.AddToRoleAsync(newEmployee, "Employee");
                if (!model.IsEmployee)
                {
                    await _userManager.AddToRoleAsync(newEmployee, "Manager");
                }

                if (result.Succeeded)
                {
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(newEmployee);
                    createEmployee.Email = newEmployee.Email;
                    createEmployee.Token = token;
                    createEmployee.Result = result;
                    createEmployee.Password = password;
                }
                else
                {
                    createEmployee.Result = result;
                }
                return createEmployee;
            }
            else
            {
                if (userEmail != null)
                {
                    createEmployee.Errors.Add("Email already exist.");
                }
                if (userUserName != null)
                {
                    createEmployee.Errors.Add("User Name already exist.");
                }
                createEmployee.Model = model;
                return createEmployee;
            }
        }

        public async Task Delete(Guid id)
        {
            AppUser leave = await _appUserRepository.GetDefault(x => x.Id == id);
            if (leave != null)
            {
                leave.StatuId = Status.Deleted.GetHashCode();
                leave.DeletedDate = DateTime.Now;
                await _appUserRepository.Delete(leave);
            }
        }

        public async Task<UpdateEmployeeDTO> GetByUserName(Guid id)
        {
            UpdateEmployeeDTO result = await _appUserRepository.GetFilteredFirstOrDefault(
            select: x => new UpdateEmployeeDTO
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                UserName = x.UserName,
                Email = x.Email,
                PhoneNumber = x.PhoneNumber,
                CountryId = x.Address.District.City.CountryId,
                CityId = x.Address.District.CityId,
                DistrictId = x.Address.DistrictId,
                AddressDescription = x.Address.Description,
                DepartmentId = x.DepartmentId,
                BirthDate = x.BirthDate,
                RecruitmentDate = x.RecruitmentDate,
                ManagerId = x.ManagerId,
                ImagePath = x.ImagePath,
                TitleId = x.TitleId
            },
            where: x => x.Id == id,
            orderby: null,
            include: x => x.Include(x => x.Address).Include(x => x.Address.District).Include(x => x.Address.District.City)
            );

            return result;
        }

        public async Task<UpdateCompanyDTO> GetCompany(int? id)
        {
            UpdateCompanyDTO result = await _companyRepository.GetFilteredFirstOrDefault(
            select: x => new UpdateCompanyDTO
            {
                CompanyId = x.Id,
                CompanyName = x.CompanyName,
                TaxNumber = x.TaxNumber,
                TaxOfficeName = x.TaxOfficeName,
                PhoneNumber = x.PhoneNumber,
                NumberOfEmployee = x.NumberOfEmployee,
                CountryId = x.Address.District.City.CountryId,
                CityId = x.Address.District.CityId,
                DistrictId = x.Address.DistrictId,
                AddressDescription = x.Address.Description,
                ManagerId = x.CompanyRepresentativeId,
                ImagePath = x.ImagePath
            },
            where: x => x.Id == id,
            orderby: null,
            include: x => x.Include(x => x.Address).Include(x => x.Address.District).Include(x => x.Address.District.City).Include(x => x.CompanyRepresentative)
            );

            return result;
        }

        public async Task<List<CompanyManagerVM>> GetCompanyManagers(int? companyId)
        {
            var companyPersonels = await _appUserRepository.GetFilteredList(
              select: x => new CompanyManagerVM()
              {
                  Id = x.Id,
                  FullName = x.FirstName + " " + x.LastName,
                  UserName = x.UserName,
                  Email = x.Email
              },
              where: x => x.CompanyId == companyId,
              orderby: x => x.OrderByDescending(x => x.FirstName)
              );
            List<CompanyManagerVM> companyManagers = new List<CompanyManagerVM>();
            foreach (var personel in companyPersonels)
            {
                if (await IsCompanyManager(personel.UserName))
                {
                    companyManagers.Add(personel);
                }
            }

            return companyManagers;
        }

        public async Task<List<DepartmentVM>> GetDepartments(int? companyId)
        {
            var departments = await _departmentRepository.GetFilteredList(
              select: x => new DepartmentVM()
              {
                  Id = x.Id,
                  Name = x.Name
              },
              where: x => x.StatuId == Status.Active.GetHashCode() && x.CompanyId == companyId,
              orderby: x => x.OrderByDescending(x => x.Name)
              );

            return departments;
        }

        public async Task<List<EmployeeVM>> GetEmployees(int companyId)
        {
            var employees = await _appUserRepository.GetFilteredList(
              select: x => new EmployeeVM()
              {
                  Id = x.Id,
                  FullName = x.FirstName + " " + x.LastName,
                  DepartmentName = x.Department.Name,
                  Title = x.Title.Name,
                  UserName = x.UserName,
                  ManagerName = x.Manager.FirstName + " " + x.Manager.LastName,
                  Phone = x.PhoneNumber,
                  Email = x.Email,
                  BirthDate = x.BirthDate.ToString(),
              },
              where: x => x.StatuId != Status.Deleted.GetHashCode() && x.CompanyId == companyId,
              orderby: x => x.OrderByDescending(x => x.CreatedDate),
              include: x => x.Include(x => x.Department).Include(x => x.Title).Include(x => x.Manager)
              );

            return employees;
        }

        public async Task<List<TitleVM>> GetTitles(int? companyId)
        {
            var titles = await _titleRepository.GetFilteredList(
             select: x => new TitleVM()
             {
                 Id = x.Id,
                 Name = x.Name

             },
             where: x => x.StatuId == Status.Active.GetHashCode() && x.CompanyId == companyId,
             orderby: x => x.OrderByDescending(x => x.Name)
             );

            return titles;
        }

        public async Task<bool> IsCompanyManager(string userName)
        {
            AppUser user = await _userManager.FindByNameAsync(userName);

            foreach (var roles in await _userManager.GetRolesAsync(user))
            {
                if (roles == "CompanyManager")
                {
                    return true;
                }
            }
            return false;
        }

        public async Task<bool> UpdateCompany(UpdateCompanyDTO model)
        {
            Company company = await _companyRepository.GetDefault(x => x.Id == model.CompanyId);
            if (model.Image != null)
            {
                using var image = Image.Load(model.Image.OpenReadStream());

                Guid guid = Guid.NewGuid();
                image.Save($"wwwroot/media/images/{guid}.jpg");

                company.ImagePath = $"/media/images/{guid}.jpg";
            }
            company.CompanyName = model.CompanyName;
            company.PhoneNumber = model.PhoneNumber;
            company.NumberOfEmployee = model.NumberOfEmployee;


            Address address = await _addressRepository.GetDefault(x => x.CompanyId == model.CompanyId);

            if (model.DistrictId != address.DistrictId || model.AddressDescription != address.Description)
            {
                address.DistrictId = model.DistrictId;
                address.Description = model.AddressDescription;
                await _addressRepository.Update(address);
            }

            company.CompanyRepresentativeId = model.ManagerId;
            return await _companyRepository.Update(company);
        }

        public async Task<IdentityResult> UpdateEmployee(UpdateEmployeeDTO model)
        {
            AppUser user = await _appUserRepository.GetDefault(x => x.Id == model.Id);
            AppUser isUserMailExists = await _userManager.FindByEmailAsync(model.Email);
            IdentityError errorEmail = new IdentityError();
            if (user.Email != model.Email)
            {
                if (isUserMailExists == null)
                {
                    await _userManager.SetEmailAsync(user, model.Email);
                }
                else
                {
                    errorEmail.Description = "Email already exist.";
                }
            }
            AppUser isUserNameExists = await _userManager.FindByNameAsync(model.UserName);
            IdentityError errorUserName = new IdentityError();
            if (user.UserName != model.UserName)
            {
                if (isUserNameExists == null)
                {
                    await _userManager.SetUserNameAsync(user, model.UserName);
                }
                else
                {
                    errorUserName.Description = "User name already exist.";
                }
            }
            if (model.Image != null)
            {
                using var image = Image.Load(model.Image.OpenReadStream());

                Guid guid = Guid.NewGuid();
                image.Save($"wwwroot/media/images/{guid}.jpg");

                user.ImagePath = $"/media/images/{guid}.jpg";
            }
            user.BirthDate = model.BirthDate;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.PhoneNumber = model.PhoneNumber;
            user.DepartmentId = model.DepartmentId;
            user.RecruitmentDate = model.RecruitmentDate;
            user.ManagerId = model.ManagerId;
            user.TitleId = model.TitleId;

            if (!model.IsEmployee && !(await IsCompanyManager(user.UserName)))
            {
                await _userManager.AddToRoleAsync(user, "Employee");
            }
            if (model.IsEmployee && (await IsCompanyManager(user.UserName)))
            {
                await _userManager.AddToRoleAsync(user, "Company Manager");
            }


            Address address = await _addressRepository.GetDefault(x => x.AppUserId == model.Id);
            if (user.Address != null)
            {
                if (model.DistrictId != address.DistrictId || model.AddressDescription != address.Description)
                {
                    address.DistrictId = model.DistrictId;
                    address.Description = model.AddressDescription;
                    await _addressRepository.Update(address);
                }
            }
            else
            {
                user.Address = new Address()
                {
                    CreatedDate = DateTime.Now,
                    Description = model.AddressDescription,
                    DistrictId = model.DistrictId,
                };
            }


            if (errorEmail.Description == null && errorUserName.Description == null)
                return await _userManager.UpdateAsync(user);
            else
            {
                if (errorEmail.Description != null)
                {
                    if (errorUserName.Description != null)
                    {
                        return IdentityResult.Failed(errorEmail, errorUserName);

                    }
                    return IdentityResult.Failed(errorEmail);
                }
                else
                    return IdentityResult.Failed(errorUserName);
            }
        }
    }
}
