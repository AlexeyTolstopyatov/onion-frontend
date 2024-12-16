using System.IO;
using System.Xml.Linq;
using Onion.Desktop.Model.MainWindow;
using Onion.Desktop.ViewModel.HomePage;
using Onion.Desktop.ViewModel.MainWindow;
using Onion.Desktop.ViewModel.SettingsPage;
using Wpf.Ui.Controls;

namespace Onion.Desktop.Services;

public struct StartupConfig
{
    public string Language;
    public string Minecraft;
    public string Modifications;
    public int PanelMode;
}
public class StartupService : IOnionService
{
    public static StartupService Instance { get; } = new();

    private MainWindowViewModel _mainWindowModel;

    public MainWindowViewModel MainWindowViewModel => 
        _mainWindowModel;

    /// <summary>
    /// Loading information from XML documents
    /// or get default values from Application's insights.
    /// </summary>
    /// <returns></returns>
    public void OnionMain()
    {
        XDocument configuration = XDocument.Load(AppDomain.CurrentDomain.BaseDirectory + "Onion.Config.xml");
        
        string package =
            configuration
                .Descendants()
                .Elements("Culture")
                .First().Value;

        StartupConfig cfg = new()
        {
            Language = configuration
                .Document!
                .Descendants()
                .Elements("Culture")
                .First()
                .Value,
            Minecraft = configuration
                .Document!
                .Descendants()
                .Elements("Minecraft")
                .First()
                .Value,
            Modifications = configuration
                .Document!
                .Descendants()
                .Elements("Modifications")
                .First()
                .Value,
            PanelMode = int.Parse(configuration.Document
                .Descendants()
                .Elements("NavigationPanel")
                .First()
                .Value)
        };
        
        if (File.Exists(package))
        {
            _mainWindowModel = LoadPackage(package, ref cfg);
            Console.WriteLine($"Detected language package. {package}");
        }
        else 
            _mainWindowModel = LoadPackage(ref cfg);
        
    }

    /// <summary>
    /// Loads default language package
    /// from Application insights
    /// </summary>
    /// <returns>All View-Models with updated culture</returns>
    private MainWindowViewModel LoadPackage(ref StartupConfig config)
    {
        LanguagesModel model = new()
        {
            SettingsLanguage = "Choose language",
            SettingsMinecraftCatalog = "Select Minecraft catalog",
            SettingsModCatalog = "Select your modification's catalog",
            NavigationAbout = "About",
            NavigationYourProjects = "Your Projects",
            NavigationSettings = "Settings",
            AboutTextBlock = "Simple Modifications manager, which helps to run Java-Minecraft with your favourite modifications" +
                             "Helps you to build new mod-packs and saves them in .ZIP capsules for future usage." + 
                             "\n\n You may not open /mod directories and by-hand copy every time many Java Archives, and you may " +
                             "use Onion and double-click to selected Mod-pack. It will copied to /mod directory."
        };
        HomePageViewModel homeViewModel = new()
        {
            AboutMessage = model.AboutTextBlock
        };
        
        SettingsPageViewModel settingsViewModel = new(
            config.Modifications,
            config.Minecraft,
            model.SettingsLanguage,
            model.SettingsMinecraftCatalog,
            model.SettingsModCatalog,
            config.Language);

        return new MainWindowViewModel(settingsViewModel, homeViewModel)
        {
            NavigationSettings = model.NavigationSettings,
            NavigationProjects = model.NavigationYourProjects,
            NavigationAbout = model.NavigationAbout,
            DisplayMode = 0
        };
    }
    
    /// <summary>
    /// Loads detected package
    /// from Application's root catalog
    /// </summary>
    /// <returns>All View-Model with updated culture</returns>
    private MainWindowViewModel LoadPackage(string path, ref StartupConfig config)
    {
        XDocument culture = XDocument.Load(AppDomain.CurrentDomain.BaseDirectory + path);
        LanguagesModel model = new()
        {
            AboutTextBlock = culture.Document!.Descendants("Culture").Elements("AboutMessage").First().Value,
            NavigationAbout = culture.Document!.Descendants("Culture").Elements("NavigationAbout").First().Value,
            NavigationSettings = culture.Document!.Descendants("Culture").Elements("NavigationSettings").First().Value,
            NavigationYourProjects = culture.Document!.Descendants("Culture").Elements("NavigationExplorer").First().Value,
            SettingsLanguage = culture.Document!.Descendants("Culture").Elements("SettingsLanguage").First().Value,
            SettingsMinecraftCatalog = culture.Document!.Descendants("Culture").Elements("SettingsMinecraftCatalog").First().Value,
            SettingsModCatalog = culture.Document!.Descendants("Culture").Elements("SettingsModCatalog").First().Value
        };
        
        HomePageViewModel homeViewModel = new()
        {
            AboutMessage = model.AboutTextBlock
        };
        
        SettingsPageViewModel settingsViewModel = new(
            config.Modifications,
            config.Minecraft,
            model.SettingsLanguage,
            model.SettingsMinecraftCatalog,
            model.SettingsModCatalog,
            config.Language);

        return new MainWindowViewModel(settingsViewModel, homeViewModel)
        {
            NavigationSettings = model.NavigationSettings,
            NavigationProjects = model.NavigationYourProjects,
            NavigationAbout = model.NavigationAbout,
            DisplayMode = (NavigationViewPaneDisplayMode)config.PanelMode
        };
    }

}