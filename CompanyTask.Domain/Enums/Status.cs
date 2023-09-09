using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CompanyTask.Domain.Enums
{
    public enum Status
    {
        [Display(Name = "Active")]
        Active = 1,
        [Display(Name = "Passive")]
        Passive,
        [Display(Name = "Deleted")]
        Deleted
    }
}
