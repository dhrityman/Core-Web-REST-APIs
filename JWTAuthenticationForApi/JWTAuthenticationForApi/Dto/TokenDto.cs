namespace JWTAuthenticationForApi.Dto
{
    /// <summary>
    /// Step 44: Add TokenDto class in Dto Folder.
    /// </summary>
    public class TokenDto
    {
        public TokenDto()
        {
            
        }

        public TokenDto(string token, string message)
        {
            this.Token = token;
            this.Message= message;
        }
        public string Token { get; set; }
        public string Message { get; set; }
    }
}
