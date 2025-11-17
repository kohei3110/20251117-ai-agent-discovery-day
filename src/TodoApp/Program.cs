using TodoApp;

class Program
{
    static void Main(string[] args)
    {
        var manager = new TodoManager();
        var running = true;

        Console.WriteLine("═══════════════════════════════════");
        Console.WriteLine("   C# To-Do アプリケーション");
        Console.WriteLine("═══════════════════════════════════");
        Console.WriteLine();

        while (running)
        {
            DisplayMenu(manager);
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddTodo(manager);
                    break;
                case "2":
                    ListTodos(manager);
                    break;
                case "3":
                    ViewTodoDetails(manager);
                    break;
                case "4":
                    CompleteTodo(manager);
                    break;
                case "5":
                    UncompleteTodo(manager);
                    break;
                case "6":
                    DeleteTodo(manager);
                    break;
                case "0":
                    running = false;
                    Console.WriteLine("\nアプリケーションを終了します。");
                    break;
                default:
                    Console.WriteLine("\n無効な選択です。もう一度お試しください。");
                    break;
            }

            if (running)
            {
                Console.WriteLine("\nEnterキーを押して続行...");
                Console.ReadLine();
            }
        }
    }

    static void DisplayMenu(TodoManager manager)
    {
        Console.Clear();
        Console.WriteLine("═══════════════════════════════════");
        Console.WriteLine($"   未完了タスク: {manager.GetIncompleteCount()}件");
        Console.WriteLine("═══════════════════════════════════");
        Console.WriteLine();
        Console.WriteLine("メニュー:");
        Console.WriteLine("  1. To-Do を追加");
        Console.WriteLine("  2. To-Do 一覧を表示");
        Console.WriteLine("  3. To-Do の詳細を表示");
        Console.WriteLine("  4. To-Do を完了にする");
        Console.WriteLine("  5. To-Do を未完了に戻す");
        Console.WriteLine("  6. To-Do を削除");
        Console.WriteLine("  0. 終了");
        Console.WriteLine();
        Console.Write("選択してください: ");
    }

    static void AddTodo(TodoManager manager)
    {
        Console.WriteLine("\n─── To-Do の追加 ───");
        Console.Write("タイトル: ");
        var title = Console.ReadLine() ?? "";

        if (string.IsNullOrWhiteSpace(title))
        {
            Console.WriteLine("エラー: タイトルは必須です。");
            return;
        }

        Console.Write("説明（省略可能）: ");
        var description = Console.ReadLine() ?? "";

        var todo = manager.AddTodo(title, description);
        Console.WriteLine($"\n✓ To-Do を追加しました（ID: {todo.Id}）");
    }

    static void ListTodos(TodoManager manager)
    {
        Console.WriteLine("\n─── To-Do 一覧 ───");
        var todos = manager.GetAllTodos();

        if (todos.Count == 0)
        {
            Console.WriteLine("To-Do がありません。");
            return;
        }

        foreach (var todo in todos)
        {
            Console.WriteLine(todo.ToString());
        }
        
        Console.WriteLine($"\n合計: {todos.Count}件（未完了: {manager.GetIncompleteCount()}件）");
    }

    static void ViewTodoDetails(TodoManager manager)
    {
        Console.WriteLine("\n─── To-Do の詳細 ───");
        Console.Write("To-Do ID: ");
        
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("エラー: 無効なIDです。");
            return;
        }

        var todo = manager.GetTodoById(id);
        if (todo == null)
        {
            Console.WriteLine($"エラー: ID {id} の To-Do が見つかりません。");
            return;
        }

        Console.WriteLine();
        Console.WriteLine(todo.ToDetailedString());
    }

    static void CompleteTodo(TodoManager manager)
    {
        Console.WriteLine("\n─── To-Do を完了にする ───");
        Console.Write("To-Do ID: ");

        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("エラー: 無効なIDです。");
            return;
        }

        if (manager.CompleteTodo(id))
        {
            Console.WriteLine($"✓ ID {id} の To-Do を完了にしました。");
        }
        else
        {
            Console.WriteLine($"エラー: ID {id} の To-Do を完了にできませんでした。");
        }
    }

    static void UncompleteTodo(TodoManager manager)
    {
        Console.WriteLine("\n─── To-Do を未完了に戻す ───");
        Console.Write("To-Do ID: ");

        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("エラー: 無効なIDです。");
            return;
        }

        if (manager.UncompleteTodo(id))
        {
            Console.WriteLine($"✓ ID {id} の To-Do を未完了に戻しました。");
        }
        else
        {
            Console.WriteLine($"エラー: ID {id} の To-Do を未完了に戻せませんでした。");
        }
    }

    static void DeleteTodo(TodoManager manager)
    {
        Console.WriteLine("\n─── To-Do を削除 ───");
        Console.Write("To-Do ID: ");

        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("エラー: 無効なIDです。");
            return;
        }

        var todo = manager.GetTodoById(id);
        if (todo == null)
        {
            Console.WriteLine($"エラー: ID {id} の To-Do が見つかりません。");
            return;
        }

        Console.Write($"本当に「{todo.Title}」を削除しますか？ (y/N): ");
        var confirm = Console.ReadLine()?.ToLower();

        if (confirm == "y" || confirm == "yes")
        {
            if (manager.DeleteTodo(id))
            {
                Console.WriteLine($"✓ ID {id} の To-Do を削除しました。");
            }
            else
            {
                Console.WriteLine($"エラー: ID {id} の To-Do を削除できませんでした。");
            }
        }
        else
        {
            Console.WriteLine("削除をキャンセルしました。");
        }
    }
}
