using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KanbanAPI.Models
{
    [Table("Status")]
    public class TicketStatus
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public byte Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Name { get; set; } = null!;

        [ForeignKey("Board")]
        public int BoardId { get; set; }
        public Board Board { get; set; } = null!;
    }
}