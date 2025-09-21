using MoneyManager.Domain.Enums;

namespace MoneyManager.Domain.Entities;

public class Account  : BaseEntity
{
    public Guid UserId { get; set; }
    public string Title { get; set; } = null!;
    public AccountType Type { get; set; } // Cash / Card / Bank / Crypto / Other
    public string Currency { get; set; } = null!;
    public decimal Balance { get; set; }
    public bool IsArchived { get; set; }
    public DateTime? ClosedAt { get; set; }
    
    public User User { get; set; } = null!;
    public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    public ICollection<Transfer> TransfersAsSource { get; set; } = new List<Transfer>();
    public ICollection<Transfer> TransfersAsDest { get; set; } = new List<Transfer>();
}
