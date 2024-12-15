using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using Onion.Desktop.Model.MainWindow;
using Onion.Desktop.Reflect;
using Onion.Desktop.View;
using Wpf.Ui.Controls;
using Wpf.Ui.Input;

namespace Onion.Desktop.ViewModel.MainWindow;

public partial class MainWindowViewModel : OnionViewModel
{
    // Navigation menu-items Content
    private string _yourModPacks = "Your Projects";
    private string _yourSettings = "Settings";

    public MainWindowViewModel()
    {
        
    }
    
    public string YourSettings
    {
        get => _yourSettings;
        set
        {
            _yourSettings = value;
            OnPropertyChanged(nameof(_yourSettings));
        }
    }
    
    public string YourModPacks
    {
        get => _yourModPacks;
        set
        {
            _yourModPacks = value;
            OnPropertyChanged(nameof(_yourModPacks));
        }
    }
}