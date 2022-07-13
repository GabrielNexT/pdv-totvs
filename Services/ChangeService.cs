public class ChangeService : IChangeService
{
  private readonly ChangeContext context;
  private readonly IChangeRepository repository;

  public ChangeService(ChangeContext context, IChangeRepository repository)
  {
    this.context = context;
    this.repository = repository;
  }

  public ChangeResponse processPurchase(ChangeRequest request)
  {
    Change change = new Change();

    change.AmountReceived = (long)(request.AmountReceived * 100);
    change.PurchaseValue = (long)(request.PurchaseValue * 100);
    change.ChangeValue = change.AmountReceived - change.PurchaseValue;

    context.Changes.Add(change);
    context.SaveChanges();

    return this.castChangeToChangeResponse(change.ChangeValue);
  }

  public IEnumerable<Change> getAllPurchases()
  {
    return this.repository.GetAll();
  }

  private ChangeResponse castChangeToChangeResponse(long total)
  {
    ChangeResponse response = new ChangeResponse();
    long[] defaultCoins = new long[] { 10000, 5000, 2000, 1000, 50, 10, 5, 1 };
    List<(long, long)> changeCoins = new List<(long, long)>();

    for (int i = 0; i < defaultCoins.Length; i++)
    {
      long coin = defaultCoins[i];
      long amount = total / coin;
      total -= (amount * coin);
      if (amount > 0) changeCoins.Add((coin, amount));
    }

    if (changeCoins.Count > 0)
    {
      response.message = "Entregar";
    }

    for (int i = 0; i < changeCoins.Count; i++)
    {
      decimal coin = (decimal)changeCoins[i].Item1 / 100;
      long coinAmount = changeCoins[i].Item2;
      string type = changeCoins[i].Item1 >= 1000 ? "nota(s)" : "moeda(s)";

      if (i != changeCoins.Count - 1)
      {
        response.message += $" {coinAmount} {type} de R${coin:0.00}{(i != changeCoins.Count - 2 ? "," : null)}";
      }
      else
      {
        response.message += $"{(i >= 1 ? " e" : null)} {coinAmount} {type} de R${coin:0.00}";
      }

    }

    return response;
  }
}