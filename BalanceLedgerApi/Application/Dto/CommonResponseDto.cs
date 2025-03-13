namespace BalanceLedgerApi.Application.Dto
{
    public class CommonResponseDto<T>
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }
        public string? Error { get; set; }

        public static CommonResponseDto<T> SuccessResponse(T data, string message = "Operation successful.")
            => new()
            {
                Success = true,
                Message = message,
                Data = data
            };

        public static CommonResponseDto<T> ErrorResponse(string error, string message = "Operation failed.")
             => new()
             {
                 Success = false,
                 Message = message,
                 Error = error
             };
    }
}
