using MyFirstApi.Dto;

namespace MyFirstApi.IService
{
    public interface IEmployeeService
    {
        Task<Tuple<int, List<EmployeeDto>>> GetAllEmployeeAsyn();

        Task<Tuple<int, string>> CreateEmployee(EmployeeDto employee);

        Task<Tuple<int, string>> UpdateEmployee(EmployeeDto employee);

        Task<Tuple<int, string>> DeleteEmployee(Guid Id);
    }
}
