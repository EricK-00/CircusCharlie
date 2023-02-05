using UnityEngine;

public static partial class Functions
{
    #region Print log functions
    [System.Diagnostics.Conditional("DEBUG_MODE")]
    public static void Log(object message)
    {
#if DEBUG_MODE
        Debug.Log(message);
#endif
    }

    [System.Diagnostics.Conditional("DEBUG_MODE")]
    public static void Log(object message, Object context)
    {
#if DEBUG_MODE
        Debug.Log(message, context);
#endif
    }
    #endregion

    #region Assert for debug
    [System.Diagnostics.Conditional("DEBUG_MODE")]
    public static void Assert(bool condition)
    {
#if DEBUG_MODE
        Debug.Assert(condition);
#endif
    }

    [System.Diagnostics.Conditional("DEBUG_MODE")]
    public static void Assert(bool condition, object message)
    {
#if DEBUG_MODE
        Debug.Assert(condition, message);
#endif
    }
    #endregion

    #region LogWarning
    [System.Diagnostics.Conditional("DEBUG_MODE")]
    public static void LogWarning(object message)
    {
#if DEBUG_MODE
        Debug.LogWarning(message);
#endif
    }
    #endregion

    #region Valid Function
    public static bool IsValid<T>(this T refType)
    {
        bool isValid = !refType.Equals(null);

        return isValid;
    }
    #endregion
}