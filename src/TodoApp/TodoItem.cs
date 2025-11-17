namespace TodoApp;

/// <summary>
/// Represents a single to-do item
/// </summary>
public class TodoItem
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool IsCompleted { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? CompletedAt { get; set; }

    public TodoItem()
    {
        CreatedAt = DateTime.Now;
    }

    public override string ToString()
    {
        var status = IsCompleted ? "✓" : " ";
        return $"[{status}] {Id}. {Title}";
    }

    public string ToDetailedString()
    {
        var status = IsCompleted ? "完了" : "未完了";
        var completed = CompletedAt.HasValue ? $"\n  完了日時: {CompletedAt:yyyy/MM/dd HH:mm}" : "";
        return $"ID: {Id}\n" +
               $"タイトル: {Title}\n" +
               $"説明: {Description}\n" +
               $"状態: {status}\n" +
               $"作成日時: {CreatedAt:yyyy/MM/dd HH:mm}" +
               completed;
    }
}
