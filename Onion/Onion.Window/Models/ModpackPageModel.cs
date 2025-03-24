using System.Data;
using System.IO;
using System.Threading.Tasks;
using Onion.Window.Services;

namespace Onion.Window.Models;

public sealed class ModpackPageModel
{
    private string _path;
    
    public ModpackPageModel(string path)
    {
        _path = path;
        if (System.IO.Path.HasExtension(path)) 
            ProcessDataFromCompressedDirectory();
        else 
            _ = ProcessDataFromDirectory();

        Path = @"Эта страница содержит таблицу с информацией о содержащихся модификациях и модулях, для Minecraft.";
    }
    /// <summary>
    /// Processes models DataTables using list of mods
    /// </summary>
    private Task ProcessDataFromDirectory()
    {
        ModInfoLoader mInst = new();
        EntriesDataTable = mInst.LoadModsData(_path);
        DataTable = mInst.LoadOnionScheme();
        Name = (mInst.Name != string.Empty) ? mInst.Name!.ToUpper() : "БЕЗ НАЗВАНИЯ";
        WarningManifestNotFound = (mInst.Name != string.Empty) ? "" : "Информации о каталоге нет.";

        return Task.CompletedTask;
    }
    /// <summary>
    /// Processes models DataTables using compressed list
    /// </summary>
    private void ProcessDataFromCompressedDirectory()
    {
        
    }
    /// <summary>
    /// Onion short information table about modpack 
    /// </summary>
    public DataTable? DataTable
    {
        get;
        set;
    }
    /// <summary>
    /// List of files and loaders metadata
    /// </summary>
    public DataTable? EntriesDataTable
    {
        get;
        set;
    }
    /// <summary>
    /// Name of modpack
    /// </summary>
    public string? Name
    {
        get;
        set;
    }

    /// <summary>
    /// Path of modpack
    /// </summary>
    public string? Path
    {
        get;
        set;
    }
    /// <summary>
    /// Shows when Onion Manifest not exists 
    /// </summary>
    public string WarningManifestNotFound
    {
        get;
        set;
    }
    /// <summary>
    /// Path of .minecraft/mod for moving of copying
    /// selected modifications package.
    /// </summary>
    public string? MinecraftModsPath
    {
        get;
        set;
    }
}