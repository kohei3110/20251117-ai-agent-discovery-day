using System.Text.Json;

namespace TodoApp;

/// <summary>
/// ToDoアイテムの管理クラス
/// </summary>
public class TodoManager
{
    private List<TodoItem> _todos;
    private readonly string _filePath;
    private int _nextId;

    public TodoManager(string filePath = "todos.json")
    {
        _filePath = filePath;
        _todos = new List<TodoItem>();
        _nextId = 1;
        LoadFromFile();
    }

    /// <summary>
    /// 新しいToDoアイテムを追加
    /// </summary>
    public void AddTodo(string title, string description = "")
    {
        var todo = new TodoItem
        {
            Id = _nextId++,
            Title = title,
            Description = description,
            IsCompleted = false,
            CreatedAt = DateTime.Now
        };
        _todos.Add(todo);
        SaveToFile();
        Console.WriteLine($"ToDoアイテム「{title}」を追加しました。");
    }

    /// <summary>
    /// すべてのToDoアイテムを表示
    /// </summary>
    public void ListTodos(bool showAll = true)
    {
        var todosToShow = showAll ? _todos : _todos.Where(t => !t.IsCompleted).ToList();

        if (!todosToShow.Any())
        {
            Console.WriteLine("ToDoアイテムがありません。");
            return;
        }

        Console.WriteLine("\n=== ToDoリスト ===");
        foreach (var todo in todosToShow)
        {
            Console.WriteLine(todo);
        }
        Console.WriteLine();
    }

    /// <summary>
    /// ToDoアイテムを完了にする
    /// </summary>
    public void CompleteTodo(int id)
    {
        var todo = _todos.FirstOrDefault(t => t.Id == id);
        if (todo == null)
        {
            Console.WriteLine($"ID {id} のToDoアイテムが見つかりません。");
            return;
        }

        todo.IsCompleted = true;
        SaveToFile();
        Console.WriteLine($"ToDoアイテム「{todo.Title}」を完了しました。");
    }

    /// <summary>
    /// ToDoアイテムを削除
    /// </summary>
    public void DeleteTodo(int id)
    {
        var todo = _todos.FirstOrDefault(t => t.Id == id);
        if (todo == null)
        {
            Console.WriteLine($"ID {id} のToDoアイテムが見つかりません。");
            return;
        }

        _todos.Remove(todo);
        SaveToFile();
        Console.WriteLine($"ToDoアイテム「{todo.Title}」を削除しました。");
    }

    /// <summary>
    /// ToDoアイテムをファイルに保存
    /// </summary>
    private void SaveToFile()
    {
        try
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            var json = JsonSerializer.Serialize(_todos, options);
            File.WriteAllText(_filePath, json);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"保存エラー: {ex.Message}");
        }
    }

    /// <summary>
    /// ToDoアイテムをファイルから読み込み
    /// </summary>
    private void LoadFromFile()
    {
        try
        {
            if (File.Exists(_filePath))
            {
                var json = File.ReadAllText(_filePath);
                _todos = JsonSerializer.Deserialize<List<TodoItem>>(json) ?? new List<TodoItem>();
                _nextId = _todos.Any() ? _todos.Max(t => t.Id) + 1 : 1;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"読み込みエラー: {ex.Message}");
            _todos = new List<TodoItem>();
            _nextId = 1;
        }
    }

    /// <summary>
    /// 統計情報を表示
    /// </summary>
    public void ShowStats()
    {
        var total = _todos.Count;
        var completed = _todos.Count(t => t.IsCompleted);
        var pending = total - completed;

        Console.WriteLine("\n=== 統計情報 ===");
        Console.WriteLine($"合計: {total} 件");
        Console.WriteLine($"完了: {completed} 件");
        Console.WriteLine($"未完了: {pending} 件");
        Console.WriteLine();
    }
}
