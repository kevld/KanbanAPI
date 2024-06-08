using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KanbanAPI.Models
{
    [Table("Boards")]
    public class Board
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(50)]
        [Required]
        public string Title { get; set; } = null!;

        [MaxLength(250)]
        public string? Description { get; set; }

        [InverseProperty("Board")]
        public ICollection<Ticket> Tickets { get; } = new List<Ticket>();
    }

    public class BoardEntityTypeConfiguration : IEntityTypeConfiguration<Board>
    {
        public void Configure(EntityTypeBuilder<Board> builder)
        {
            builder.ToTable("Boards");

            builder.HasKey(x => x.Id);

            builder
            .Property(x => x.Title)
            .HasMaxLength(50)
            .IsRequired();

            builder
            .Property(x => x.Description)
            .HasMaxLength(250);

            builder
            .HasMany(x => x.Tickets)
            .WithOne(x => x.Board);
        }
    }

}