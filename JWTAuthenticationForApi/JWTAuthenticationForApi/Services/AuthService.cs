using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Server.Kestrel.Transport.NamedPipes;
using Microsoft.EntityFrameworkCore;
using JWTAuthenticationForApi.Data;
using JWTAuthenticationForApi.Dto;
using JWTAuthenticationForApi.IService;
using System.Security.Claims;
using System.Security;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

namespace JWTAuthenticationForApi.Services
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

        public async Task<Tuple<int, TokenDto>> LoginUser(UserDto dto)
        {
            var tokenDto = new TokenDto();
            try
            {
                
                // To Check incomplete data
                if (dto ==null)
                {
                    tokenDto = new TokenDto(string.Empty, "Please fill all the details");
                    return new Tuple<int, TokenDto>(1, tokenDto);
                }
                /// To check the passed email is exist. 
                var existingUser = await _context.AccountUser.FirstOrDefaultAsync(x =>x.Email == dto.Email);
                if(existingUser == null)
                {
                    tokenDto = new TokenDto(string.Empty, "This user not exist, Please login");
                    return new Tuple<int, TokenDto>(0, tokenDto);
                }
                /// To check the passed password is exist. 
                var passwordhasher = new PasswordHasher<string>();
                var verifyPassword = passwordhasher.VerifyHashedPassword(dto.Email, existingUser.Password, dto.Password);

                if (verifyPassword == PasswordVerificationResult.Success)
                {
                    //Step 43: Call Method GetJwtToken to get the token
                    var token = this.GetJwtToken(new UserDto(existingUser));
                    tokenDto = new TokenDto(token, "Login Sucessfull");
                    return new Tuple<int, TokenDto>(2, tokenDto);
                }
                else if (verifyPassword == PasswordVerificationResult.SuccessRehashNeeded)
                {
                    existingUser.Password = PasswordHashing(dto);
                    _context.AccountUser.Update(existingUser);
                    _context.SaveChanges();

                    //Step 43: Call Method GetJwtToken to get the token
                    var token = this.GetJwtToken(new UserDto(existingUser));
                    tokenDto = new TokenDto(token, "Login Sucessfull with hash generated");
                    return new Tuple<int, TokenDto>(2, tokenDto);
                }
                else if (verifyPassword == PasswordVerificationResult.Failed)
                {
                    tokenDto = new TokenDto(string.Empty, "Password Incorrect");
                    return new Tuple<int, TokenDto>(1, tokenDto);
                }
                tokenDto = new TokenDto(string.Empty, "");
                return new Tuple<int, TokenDto>(2, tokenDto);

            }
            catch (Exception ex)
            {
                tokenDto = new TokenDto(string.Empty, "Technical error, Something went Wrong");
                return new Tuple<int, TokenDto>(3, tokenDto);
            }           
        }

        #region Generate Claims at the time of Login

        /// <summary>
        /// Step 42: :Creating JWT Token
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        private string GetJwtToken(UserDto dto)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name,dto.Name),
                new Claim(ClaimTypes.Email,dto.Email),
                new Claim(ClaimTypes.NameIdentifier,dto.Id.ToString())
            };

            //Add key from 'JWT:Key' from the file 'appsettings.json'
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("b4454b026c6256e1b6f35d85fb00b2ff868a6b5fb5e6eb6153623a7a67dcee5e"));

            var creds=new SigningCredentials(key,SecurityAlgorithms.HmacSha256);

            /*
             * Creating JWT Token
             */
            var token = new JwtSecurityToken(
                issuer: "dhrityman-client",
                audience: "dhrityman",
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(1),
                signingCredentials :creds
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        #endregion Generate Claims at the time of Login

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
