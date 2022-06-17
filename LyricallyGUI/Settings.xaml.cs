using System.Windows;

namespace LyricallyGUI;

public partial class Settings
{
    public Settings()
    {
        InitializeComponent();
    }
    
    private void ButtonSettings_OnClick(object sender, RoutedEventArgs e)
    {
        MainWindow.ToggleContent();
    }
}