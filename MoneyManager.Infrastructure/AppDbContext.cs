using Microsoft.EntityFrameworkCore;
using MoneyManager.Domain.Entities;

namespace MoneyManager.Infrastructure;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Account> Accounts { get; set; } = null!;
    public DbSet<Transaction> Transactions { get; set; } = null!;
    public DbSet<Transfer> Transfers { get; set; } = null!;
    public DbSet<SharedCategory> SharedCategories { get; set; } = null!;
    public DbSet<CustomCategory> CustomCategories { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasIndex(u => u.Email).IsUnique();
            entity.Property(u => u.Email).IsRequired().HasMaxLength(254);

        });

        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasIndex(a => new { a.UserId, a.Title }).IsUnique();

            entity.Property(a => a.Title).IsRequired().HasMaxLength(100);
            entity.Property(a => a.Currency).IsRequired().HasMaxLength(3);
            entity.Property(a => a.Balance).HasPrecision(18, 2);
            entity.Property(a => a.Type).HasConversion<string>();

            entity.HasOne(a => a.User)
                .WithMany(u => u.Accounts)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.Property(t => t.Amount).HasPrecision(18, 2);

            entity.HasIndex(t => new { t.AccountId, t.OccurredAt });

            entity.HasOne(t => t.Account)
                .WithMany(a => a.Transactions)
                .HasForeignKey(t => t.AccountId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(t => t.SharedCategory)
                .WithMany(c => c.Transactions)
                .HasForeignKey(t => t.SharedCategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(t => t.CustomCategory)
                .WithMany(c => c.Transactions)
                .HasForeignKey(t => t.CustomCategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.ToTable(t => t.HasCheckConstraint(
                "CK_Transactions_CategoryXor",
                "(CASE WHEN \"SharedCategoryId\" IS NOT NULL THEN 1 ELSE 0 END) + " +
                "(CASE WHEN \"CustomCategoryId\" IS NOT NULL THEN 1 ELSE 0 END) = 1"
            ));
        });

        modelBuilder.Entity<Transfer>(entity =>
        {
            entity.Property(t => t.SourceAmount).HasPrecision(18, 2);
            entity.Property(t => t.DestinationAmount).HasPrecision(18, 2);
            entity.Property(t => t.FxRate).HasPrecision(18, 8);

            entity.HasIndex(t => new { t.UserId, t.OccurredAt });

            entity.HasOne(t => t.SourceAccount)
                .WithMany(a => a.TransfersAsSource)
                .HasForeignKey(t => t.SourceAccountId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(t => t.DestinationAccount)
                .WithMany(a => a.TransfersAsDest)
                .HasForeignKey(t => t.DestinationAccountId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(t => t.User)
                .WithMany(u => u.Transfers)
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            
        });
        
        modelBuilder.Entity<SharedCategory>(entity =>
        {
            entity.HasIndex(c => new { c.Title, c.Type }).IsUnique();
            entity.Property(c => c.Title).IsRequired().HasMaxLength(100);
            entity.Property(c => c.Type).HasConversion<string>();

            entity.HasMany(c => c.Transactions)
                .WithOne(t => t.SharedCategory)
                .HasForeignKey(t => t.SharedCategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<CustomCategory>(entity =>
        {
            entity.HasIndex(c => new { c.UserId, c.Title, c.Type }).IsUnique();
            entity.Property(c => c.Title).IsRequired().HasMaxLength(100);
            entity.Property(c => c.Type).HasConversion<string>();

            entity.HasMany(c => c.Transactions)
                .WithOne(t => t.CustomCategory)
                .HasForeignKey(t => t.CustomCategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}