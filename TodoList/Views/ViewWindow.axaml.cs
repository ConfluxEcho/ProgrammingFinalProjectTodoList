using Avalonia.Controls;
using Avalonia.Interactivity;
using TodoList;

namespace TodoList;

public partial class ViewWindow : Window
{
    public ViewWindow(short priority, string deadline, string color, bool finished)
    {
        InitializeComponent();

        PriorityTextBox.Text = priority.ToString();
        DeadlineTextBox.Text = deadline;
        ColorTextBox.Text = color;
        FinishedTextBox.Text = finished.ToString();
    }

    public void CloseWindow(object? sender, RoutedEventArgs e)
    {
        this.Close();
    }
}