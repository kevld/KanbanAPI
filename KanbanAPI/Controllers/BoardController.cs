using KanbanAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KanbanAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoardController : ControllerBase
    {
        private readonly KbContext _context;

        public BoardController(KbContext kbContext)
        {
            _context = kbContext;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _context.Boards.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var board = await _context.Boards.FirstOrDefaultAsync(x => x.Id == id);

            if (board == null)
                return NotFound();

            return Ok(board);
        }

        [HttpPost]
        public async Task<IActionResult> Add(Board board)
        {
            try
            {
                await _context.Boards.AddAsync(board);
                await _context.SaveChangesAsync();

                return StatusCode(201, board);
            }
            catch (System.Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(Board board)
        {
            try
            {
                _context.Boards.Update(board);
                await _context.SaveChangesAsync();

                return Ok(board);
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
                var board = _context.Boards.Find(id);

                if (board == null)
                    return NotFound();

                _context.Boards.Remove(board);

                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (System.Exception e)
            {
                return StatusCode(500, e);
            }
        }
    }
}