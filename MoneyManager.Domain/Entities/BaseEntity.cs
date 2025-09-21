namespace MoneyManager.Domain.Entities;

public abstract class BaseEntity
{
    public Guid Id{get;set;} = Guid.NewGuid();
    public DateTime CreatedAt{get;set;} = DateTime.UtcNow;
}