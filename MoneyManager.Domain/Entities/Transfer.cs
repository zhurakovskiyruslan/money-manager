namespace MoneyManager.Domain.Entities;

public class Transfer : BaseEntity
{
    public Guid UserId { get; set; }
    public Guid SourceAccountId { get; set; }
    public Guid DestinationAccountId { get; set; }
    
    public decimal SourceAmount { get; set; }
    public decimal DestinationAmount { get; set; }
    public decimal FxRate { get; set; }
    public string? Description { get; set; }
    public DateTime OccurredAt { get; set; }
    public DateTime? UpdatedAt { get; set; } 
    
    public User User { get; set; } = null!;
    public Account SourceAccount { get; set; } = null!;
    public Account DestinationAccount { get; set; } = null!;
}