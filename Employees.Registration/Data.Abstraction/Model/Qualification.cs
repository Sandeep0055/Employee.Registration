
using Data.Abstraction.Model;
using Data.Abstraction.Models;
using System.Collections.Generic;

namespace Data.Abstraction.Models
{
    public class Qualification: IModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public virtual IReadOnlyCollection<Employee> Employees { get; set; }

    }
}
