using AkiraBot.Bot;
using AkiraBot.CI.Attributes;
using AkiraBot.ExchangeClients;
using AkiraBot.ExchangeClients.Clients;
using AkiraBot.ExchangesRestAPI.Options;
using AkiraBot.Utilities.CommonTools;

namespace AkiraBot.CI.Commands;

public sealed class ClientCommands : MultiCommandsObject<IExchangeClient>
{
    public ClientCommands()
    {
        //CommandHelper.GetConsoleCommands<IExchangeClient>(this, typeof(ClientCommands));
        InitFuncCommands(this);
    }
    
    [ConsoleCommand(ConsoleKey.D1)]
    public NiceHashClient? CreateNiceHashClientCommand()
    {
        var cfg = ConfigInitializer.GetClientConfig();
        
        if (cfg == null)
        {
            Console.WriteLine("Конфиг пуст. Сначала заполните его.");
            Thread.Sleep(2500);
            return null;
        }
        
        var client = new NiceHashClient( 
            new NiceHashOptions
            {
                PublicKey = cfg.NiceHashInfo.PublicKey,
                SecretKey = cfg.NiceHashInfo.SecretKey,
                OrganizationId = cfg.NiceHashInfo.OrganizationId
            }
        );
        Console.Write("Биржа ");
        ConsoleHelper.Write("Nice Hash ", ConsoleColor.Green);
        Console.WriteLine("выбрана.");
        
        return client;
    }

    public override void PrintCommands()
    {
        Console.Clear();
        ConsoleHelper.Write("[Q]", ConsoleColor.Red);
        ConsoleHelper.WriteLine(" - вернуться назад", ConsoleColor.Gray);

        ConsoleHelper.Write("[1]", ConsoleColor.Red);
        ConsoleHelper.WriteLine(" - Nice Hash", ConsoleColor.Gray);

        ConsoleHelper.Write("[2]", ConsoleColor.Red);
        ConsoleHelper.WriteLine(" - Binance", ConsoleColor.Gray);
    }
}