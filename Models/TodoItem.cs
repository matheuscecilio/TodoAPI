namespace TodoAPI.Models;

public class TodoItem
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string? Title { get; set; }
    public string? Description { get; set; }
    public DateTime Date { get; set; }
    public bool IsComplete { get; set; }
}