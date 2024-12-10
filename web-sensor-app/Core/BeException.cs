using System;

namespace web_sensor_app.Core;
public class BeException : Exception
{
    public BeException(string userMessage, int errorCode, string? userMessageCode = null, string? message = null, Exception? innerException = null, string? innerServiceException = null) : base(message)
    {
        UserMessage = userMessage;
        ErrorCode = errorCode;
        UserMessageCode = userMessageCode;
        InnerServiceException = innerServiceException;
    }

    public string UserMessage { get; set; }
    public int ErrorCode { get; set; }
    public string? UserMessageCode { get; set; }
    public string? InnerServiceException { get; set; }
}