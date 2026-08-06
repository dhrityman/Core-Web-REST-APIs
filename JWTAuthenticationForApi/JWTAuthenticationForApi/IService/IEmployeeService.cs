using JWTAuthenticationForApi.Dto;

namespace JWTAuthenticationForApi.IService
{
    public interface IEmployeeService
    {
        Task<Tuple<int, List<EmployeeDto>>> GetAllEmployeeAsyn();

        Task<Tuple<int, string>> CreateEmployee(EmployeeDto employee);

        Task<Tuple<int, string>> UpdateEmployee(EmployeeDto employee);

        Task<Tuple<int, string>> DeleteEmployee(Guid Id);

        Task<Tuple<int, EmployeeDto>> GetEmployeeById(Guid Id);
    }
}
