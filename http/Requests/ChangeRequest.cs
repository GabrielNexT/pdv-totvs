using System.ComponentModel.DataAnnotations;

public class ChangeRequest
{
  [Required(ErrorMessage = "É necessario informar um valor recebido.")]
  [Range(1, Int64.MaxValue, ErrorMessage = "O valor recebido deve ser maior que 1 e menor que Int64 - 1")]
  public decimal AmountReceived { get; set; }
  [Required(ErrorMessage = "É necessario informar um valor de compra.")]
  [Range(1, Int64.MaxValue, ErrorMessage = "O valor da compra deve ser maior que 1 e menor que Int64 - 1")]
  public decimal PurchaseValue { get; set; }

  private decimal Change
  {
    get
    {
      return this.AmountReceived - this.PurchaseValue;
    }
  }

}