using System.ComponentModel.DataAnnotations;

public class ChangeResponse
{
  public decimal Change { get; set; }
  public string message { get; set; }
  public Dictionary<string, int> coins { get; set; }
}