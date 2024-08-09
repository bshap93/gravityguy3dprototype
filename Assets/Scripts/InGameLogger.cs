using UnityEngine;

public static class GameLogger
{
    public static ConsoleManager consoleManager;

    public static void Initialize(ConsoleManager manager)
    {
        consoleManager = manager;
    }

    public static void Log(string message)
    {
        if (consoleManager != null)
            consoleManager.AddEntry(message, EntryType.System);

        Debug.Log(message);
    }

    public static void LogAction(string action)
    {
        if (consoleManager != null)
            consoleManager.AddEntry($"Action: {action}", EntryType.Action);

        Debug.Log($"Action: {action}");
    }

    public static void LogEnvironmental(string data)
    {
        if (consoleManager != null)
            consoleManager.AddEntry($"Env: {data}", EntryType.Environmental);

        Debug.Log($"Env: {data}");
    }
}
