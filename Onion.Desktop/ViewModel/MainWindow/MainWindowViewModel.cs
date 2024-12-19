using System.Windows.Automation.Provider;
using System.Windows.Navigation;
using Onion.Desktop.ViewModel.HomePage;
using Onion.Desktop.ViewModel.SettingsPage;
using Wpf.Ui.Controls;

namespace Onion.Desktop.ViewModel.MainWindow;

public partial class MainWindowViewModel : OnionViewModel
{
    public SettingsPageViewModel SettingsViewModel { get; set; }
    public HomePageViewModel HomeViewModel { get; set; }

    public NavigationViewPaneDisplayMode DisplayMode { get; set; }
    
    private string _navigationProjects = "Your Projects";
    private string _navigationSettings = "Settings";
    private string _navigationAbout = "About";

    public MainWindowViewModel(SettingsPageViewModel settingsViewModel, HomePageViewModel homeViewModel)
    {
        SettingsViewModel = settingsViewModel;
        HomeViewModel = homeViewModel;
    }

    public string ApplicationInfo => "Onion [alpha]";
    
    public string NavigationSettings
    {
        get => _navigationSettings;
        set
        {
            _navigationSettings = value;
            OnPropertyChanged(nameof(NavigationSettings));
        }
    }
    
    public string NavigationProjects
    {
        get => _navigationProjects;
        set
        {
            _navigationProjects = value;
            OnPropertyChanged(nameof(NavigationProjects));
        }
    }

    public string NavigationAbout
    {
        get => _navigationAbout;
        set
        {
            _navigationAbout = value;
            OnPropertyChanged(nameof(NavigationAbout));
        }
    }
}