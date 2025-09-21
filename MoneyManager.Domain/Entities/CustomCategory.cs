using MoneyManager.Domain.Enums;

namespace MoneyManager.Domain.Entities;

public class CustomCategory : BaseEntity
{
    public Guid UserId { get; set; }
    
    public string Title { get; set; } = null!;
    public CategoryType Type { get; set; } // Expense / Income
    public bool IsActive { get; set; } = true;
    public bool IsDeleted { get; set; } = false;
    
    public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    public User User { get; set; } = null!;
}
