namespace RecipeBackend.Core.Exceptions;

public class DoesNotExistException(string message) : Exception(message)
{
    public static void ThrowIfNull(object? obj, string message)
    {
        if (obj == null)
        {
            throw new DoesNotExistException(message);
        }
    }
}