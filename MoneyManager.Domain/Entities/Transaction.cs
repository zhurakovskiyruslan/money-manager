namespace MoneyManager.Domain.Entities;

public class Transaction : BaseEntity
{
    public Guid AccountId { get; set; }
    public Guid? SharedCategoryId { get; set; }
    public Guid? CustomCategoryId { get; set; }
    
    public decimal Amount { get; set; }
    public string? Description { get; set; }
    public DateTime OccurredAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool IsDeleted { get; set; }
    
    public Account Account { get; set; } = null!;
    public SharedCategory? SharedCategory { get; set; }
    public CustomCategory? CustomCategory { get; set; }
}