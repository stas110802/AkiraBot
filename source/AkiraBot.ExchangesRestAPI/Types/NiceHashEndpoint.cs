using System.ComponentModel;

namespace AkiraBot.ExchangesRestAPI.Types;

public enum NiceHashEndpoint
{
    [Description("/api/v2/time")] ServerTime,
    [Description("/main/api/v2/accounting/accounts2")] Balances,
    [Description("/exchange/api/v2/info/prices")] CurrentPrices,
    [Description("/exchange/api/v2/order")] Order,
    [Description("/exchange/api/v2/info/myOrders")] MyOrders,
    [Description("/exchange/api/v2/info/cancelAllOrders")] CancelAllOrders,
    [Description("/main/api/v2/accounting/withdrawal")] Withdrawal
}