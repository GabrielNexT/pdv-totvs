using Microsoft.AspNetCore.Mvc;

[Route("api/v1/changes")]
[ApiController]
public class ChangeController : ControllerBase
{

  [HttpPost]
  public ActionResult<String> Save([FromBody] ChangeRequest body)
  {
    if (!ModelState.IsValid)
    {
      return BadRequest(ModelState);
    }
    else if (body.AmountReceived < body.PurchaseValue)
    {
      ModelState.AddModelError("Change", "É necessaŕio que o valor recebido seja maior que o total da compra.");
      return BadRequest(ModelState);
    }


    return Ok("Hello World!");
  }
}