using System.Text.Json;

namespace TodoApp;

/// <summary>
/// Manages the to-do list and provides CRUD operations
/// </summary>
public class TodoManager
{
    private List<TodoItem> _todos;
    private int _nextId;
    private readonly string _dataFilePath;

    public TodoManager(string dataFilePath = "todos.json")
    {
        _todos = new List<TodoItem>();
        _nextId = 1;
        _dataFilePath = dataFilePath;
        LoadFromFile();
    }

    /// <summary>
    /// Adds a new to-do item
    /// </summary>
    public TodoItem AddTodo(string title, string description = "")
    {
        var todo = new TodoItem
        {
            Id = _nextId++,
            Title = title,
            Description = description
        };
        _todos.Add(todo);
        SaveToFile();
        return todo;
    }

    /// <summary>
    /// Gets all to-do items
    /// </summary>
    public List<TodoItem> GetAllTodos()
    {
        return _todos.OrderBy(t => t.IsCompleted).ThenBy(t => t.Id).ToList();
    }

    /// <summary>
    /// Gets a specific to-do item by ID
    /// </summary>
    public TodoItem? GetTodoById(int id)
    {
        return _todos.FirstOrDefault(t => t.Id == id);
    }

    /// <summary>
    /// Marks a to-do item as completed
    /// </summary>
    public bool CompleteTodo(int id)
    {
        var todo = GetTodoById(id);
        if (todo != null && !todo.IsCompleted)
        {
            todo.IsCompleted = true;
            todo.CompletedAt = DateTime.Now;
            SaveToFile();
            return true;
        }
        return false;
    }

    /// <summary>
    /// Marks a to-do item as incomplete
    /// </summary>
    public bool UncompleteTodo(int id)
    {
        var todo = GetTodoById(id);
        if (todo != null && todo.IsCompleted)
        {
            todo.IsCompleted = false;
            todo.CompletedAt = null;
            SaveToFile();
            return true;
        }
        return false;
    }

    /// <summary>
    /// Deletes a to-do item
    /// </summary>
    public bool DeleteTodo(int id)
    {
        var todo = GetTodoById(id);
        if (todo != null)
        {
            _todos.Remove(todo);
            SaveToFile();
            return true;
        }
        return false;
    }

    /// <summary>
    /// Gets the count of incomplete to-do items
    /// </summary>
    public int GetIncompleteCount()
    {
        return _todos.Count(t => !t.IsCompleted);
    }

    /// <summary>
    /// Saves to-do items to a JSON file
    /// </summary>
    private void SaveToFile()
    {
        try
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            var json = JsonSerializer.Serialize(new
            {
                NextId = _nextId,
                Todos = _todos
            }, options);
            File.WriteAllText(_dataFilePath, json);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"データの保存中にエラーが発生しました: {ex.Message}");
        }
    }

    /// <summary>
    /// Loads to-do items from a JSON file
    /// </summary>
    private void LoadFromFile()
    {
        try
        {
            if (File.Exists(_dataFilePath))
            {
                var json = File.ReadAllText(_dataFilePath);
                var data = JsonSerializer.Deserialize<JsonElement>(json);
                
                _nextId = data.GetProperty("NextId").GetInt32();
                
                var todosArray = data.GetProperty("Todos");
                _todos = JsonSerializer.Deserialize<List<TodoItem>>(todosArray.GetRawText()) 
                         ?? new List<TodoItem>();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"データの読み込み中にエラーが発生しました: {ex.Message}");
            _todos = new List<TodoItem>();
            _nextId = 1;
        }
    }
}
