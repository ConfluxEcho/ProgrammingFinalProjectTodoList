using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Avalonia.Media;

namespace TodoList.Services;
using System.IO;

public class Seznam
{
    public static Seznam Instance = new Seznam();

    public string Path = "../../../Services/Database.txt";

    public void LogActivity(string action)
    {
        string logLine = $"[{DateTime.Now:yyyy-MM-dd HH:mm}] {action}";
        File.AppendAllText("../../../Services/ActivityHistory.txt", logLine + Environment.NewLine);
    }

    public void DeleteActivityHistory()
    {
        File.WriteAllLines("../../../Services/ActivityHistory.txt", new List<string>());
    }

    public List<string> GetHistoryActivity()
    {
        return File.ReadAllLines("../../../Services/ActivityHistory.txt").ToList();
    }
    
    public void DeleteTaskFromList(TaskDefinition task)
    {
        List<TaskDefinition> tasks = GetTasksFromList();
        var taskToDelete = tasks.FirstOrDefault(x => x.Name == task.Name);
        if (taskToDelete != null)
        {
            tasks.Remove(taskToDelete);
        }
        SaveAllTasks(tasks);
    }

    public void AddTaskToList(TaskDefinition task)
    {
        List<TaskDefinition> tasks = GetTasksFromList();
        tasks.Add(task);

        List<string> newLines = new List<string>();
        foreach (var T in tasks)
        {
            string line = $"{T.Name};{T.Priority};{T.Deadline};{T.Finished};{T.Color}";
            newLines.Add(line);
        }
        
        File.WriteAllLines(Path, newLines);
    }
    
    public void SaveAllTasks(List<TaskDefinition> tasks)
    {
        List<string> newLines = new List<string>();
        foreach (var T in tasks)
        {
            string line = $"{T.Name};{T.Priority};{T.Deadline};{T.Finished};{T.Color}";
            newLines.Add(line);
        }
    
        File.WriteAllLines(Path, newLines);
    }

    public List<TaskDefinition> GetTasksFromList()
    {
        List<TaskDefinition> tasks = new List<TaskDefinition>();

        if (!File.Exists(Path)) return tasks;

        var lines = File.ReadAllLines(Path);
        
        foreach(var line in lines)
        {
            if (string.IsNullOrWhiteSpace(line)) continue;

            try
            {
                string[] parts = line.Split(';');

                if (parts.Length >= 5)
                {
                    string name = parts[0].Trim();
                    short priority = short.Parse(parts[1].Trim());
                    string deadline = parts[2].Trim();
                    bool finished = bool.Parse(parts[3].Trim());
                    string color = parts[4].Trim();
                    
                    tasks.Add(new TaskDefinition(name, priority, deadline, finished, color));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error parsing line: {e.Message}");
            }
        }

        return tasks;
    }
}