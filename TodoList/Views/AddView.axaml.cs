using System;
using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Interactivity;
using TodoList;
using TodoList.Services;

namespace TodoList;

public partial class AddView : Window
{
    public AddView()
    {
        InitializeComponent();
    }
    
    private void SubmitDetails(object? sender, RoutedEventArgs e)
    {
        string priority = PriorityTextBox.Text;
        short intPriority = -1;
        try
        {
            intPriority = short.Parse(priority);
        }
        catch (Exception ex)
        {
            PriorityTextBox.Clear();
        }
        string deadline = DeadlineTextBox.Text;
        string color = ColorTextBox.Text;
        List<string> availableColors = new List<string> {"Red", "Blue", "Green", "Yellow", "Orange"};

        if (intPriority < 0 || intPriority > 10)
        {
            PriorityTextBox.Clear();
        }

        if (!string.IsNullOrWhiteSpace(color) && !availableColors.Contains(color))
        {
            ColorTextBox.Clear();
        }

        if (!string.IsNullOrWhiteSpace(PriorityTextBox.Text) && !string.IsNullOrWhiteSpace(DeadlineTextBox.Text)
                                                             && !string.IsNullOrWhiteSpace(ColorTextBox.Text))
        {
            List<string> output = new List<string> {PriorityTextBox.Text, DeadlineTextBox.Text, ColorTextBox.Text};
            this.Close(output);
        }
    }
}