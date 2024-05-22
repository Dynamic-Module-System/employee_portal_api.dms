public class RequestResult<T>
{
    public bool IsSuccess { get; private set; }
    public bool IsError { get; private set; }
    public IEnumerable<string> Errors { get; private set; }
    public T Result { get; private set; }

    private RequestResult(bool isSuccess, bool isError, IEnumerable<string> errors, T data)
    {
        IsSuccess = isSuccess;
        //IsError = isError;
        Errors = errors;
        Result = data;
    }

    public static RequestResult<T> CreateSuccessful(T data)
    {
        return new RequestResult<T>(true, false, Enumerable.Empty<string>(), data);
    }

    public static RequestResult<T> CreateUnsuccessful(IEnumerable<string> errors)
    {
        return new RequestResult<T>(false, true, errors, default);
    }
}
