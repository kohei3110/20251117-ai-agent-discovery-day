namespace TodoApp;

/// <summary>
/// ToDoアイテムを表すクラス
/// </summary>
public class TodoItem
{
    /// <summary>
    /// ToDoアイテムのID
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// ToDoアイテムのタイトル
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// ToDoアイテムの説明
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// 完了フラグ
    /// </summary>
    public bool IsCompleted { get; set; }

    /// <summary>
    /// 作成日時
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// ToDoアイテムの文字列表現
    /// </summary>
    public override string ToString()
    {
        var status = IsCompleted ? "✓" : " ";
        return $"[{status}] {Id}. {Title} - {Description} (作成日: {CreatedAt:yyyy/MM/dd HH:mm})";
    }
}
