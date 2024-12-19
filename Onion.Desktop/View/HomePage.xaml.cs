using System.Windows.Controls;
using Onion.Desktop.Services;

namespace Onion.Desktop.View;

public partial class HomePage : Page
{
    public HomePage()
    {
        InitializeComponent();
        DataContext = StartupService
            .Instance
            .MainWindowViewModel
            .HomeViewModel;
    }
}