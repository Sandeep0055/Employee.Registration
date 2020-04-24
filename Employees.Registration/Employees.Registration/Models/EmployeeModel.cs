using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employees.Registration.Models
{
    public class EmployeeModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string QualificationId { get; set; }
        public int? Experience { get; set; }
        public DateTime? JoiningDate { get; set; }

        public int? Salary { get; set; }
        public string Designation { get; set; }
        public string Hobbyies { get; set; }
        public string Qualification { get; set; }

    }
}
