namespace HomestockedAPI.Models
{
    public class ItemDTO
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Type { get; set; }
        public string? Description { get; set; }
        public string? Location { get; set; }
        public bool IsInStock { get; set; }
        public long QtyOnHand { get; set; }
    }
}
