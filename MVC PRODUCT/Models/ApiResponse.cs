namespace MVC_PRODUCT.Models
{
    public class ApiResponse<T>
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public List<string> Errors { get; set; }

        public static ApiResponse<T>Success(T data,string Message = "Success")
        {
            return new ApiResponse<T>
            {
                IsSuccess = true,
                Message = Message,
                Data = data,
                Errors = null
            };
        }

        public static ApiResponse<T>Fail(List<string>Errors, string Message="Validation Failed")
        {
            return new ApiResponse<T>
            {
                IsSuccess = false,
                Data = default,
                Message = Message,
                Errors = Errors

            };
           
        }
      
    }
}
