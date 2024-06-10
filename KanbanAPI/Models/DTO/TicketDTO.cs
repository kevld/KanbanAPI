namespace KanbanAPI.Models.DTO;

public class TicketDTO
{
    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public byte StatusId { get; set; }

    public int BoardId { get; set; }

}