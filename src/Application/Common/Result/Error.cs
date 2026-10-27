namespace WorkOMat.Application.Common.Result;

public class ErrorKind
{
    public static readonly ErrorKind NotFound = new ErrorKind("NotFound");
    public static readonly ErrorKind Validation = new ErrorKind("Validation");
    public static readonly ErrorKind Unauthorized = new ErrorKind("Unauthorized");
    public static readonly ErrorKind Forbidden = new ErrorKind("Forbidden");
    public static readonly ErrorKind Conflict = new ErrorKind("Conflict");
    public static readonly ErrorKind InternalServerError = new ErrorKind("InternalServerError");

    public string Name { get; init; }

    private ErrorKind(string name)
    {
        Name = name;
    }

    override public string ToString()
    {
        return Name;
    }
}

public class Error
{
    public ErrorKind Kind { get; init; }
    public string Code { get; init; }
    public string? Message { get; init; }

    public Error(ErrorKind kind, string code, string? message = null)
    {
        Kind = kind;
        Code = code;
        Message = message;
    }
}