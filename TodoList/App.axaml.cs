using System;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;

using TodoList.Services;
namespace TodoList;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow();
        }

        base.OnFrameworkInitializationCompleted();
    }
    
    public static void ChangeTheme(bool useThemeTwo)
    {
        if (Current == null) return;

        if (useThemeTwo)
        {
            Current.Resources["ThemePrimary"] = Avalonia.Media.Brush.Parse("#6200EE");
            Current.Resources["ThemeBackground"] = Avalonia.Media.Brush.Parse("#212121");
            Current.Resources["ThemeForeground"] = Avalonia.Media.Brush.Parse("White");
        }
        else
        {
            Current.Resources["ThemePrimary"] = Avalonia.Media.Brush.Parse("#DEB887");
            Current.Resources["ThemeBackground"] = Avalonia.Media.Brush.Parse("White");
            Current.Resources["ThemeForeground"] = Avalonia.Media.Brush.Parse("Black");
        }
    }
}