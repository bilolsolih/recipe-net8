using System.Diagnostics.CodeAnalysis;

namespace RecipeBackend.Core.Exceptions;

public class DoesNotExistException(string message) : Exception(message)
{
    public static void ThrowIfNull([NotNull] object? obj, string message)
    {
        if (obj == null)
        {
            throw new DoesNotExistException(message);
        }
    }
    
    public static void ThrowIf(bool value, string message)
    {
        if (value)
        {
            throw new DoesNotExistException(message);
        }
    }

    public static void ThrowIfNot(bool value, string message)
    {
        if (!value)
        {
            throw new DoesNotExistException(message);
        }
    }
}