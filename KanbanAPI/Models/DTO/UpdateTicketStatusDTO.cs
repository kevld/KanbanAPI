namespace KanbanAPI.Models.DTO;

public class UpdateTicketStatusDTO
{
    public int TicketId { get; set; }
    public byte StatusId { get; set; }
}