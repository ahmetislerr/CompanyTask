using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyTask.Application.Models.Vms.PersonelVMs
{
    public class RegisterVM
    {
        public IdentityResult Result { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
