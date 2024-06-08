using KanbanAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KanbanAPI.Controllers;

[ApiController]
[Route("[Controller]")]
public class StatusController : ControllerBase
{
    private readonly KbContext _context;

    public StatusController(KbContext context)
    {
        _context = context;
    }

    [HttpGet("")]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _context.Status.ToListAsync());
    }

    [HttpGet("/{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var board = await _context.Status.FirstOrDefaultAsync(x => x.Id == id);

        if (board == null)
            return NotFound();

        return Ok(board);
    }

    [HttpPost]
    public async Task<IActionResult> Add(TicketStatus status)
    {
        try
        {
            await _context.Status.AddAsync(status);
            await _context.SaveChangesAsync();

            return StatusCode(201, status);
        }
        catch (System.Exception e)
        {
            return StatusCode(500, e);
        }
    }

    [HttpPut]
    public async Task<IActionResult> Update(TicketStatus status)
    {
        try
        {
            _context.Status.Update(status);
            await _context.SaveChangesAsync();

            return Ok(status);
        }
        catch (System.Exception e)
        {
            return StatusCode(500, e);
        }
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var status = _context.Status.Find(id);

            if (status == null)
                return NotFound();

            _context.Status.Remove(status);

            await _context.SaveChangesAsync();

            return NoContent();
        }
        catch (System.Exception e)
        {
            return StatusCode(500, e);
        }
    }
}