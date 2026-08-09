using ApiVersioning.Dto;
using ApiVersioning.GenericResponse;
using ApiVersioning.IService;
using ApiVersioning.Services;
using Asp.Versioning;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
//Step 38
namespace ApiVersioning.Controllers.v2
{
    /// <summary>
    /// 
    /// </summary>
    //[Route("api/[controller]")]
    [Route("api/Employee")] //Step 43
    [Route("api/v{version:apiVersion}/[controller]")] //Step 35
    [ApiVersion("2.0")] //Step 39
    [ApiController]
    public class EmployeeController(IEmployeeService employeeService) : ControllerBase
    {
        [HttpGet("GetAllEmployee")]
        public async Task<IActionResult> GetAllEmployeeAsyn()
        {
            try
            {
                var result = await employeeService.GetAllEmployeeAsyn();
                // Not use Count, becuase it may possible there may be millanse of record, Please check Any() if one recods found it will stop immediatly
                if (result.Item2.Any())
                {
                    //return Ok(  result.Item2);
                    return Ok(ResponseResult<List<EmployeeDto>>.Sucess(result.Item2, "Employee found in V2"));
                }
                else
                {
                    return Ok(ResponseResult<List<EmployeeDto>>.Failure(null, "No Employee Found in V2 Controller"));
                }
                if (result.Item1 == 2)
                {
                    return BadRequest("Technical error, Something went Wrong in V2 Controller");
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Technical error, Something went Wrong in V2 Controller");
            }
        }

        /// <summary>
        /// 
        /// Use 'Model Binder [FromBody]', There are four or five type of Model Binder
        /// </summary>
        /// <param name="employeeDto"></param>
        /// <returns></returns>
        [HttpPost("CreateEmployee")]
        public async Task<IActionResult> CreateEmployee([FromBody]EmployeeDto employeeDto)
        {
            try
            {
                var result = await employeeService.CreateEmployee(employeeDto);
                if (result.Item1 == 0)
                {
                    return Ok(ResponseResult<string>.Failure(null, result.Item2));
                }
                else
                {
                    return Ok(ResponseResult<string>.Sucess(null, result.Item2));
                }
            }
            catch(Exception ex)
            {
                return Ok(ResponseResult<string>.Failure(null, "Employee not created, due to some technical error in V2 Controller"));
            }
        }


        /// <summary>
        /// 
        /// Use 'Model Binder [FromBody]', There are four or five type of Model Binder
        /// </summary>
        /// <param name="employeeDto"></param>
        /// <returns></returns>
        [HttpPut("UpdateEmployee")]
        public async Task<IActionResult> UpdateEmployee([FromBody] EmployeeDto employeeDto)
        {
            try
            {
                var result = await employeeService.UpdateEmployee(employeeDto);
                if (result.Item1 == 0)
                {
                    return Ok(ResponseResult<string>.Failure(null, result.Item2));
                }
                else
                {
                    return Ok(ResponseResult<string>.Sucess(null, result.Item2));
                }
            }
            catch (Exception ex)
            {
                return Ok(ResponseResult<string>.Failure(null, "Employee not updated, due to some technical error in V2 Controller"));
            }
        }

        /// <summary>
        /// 
        /// Use 'Model Binder [FromBody]', There are four or five type of Model Binder
        /// </summary>
        /// <param name="Id"> Employee ID</param>
        /// <returns></returns>
        [HttpDelete("DeleteEmployee")]
        public async Task<IActionResult> DeleteEmployee(Guid Id)
        {
            try
            {
                var result = await employeeService.DeleteEmployee(Id);
                if (result.Item1 == 0)
                {
                    return Ok(ResponseResult<string>.Failure(null, result.Item2));
                }
                else
                {
                    return Ok(ResponseResult<string>.Sucess(null, result.Item2));
                }
            }
            catch (Exception ex)
            {
                return Ok(ResponseResult<string>.Failure(null, "Employee not deleted, due to some technical error in V2 Controller"));
            }
        }

        /// <summary>
        /// Get Employee Detail by Employee Id. 
        /// Decorate with 'Model Binder [FromRoute]' and  [HttpGet("GetEmployeeById/{Id}")]
        /// </summary>
        /// <param name="Id"> Employee ID</param>
        /// <returns></returns>
        [HttpGet("GetEmployeeById/{Id}")]
        public async Task<IActionResult> GetEmployeeById([FromRoute]Guid Id)
        {
            try
            {
                var result = await employeeService.GetEmployeeById(Id);
                if (result.Item1 == 0)
                {
                    return Ok(ResponseResult<EmployeeDto>.Failure(result.Item2, "Employee not found in V2 Controller"));
                }
                else
                {
                    return Ok(ResponseResult<EmployeeDto>.Sucess(result.Item2, "Employee found in V2 Controller"));
                }
            }
            catch (Exception ex)
            {
                return Ok(ResponseResult<EmployeeDto>.Failure(null, "Employee not found, due to some technical error in V2 Controller"));
            }
        }

    }
}
