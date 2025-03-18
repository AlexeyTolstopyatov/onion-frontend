using System;
using System.Windows;
using System.Windows.Controls;
using Wpf.Ui.Appearance;

namespace Onion.Window.Views;

public partial class ModpackPage : Page
{
    public ModpackPage()
    {
        InitializeComponent();
        SwitchTheme(ApplicationThemeManager.IsMatchedDark());
    }

    private void SwitchTheme(bool isDarkTheme)
    {
        App? app = (App)Application.Current;

        Uri themeUri = isDarkTheme 
            ? new Uri("Themes/Light.xaml", UriKind.Relative)
            : new Uri("Themes/Dark.xaml", UriKind.Relative);

        ResourceDictionary themeDict = new () { Source = themeUri };
        app.Resources.MergedDictionaries.Add(themeDict);
    }
}