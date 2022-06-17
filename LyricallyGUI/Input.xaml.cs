using System.Windows;

namespace LyricallyGUI;

public partial class Input
{
    public string InputText = string.Empty;
    
    public Input()
    {
        InitializeComponent();
    }

    private void btnSubmit_Click(object sender, RoutedEventArgs e)
    {
        if (!string.IsNullOrEmpty(TxtToken.Text))
        {
            InputText = TxtToken.Text;
            Close();
        }
        else
            MessageBox.Show("Must provide a user name in the textbox.");
    }
}