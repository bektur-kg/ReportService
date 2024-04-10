namespace FonTech.Domain.Results;

public class BaseResult
{
    public bool IsSuccess => ErrorMessage == null;
    public string ErrorMessage { get; set; }
    public int? ErrorCode { get; set; }
}

public class BaseResult<T> : BaseResult
{
    public T Data { get; set; }

    public BaseResult(string errorMessage, int errorCode, T data)
    {
        Data = data;
        ErrorMessage = errorMessage;
        ErrorCode = errorCode;
    }

    public BaseResult() { }
}