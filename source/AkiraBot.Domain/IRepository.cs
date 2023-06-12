namespace AkiraBot.Domain;

public interface IRepository<T> : IDisposable
    where T : EntityBase
{
    public IEnumerable<T> GetAll();
    public T? GetById(int id);
    public void Create(T item);
    public void Update(T item);
    public void Delete(T item);
}