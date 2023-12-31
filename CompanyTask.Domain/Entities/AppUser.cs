﻿using Microsoft.AspNetCore.Identity;

namespace CompanyTask.Domain.Entities
{
    public class AppUser : IdentityUser<Guid>, IBaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? DepartmentId { get; set; }
        public DateTime? RecruitmentDate { get; set; }
        public DateTime? BirthDate { get; set; }
        public int? BloodTypeId { get; set; }
        public int? TitleId { get; set; }
        public int? CompanyId { get; set; }
        public Guid? ManagerId { get; set; }
        public string? ImagePath { get; set; }

        public int? StatuId { get; set; }
        public DateTime CreatedDate { get; set; }//??
        public DateTime? ModifiedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public Statu? Statu { get; set; }


        //Navigation Properties
        public Department? Department { get; set; }
        public AppUser? Manager { get; set; }
        public Company? CompanyRepresentative { get; set; }
        public List<AppUser>? Employees { get; set; }
        public Address? Address { get; set; }
        public Title? Title { get; set; }
        public Company? Company { get; set; }
        public List<WorkShift> WorkShifts { get; set; }
    }
}
