using System.Windows.Input;
using System.Xml.Linq;
using MicaWPF.Controls;
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
        _writeManifestCommand = new RelayCommand<MicaWindow>(WriteManifest!);
    }

    public ICommand WriteManifestCommand
    {
        get => _writeManifestCommand;
        set => SetField(ref _writeManifestCommand, value);
    }

    private void WriteManifest(MicaWindow window)
    {
        XDocument manifest = new(
            new XElement(
                "modpack", 
                new XElement("name", Model.Name), 
                new XElement("loader", Model.Loader), 
                new XElement("loaderVersion", Model.LoaderVersion), 
                new XElement("minecraftVersion", Model.MinecraftVersion)));
        
        manifest.Save(Model.Path! + "\\OnionTable.oxt");
        window.Close();
    }
}