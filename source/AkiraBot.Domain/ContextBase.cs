namespace AkiraBot.Domain;

public abstract class ContextBase
{
    protected ContextBase()
    {
        Context = new ApplicationContext();
    }
    
    protected ApplicationContext Context { get; set; }
}