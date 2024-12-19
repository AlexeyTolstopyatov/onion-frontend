using System.ComponentModel;
using System.IO;

namespace Onion.Desktop.Model;

public class EntityModel : INotifyPropertyChanged
{
    public string FilePath { get; set; }
    
    public EntityModel(string path)
    {
        FilePath = path;
    }
    
    public event PropertyChangedEventHandler? PropertyChanged;
    private void OnPropertyChanged(string propName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
    }
    
    public string FileName => Path.GetFileName(FilePath);
    
    private string _myState;
    public string MyState
    {
        get => _myState;
        set 
        {
            _myState = value;
            OnPropertyChanged(nameof(MyState));
        }
    }
}