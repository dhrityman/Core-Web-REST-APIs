using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using JWTAuthenticationForApi.Dto;
using JWTAuthenticationForApi.GenericResponse;
using JWTAuthenticationForApi.IService;

namespace JWTAuthenticationForApi.Controllers
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
                    return NotFound(ResponseResult<TokenDto>.Failure(result.Item2, result.Item2.Message));

                }
                //This is the case of  "To Check incomplete data".
                if (result.Item1 == 1)
                {

                    //return BadRequest(result.Item2);
                    return BadRequest(ResponseResult<TokenDto>.Failure(result.Item2, result.Item2.Message));
                }
                //This is the case of  "Login Sucessfull".
                //return Ok(result.Item2);
                return Ok(ResponseResult<TokenDto>.Sucess(result.Item2, result.Item2.Message));

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
