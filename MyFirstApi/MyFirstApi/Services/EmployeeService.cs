using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.Identity.Client;
using MyFirstApi.Data;
using MyFirstApi.Dto;
using MyFirstApi.Entities;
using MyFirstApi.IService;

namespace MyFirstApi.Services
{
    /// <summary>
    /// This Service is used for to handle Employee details.
    /// Use Dependency Injection at class level by using (AppDBContext context)
    /// </summary>
    /// <param name="context"></param>
    public class EmployeeService(AppDBContext _context) : IEmployeeService
    {

        public async Task<Tuple<int, List<EmployeeDto>>> GetAllEmployeeAsyn()
        {
            try
            {
                /*Use Projection by using select for employees to convert into EmployeeDto
                 * AsNoTracking():=> Make the select very fast, it will not track Entity level validation. 
                 *                   It is only use at the time of data read, not use for create, update and delete.
                 * */
                return new Tuple<int, List<EmployeeDto>>(1, await _context.Employees.AsNoTracking().Select(x => new EmployeeDto(x)).ToListAsync());
                //return new Tuple<int, List<EmployeeDto>>(1, await _context.Employees.Select(x => new EmployeeDto(x)).ToListAsync());


                // Both the way you can do
                //return new Tuple<int, List<EmployeeDto>>(1, await _context.Employees.Select(x => new EmployeeDto {
                //    Id = x.Id,
                //    Name = x.Name,
                //    CreatedDate = x.CreatedDate,
                //    LastModifieddDate = x.LastModifieddDate,
                //    DOB = x.DOB,
                //    Position = x.Position,
                //    Department = x.Department,
                //    EmailAddress = x.EmailAddress
                //}).ToListAsync());         

            }
            catch (Exception ex)
            {
                return new Tuple<int, List<EmployeeDto>>(2, null);
            }

        }

        /// <summary>
        /// Create Employee
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public async Task<Tuple<int, string>> CreateEmployee(EmployeeDto employee)
        {
            try
            {
                var existing = await _context.Employees.AnyAsync(x => x.EmailAddress == employee.EmailAddress);
                if (existing)
                {
                    return new Tuple<int, string>(0, "Employee already exist with same Email address");
                }
                await _context.Employees.AddAsync(new Employee
                {
                    Id = Guid.NewGuid(),
                    CreatedDate = DateTime.Now,
                    LastModifieddDate = null,
                    Department = employee.Department,
                    DOB = employee.DOB,
                    Name = employee.Name,
                    EmailAddress = employee.EmailAddress,
                    Position = employee.Position
                });

                await _context.SaveChangesAsync();

                return new Tuple<int, string>(1, employee.Name + ",  Employee created Sucessfully");
            }
            catch (Exception ex)
            {
                return new Tuple<int, string>(0, employee.Name + ",  Employee not created, due to some technical error");
            }
        }


        /// <summary>
        /// Update Employee
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public async Task<Tuple<int, string>> UpdateEmployee(EmployeeDto employee)
        {
            try
            {
                if (employee == null)
                {
                    return new Tuple<int, string>(0, "Please fill all the required Employee details, to update employee");
                }
                var existing = await _context.Employees.FirstOrDefaultAsync(x => x.EmailAddress == employee.EmailAddress);
                if (existing == null)
                {
                    return new Tuple<int, string>(0, "Employee not exist with the given Employee Email address");
                }

                //Check the Null value by using '??' in the employee property (Which is send through API)
                existing.LastModifieddDate = DateTime.Now;
                existing.Department = employee.Department ?? existing.Department;
                existing.DOB = employee.DOB ?? existing.DOB;
                existing.Name = employee.Name ?? existing.Name;
                existing.EmailAddress = employee.EmailAddress ?? existing.EmailAddress;
                existing.Position = employee.Position ?? existing.Position;

                _context.Employees.Update(existing);
                await _context.SaveChangesAsync();

                return new Tuple<int, string>(1, employee.Name + ",  Employee updated Sucessfully");
            }
            catch (Exception ex)
            {
                return new Tuple<int, string>(0, employee.Name + ",  Employee not updated, due to some technical error");
            }
        }

        public async Task<Tuple<int, string>> DeleteEmployee(Guid Id)
        {

            try
            {

                var existing = await _context.Employees.FirstOrDefaultAsync(x => x.Id == Id);
                if (existing == null)
                {
                    return new Tuple<int, string>(0, "Employee Id not exist, Please provide valid employee Id to delete a employee");
                }


                _context.Employees.Remove(existing);
                await _context.SaveChangesAsync();

                return new Tuple<int, string>(1, existing.Name + ",  Employee deleted Sucessfully");
            }
            catch (Exception ex)
            {
                return new Tuple<int, string>(0, "Employee not deleted, due to some technical error");
            }

        }


        public async Task<Tuple<int, EmployeeDto>> GetEmployeeById(Guid Id)
        {
            try
            {
                var existing = await _context.Employees.FirstOrDefaultAsync(x => x.Id == Id);
                if (existing == null)
                {
                    return new Tuple<int, EmployeeDto>(0, null);
                }

                return new Tuple<int, EmployeeDto>(1, new EmployeeDto(existing));
                
            }
            catch (Exception ex)
            {
                return new Tuple<int, EmployeeDto>(0, null);
            }

        }
    }
}
