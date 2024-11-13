﻿using System.Diagnostics.CodeAnalysis;

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
}