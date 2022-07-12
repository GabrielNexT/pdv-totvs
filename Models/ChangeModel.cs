using Microsoft.EntityFrameworkCore;

public class ChangeContext : DbContext
{

  public ChangeContext(DbContextOptions<ChangeContext> options) : base(options)
  {

  }

  public DbSet<Change> Changes { get; set; }

}

public class Change
{
  public long ChangeId { get; set; }
  public long AmountReceived { get; set; }
  public long PurchaseValue { get; set; }
  public DateTime DateCreated { get; set; } = DateTime.Now;
  public long getChange()
  {
    return this.AmountReceived - this.PurchaseValue;
  }

}