using AkiraBot.CI.Utilits;

namespace AkiraBot.CI;

public abstract class MultiCommandsObject<TResult> : VoidCommandsObject
    where TResult : class
{
    public TResult? ReadFuncCommandKey()
    {
        var key = new ConsoleKey();
        TResult? result = null;
        
        while (key != ConsoleKey.Q &&
               result == null)
        {
            key = Console.ReadKey(true).Key;
            result = InvokeFuncCommand(key);
        }
        
        return result;
    }
    
    protected void InitFuncCommands<TValue>(TValue target)
    {
        if (target == null) throw new ArgumentNullException(nameof(target));
        FuncCommands = CommandHelper.GetConsoleCommands<Func<TResult>>(target, target.GetType());
    }

    private Dictionary<ConsoleKey, Func<TResult>>? FuncCommands { get; set; }
    
    private TResult? InvokeFuncCommand(ConsoleKey key)
    {
        if (FuncCommands == null) 
            throw new NullReferenceException($"{nameof(Commands)} uninitialized");
        
        var res = GetMethodValueByKey(key);
            
        return res;
    }
    
    private TResult? GetMethodValueByKey(ConsoleKey key)
    {
        if (FuncCommands == null)
            throw new ArgumentNullException($"{FuncCommands} is null");
        
        var action = FuncCommands.ContainsKey(key) ? FuncCommands[key] : null;

        return action?.Invoke();
    }
}