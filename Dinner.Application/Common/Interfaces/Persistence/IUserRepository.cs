namespace Dinner.Application.Common.Interfaces.Persistence;

using Domain.Entities;

public interface IUserRepository
{
    User? GetUserByEmail(string email);
    
    void Add(User user);
}