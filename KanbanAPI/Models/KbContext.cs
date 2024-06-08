using Microsoft.EntityFrameworkCore;

namespace KanbanAPI.Models
{
    public class KbContext : DbContext
    {
        public DbSet<Board> Boards { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TicketStatus> Status { get; set; }

        public KbContext(DbContextOptions options) : base(options)
        {

        }
    }
}