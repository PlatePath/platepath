namespace PlatePath.API.Singleton;

public enum ErrorCode
{
    OK = 0,
    DefaultError = -1,
    AccessDenied = -1001,
    InvalidParameters = -1003,
    AuthenticationError = -1004,
    DbError = -1005,
    UserAlreadyExists = -1006,
    CreateUserFailed = -1007
}

public static class ErrorCodes
{
    public static Dictionary<int, string> Messages { get; set; } = new Dictionary<int, string>()
    {
        [(int)ErrorCode.OK] = "OK",
        [(int)ErrorCode.DefaultError] = "ERROR: INTERNAL EXCEPTION",
        [(int)ErrorCode.AccessDenied] = "ERROR: ACCESS DENIED",
        [(int)ErrorCode.InvalidParameters] = "ERROR: INVALID PARAMETERS",
        [(int)ErrorCode.AuthenticationError] = "ERROR: AUTHENTICATION ERROR",
        [(int)ErrorCode.DbError] = "ERROR: DATABASE EXCEPTION",
        [(int)ErrorCode.UserAlreadyExists] = "ERROR: USER ALREADY EXISTS",
        [(int)ErrorCode.CreateUserFailed] = "ERROR: CREATING USER FAILED"
    };
}