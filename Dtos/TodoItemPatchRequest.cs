namespace TodoAPI.Dtos;

public class TodoItemPatchRequest
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public DateTime Date { get; set; }
    public bool IsComplete { get; set; }
}
