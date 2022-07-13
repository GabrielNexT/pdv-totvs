using Moq;
using NUnit.Framework;
using Microsoft.EntityFrameworkCore;

[TestFixture]
public class ChangeServiceTest
{
  private Mock<IChangeRepository> mockRepository;
  private ChangeContext mockChangeContext;
  private IChangeService changeService;

  [SetUp]
  public void Setup()
  {
    var options = new DbContextOptionsBuilder<ChangeContext>().UseInMemoryDatabase(databaseName: "test").Options;

    this.mockRepository = new Mock<IChangeRepository>();
    this.mockChangeContext = new ChangeContext(options);
    this.changeService = new ChangeService(this.mockChangeContext, this.mockRepository.Object);

  }

  [Test]
  public void shouldReturnCorrectMessage()
  {
    var response = this.changeService.processPurchase(this.GetRequest(10, 9));

    Assert.That("Entregar 2 moeda(s) de R$0,50".Equals(response.message));
  }

  [Test]
  public void shouldReturnNull()
  {
    var response = this.changeService.processPurchase(this.GetRequest(1, 1));
    Assert.IsNull(response.message);
  }

  [Test]
  public void shouldReturnOneCoin()
  {
    var response = this.changeService.processPurchase(this.GetRequest(1.01m, 1));
    Assert.That("Entregar 1 moeda(s) de R$0,01".Equals(response.message));
  }

  [Test]
  public void shouldReturn20BrlAnd10Brl()
  {
    var response = this.changeService.processPurchase(this.GetRequest(130, 100));
    Assert.That("Entregar 1 nota(s) de R$20,00 e 1 nota(s) de R$10,00".Equals(response.message));
  }

  [Test]
  public void shouldReturn50Brl20BrlAnd10Brl()
  {
    var response = this.changeService.processPurchase(this.GetRequest(180, 100));
    Assert.That("Entregar 1 nota(s) de R$50,00, 1 nota(s) de R$20,00 e 1 nota(s) de R$10,00".Equals(response.message));
  }

  [Test]
  public void shouldReturnAllAvaliableBillsAndCoins()
  {
    var response = this.changeService.processPurchase(this.GetRequest(180.67m, 0.01m));
    Assert.That("Entregar 1 nota(s) de R$100,00, 1 nota(s) de R$50,00, 1 nota(s) de R$20,00, 1 nota(s) de R$10,00, 1 moeda(s) de R$0,50, 1 moeda(s) de R$0,10, 1 moeda(s) de R$0,05 e 1 moeda(s) de R$0,01".Equals(response.message));
  }


  private ChangeRequest GetRequest(decimal received, decimal purchase)
  {
    ChangeRequest request = new ChangeRequest();
    request.AmountReceived = received;
    request.PurchaseValue = purchase;
    return request;
  }

}