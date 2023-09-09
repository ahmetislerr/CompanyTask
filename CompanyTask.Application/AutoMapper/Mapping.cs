using AutoMapper;
using CompanyTask.Application.Models.DTOs.AccountDTOs;
using CompanyTask.Application.Models.DTOs.CompanyDTO;
using CompanyTask.Application.Models.DTOs.CompanyManagerDTO;
using CompanyTask.Application.Models.DTOs.DepartmentDTOs;
using CompanyTask.Application.Models.DTOs.TitleDTOs;
using CompanyTask.Domain.Entities;

namespace CompanyTask.Application.AutoMapper
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<AppUser, RegisterDTO>().ReverseMap();
            CreateMap<AppUser, UpdateProfileDTO>().ReverseMap();

            CreateMap<Department, CreateDepartmentDTO>().ReverseMap();
            CreateMap<Department, UpdateDepartmentDTO>().ReverseMap();

            CreateMap<Title, CreateTitleDTO>().ReverseMap();
            CreateMap<Title, UpdateTitleDTO>().ReverseMap();

            CreateMap<AppUser, CreateEmployeeDTO>().ReverseMap();

            CreateMap<Company, UpdateCompanyDTO>().ReverseMap();
            CreateMap<Company, RegisterDTO>().ReverseMap();

            CreateMap<Address, RegisterDTO>().ReverseMap();
        }
    }
}
