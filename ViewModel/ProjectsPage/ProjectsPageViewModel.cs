using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows.Input;
using Onion.Desktop.Model;
using Onion.Desktop.Services;
using Wpf.Ui.Input;

namespace Onion.Desktop.ViewModel.ProjectsPage;
public class ProjectsPageViewModel : INotifyPropertyChanged 
{
    private ObservableCollection<EntityModel?> _items = new();
    private readonly CancellationTokenSource _cancellationTokenSource;

    //private ICommand _addNewProjectCommand;
    private ICommand _removeProjectCommand = null!;

    public event PropertyChangedEventHandler? PropertyChanged;
    public ProjectsPageViewModel()
    {
        _cancellationTokenSource = new CancellationTokenSource();
        LoadDataAsync(_cancellationTokenSource.Token);
        
    }
    public ObservableCollection<EntityModel?> Items
    {
        get => _items;
        set
        {
            _items = value;
            OnPropertyChanged(nameof(Items));
            Console.WriteLine("Items updated");
        }
    }
    public ICommand RemoveProjectCommand => 
        _removeProjectCommand ??= new RelayCommand<EntityModel>(RemoveProject); // T = <EntityModel>

    private async void RemoveProject(EntityModel? project)
    {
        if (project == null) return;
        try 
        {
            await Task.Run(() => File.Delete(project.FilePath)); // remove file on disk
            Items.Remove(project); 
        }
        catch (Exception ex) 
        {
             // Log Error
             OnUnload();
        }
    }
    private void LoadDataAsync(CancellationToken cancellationToken) 
    { 
        try
        {
            EntityService
                .GetInstance()
                .OnionMain();

            var listFiles = EntityService
                .GetInstance()
                .ArchiveItems;
            
            Console.WriteLine($"Weight: {listFiles.Count}. External: {EntityService.GetInstance().ArchiveItems.Count}");
            
            Console.WriteLine("EntityService Invoked");

            // if (cancellationToken.IsCancellationRequested)
            // {
            //     Console.WriteLine("Cancel request from " + nameof(LoadDataAsync));
            //     return;
            // }

            foreach (var file in listFiles) 
            {
                Console.WriteLine(file);
                Items.Add(file); 
            } 
         
        }
        catch (OperationCanceledException) 
        { 
            Console.WriteLine("Cancelled (internal exception)");
              // Task was canceled. Maybe abort
        }
        catch (Exception ex) 
        {
            Console.WriteLine(ex.ToString());
             // ... Give trouble now!
        }
    }

    private void OnUnload()
    {
        _cancellationTokenSource.Cancel();
        Console.WriteLine("Cancelled!");
    }

    private void OnPropertyChanged(string propertyName)
    {
        Console.WriteLine("OnPropertyChanged Invoked");
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}