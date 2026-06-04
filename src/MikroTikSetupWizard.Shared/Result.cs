namespace MikroTikSetupWizard.Shared;

public sealed record Result<T>(bool IsSuccess, T? Value, string Error)
{
    public static Result<T> Success(T value)
    {
        return new Result<T>(true, value, string.Empty);
    }

    public static Result<T> Failure(string error)
    {
        return new Result<T>(false, default, error);
    }
}
