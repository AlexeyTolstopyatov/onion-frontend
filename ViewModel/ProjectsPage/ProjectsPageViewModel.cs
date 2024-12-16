using System.Collections.ObjectModel;
using System.ComponentModel;
using Onion.Desktop.Model;
using Onion.Desktop.Services;

namespace Onion.Desktop.ViewModel.ProjectsPage;

public class ProjectsPageViewModel : INotifyPropertyChanged
{
    public ObservableCollection<FilesystemEntityModel> Items { get; set; }

    public ProjectsPageViewModel()
    {
        EntityService.Instance.OnionMain();
        Items = new();
        foreach (var item in EntityService.Instance.ArchiveItems)
        {
            Items.Add(item);
        }
        OnPropertyChanged(nameof(Items));
        Console.WriteLine("updated");
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    private void OnPropertyChanged(string propName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
    }
}