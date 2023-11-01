namespace HomestockedAPI.Models
{
    public class Item
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Type { get; set; }
        public string? Description { get; set; }
        public string? Location { get; set; }
        public bool IsInStock { get; set; }
        public long QtyOnHand { get; set; }
        public string? Secret { get; set; }
    }
}
