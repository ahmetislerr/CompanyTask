﻿using AutoMapper;
using CompanyTask.Application.Models.DTOs.DepartmentDTOs;
using CompanyTask.Domain.Entities;
using CompanyTask.Domain.Enums;
using CompanyTask.Domain.Repositories;

namespace CompanyTask.Application.Services.DepartmentService
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;

        public DepartmentService(IDepartmentRepository departmentRepository, IMapper mapper)
        {
            _departmentRepository = departmentRepository;
            _mapper = mapper;
        }

        public async Task<bool> Create(CreateDepartmentDTO model, int? companyId)
        {
            Department department = _mapper.Map<Department>(model);
            department.StatuId = Status.Active.GetHashCode();
            department.CompanyId = companyId;
            return await _departmentRepository.Add(department);
        }

        public async Task Delete(int id)
        {
            Department department = await _departmentRepository.GetDefault(x => x.Id == id);
            if (department != null)
            {
                department.StatuId = Status.Deleted.GetHashCode();
                await _departmentRepository.Delete(department);
            }
        }

        public async Task<UpdateDepartmentDTO> GetById(int id)
        {
            Department department = await _departmentRepository.GetDefault(x => x.Id == id);
            return _mapper.Map<UpdateDepartmentDTO>(department);
        }

        public async Task<bool> Update(UpdateDepartmentDTO model)
        {
            Department department = _mapper.Map<Department>(model);
            return await _departmentRepository.Update(department);
        }
    }
}
