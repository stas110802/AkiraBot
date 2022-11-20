using AkiraBot.CI.Attributes;

namespace AkiraBot.CI.Commands;

public sealed class MainCommands : VoidCommandsObject
{
    public override void PrintCommands()
    {
        Console.WriteLine("[1] - doSome command");
    }

    [ConsoleCommand(ConsoleKey.D1)]
    public void DoSome()
    {
        Console.WriteLine("cmd is executed");
    }
}