using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using TodoList.Services;
using Avalonia.Data.Converters;

namespace TodoList.Views;

public partial class LogHistory : UserControl
{
    public LogHistory()
    {
        InitializeComponent();
        
        Seznam seznam = new Seznam();
        List<string> logs = seznam.GetHistoryActivity();
        foreach (var log in logs)
        {
            var label = new TextBlock();
            label.Text = log;
            label.FontSize = 12;
            label.HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Stretch;
            label.Bind(TextBlock.ForegroundProperty, new Avalonia.Markup.Xaml.MarkupExtensions.DynamicResourceExtension("ThemeForeground"));
            Scroller.Children.Add(label);
        }
    }

    private void ReturnToMain(object sender, RoutedEventArgs e)
    {
        var main = new MainWindow();
        main.Show();

        var current = (Window)TopLevel.GetTopLevel(this)!;
        current.Close();
    }

    public void DeleteHistory(object sender, RoutedEventArgs e)
    {
        Seznam seznam = new Seznam();
        seznam.DeleteActivityHistory();
        Scroller.Children.Clear();
    }
}