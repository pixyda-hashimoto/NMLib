using System.Diagnostics;

public static class Logger
{
    [Conditional("DEBUG")]
    public static void Log(object message) {
        UnityEngine.Debug.Log(message);
    }
    [Conditional("DEBUG")]
    public static void Warning(object message) {
        UnityEngine.Debug.LogWarning(message);
    }
    [Conditional("DEBUG")]
    public static void Error(object message) {
        UnityEngine.Debug.LogError(message);
    }
    [Conditional("DEBUG")]
    public static void Assert(bool condition, object message) {
       UnityEngine.Debug.Assert(condition, message);
    }
}

