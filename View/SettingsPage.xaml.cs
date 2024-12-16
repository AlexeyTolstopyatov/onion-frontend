using System.Windows.Controls;
using Onion.Desktop.Services;

namespace Onion.Desktop.View;

public partial class SettingsPage : Page
{
    public SettingsPage()
    {
        InitializeComponent();
        DataContext = StartupService
            .Instance
            .MainWindowViewModel
            .SettingsViewModel;
    }
}