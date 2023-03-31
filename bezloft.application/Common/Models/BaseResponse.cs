using System.Diagnostics;

namespace bezloft.application.Common.Models;


public class BaseResponse
{
    public bool Status { get; set; }

    public string Message { get; set; }

    public string TraceId => Activity.Current?.TraceId.ToString();

    public BaseResponse()
        : this(true, string.Empty)
    {
    }

    public BaseResponse(bool status, string message)
    {
        Status = status;
        Message = message;
    }
}
public class BaseResponse<T>
{
    public bool Status { get; set; }

    public string Message { get; set; }

    public string TraceId => Activity.Current?.TraceId.ToString();

    public T Data { get; set; }

    public BaseResponse()
        : this(true, string.Empty)
    {
    }

    public BaseResponse(bool status, string message)
    {
        Status = status;
        Message = message;
    }

    public BaseResponse(bool status, string message, T data)
    {
        Status = status;
        Message = message;
        Data = data;
    }
}