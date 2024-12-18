namespace RecipeBackend.Core.Exceptions;

public class AlreadyExistsException(string message) : Exception(message)
{
    public static void ThrowIf(bool isTrue, string message)
    {
        if (isTrue)
        {
            throw new AlreadyExistsException(message);
        }
    }
}