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

    public User? GetUserByLogin(string login)
    {
        return Context.Users.FirstOrDefault(x => x.Login == login);
    }
    
    public User? GetUserByEmail(string email)
    {
        return Context.Users.FirstOrDefault(x => x.Email == email);
    }

    public bool IsUserCreateByLogin(string login)
    {
        var user = GetUserByLogin(login);
        return user != null;
    }
    
    public bool IsUserCreateByEmail(string email)
    {
        var user = GetUserByEmail(email);
        return user != null;
    }

    public void Dispose()
    {
        GC.SuppressFinalize(Context);
    }
}