using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using RecipeBackend.Core.Exceptions;

namespace RecipeBackend.Core;

public class CoreExceptionsFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is AlreadyExistsException)
        {
            context.Result = new ObjectResult($"You are entering a duplicate value for a unique column. {context.Exception.Message}")
            {
                StatusCode = 409
            };

            context.ExceptionHandled = true;
        }
        else if (context.Exception is DoesNotExistException)
        {
            context.Result = new ObjectResult($"Object with the given credentials does not exist. {context.Exception.Message}")
            {
                StatusCode = 404
            };
        }
        else if (context.Exception is InvalidFileException)
        {
            context.Result = new ObjectResult($"Problem with the uploaded file. {context.Exception.Message}")
            {
                StatusCode = 400
            };
        }
    }
}