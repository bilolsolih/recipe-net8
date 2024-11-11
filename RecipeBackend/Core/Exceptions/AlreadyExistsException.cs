namespace RecipeBackend.Core.Exceptions;

public class AlreadyExistsException(string message) : Exception(message)
{
}