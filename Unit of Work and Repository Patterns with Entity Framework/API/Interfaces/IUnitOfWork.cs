namespace API.Interfaces;

public interface IUnitOfWork
{
    IUserRepository Users { get; }
    Task Complete();
}
