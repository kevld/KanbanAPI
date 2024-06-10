using KanbanAPI.Models;
using KanbanAPI.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KanbanAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TicketController : ControllerBase
{
    private readonly KbContext _context;

    public TicketController(KbContext context)
    {
        _context = context;
    }

    [HttpGet("board/{id}")]
    public async Task<IActionResult> GetByBoardId(int id)
    {
        var result = await _context.Tickets.Where(x => x.BoardId == id)
        .Include(x => x.Status)
        .ToListAsync();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var board = await _context.Tickets.FirstOrDefaultAsync(x => x.Id == id);

        if (board == null)
            return NotFound();

        return Ok(board);
    }

    // TODO: fix this
    [HttpPost]
    public async Task<IActionResult> Add(TicketDTO ticketDTO)
    {
        try
        {
            var board = await _context.Boards.FindAsync(ticketDTO.BoardId);
            var status = await _context.Status.FindAsync(ticketDTO.StatusId);

            if(board == null || status == null)
                return NotFound();

            var ticket = new Ticket()
            {
                BoardId = ticketDTO.BoardId,
                StatusId = ticketDTO.StatusId,
                Description = ticketDTO.Description,
                Title = ticketDTO.Title
            };

            //board.Tickets.Add(ticket);

            await _context.Tickets.AddAsync(ticket);
            await _context.SaveChangesAsync();

            return StatusCode(201, ticket.Id);
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

    [HttpPut("Status")]
    public async Task<IActionResult> UpdateStatus(UpdateTicketStatusDTO ticketStatusDTO)
    {
        var ticket = await _context.Tickets.FirstOrDefaultAsync(x => x.Id == ticketStatusDTO.TicketId);
        var status = await _context.Status.FirstOrDefaultAsync(x => x.Id == ticketStatusDTO.StatusId);

        if(ticket == null || status == null)
            return NotFound($"ticket : {ticketStatusDTO.TicketId}, status: {ticketStatusDTO.StatusId}");

        try
        {
            ticket.Status = status;

            await _context.SaveChangesAsync();

            return Ok(ticketStatusDTO);
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