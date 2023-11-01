using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HomestockedAPI.Models;

namespace HomestockedAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ItemsController : ControllerBase
{
    private readonly ItemContext _context;

    public ItemsController(ItemContext context)
    {
        _context = context;
    }

    // GET: api/Items
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ItemDTO>>> GetItems()
    {
        return await _context.Items.Select(x => ItemToDTO(x)).ToListAsync();
    }

    // GET: api/Items/5
    [HttpGet("{id}")]
    public async Task<ActionResult<ItemDTO>> GetItem(long id)
    {
        var item = await _context.Items.FindAsync(id);

        if (item == null)
        {
            return NotFound();
        }

        return ItemToDTO(item);
    }

    // PUT: api/Items/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutItem(long id, ItemDTO itemDTO)
    {
        if (id != itemDTO.Id)
        {
            return BadRequest();
        }

        var item = await _context.Items.FindAsync(id);
        if (item == null)
        {
            return NotFound();
        }

        item.Name = itemDTO.Name;
        item.Type = itemDTO.Type;
        item.Description = itemDTO.Description;
        item.Location = itemDTO.Location;
        item.IsInStock = itemDTO.IsInStock;
        item.QtyOnHand = itemDTO.QtyOnHand;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException) when (ItemExists(id))
            {
                return NotFound();
            }

        return NoContent();
    }

    // POST: api/Items
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<ItemDTO>> PostItem(ItemDTO itemDTO)
    {
        var item = new Item
        {
            Name = itemDTO.Name,
            Type = itemDTO.Type,
            Description = itemDTO.Description,
            Location = itemDTO.Location,
            IsInStock = itemDTO.IsInStock,
            QtyOnHand = itemDTO.QtyOnHand
    };

        _context.Items.Add(item);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetItem), new { id = item.Id }, ItemToDTO(item));
    }

    // DELETE: api/Items/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteItem(long id)
    {
        var item = await _context.Items.FindAsync(id);
        if (item == null)
        {
            return NotFound();
        }

        _context.Items.Remove(item);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool ItemExists(long id)
    {
        return (_context.Items?.Any(e => e.Id == id)).GetValueOrDefault();
    }
    private static ItemDTO ItemToDTO(Item item) =>
        new ItemDTO
        {
            Id = item.Id,
            Name = item.Name,
            Type = item.Type,
            Description = item.Description,
            Location = item.Location,
            IsInStock = item.IsInStock,
            QtyOnHand = item.QtyOnHand
        };
}
