using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KanbanAPI.Models
{
    [Table("Tickets")]
    public class Ticket
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(50)]
        [Required]
        public string Title { get; set; } = null!;

        [MaxLength(500)]
        public string? Description { get; set; }

        [ForeignKey("Status")]
        public byte StatusId { get; set; }
        public TicketStatus Status { get; set; } = null!;

        [ForeignKey("Board")]
        public int BoardId { get; set; }
        public Board Board { get; set; } = null!;
        
    }

    public class TicketEntityTypeConfiguration : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder
            .HasKey(x => x.Id);

            builder
            .Property(x => x.Title)
            .HasMaxLength(50)
            .IsRequired();

            builder
            .HasOne(x => x.Board)
            .WithMany(x => x.Tickets);

            builder
            .HasOne(x => x.Status);
        }
    }
}