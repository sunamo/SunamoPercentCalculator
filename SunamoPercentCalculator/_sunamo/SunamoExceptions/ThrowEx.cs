namespace SunamoPercentCalculator._sunamo.SunamoExceptions;

internal partial class ThrowEx
{
    /// <summary>
    /// Throws a divide by zero exception.
    /// </summary>
    /// <returns>True if the exception was thrown.</returns>
    internal static bool DivideByZero() { return ThrowIsNotNull(Exceptions.DivideByZero(FullNameOfExecutedCode())); }

    /// <summary>
    /// Gets the full name of the currently executed code (type.method).
    /// </summary>
    /// <returns>The full name of the executed code.</returns>
    internal static string FullNameOfExecutedCode()
    {
        Tuple<string, string, string> placeOfException = Exceptions.PlaceOfException();
        string fullName = FullNameOfExecutedCode(placeOfException.Item1, placeOfException.Item2, true);
        return fullName;
    }

    /// <summary>
    /// Gets the full name of the executed code from the specified type and method name.
    /// </summary>
    /// <param name="typeSource">The type source - can be Type, MethodBase, or string.</param>
    /// <param name="methodName">The method name.</param>
    /// <param name="isFromThrowEx">Whether the call originates from ThrowEx, affecting stack depth.</param>
    /// <returns>The full name in format "type.method".</returns>
    static string FullNameOfExecutedCode(object typeSource, string methodName, bool isFromThrowEx = false)
    {
        if (methodName == null)
        {
            int depth = 2;
            if (isFromThrowEx)
            {
                depth++;
            }

            methodName = Exceptions.CallingMethod(depth);
        }
        string typeFullName;
        if (typeSource is Type actualType)
        {
            typeFullName = actualType.FullName ?? "Type cannot be get via type is Type";
        }
        else if (typeSource is MethodBase method)
        {
            typeFullName = method.ReflectedType?.FullName ?? "Type cannot be get via type is MethodBase";
            methodName = method.Name;
        }
        else if (typeSource is string)
        {
            typeFullName = typeSource.ToString() ?? "Type cannot be get via type is string";
        }
        else
        {
            Type resolvedType = typeSource.GetType();
            typeFullName = resolvedType.FullName ?? "Type cannot be get via type.GetType()";
        }
        return string.Concat(typeFullName, ".", methodName);
    }

    /// <summary>
    /// Throws an exception if the exception message is not null.
    /// </summary>
    /// <param name="exception">The exception message to throw.</param>
    /// <param name="shouldThrow">Whether to actually throw the exception or just return true.</param>
    /// <returns>True if the exception message was not null.</returns>
    internal static bool ThrowIsNotNull(string? exception, bool shouldThrow = true)
    {
        if (exception != null)
        {
            Debugger.Break();
            if (shouldThrow)
            {
                throw new Exception(exception);
            }
            return true;
        }
        return false;
    }
}
