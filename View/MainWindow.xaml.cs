using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;
using MicaWPF.Controls;
using Onion.Desktop.ViewModel;
using Onion.Desktop.ViewModel.MainWindow;
using Wpf.Ui.Appearance;
using Wpf.Ui.Controls;

namespace Onion.Desktop.View;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : MicaWindow
{
    public MainWindow()
    {
        InitializeComponent();
        // DataContext = PrepareConfiguration();
        ApplicationThemeManager.ApplySystemTheme(); // Using WPF-UI controls
    }
}