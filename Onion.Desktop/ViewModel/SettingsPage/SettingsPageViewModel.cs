namespace Onion.Desktop.ViewModel.SettingsPage;

public class SettingsPageViewModel : OnionViewModel
{
    private string _localPacksStorage;
    private string _minecraftPacksStorage;
    private string _languageChooseString;
    private string _minecraftPacksStorageString;
    private string _localPacksStorageString;
    private string _language;

    public SettingsPageViewModel(
        string localPacksStorage, 
        string minecraftPacksStorage, 
        string languageChooseString, 
        string minecraftPacksStorageString, 
        string localPacksStorageString, 
        string language)
    {
        _localPacksStorage = localPacksStorage;
        _minecraftPacksStorage = minecraftPacksStorage;
        _languageChooseString = languageChooseString;
        _minecraftPacksStorageString = minecraftPacksStorageString;
        _localPacksStorageString = localPacksStorageString;
        _language = language;
    }

    public string LanguagePackage
    {
        get => _language;
        set
        {
            _language = value;
            OnPropertyChanged(nameof(LanguagePackage));
        }
    }
    public string LanguageChooseString
    {
        get => _languageChooseString;
        set
        {
            _languageChooseString = value;
            OnPropertyChanged(nameof(LanguageChooseString));
        }
    }

    public string LocalStorageString
    {
        get => _localPacksStorageString;
        set
        {
            _localPacksStorageString = value;
            OnPropertyChanged(nameof(LocalStorageString));
        }
    }

    public string LocalStoragePath
    {
        get => _localPacksStorage;
        set
        {
            _localPacksStorage = value;
            OnPropertyChanged(nameof(LocalStoragePath));
        }
    }

    public string MinecraftStoragePath
    {
        get => _minecraftPacksStorage;
        set
        {
            _minecraftPacksStorage = value;
            OnPropertyChanged(nameof(MinecraftStoragePath));
        }
    }

    public string MinecraftStoragePathString
    {
        get => _minecraftPacksStorageString;
        set
        {
            _minecraftPacksStorageString = value;
            OnPropertyChanged(nameof(MinecraftStoragePathString));
        }
    }
}