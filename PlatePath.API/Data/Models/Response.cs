using PlatePath.API.Singleton;

namespace PlatePath.API.Data.Models;

public record BaseResponse
{
    public BaseResponse() { }
    public BaseResponse(ErrorCode error, string? errorString = null)
    {
        ErrorCode = (int)error;
        Success = ErrorCode is (int)Singleton.ErrorCode.OK;

        if (!string.IsNullOrEmpty(errorString))
        {
            ErrorString = errorString;
        }
        else
        {
            ErrorCodes.Messages.TryGetValue(ErrorCode, out var message);
            ErrorString = message ?? "ERROR: UNPARSABLE ERROR";
        }
    }
    public bool Success { get; set; }
    public int ErrorCode { get; set; }
    public string? ErrorString { get; set; }
};
