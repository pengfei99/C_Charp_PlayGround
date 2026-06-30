# C# Tutorial 2

If you want to learn the basic on how to create a C# dotnet project, you need to check the `tutorial 1 TestApp`/README.md

In this tutorial, we will learn:
1. how to separate concerns in separate classes
2. how to mange packages and reference


## Context

We want to build a professional-grade Task Management CLI Application. The architecture of the application:
- `TaskItem.cs`: task data model class which defines the attributes of a task
- `TaskService.cs`: the service layer of the application that manages our tasks (adding, viewing, and completing them).
- `Program.cs`: the user interface layer that handles user input calls the selected service and render the system output


You can find the sample code in the `.cs files`. 


### TaskService.cs

You can find the below method in the `TaskService.cs`:
- public void AddTask(string title): add a new task to the task list
- public List<TaskItem> GetAllTasks(): list all existing tasks in the list
- public bool CompleteTask(int id): set status of a task to complete
- private void SaveTasksToFile(): persist the task list to a json file
- private void LoadTasksFromFile(): load a json file to task list

You can notice we use two packages `using System.IO;` and `using System.Text.Json;`. We don't need to modify the `.csproj` to add these packages, because they native packages.

> You can notice `namespace TaskMasterCLI;` at the beginning of the file. It means this class is a part of the package `TaskMasterCLI`

### `Program.cs`

In `Program.cs`, you can notice

```cs
using TaskMasterCLI;
using Spectre.Console;
```

It means `Program.cs` is not a part of the package `TaskMasterCLI`, it uses the package `TaskMasterCLI` as dependencies.

It uses a second package `Spectre.Console`, this package is not a native C# package, so you need to modify your `.csproj`.

You can use the dotnet command to add it
```powershell
dotnet add package Spectre.Console
```
or you can edit it directly by adding 
```xml
  <ItemGroup>
    <PackageReference Include="Spectre.Console" Version="0.57.1" />
  </ItemGroup>
```