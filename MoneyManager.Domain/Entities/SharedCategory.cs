using MoneyManager.Domain.Enums;

namespace MoneyManager.Domain.Entities;

public class SharedCategory : BaseEntity
{
    public string Title { get; set; } = null!;
    public CategoryType Type { get; set; } // Expense / Income
    public bool IsActive { get; set; } = true;
    
    public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}