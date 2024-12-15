using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Onion.Desktop.ViewModel;

public class OnionViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// Изменяет свойство под указанным названием
    /// </summary>
    protected virtual void OnPropertyChanged(string propertyName)
    {
        if (string.IsNullOrEmpty(propertyName))
            return;
        
        PropertyChanged?.Invoke(
            this, 
            new PropertyChangedEventArgs(propertyName)
        );
    }
}