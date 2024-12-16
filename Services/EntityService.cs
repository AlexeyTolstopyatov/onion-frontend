using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using Onion.Desktop.Model;

namespace Onion.Desktop.Services;

/// <summary>
/// Needs for detecting File system objects
/// and creating entities for 
/// </summary>
public class EntityService : IOnionService
{
    public static EntityService Instance => new();

    /// <summary>
    /// Contains all seeked Filesystem entities
    /// </summary>
    public List<FilesystemEntityModel> ArchiveItems { get; private set; } = new();
    
    
    /// <summary>
    /// Searches all Archives inside current catalog (Set-up in Config API)
    /// </summary>
    public void OnionMain()
    {
        string whereFind = StartupService
            .Instance
            .MainWindowViewModel
            .SettingsViewModel
            .LocalStoragePath;
        try
        {
            foreach (string file in Directory.EnumerateFiles("D:/mods", "*.*", SearchOption.AllDirectories))
            {
                ArchiveItems.Add(new FilesystemEntityModel(file));
            }
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}