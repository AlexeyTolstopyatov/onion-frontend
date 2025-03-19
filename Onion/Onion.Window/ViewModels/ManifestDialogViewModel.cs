using System.Windows.Input;
using Onion.Window.Models;
using Wpf.Ui.Input;
using MessageBox = Wpf.Ui.Controls.MessageBox;

namespace Onion.Window.ViewModels;

public class ManifestDialogViewModel : NotifyPropertyChanged
{
    public ManifestDialogModel Model { get; }
    private ICommand _writeManifestCommand;
    
    #pragma warning disable
    public ManifestDialogViewModel()
    {
    }
    #pragma warning restore
    
    public ManifestDialogViewModel(ManifestDialogModel model)
    {
        Model = model;
        _writeManifestCommand = new RelayCommand<string>(WriteManifest!);
    }

    public ICommand WriteManifestCommand
    {
        get => _writeManifestCommand;
        set => SetField(ref _writeManifestCommand, value);
    }

    private void WriteManifest(string _)
    {
        new MessageBox()
        {
            Title = "Onion Test",
            Content = $"{Model.Name}, {Model.Loader} {Model.LoaderVersion}, Minecraft {Model.MinecraftVersion}"
        }.ShowDialogAsync();
        
        // Set up XAML document [Name.oxt]
    }
}