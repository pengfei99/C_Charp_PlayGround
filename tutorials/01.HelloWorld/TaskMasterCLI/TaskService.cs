using System.IO;
using System.Text.Json;

namespace TaskMasterCLI;

public class TaskService
{
    // The name of the file where our data will live
    private const string FileName = "tasks.json";
    
    private List<TaskItem> _tasks = new();
    private int _nextId = 1;

    // The constructor now automatically triggers the Load routine
    public TaskService()
    {
        LoadTasksFromFile();
    }

    public void AddTask(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException("Task title cannot be empty.");
        }

        _tasks.Add(new TaskItem { Id = _nextId++, Title = title });
        SaveTasksToFile(); // Save instantly!
    }

    public List<TaskItem> GetAllTasks()
    {
        return _tasks.ToList();
    }

    public bool CompleteTask(int id)
    {
        var task = _tasks.FirstOrDefault(t => t.Id == id);
        if (task == null) return false;

        task.IsCompleted = true;
        SaveTasksToFile(); // Save instantly!
        return true;
    }

    // --- PERSISTENCE LOGIC ---

    private void SaveTasksToFile()
    {
        try
        {
            // JsonSerializerOptions makes the output JSON "pretty" and readable for humans
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(_tasks, options);
            
            // Write the JSON string to our file on disk
            File.WriteAllText(FileName, jsonString);
        }
        catch (Exception ex)
        {
            // Real-world practice: Don't let a save crash the app, but log/notify the user
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n[Error saving data to disk: {ex.Message}]");
            Console.ResetColor();
        }
    }

    private void LoadTasksFromFile()
    {
        try
        {
            // If the file doesn't exist yet, just exit quietly (first time running the app)
            if (!File.Exists(FileName)) return;

            string jsonString = File.ReadAllText(FileName);
            
            // Deserialize back into our List object
            var deserializedTasks = JsonSerializer.Deserialize<List<TaskItem>>(jsonString);
            
            if (deserializedTasks != null && deserializedTasks.Count > 0)
            {
                _tasks = deserializedTasks;
                
                // Crucial step: Set the next ID tracker to be higher than the maximum ID found 
                // in the file so we don't accidentally duplicate IDs for new tasks.
                _nextId = _tasks.Max(t => t.Id) + 1;
            }
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n[Error loading data from disk: {ex.Message}]");
            Console.ResetColor();
        }
    }
}