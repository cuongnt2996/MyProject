namespace WebApi.Models;
public class AccessChecked
{
    public int AccessId { get; set; }
    public string AccessName { get; set; }=null!;
    public string? AccessUrl { get; set; }
    public int? ParentId { get; set; }
    public bool Checked { get; set; }
    public List<AccessChecked>? Children { get; set; }
}