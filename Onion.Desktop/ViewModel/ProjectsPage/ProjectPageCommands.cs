using System.Windows.Input;
using Onion.Desktop.Model;
using Onion.Desktop.Reflect;
using Onion.Desktop.Services;
using Wpf.Ui.Input;

namespace Onion.Desktop.ViewModel.ProjectsPage;

public partial class ProjectsPageViewModel
{
    private ICommand _enumerateFilesCommand;
    private string _homeDirectoryFlag = "/";
    /// <summary>
    /// Returns Next directory by set modification catalog
    /// </summary>
    private async void EnumerateFilesIn(string? path)
    {
        // Remember: path is abstract path, which contains only next catalog by "Home"
        // Moves <SettingsModCatalogPath>\path
        // Prepare reported catalog
        if (path == _homeDirectoryFlag)
            // Move backwards
            return;
        
        // Call driver
        await Task.Run(EntityService.GetInstance().OnionMain);
    }

    public ICommand EnumerateFilesCommand
    {
        get => _enumerateFilesCommand;
        set
        {
            // fixme: make correct driver call, instead.
            _enumerateFilesCommand = value;
            OnPropertyChanged(nameof(EnumerateFilesCommand));
            _enumerateFilesCommand.Execute(null);
        }
    }
    
}