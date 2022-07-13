using Microsoft.EntityFrameworkCore;

public class Change
{
  public long Id { get; set; }
  public long AmountReceived { get; set; }
  public long PurchaseValue { get; set; }
  public long ChangeValue { get; set; }

}

public class ChangeContext : DbContext
{

  public ChangeContext(DbContextOptions<ChangeContext> options) : base(options)
  {
  }
  public DbSet<Change> Changes { get; set; }

}