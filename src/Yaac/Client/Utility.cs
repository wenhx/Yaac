using System.Diagnostics;
using Yaac.Shared;

namespace Yaac.Client;

internal static class Utility
{
    /// <summary>
    /// The Debug.WriteLine method isn't working.
    /// </summary>
    /// <param name="text"></param>
    [Conditional("DEBUG")]
    public static void ConsoleDebug(string text)
    {
        Console.WriteLine(text);
    }
}
