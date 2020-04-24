using Data.Abstraction.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Sqlite.Mappings
{
   public class EmployeeMapping : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();
           

            builder.Property(x => x.Name);
            builder.Property(x => x.DateOfBirth);
            builder.Property(x => x.Experience);
            builder.Property(x => x.JoiningDate);
            builder.Property(x => x.Salary);
            builder.Property(x => x.Designation); 
            builder.Property(x => x.Hobbyies);
            builder.Property(x => x.QualificationId);
            builder.HasOne(x => x.Qualification)
               .WithMany(x => x.Employees)
               .HasForeignKey(x => x.QualificationId)
               .OnDelete(DeleteBehavior.Restrict);
            builder.ToTable("Employees");
        }
    }
}
