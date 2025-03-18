using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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

    public MainWindowViewModel()
    {
        _openDialogCommand = new RelayCommand<string>(OpenDialog!);
        _minecraftPathDialogCommand = new RelayCommand<string>(MinecraftPathOpenDialog!);
        _allowOperatorBlocks = false;
        _expandedOperatorsBlocks = false;
    }

    /// <summary>
    /// Opens OpenFileDialog for choosing compressed list
    /// </summary>
    /// <param name="x"></param>
    private void OpenDialog(string x)
    {
        VistaFileDialog dialog = new VistaOpenFileDialog()
        {
            CheckFileExists = true,
            CheckPathExists = true,
            ValidateNames = true,
            Filter = "Архив .zip|*.zip"
        };
        dialog.ShowDialog();
        
    }

    /// <summary>
    /// Memorize selected Minecraft catalog for next usage
    /// from other models/constructors.
    /// </summary>
    /// <param name="x"></param>
    private void MinecraftPathOpenDialog(string x)
    {
        VistaFolderBrowserDialog dialog = new();
        dialog.ShowDialog();

        if (dialog.SelectedPath != string.Empty)
            MinecraftPath = dialog.SelectedPath;
        
        ShowPage(new ModpackPage{DataContext = new ModpackPageViewModel(dialog.SelectedPath)});
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
    
    private string? _minecraftPath;
    private Page _currentContent;

    public Page CurrentContent
    {
        get => _currentContent;
        set => SetField(ref _currentContent, value);
    }

    #region PublicFields
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