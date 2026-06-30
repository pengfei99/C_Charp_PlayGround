using TaskMasterCLI;
using Spectre.Console;

// Initialize our service
TaskService taskService = new TaskService();
bool running = true;

// 1. Render a beautiful header Panel
AnsiConsole.Write(
    new Panel("[bold cyan]TASKMASTER CLI v2.0[/]")
        .Header("[yellow]Welcome[/]")
        .Border(BoxBorder.Rounded)
        .BorderColor(Color.Cyan1)
        .Padding(1, 1, 1, 1)
);

while (running)
{
    Console.WriteLine("\nChoose an option:");
    Console.WriteLine("1. View Tasks");
    Console.WriteLine("2. Add Task");
    Console.WriteLine("3. Complete a Task");
    Console.WriteLine("4. Exit");
    Console.Write("\nSelect [1-4]: ");

    string? input = Console.ReadLine();

    switch (input)
    {
        case "1":
            DisplayTasks(taskService);
            break;
        case "2":
            AddTaskUI(taskService);
            break;
        case "3":
            CompleteTaskUI(taskService);
            break;
        case "4":
            running = false;
            Console.WriteLine("Goodbye! Keep crushing your goals.");
            break;
        default:
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Invalid option. Please try again.");
            Console.ResetColor();
            break;
    }
}

// UI Helper Methods
void DisplayTasks(TaskService service)
{
    var tasks = service.GetAllTasks();
    Console.WriteLine("\n--- YOUR TASKS ---");
    
    if (tasks.Count == 0)
    {
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine("No tasks found. Add some code to do!");
        Console.ResetColor();
        return;
    }

    foreach (var task in tasks)
    {
        string status = task.IsCompleted ? "[✓] Done" : "[ ] Pending";
        if (task.IsCompleted) Console.ForegroundColor = ConsoleColor.Green;
        
        Console.WriteLine($"{task.Id}. {status} - {task.Title} (Created: {task.CreatedAt:yyyy-MM-dd HH:mm})");
        Console.ResetColor();
    }
}

void AddTaskUI(TaskService service)
{
    Console.Write("\nEnter task title: ");
    string? title = Console.ReadLine();
    
    try
    {
        service.AddTask(title ?? string.Empty);
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Task added successfully!");
    }
    catch (ArgumentException ex)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"Error: {ex.Message}");
    }
    finally
    {
        Console.ResetColor();
    }
}

void CompleteTaskUI(TaskService service)
{
    Console.Write("\nEnter the ID of the task to complete: ");
    if (int.TryParse(Console.ReadLine(), out int id))
    {
        bool success = service.CompleteTask(id);
        if (success)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Task #{id} marked as complete!");
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Task ID not found.");
        }
    }
    else
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Please enter a valid numeric ID.");
    }
    Console.ResetColor();
}