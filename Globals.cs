global using static Globals;
global using Spectre.Console;
using JsonFlatFileDataStore;
using System.Runtime.CompilerServices;
// ReSharper disable RedundantAssignment

// ReSharper disable once CheckNamespace
public static class Globals
{
    [ModuleInitializer]
    public static void Timestamp()
    {
        var iteration = int.Parse(Environment.GetEnvironmentVariable("DOTNET_WATCH_ITERATION") ?? "2");
        if (iteration <= 1)
        {
            Console.Clear();
        }
        Console.SetCursorPosition(0, Math.Max(Console.CursorTop - 1, 0));

        var rule = new Rule($"[yellow]#{iteration} - {DateTime.Now.ToLongTimeString()}[/]")
        {
            Style = "green",
            Alignment = Justify.Left,
            Border = BoxBorder.Double
        };
        AnsiConsole.Write(rule);
        Store.Reload();

        AppDomain.CurrentDomain.ProcessExit += (_, _) =>
        {
            AnsiConsole.Write(new Rule($"end #{iteration}")
            {
                Style = "red dim",
                Alignment = Justify.Left,
                Border = BoxBorder.Double
            });
            while (Store.IsUpdating) {}
            Store.Dispose();
        };
    }
    
    public static DataStore Store => 
        new("data.json");

    public static T Save<T>(this DataStore store, T value, [CallerArgumentExpression("value")] string? key = null)
    {
        store.ReplaceItem(key, value, true);
        return value;
    }

    public static T Get<T>(this DataStore store, T value, T defaultValue = default!, [CallerArgumentExpression("value")] string? key = null)
    {
        return store.GetKeys().ContainsKey(key!)
            ? value = store.GetItem<T>(key)
            : Save(store, defaultValue, key);
    }

    public static void Clear(this DataStore store)
    {
        store.UpdateAll("{}");
    }
} 