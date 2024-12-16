namespace Onion.Desktop.ViewModel.ProjectsPage;

public class ProjectsPageViewModel : OnionViewModel
{
    private List<string> _items;

    public IEnumerable<string> Items
    {
        get => _items;
        set
        {
            _items = new List<string>(value);
            OnPropertyChanged(nameof(Items));
        }
    }
}