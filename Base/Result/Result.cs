namespace DevUtility.Base
{
    public class Result
    {
        public string Code { get; }
        public string Message { get; }
        public string? ErrorContent { get; }

        public Result(string code, string message, string? errorContent = null)
        {
            Code = code;
            Message = message;
            ErrorContent = errorContent;
        }

        public static Result OK()
        {
            return new Result("00", "Ok");
        }

        public static Result Error(string code, string msg, string? errorContent = null)
        {
            return new Result(code, msg, errorContent);
        }

        public static Result Exception(string msg, Exception ex)
        {
            return new Result("99", msg, ex.ToString());
        }

        public bool IsOk()
        {
            return Code == "00";
        }

        public static Result<T> Ok<T>(T data)
        {
            return new Result<T>("00", "Ok", data);
        }

        public static Result<T> Error<T>(string code, string message, string? errorContent = null)
        {
            return new Result<T>(code, message, default, errorContent);
        }

        public static Result<T> Exception<T>(string message, Exception ex)
        {
            return new Result<T>("99", message, default, ex.ToString());
        }
    }
}