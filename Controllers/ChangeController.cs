using Microsoft.AspNetCore.Mvc;

[Route("api/v1/changes")]
[ApiController]
public class ChangeController : ControllerBase
{
  private readonly IChangeService changeService;

  public ChangeController(IChangeService changeService)
  {
    this.changeService = changeService;
  }

  [HttpPost]
  public ActionResult<Change> Save([FromBody] ChangeRequest body)
  {
    if (!ModelState.IsValid)
    {
      return BadRequest(ModelState);
    }
    if (body.AmountReceived < body.PurchaseValue)
    {
      ModelState.AddModelError("Change", "É necessaŕio que o valor recebido seja maior que o total da compra.");
      return BadRequest(ModelState);
    }

    return Ok(changeService.processPurchase(body));
  }

  [HttpGet]
  public ActionResult<Change> GetAction()
  {
    return Ok(this.changeService.getAllPurchases());
  }
}