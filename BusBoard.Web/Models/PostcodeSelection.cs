namespace BusBoard.Web.Models
{
  public class PostcodeSelection
  {
    public string Postcode { get; set; }
    public string Error { get; set; }
    public int MaxDistance { get; set; }
  }
}