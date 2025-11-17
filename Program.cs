using TodoApp;

Console.WriteLine("=== C# ToDoアプリ ===");
Console.WriteLine();

var manager = new TodoManager();

while (true)
{
    Console.WriteLine("コマンドを選択してください:");
    Console.WriteLine("1. ToDoアイテムを追加");
    Console.WriteLine("2. ToDoリストを表示");
    Console.WriteLine("3. 未完了のToDoを表示");
    Console.WriteLine("4. ToDoを完了にする");
    Console.WriteLine("5. ToDoを削除");
    Console.WriteLine("6. 統計情報を表示");
    Console.WriteLine("7. 終了");
    Console.Write("\n選択 (1-7): ");

    var choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            Console.Write("タイトルを入力: ");
            var title = Console.ReadLine() ?? "";
            if (string.IsNullOrWhiteSpace(title))
            {
                Console.WriteLine("タイトルは必須です。");
                break;
            }
            Console.Write("説明を入力 (省略可): ");
            var description = Console.ReadLine() ?? "";
            manager.AddTodo(title, description);
            break;

        case "2":
            manager.ListTodos(true);
            break;

        case "3":
            manager.ListTodos(false);
            break;

        case "4":
            Console.Write("完了にするToDoのIDを入力: ");
            if (int.TryParse(Console.ReadLine(), out int completeId))
            {
                manager.CompleteTodo(completeId);
            }
            else
            {
                Console.WriteLine("無効なIDです。");
            }
            break;

        case "5":
            Console.Write("削除するToDoのIDを入力: ");
            if (int.TryParse(Console.ReadLine(), out int deleteId))
            {
                manager.DeleteTodo(deleteId);
            }
            else
            {
                Console.WriteLine("無効なIDです。");
            }
            break;

        case "6":
            manager.ShowStats();
            break;

        case "7":
            Console.WriteLine("アプリを終了します。");
            return;

        default:
            Console.WriteLine("無効な選択です。1-7の数字を入力してください。");
            break;
    }

    Console.WriteLine();
}
