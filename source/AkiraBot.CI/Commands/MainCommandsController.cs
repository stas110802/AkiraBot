namespace AkiraBot.CI.Commands;

public sealed class MainCommandsController
{
   private readonly MainCommands _mainCommands;

   public MainCommandsController()
   {
      _mainCommands = new MainCommands();
   }

   public void StartReadCommands()
   {
      _mainCommands.PrintCommands();
      _mainCommands.ReadActionCommandKey();
   }
}