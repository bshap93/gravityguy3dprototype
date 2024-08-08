using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ConsoleManager : MonoBehaviour
{
    [SerializeField] private Text consoleText;
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private Toggle actionToggle;
    [SerializeField] private Toggle envToggle;
    [SerializeField] private Toggle systemToggle;

    private List<ConsoleEntry> entries = new List<ConsoleEntry>();
    private int maxEntries = 100;

    private void Start()
    {
        if (actionToggle != null)
            actionToggle.onValueChanged.AddListener((_) => UpdateConsoleText());

        if (envToggle != null)
            envToggle.onValueChanged.AddListener((_) => UpdateConsoleText());

        if (systemToggle != null)
            systemToggle.onValueChanged.AddListener((_) => UpdateConsoleText());
    }

    public void AddEntry(string message, EntryType type)
    {
        entries.Add(new ConsoleEntry(message, type));
        if (entries.Count > maxEntries)
            entries.RemoveAt(0);

        UpdateConsoleText();
    }

    private void UpdateConsoleText()
    {
        consoleText.text = "";
        foreach (var entry in entries)
        {
            if ((entry.Type == EntryType.Action && actionToggle.isOn) ||
                (entry.Type == EntryType.Environmental && envToggle.isOn) ||
                (entry.Type == EntryType.System && systemToggle.isOn))
            {
                consoleText.text += FormatEntry(entry) + "\n";
            }
        }

        // Scroll to bottom
        Canvas.ForceUpdateCanvases();
        scrollRect.verticalNormalizedPosition = 0f;
    }

    private string FormatEntry(ConsoleEntry entry)
    {
        string colorTag = entry.Type switch
        {
            EntryType.Action => "blue",
            EntryType.Environmental => "green",
            EntryType.System => "red",
            _ => "white"
        };

        return $"<color={colorTag}>[{entry.Timestamp:HH:mm:ss}] [{entry.Type}] {entry.Message}</color>";
    }
    public void AddLine(string s)
    {
        AddEntry(s, EntryType.System);
    }
}

public enum EntryType
{
    Action,
    Environmental,
    System
}

public class ConsoleEntry
{
    public string Message { get; set; }
    public EntryType Type { get; set; }
    public System.DateTime Timestamp { get; set; }

    public ConsoleEntry(string message, EntryType type)
    {
        Message = message;
        Type = type;
        Timestamp = System.DateTime.Now;
    }
}
