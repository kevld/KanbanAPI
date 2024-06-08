using KanbanAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KanbanAPI.Controllers;

[ApiController]
[Route("[Controller]")]
public class TicketController : ControllerBase
{
    private readonly KbContext _context;

    public TicketController(KbContext context)
    {
        _context = context;
    }

    [HttpGet("/board/{id}")]
    public async Task<IActionResult> GetByBoardId(int boardId)
    {
        var result = await _context.Tickets.Where(x => x.BoardId == boardId).ToListAsync();
        return Ok(result);
    }

    [HttpGet("/{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var board = await _context.Tickets.FirstOrDefaultAsync(x => x.Id == id);

        if (board == null)
            return NotFound();

        return Ok(board);
    }

    [HttpPost]
    public async Task<IActionResult> Add(Ticket ticket)
    {
        try
        {
            await _context.Tickets.AddAsync(ticket);
            await _context.SaveChangesAsync();

            return StatusCode(201, ticket);
        }
        catch (System.Exception e)
        {
            return StatusCode(500, e);
        }
    }

    [HttpPut]
    public async Task<IActionResult> Update(Ticket ticket)
    {
        try
        {
            _context.Tickets.Update(ticket);
            await _context.SaveChangesAsync();

            return Ok(ticket);
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
            var ticket = _context.Tickets.Find(id);

            if (ticket == null)
                return NotFound();

            _context.Tickets.Remove(ticket);

            await _context.SaveChangesAsync();

            return NoContent();
        }
        catch (System.Exception e)
        {
            return StatusCode(500, e);
        }
    }

    [HttpPut("/status")]
    public async Task<IActionResult> UpdateStatus(int ticketId, byte statusId)
    {
        var ticket = _context.Tickets.Find(ticketId);

        if (ticket == null)
            return NotFound();

        var status = _context.Status.Find(statusId);

        if (status == null)
            return NotFound();

        try
        {
            ticket.Status = status;
            await _context.SaveChangesAsync();

            return Ok(ticket);
        }
        catch (System.Exception e)
        {
            return StatusCode(500, e);
        }
    }
}