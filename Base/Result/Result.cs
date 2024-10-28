namespace DevUtility.Base
{
    public class Result
    {
        public string? Code { get; set; }
        public string? Message { get; set; }
        public Exception? Ex { get; set; }

        public Result(string? code, string? msg, Exception? ex = null)
        {
            Code = code;
            Message = msg;
            Ex = ex;
        }
        public Result()
        {
        }

        public static Task<Result> Ok()
        {
            return Task.FromResult(new Result("00", "Ok"));
        }

        public static Task<Result> Error(string? code, string? msg)
        {
            return Task.FromResult(new Result(code, msg));
        }

        public static Task<Result> Exception(string? msg, Exception? ex)
        {
            return Task.FromResult(new Result("99", msg, ex));
        }

        public bool IsOk()
        {
            return Code == "00";
        }

        public bool IsError()
        {
            return Code != "00";
        }
    }

    public class Result<T>
    {
        public string? Code { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }
        public Exception? Ex { get; set; }

        public Result()
        {
        }

        public Result(string? code, string? msg, T? data = default, Exception? ex = null)
        {
            Code = code;
            Message = msg;
            Data = data;
            Ex = ex;
        }

        public static Task<Result<T>> Ok(T data)
        {
            return Task.FromResult(new Result<T>("00", "Ok", data));
        }

        public static Task<Result<T>> Error(string? code, string? msg)
        {
            return Task.FromResult(new Result<T>(code, msg));
        }

        public static Task<Result<T>> Exception(string? msg, Exception? ex)
        {
            return Task.FromResult(new Result<T>("99", msg, default, ex));
        }

        public bool IsOk()
        {
            return Code == "00";
        }

        public bool IsException()
        {
            return Code == "99";
        }
        public bool IsError()
        {
            return !IsOk() && !IsException();
        }
    }
}