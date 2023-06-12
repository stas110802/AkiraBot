using AkiraBot.Domain.Models;

namespace AkiraBot.Domain.Repositories;

public class UserRepository : ContextBase, IRepository<User>
{
    public IEnumerable<User> GetAll()
    {
        return Context.Users.AsEnumerable();
    }

    public User? GetById(int id)
    {
        return Context.Users
            .FirstOrDefault(x => x.Id == id);
    }

    public void Create(User item)
    {
        Context.Users.Add(item);
        Context.SaveChanges();
    }

    public void Update(User item)
    {
        var entity = Context.Users.Find(item.Id);
        if(entity == null) return;
        Context.Entry(entity).CurrentValues.SetValues(item);
    }

    public void Delete(User item)
    {
        Context.Users.Remove(item);
    }

    public void Dispose()
    {
        GC.SuppressFinalize(Context);
    }
}