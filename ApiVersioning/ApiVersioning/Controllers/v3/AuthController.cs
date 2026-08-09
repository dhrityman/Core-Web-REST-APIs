using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApiVersioning.Dto;
using ApiVersioning.GenericResponse;
using ApiVersioning.IService;
using Asp.Versioning;

//Step 38
namespace ApiVersioning.Controllers.v3
{
    //[Route("api/[controller]")]
    [Route("api/Auth")] //Step 43
    [Route("api/v{version:apiVersion}/[controller]")] //Step 35
    [ApiVersion("3.0")]//Step 39 
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService=authService;
        }
        /// <summary>
        /// This methos is used for Login and validate the details at the time of Login
        /// 
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns></returns>
        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserDto userDto)
        {
            try
            {
                // If Service method (AuthService :IAuthService.LoginUser) is asynchonous, you must use await 
                var result = await _authService.LoginUser(userDto);

                // This is the case of "This user not exist, Please login".
                if (result.Item1 == 0)
                {
                    //return NotFound(result.Item2);
                    //ResponseResult
                    return NotFound(ResponseResult<string>.Failure(null, result.Item2));

                }
                //This is the case of  "To Check incomplete data".
                if (result.Item1 == 1)
                {

                    //return BadRequest(result.Item2);
                    return BadRequest(ResponseResult<string>.Failure(null, result.Item2));
                }
                //This is the case of  "Login Sucessfull".
                //return Ok(result.Item2);
                return Ok(ResponseResult<string>.Sucess(null, result.Item2));

            }
            catch (Exception ex)
            {
                throw;
            }
        }


        /// <summary>
        /// This methos is used for Sign-up or Register and validate the details at the time of Register.
        /// 
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns></returns>
        [HttpPost("Register")]
        public async Task<IActionResult> RegisterUser(UserDto userDto)
        {
            try
            {
                // If Service method (AuthService :IAuthService.LoginUser) is asynchonous, you must use await 
                var result = await _authService.RegisterUser(userDto);

                // This is the case of "This user already exist, Please Register with new user detail".
                if (result.Item1 == 0)
                {
                    //return NotFound(result.Item2);
                    //ResponseResult
                    return BadRequest(ResponseResult<string>.Failure(null, result.Item2));

                }
                //This is the case of  "Technical error, Something went Wrong".
                if (result.Item1 == 2)
                {

                    //return BadRequest(result.Item2);
                    return BadRequest(ResponseResult<string>.Failure(null, result.Item2));
                }
                //This is the case of  "User register sucessfully".
                //return Ok(result.Item2);
                return Ok(ResponseResult<string>.Sucess(null, result.Item2));

            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
