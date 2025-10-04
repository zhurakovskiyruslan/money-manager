namespace MoneyManager.Domain.Entities;

public class User : BaseEntity
{
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string BaseCurrency { get; set; } = null!;
    public string TimeZone { get; set; } = null!;
    public Guid AppUserId { get; set; }
    public ICollection<Account> Accounts { get; set; } = new List<Account>();
    public ICollection<Transfer>  Transfers { get; set; } = new List<Transfer>();
    public ICollection<CustomCategory> CustomCategories { get; set; } = new List<CustomCategory>();
}