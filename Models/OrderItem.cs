public class OrderItem
{
    public int? ProductTypeId { get; set; }
    public string? Name { get; set; }
    public string? Website { get; set; }
    public string? ItemUrl { get; set; }
    public string? OrderUrl { get; set; }
    public bool? Ordered { get; set; }
    public bool? Paid { get; set; }
    public bool? Received { get; set; }
    public DateTime? ReleaseDate { get; set; }
    public DateTime? RoughDate { get; set; }
    public int? Month => RoughDate.HasValue ? RoughDate.Value.Month : null;
    public string? Price { get; set; }
    public int? PaidFrom { get; set; }
}