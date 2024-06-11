namespace KanbanAPI.Models.DTO;

public class CreateStatusDTO 
{
    public int BoardId { get; set; }
    public string Name { get; set; } = null!;
}