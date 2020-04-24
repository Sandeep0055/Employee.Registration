


using Data.Abstraction.Models;
using System;

namespace Data.Abstraction.Model
{
    public class Employee : IModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public long QualificationId { get; set; }
        public int? Experience { get; set; }
        public DateTime? JoiningDate { get; set; }

        public int? Salary { get; set; }
        public string Designation { get; set; }
        public string Hobbyies { get; set; }
        public virtual Qualification Qualification { get; set; }


    }
}
