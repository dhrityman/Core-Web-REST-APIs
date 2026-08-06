using JWTAuthenticationForApi.Entities;

namespace JWTAuthenticationForApi.Dto
{
    public class UserDto
    {
        public UserDto()
        {
            
        }

        public UserDto(User user)
        {
            this.Id=user.Id;
            this.Name=user.Name;
            this.Email=user.Email;
            this.Password=user.Password;
        }

        public Guid Id { get; set; } = Guid.NewGuid();
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
