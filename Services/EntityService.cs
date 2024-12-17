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
    private static EntityService _instance { get; set; }
    
    public static EntityService GetInstance()
    {
        return _instance ??= new EntityService();
    }

    /// <summary>
    /// Contains all seeked Filesystem entities
    /// </summary>
    public List<EntityModel> ArchiveItems { get; private set; } = new();
    
    
    /// <summary>
    /// Searches all Archives inside current catalog (Set-up in Config API)
    /// </summary>
    public void OnionMain()
    {
        List<string> model = new();
        
        if (model == null) 
            throw new ArgumentNullException(nameof(model));

        string whereFind = StartupService
            .Instance
            .MainWindowViewModel
            .SettingsViewModel
            .LocalStoragePath;
        try
        {
            model.AddRange(
                Directory.EnumerateFiles(
                    whereFind,
                    "*.*",
                    SearchOption.AllDirectories));
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        finally
        {
            ArchiveItems = model
                .Select(i => new EntityModel(i))
                .ToList();
            Console.WriteLine("Files moved in global field");
        }
    }
}