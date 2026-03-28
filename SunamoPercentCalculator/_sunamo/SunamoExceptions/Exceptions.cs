namespace SunamoPercentCalculator._sunamo.SunamoExceptions;

internal sealed partial class Exceptions
{
    /// <summary>
    /// Prepares a prefix string for exception messages.
    /// </summary>
    /// <param name="prefix">The prefix to prepend to the exception message.</param>
    /// <returns>The formatted prefix or empty string if null/whitespace.</returns>
    internal static string CheckBefore(string prefix)
    {
        return string.IsNullOrWhiteSpace(prefix) ? string.Empty : prefix + ": ";
    }

    /// <summary>
    /// Gets the place of exception from the current stack trace.
    /// </summary>
    /// <param name="isFillAlsoFirstTwo">Whether to also fill the type and method name from the first non-ThrowEx frame.</param>
    /// <returns>A tuple containing type name, method name, and stack trace text.</returns>
    internal static Tuple<string, string, string> PlaceOfException(bool isFillAlsoFirstTwo = true)
    {
        StackTrace stackTrace = new();
        var stackTraceText = stackTrace.ToString();
        var lines = stackTraceText.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToList();
        lines.RemoveAt(0);
        var i = 0;
        string typeName = string.Empty;
        string methodName = string.Empty;
        for (; i < lines.Count; i++)
        {
            var line = lines[i];
            if (isFillAlsoFirstTwo)
                if (!line.StartsWith("   at ThrowEx"))
                {
                    TypeAndMethodName(line, out typeName, out methodName);
                    isFillAlsoFirstTwo = false;
                }
            if (line.StartsWith("at System."))
            {
                lines.Add(string.Empty);
                lines.Add(string.Empty);
                break;
            }
        }
        return new Tuple<string, string, string>(typeName, methodName, string.Join(Environment.NewLine, lines));
    }

    /// <summary>
    /// Extracts type and method name from a stack trace line.
    /// </summary>
    /// <param name="stackFrameLine">A single line from the stack trace.</param>
    /// <param name="typeName">The extracted type name.</param>
    /// <param name="methodName">The extracted method name.</param>
    internal static void TypeAndMethodName(string stackFrameLine, out string typeName, out string methodName)
    {
        var trimmedText = stackFrameLine.Split("at ")[1].Trim();
        var methodFullPath = trimmedText.Split('(')[0];
        var parts = methodFullPath.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries).ToList();
        methodName = parts[^1];
        parts.RemoveAt(parts.Count - 1);
        typeName = string.Join(".", parts);
    }

    /// <summary>
    /// Gets the name of the calling method at the specified stack frame depth.
    /// </summary>
    /// <param name="depth">The stack frame depth to retrieve the method name from.</param>
    /// <returns>The name of the calling method.</returns>
    internal static string CallingMethod(int depth = 1)
    {
        StackTrace stackTrace = new();
        var methodBase = stackTrace.GetFrame(depth)?.GetMethod();
        if (methodBase == null)
        {
            return "Method name cannot be get";
        }
        var methodName = methodBase.Name;
        return methodName;
    }

    /// <summary>
    /// Creates a divide by zero error message with an optional prefix.
    /// </summary>
    /// <param name="prefix">The prefix to prepend to the error message.</param>
    /// <returns>The formatted error message.</returns>
    internal static string? DivideByZero(string prefix)
    {
        return CheckBefore(prefix) + " is dividing by zero.";
    }
}
