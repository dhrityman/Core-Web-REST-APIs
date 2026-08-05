using MyFirstApi.Entities;
using System.ComponentModel.DataAnnotations;

namespace MyFirstApi.Dto
{
    public class EmployeeDto
    {
        public EmployeeDto()
        {
        }
        public EmployeeDto(Employee employee)
        {
            if (employee != null)
            {
                this.Id = employee.Id;
                this.Name = employee.Name;
                this.CreatedDate = employee.CreatedDate;
                this.LastModifieddDate = employee.LastModifieddDate;
                this.DOB = employee.DOB;
                this.Position = employee.Position;
                this.Department = employee.Department;
                this.EmailAddress = employee.EmailAddress;
            }
        }
        public Guid Id { get; set; }

        public string? Name { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? LastModifieddDate { get; set; }

        public DateOnly? DOB { get; set; }

        public string? Position { get; set; }

        public string? Department { get; set; }

        public string? EmailAddress { get; set; }
    }
}
