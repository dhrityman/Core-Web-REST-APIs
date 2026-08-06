using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using JWTAuthenticationForApi.Dto;
using JWTAuthenticationForApi.GenericResponse;
using JWTAuthenticationForApi.IService;
using JWTAuthenticationForApi.Services;
using System.ComponentModel;
using Microsoft.AspNetCore.Authorization;

namespace JWTAuthenticationForApi.Controllers
{
    /// <summary>
    /// [Authorize]:=>Step 41: Authorize all the APIs in EmployeeController.cs
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
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
                    return Ok(ResponseResult<List<EmployeeDto>>.Sucess(result.Item2, "Employee found"));
                }
                else
                {
                    return Ok(ResponseResult<List<EmployeeDto>>.Failure(null, "No Employee Found"));
                }
                if (result.Item1 == 2)
                {
                    return BadRequest("Technical error, Something went Wrong");
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Technical error, Something went Wrong");
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
                return Ok(ResponseResult<string>.Failure(null, "Employee not created, due to some technical error"));
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
                return Ok(ResponseResult<string>.Failure(null, "Employee not updated, due to some technical error"));
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
                return Ok(ResponseResult<string>.Failure(null, "Employee not deleted, due to some technical error"));
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
                    return Ok(ResponseResult<EmployeeDto>.Failure(result.Item2, "Employee not found"));
                }
                else
                {
                    return Ok(ResponseResult<EmployeeDto>.Sucess(result.Item2, "Employee found"));
                }
            }
            catch (Exception ex)
            {
                return Ok(ResponseResult<EmployeeDto>.Failure(null, "Employee not found, due to some technical error"));
            }
        }

    }
}
