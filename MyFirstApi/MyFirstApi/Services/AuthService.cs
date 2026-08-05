using Microsoft.AspNetCore.Identity;
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
        /// Use Dependaency Injection at contructor level by using AppDBContext through constructor.
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
                // To Check incomplete data
                if(dto ==null)
                {
                    return new Tuple<int, string>(1, "Please fill all the details");
                }
                /// To check the passed email is exist. 
                var existingUser = await _context.AccountUser.FirstOrDefaultAsync(x =>x.Email == dto.Email);
                if(existingUser == null)
                {
                    return new Tuple<int,string>(0, "This user not exist, Please login");
                }
                /// To check the passed password is exist. 
                var passwordhasher = new PasswordHasher<string>();
                var verifyPassword = passwordhasher.VerifyHashedPassword(dto.Email, existingUser.Password, dto.Password);

                if (verifyPassword == PasswordVerificationResult.Success)
                {
                    return new Tuple<int, string>(2, "Login Sucessfull");
                }
                else if (verifyPassword == PasswordVerificationResult.SuccessRehashNeeded)
                {
                    existingUser.Password = PasswordHashing(dto);
                    _context.AccountUser.Update(existingUser);
                    _context.SaveChanges();
                    return new Tuple<int, string>(2, "Login Sucessfull with hash generated");
                }
                else if (verifyPassword == PasswordVerificationResult.Failed)
                {
                    return new Tuple<int, string>(1, "Password Incorrect");
                }
                return new Tuple<int, string>(2, "");

            }
            catch (Exception ex)
            {
                return new Tuple<int, string>(3, "Technical error, Something went Wrong");
            }           
        }

        public async Task<Tuple<int, string>> RegisterUser(UserDto dto)
        {
            try
            {
                var existingUser = await _context.AccountUser.AnyAsync(x => x.Email == dto.Email);
                
                if (existingUser)
                {
                    return new Tuple<int, string>(0, "This user already exist, Please Register with new user detail");
                }

                _context.AccountUser.Add(new Entities.User
                {
                    Id = Guid.NewGuid(),
                    Email = dto.Email,
                    Name = dto.Name,
                    Password = this.PasswordHashing(dto)
                });
                await _context.SaveChangesAsync();

                return new Tuple<int, string>(1, "User register sucessfully");
            }
            catch(Exception ex)
            {
                return new Tuple<int, string>(2, "Technical error, Something went Wrong");
            }
        }

        /// <summary>
        /// To encrypted the password to same in database
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        private string PasswordHashing(UserDto dto)
        {
            var passwordHasher= new PasswordHasher<string>();
            var hash= passwordHasher.HashPassword(dto.Email,dto.Password);
            return hash;
        }
    }
}
