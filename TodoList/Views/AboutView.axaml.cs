using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using TodoList.Services;

namespace TodoList;

public partial class AboutWindow : UserControl
{
    public AboutWindow()
    {
        InitializeComponent();
    }
    
    private void B_HomeWindow(object? sender, RoutedEventArgs e)
    {
        Content = new HomeWindow();
    }

    private void B_MainWindow(object? sender, RoutedEventArgs e)
    {
        var main = new MainWindow();
        main.Show();

        var current = (Window)TopLevel.GetTopLevel(this)!;
        current.Close();
    }
    
    private void OnThemeToggleButtonClick(object? sender, RoutedEventArgs e)
    {
        // False -> Default theme; True -> Different theme
        if(sender is Button clickedButton)
        {
            Console.WriteLine(clickedButton.Content);
            if (clickedButton.Name == "M1")
            {
                App.ChangeTheme(false);
            }
            else if (clickedButton.Name == "M2")
            {
                App.ChangeTheme(true);
            }
        }
        Seznam seznam = new Seznam();
        seznam.LogActivity("Změna motivu aplikace");
    }
}