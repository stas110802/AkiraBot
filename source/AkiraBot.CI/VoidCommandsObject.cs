using AkiraBot.CI.Utilits;

namespace AkiraBot.CI;

public abstract class VoidCommandsObject
{
    public abstract void PrintCommands();
    
    public void ReadActionCommandKey()
    {
        var key = new ConsoleKey();
        while (key != ConsoleKey.Q)
        {
            key = Console.ReadKey(true).Key;
            InvokeActionCommand(key);
        }
    }

    protected void InitVoidCommands(object target)
    {
        if (target == null) throw new ArgumentNullException(nameof(target));
        VoidCommands = CommandHelper.GetConsoleCommands<Action>(target, target.GetType());
    }

    private Dictionary<ConsoleKey, Action>? VoidCommands { get; set; }

    private void InvokeActionCommand(ConsoleKey key)
    {
        if (VoidCommands != null)
            InvokeMethodByKey(key);
        else
            throw new NullReferenceException($"{nameof(VoidCommands)} uninitialized");
    }
    
    private void InvokeMethodByKey(ConsoleKey key)
    {
        var action = VoidCommands.ContainsKey(key) ? VoidCommands[key] : null;
        if (action == null) return;
        
        action.Invoke();
        PrintCommands();// нужно ли это??
    }
}