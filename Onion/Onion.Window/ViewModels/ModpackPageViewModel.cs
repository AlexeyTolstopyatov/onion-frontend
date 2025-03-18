using System.Threading.Tasks;
using Onion.Window.Models;

namespace Onion.Window.ViewModels;

public class ModpackPageViewModel : NotifyPropertyChanged
{
    #pragma warning disable 
    public ModpackPageViewModel() {}
    #pragma warning restore
    
    public ModpackPageViewModel(string path)
    {
        _model = Task.FromResult(new ModpackPageModel(path)).Result;
    }
    
    private ModpackPageModel _model;

    public ModpackPageModel Model => _model;
}