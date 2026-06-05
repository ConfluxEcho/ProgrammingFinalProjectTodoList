using Avalonia.Controls;
using Avalonia.Interactivity;
using TodoList;
using TodoList.Views;

namespace TodoList;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }
    
    private void B_HomeWindow(object? sender, RoutedEventArgs e)
    {
        Content = new HomeWindow();
    }

    private void B_AboutWindow(object? sender, RoutedEventArgs e)
    {
        Content = new AboutWindow();
    }

    private void B_ActivityHistory(object? sender, RoutedEventArgs e)
    {
        Content = new LogHistory();
    }
}