using Microsoft.EntityFrameworkCore;

namespace HomestockedAPI.Models;

public class ItemContext : DbContext
{
    public ItemContext(DbContextOptions<ItemContext> options)
        : base(options)
    {
    }

    public DbSet<Item> Items { get; set; } = null!;
}

