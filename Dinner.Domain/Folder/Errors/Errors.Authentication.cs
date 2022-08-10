namespace Dinner.Domain.Folder.Errors;

using ErrorOr;

public static partial class Errors
{
    public static class Authentication
    {
        public static Error InvalidCredentials => Error.Validation(
            code: "Auth.InvalidCredentials",
            description: "Invalid credentials");
    }
}