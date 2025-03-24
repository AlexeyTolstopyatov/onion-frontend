using System;
using System.IO;
using System.Windows.Controls;
using System.Windows.Input;
using Onion.Window.Models;
using Onion.Window.Views;
using Ookii.Dialogs.Wpf;
using Wpf.Ui.Input;

namespace Onion.Window.ViewModels;

public class MainWindowViewModel : NotifyPropertyChanged
{
    #region WindowControlFlags

    private bool _allowOperatorBlocks;
    private bool _expandedOperatorsBlocks;
    
    #endregion

    private struct Pages
    {
        public Page ModWorkspacePage;
    }

    #pragma warning disable
    public MainWindowViewModel()
    {
        _openDialogCommand = new RelayCommand<string>(OpenDialog!);
        _minecraftPathDialogCommand = new RelayCommand<string>(MinecraftPathOpenDialog!);
        _openManifestDialog = new RelayCommand<string>(OpenManifestDialog!);
        _seekMinecraftCommand = new RelayCommand<string>(ReadHead!);
        _allowOperatorBlocks = false;
        _expandedOperatorsBlocks = false;
        OpenMinecraftMods();
    }
    #pragma warning restore
    /// <summary>
    /// Makes table of loaded mods
    /// </summary>
    private void OpenMinecraftMods()
    {
        string path = AppDomain.CurrentDomain.BaseDirectory + "head";
        try
        {
            MinecraftPath = File.ReadAllText(path);
        }
        catch
        {
            MinecraftPath = "Не обнаружено!";
        }
    }
    /// <summary>
    /// Reads head file and memorizes Minecraft
    /// directory
    /// </summary>
    private void ReadHead(string r)
    {
        string path = AppDomain.CurrentDomain.BaseDirectory + "head";
        
        __memorize:
        if (File.Exists(path))
        {
            MinecraftPath = File.ReadAllText(path);
            _currentPath = MinecraftPath;
            return;
        }
        VistaFolderBrowserDialog dialog = new()
        {
            Description = "Выберите .minecraft каталог.",
            Multiselect = false
        };
        dialog.ShowDialog();

        if (!string.IsNullOrEmpty(dialog.SelectedPath))
        {
            File.WriteAllText(path, dialog.SelectedPath);
            goto __memorize;
        }
    }
    /// <summary>
    /// Opens OpenFileDialog for choosing compressed list
    /// </summary>
    /// <param name="x"></param>
    private void OpenDialog(string x)
    {
        VistaFolderBrowserDialog dialog = new();
        
        dialog.ShowDialog();
        
        if (string.IsNullOrEmpty(dialog.SelectedPath)) 
            return;
        
        _currentPath = dialog.SelectedPath;
        ShowPage(new ModpackPage{DataContext = new ModpackPageViewModel(dialog.SelectedPath)});
        
        AllowOperatorsBlock = true;
        ExpandedOperatorBlock = true;
    }
    /// <summary>
    /// Memorize selected Minecraft catalog for next usage
    /// from other models/constructors.
    /// </summary>
    /// <param name="x"></param>
    private void MinecraftPathOpenDialog(string x)
    {
        _currentPath = _minecraftPath + "\\mods";
        ShowPage(new ModpackPage{DataContext = new ModpackPageViewModel(MinecraftPath + "\\mods")});
        
        AllowOperatorsBlock = true;
        ExpandedOperatorBlock = true;
    }
    /// <summary>
    /// Activates Manifest setup dialog
    /// </summary>
    private void OpenManifestDialog(string _)
    {
        new ManifestDialogView()
        {
            DataContext = new ManifestDialogViewModel(new ManifestDialogModel(){Path = _currentPath})
        }.ShowDialog();
    }
    /// <summary>
    /// Activates page
    /// </summary>
    /// <param name="page"></param>
    private void ShowPage(Page page)
    {
        CurrentContent = page;
    }

    private ICommand _openDialogCommand;
    private ICommand _minecraftPathDialogCommand;
    private ICommand _openManifestDialog;
    private ICommand _openLoadedModsCommand;
    private ICommand _seekMinecraftCommand;
    private string? _minecraftPath;
    private string? _currentPath;
    private Page _currentContent;

    public Page CurrentContent
    {
        get => _currentContent;
        set => SetField(ref _currentContent, value);
    }

    #region PublicFields

    public ICommand OpenManifestDialogCommand
    {
        get => _openManifestDialog;
        set => SetField(ref _openManifestDialog, value);
    }

    public ICommand SeekMinecraftCommand
    {
        get => _seekMinecraftCommand;
        set => SetField(ref _seekMinecraftCommand, value);
    }
    public ICommand OpenLoadedModsCommand
    {
        get => _openLoadedModsCommand;
        set => SetField(ref _openLoadedModsCommand, value);
    }
    public ICommand OpenDialogCommand
    {
        get => _openDialogCommand;
        set => SetField(ref _openDialogCommand, value);
    }
    
    public ICommand MinecraftPathDialogCommand
    {
        get => _minecraftPathDialogCommand;
        set => SetField(ref _minecraftPathDialogCommand, value);
    }
    public bool AllowOperatorsBlock
    {
        get => _allowOperatorBlocks; 
        set => SetField(ref _allowOperatorBlocks, value);
    }
    public bool ExpandedOperatorBlock
    {
        get => _expandedOperatorsBlocks;
        set => SetField(ref _expandedOperatorsBlocks, value);
    }
    public string? MinecraftPath
    {
        get => _minecraftPath;
        set => SetField(ref _minecraftPath, value);
    }
    #endregion
    
}