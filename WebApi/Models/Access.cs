namespace WebApi.Models;
public class Access
{
    public int AccessId { get; set; }
    public string AccessName { get; set; }=null!;
    public string? AccessUrl { get; set; }
    public int? ParentId { get; set; }
    public List<Access>? Children { get; set; }
}