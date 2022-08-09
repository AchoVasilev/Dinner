namespace Dinner.Infrastructure.Persistence;

using Application.Common.Interfaces.Persistence;
using Domain.Entities;

public class UserRepository : IUserRepository
{
    private static List<User> users = new List<User>();
    public User? GetUserByEmail(string email) 
        => users.SingleOrDefault(u => u.Email == email);

    public void Add(User user) 
        => users.Add(user);
}