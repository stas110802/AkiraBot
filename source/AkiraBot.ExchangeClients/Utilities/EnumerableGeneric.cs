using System.Collections;

namespace AkiraBot.ExchangeClients.Utilities;

public class EnumerableGeneric<TClass, TInterface> : IEnumerable<TInterface>
    where TClass : TInterface
{
    private readonly IList<TClass> _list;

    public EnumerableGeneric(IList<TClass> list)
    {
        _list = list;
    }

    public IEnumerator<TInterface> GetEnumerator()
    {
        foreach(var item in _list)
            yield return item;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}