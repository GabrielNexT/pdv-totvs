public interface IChangeService
{
  public ChangeResponse processPurchase(ChangeRequest request);
  public IEnumerable<Change> getAllPurchases();
}