using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using TodoList.Services;

namespace TodoList;

public partial class HomeWindow : UserControl
{
    private TaskDefinition? _selection;
    private string _currentTaskToggle;
    
    public HomeWindow()
    {
        InitializeComponent();

        _currentTaskToggle = "Unfinished";
        Seznam seznam = new Seznam();
        RefreshTasks(seznam.GetTasksFromList());
    }

    private void ReturnToMain(object? sender, RoutedEventArgs e)
    {
        var main = new MainWindow();
        main.Show();

        var current = (Window)TopLevel.GetTopLevel(this)!;
        current.Close();
    }

    public void ToggleTasks(object sender, RoutedEventArgs e)
    {
        Seznam seznam = new Seznam();
        if (_currentTaskToggle == "Unfinished")
        {
            _currentTaskToggle = "Finished";
            seznam.LogActivity($"Změna filtru: Nesplněné -> Splněné");
        }
        else
        {
            _currentTaskToggle = "Unfinished";
            seznam.LogActivity($"Změna filtru: Splněné -> Nesplněné");
        }
        var tasks = seznam.GetTasksFromList();
        RefreshTasks(tasks);
    }

    public void ShowDetails(object? sender, RoutedEventArgs e)
    {
        if (_selection == null)
        {
            return;
        }
        var details = new ViewWindow(_selection.Priority, _selection.Deadline, _selection.Color,  _selection.Finished);
        details.Show();
        
        Seznam seznam = new Seznam();
        seznam.LogActivity($"Zobrazení detailů: {_selection.Name}");
    }

    private void MarkTaskAsDone(object? sender, RoutedEventArgs e)
    {
        if (_selection == null)
        {
            return;
        }

        Seznam seznam = new Seznam();
        var tasks = seznam.GetTasksFromList();

        var taskToUpdate = tasks.FirstOrDefault(t => t.Name == _selection.Name);

        if (taskToUpdate != null)
        {
            taskToUpdate.Finished = true;

            seznam.SaveAllTasks(tasks);
            seznam.LogActivity($"Označení úkolu jako dokončené: {_selection.Name}");
            
            _selection = null;
            RefreshTasks(seznam.GetTasksFromList());
        }
    }
    
    private void RefreshTasks(List<TaskDefinition> tasks)
    {
        TaskContainer.Children.Clear();
        foreach (var task in tasks)
        {
            if (_currentTaskToggle == "Unfinished" && task.Finished)
            {
                continue;
            }
            if (_currentTaskToggle == "Finished" && !task.Finished)
            {
                continue;
            }
            
            Console.WriteLine(task.Name);
            var button = new Button
            {
                Content = task.Name,
                Margin = new Thickness(10),
                FontSize = 35,
                Background = Avalonia.Media.Brush.Parse(task.Color),
                HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Stretch
            };
            
            button.Click += (sender, e) => 
            {
                _selection = task;
                foreach (var b in TaskContainer.Children.Cast<Button>())
                {
                    Seznam seznam = new Seznam();
                    var tasks = seznam.GetTasksFromList();
                    var taskToUpdate = tasks.FirstOrDefault(t => t.Name == b.Content?.ToString());
                    if (taskToUpdate != null)
                    {
                        b.Background = Avalonia.Media.Brush.Parse(taskToUpdate.Color);
                        b.Foreground = Avalonia.Media.Brush.Parse("White");
                    }
                }
                button.Background = Avalonia.Media.Brush.Parse("White");
                button.Foreground = Avalonia.Media.Brush.Parse("Black");
                Console.WriteLine($"Selected task changed to: {_selection.Name}");
            };

            TaskContainer.Children.Add(button);
        }
    }

    private async void AddTask(object? sender, RoutedEventArgs e)
    {
        Seznam seznam = new Seznam();
        var taskName = TaskTextBox.Text;
        Console.WriteLine(taskName);

        if (taskName == null)
        {
            return;
        }

        var tasks = seznam.GetTasksFromList();
        var found = tasks.FirstOrDefault(t => t.Name == taskName);
        
        Console.WriteLine($"Tasks count: {tasks.Count}");

        foreach (var t in tasks)
        {
            Console.WriteLine($"'{t.Name}'");
        }
        
        if (found != null)
        {
            Console.WriteLine("Nope");
            return;
        }
        
        var main = new AddView();
        List<string>? taskDetails = null;
        //main.Show();
        
        var topLevel = TopLevel.GetTopLevel(this);
        if (topLevel is Window parentWindow)
        {
            taskDetails = await main.ShowDialog<List<string>>(parentWindow);
        }
        else
        {
            // Fallback if it can't find the parent window
            main.Show();
            return;
        }
        
        if (taskDetails.Count == 0)
        {
            Console.WriteLine("Task creation cancelled.");
            return;
        }

        short priority = short.Parse(taskDetails[0]);
        string deadline = taskDetails[1];
        string color = taskDetails[2];
        
        seznam.AddTaskToList(new TaskDefinition(taskName, priority, deadline, false, color));
        RefreshTasks(seznam.GetTasksFromList());
        seznam.LogActivity($"Přidání nového úkolu: {taskName}");
    }
    
    public void DeleteTask(object? sender, RoutedEventArgs e)
    {
        if (_selection == null)
        {
            return;
        }
        
        Seznam seznam = new Seznam();
        seznam.DeleteTaskFromList(_selection);
        RefreshTasks(seznam.GetTasksFromList());
        seznam.LogActivity($"Odstranění úkolu: {_selection.Name}");
    }
}