namespace SunamoPercentCalculator._sunamo.SunamoExceptions;

internal partial class ThrowEx
{
    internal static bool DivideByZero() { return ThrowIsNotNull(Exceptions.DivideByZero(FullNameOfExecutedCode())); }

    internal static string FullNameOfExecutedCode()
    {
        Tuple<string, string, string> placeOfException = Exceptions.PlaceOfException();
        string fullName = FullNameOfExecutedCode(placeOfException.Item1, placeOfException.Item2, true);
        return fullName;
    }

    static string FullNameOfExecutedCode(object typeSource, string methodName, bool isFromThrowEx = false)
    {
        if (methodName is null)
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

    internal static bool ThrowIsNotNull(string? exception, bool shouldThrow = true)
    {
        if (exception is not null)
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
