using AkiraBot.CI.Commands;
using AkiraBot.Utilities.CommonTools;
using AkiraBot.Utilities.CommonTools.Models;
using AkiraBot.Utilities.EnumUtility;

namespace AkiraBot.CI;

public sealed class StartUp
{
    private MainCommands _commands;
    
    public StartUp()
    {
        _commands = new MainCommands();
    }
    
    public void PrintStartUpMessage()
    {
        ConsoleHelper.Write("Akira-Bot ", ConsoleColor.Red);
        ConsoleHelper.Write($"greets you ", ConsoleColor.Gray);
        ConsoleHelper.Write($"{Environment.UserName}", ConsoleColor.Green);
        Thread.Sleep(3000);
        Console.Clear();
    }
    
    
    public void ReadCommands()
    {
        _commands.PrintCommands();
        _commands.ReadActionCommandKey();
    }
    
    public void CheckFoldersExists()
    {
        var pl = new PathList();
        var paths = AttributesHelperExtension.GetStringValues(pl);
        PathHelper.CheckForPathExists(paths);
    }
}