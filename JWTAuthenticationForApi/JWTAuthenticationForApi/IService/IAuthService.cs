using JWTAuthenticationForApi.Dto;

namespace JWTAuthenticationForApi.IService
{
    /// <summary>
    /// For the Abstraction of the Authentication Service, we can create an interface IAuthService. 
    /// This interface will define the contract for authentication-related operations, such as user login, registration, and token generation. By using an interface, 
    /// we can easily swap out different implementations of the authentication service without changing the code that depends on it.
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// Check Login Details for a Logged-In user.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<Tuple<int, TokenDto>> LoginUser(UserDto dto);

        /// <summary>
        /// To Create a new User
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<Tuple<int, string>> RegisterUser(UserDto dto);
    }
}
