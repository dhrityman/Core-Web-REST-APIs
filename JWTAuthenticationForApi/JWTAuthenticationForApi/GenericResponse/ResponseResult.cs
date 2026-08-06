namespace JWTAuthenticationForApi.GenericResponse
{
    /// <summary>
    /// This class is use to send service response in a proper JSON format.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ResponseResult<T>
    {
        public T? Data { get; set; }
        public string? Message { get; set; }
        public bool Status { get; set; } = false;

        public static ResponseResult<T> Sucess(T? data, string message)
        {
            return new ResponseResult<T>
            {
                Data = data,
                Message = message,
                Status = true
            };
        }

        public static ResponseResult<T> Failure(T? data, string message)
        {
            return new ResponseResult<T>
            {
                Data = data,
                Message = message,
                Status = false
            };
        }
    }
}
