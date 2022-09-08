var name = "Khalid";
var message = $"[green]Hello[/], [yellow]{name}![/]";
AnsiConsole.MarkupLine(message);

var count = 0;
count = Store.Get(count);
Console.WriteLine($"Current count: {++count}");

Store.Save(count);