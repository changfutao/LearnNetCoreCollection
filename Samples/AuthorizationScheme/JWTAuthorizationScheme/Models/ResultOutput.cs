namespace JWTAuthorizationScheme.Models
{
    public interface IResultOutput
    {
        bool Success { get; }
        string? Msg { get; }
    }

    public interface IResultOutput<T>: IResultOutput
    {
        T? Data { get; }
    }

    public class ResultOutput<T> : IResultOutput<T>
    {
        public T? Data { get; set; }

        public bool Success { get; set; }

        public string? Msg { get; set; }
    }

    public class ResultOutput
    {
        public static IResultOutput Ok<T>(T data, string? msg = null)
        {
            return new ResultOutput<T>
            {
                Data = data,
                Success = true,
                Msg = msg
            };
        }

        public static IResultOutput NotOk<T>(string msg = "", T data = default(T))
        {
            return new ResultOutput<T>
            {
                Data = data,
                Success = false,
                Msg = msg
            };
        }
    }
}
