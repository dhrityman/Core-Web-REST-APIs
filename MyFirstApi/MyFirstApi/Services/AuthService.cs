using Microsoft.AspNetCore.Server.Kestrel.Transport.NamedPipes;
using Microsoft.EntityFrameworkCore;
using MyFirstApi.Data;
using MyFirstApi.Dto;
using MyFirstApi.IService;

namespace MyFirstApi.Services
{
    public class AuthService :IAuthService
    {
        /// <summary>
        /// To use Use Dependaency Injection.
        /// </summary>
        private readonly AppDBContext _context;

        /// <summary>
        /// Use Dependaency Injection by using AppDBContext through constructor.
        /// </summary>
        /// <param name="context"></param>
        public AuthService (AppDBContext context)
        {
            _context = context;
        }

        public async Task<Tuple<int,string>> LoginUser(UserDto dto)
        {
            try
            {
                /// To check the passed email is exist. 
                var existingUser = await _context.AccountUser.FirstOrDefaultAsync(x =>x.Email == dto.Email);
                if(existingUser == null)
                {
                    return new Tuple<int,string>(0, "This user not exist, Please login");
                }
                /// To check the passed password is exist. 
                if (existingUser.Password != dto.Password)
                {
                    return new Tuple<int, string>(1, "Incorrect Password");
                }
                return new Tuple<int, string>(2, "Login Sucessfull");

            }
            catch (Exception ex)
            {
                return new Tuple<int, string>(3, "Technical error, Something went Wrong");
            }           
        }
    }
}
