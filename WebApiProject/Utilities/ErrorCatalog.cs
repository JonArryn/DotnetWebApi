namespace WebApiProject.Utilities;

public class ErrorCatalog
{
    // Error code structure: 
    // A: Authentication/Authorization
    // B: Business logic
    // V: Validation
    // S: System/Server errors

    private static readonly Dictionary<string, string> _errorDescriptions = new Dictionary<string, string>
    {
        // Authentication errors (A-series)
        ["A0001"] = "Tried to register a user that already exists",
        ["A0002"] = "Invalid login credentials provided",
        ["A0003"] = "User account is locked",
        ["A0004"] = "Generic auth error",

        // Business logic errors (B-series)
        ["B0001"] = "Generic not found error",
        ["B0002"] = "Household not found",
        ["B0003"] = "User is not a member of the specified household",
        ["B0004"] = "Generic Bad Request",

        // Validation errors (V-series)
        ["V0001"] = "Required field missing",
        ["V0002"] = "Invalid email format",
        ["V0003"] = "Generic validation error",

        // System errors (S-series)
        ["S0001"] = "Database connection failed",
        ["S0002"] = "External service unavailable",
        ["S0003"] = "Generic system error"
    };

    // Public methods to access error information
    public static string GetDescription(string errorCode)
    {
        return _errorDescriptions.TryGetValue(errorCode, out var description)
            ? description
            : "Unknown error";
    }

    // Public error code constants
    public static class AuthErrorCodes
    {
        public const string USER_ALREADY_EXISTS = "A0001";
        public const string INVALID_CREDENTIALS = "A0002";
        public const string ACCOUNT_LOCKED = "A0003";
        public const string GENERIC_AUTH_ERROR = "A0004";
    }

    public static class BusinessErrorCodes
    {
        public const string HOUSEHOLD_NOT_FOUND = "B0002";
        public const string NOT_HOUSEHOLD_MEMBER = "B0003";
        public const string GENERIC_NOT_FOUND = "B0001";
        public const string GENERIC_BAD_REQUEST = "B0004";
        public const string DUPLICATE_ENTITY = "B0005";
    }

    public static class ValidationErrorCodes
    {
        public const string REQUIRED_FIELD = "V0001";
        public const string INVALID_EMAIL = "V0002";
        public const string GENERIC_VALIDATION_ERROR = "V0003";
    }

    public static class SystemErrorCodes
    {
        public const string DATABASE_CONNECTION_FAIL = "S0001";
        public const string EXTERNAL_SERVICE_DOWN = "S0002";
        public const string GENERIC_SYSTEM_ERROR = "S0003";
    }
}