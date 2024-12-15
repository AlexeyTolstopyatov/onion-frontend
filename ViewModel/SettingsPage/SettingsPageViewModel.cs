using System.Runtime.CompilerServices;

namespace Onion.Desktop.ViewModel.SettingsPage;

public class SettingsPageViewModel : OnionViewModel
{
    private string _localPacksStorage = "D:\\";
    private string _minecraftPacksStorage = "D:\\Minecraft\\mods";
    private string _languageChooseString = "Which language do you use?";
    private string _minecraftPacksStorageString = "Where is Minecraft mods folder?";
    private string _localPacksStorageString = "Where you store mod-packs?";
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