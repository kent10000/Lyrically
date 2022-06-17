#region

using System.Windows.Controls;

// ReSharper disable CommentTypo

#endregion

namespace LyricallyGUI;

/// <summary>
///     Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow
{
    
    private static readonly LyricGetter LyricPage = new();
    private static readonly Settings Settings = new();
    private static ContentControl? _contentControl;

    public MainWindow()
    {
        InitializeComponent();
        _contentControl = ContentControl;
        _contentControl.Content = LyricPage;

    }
    public static void ToggleContent()
    {
        if (_contentControl != null && _contentControl.Content.Equals(LyricPage))
        {
            _contentControl.Content = Settings;
        }
        else
        {
            if (_contentControl != null) _contentControl.Content = LyricPage;
        }
    }
}