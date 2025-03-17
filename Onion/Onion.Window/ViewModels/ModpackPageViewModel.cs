using Onion.Window.Models;

namespace Onion.Window.ViewModels;

public class ModpackPageViewModel : NotifyPropertyChanged
{
    public ModpackPageViewModel(string path)
    {
        _model = new ModpackPageModel(path);
    }
    
    private ModpackPageModel _model;

    public ModpackPageModel Model => _model;
}