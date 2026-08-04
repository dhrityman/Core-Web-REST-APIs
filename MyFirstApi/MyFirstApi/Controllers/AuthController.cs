using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyFirstApi.Dto;
using MyFirstApi.GenericResponse;
using MyFirstApi.IService;

namespace MyFirstApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService=authService;
        }
        /// <summary>
        /// This methos is used for
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
                //This is the case of  "Incorrect Password".
                if (result.Item1 == 1)
                {

                    //return BadRequest(result.Item2);
                    return NotFound(ResponseResult<string>.Failure(null, result.Item2));
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

    }
}
