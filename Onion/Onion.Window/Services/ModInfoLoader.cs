using System;
using Tomlyn.Model;
using System.Data;
using System.IO;
using System.IO.Compression;
using System.Text.Json;
using Onion.Window.Metadata;
using Tomlyn;

namespace Onion.Window.Services;

public class ModInfoLoader
{
    public DataTable LoadModsData(string directoryPath)
    {
        DataTable table = new();
        table.Columns.Add("FileName", typeof(string));
        table.Columns.Add("Size", typeof(long));
        table.Columns.Add("Loader", typeof(string));
        table.Columns.Add("LoaderVersion", typeof(string));
        table.Columns.Add("MinecraftVersion", typeof(string));

        foreach (string filePath in Directory.GetFiles(directoryPath))
        {
            FileInfo fileInfo = new(filePath);
            try
            {
                using ZipArchive archive = ZipFile.OpenRead(filePath);
                (string loader, string version, string mcVersion) = ParseForgeBasedMetadata(archive);

                AddRow(table, fileInfo, loader, version, mcVersion);
            }
            catch (InvalidDataException)
            {
                // skip entry
                continue;
            }
        }

        return table;
    }
    /// <summary>
    /// Deserializes TOML Forge manifest
    /// </summary>
    /// <param name="archive"></param>
    /// <returns></returns>
    private (string Loader, string Version, string McVersion) ParseForgeBasedMetadata(ZipArchive archive)
    {
        ZipArchiveEntry? tomlEntry = archive.GetEntry("META-INF/mods.toml");
        if (tomlEntry != null)
        {
            using Stream stream = tomlEntry.Open();
            using StreamReader reader = new(stream);
            
            string tomlContent = reader.ReadToEnd();
            TomlTable model = Toml.Parse(tomlContent).ToModel();
                
            if (model.TryGetValue("loaderVersion", out var versionObj))
            {
                // fixme: Minecraft Forge mods [[dependencies.mod]]
                return ("Forge", versionObj.ToString()!, "?");
            }
        }
        
        ZipArchiveEntry? fabricJsonEntry = archive.GetEntry("fabric.mod.json");
        if (fabricJsonEntry != null)
            return ParseFabricBasedMetadata(fabricJsonEntry, "Fabric");
        
        
        ZipArchiveEntry? quiltJsonEntry = archive.GetEntry("quilt.mod.json");
        return quiltJsonEntry != null 
            ? ParseFabricBasedMetadata(quiltJsonEntry, "Quilt") 
            : ("[!загрузчик]", "[!загрузчик]", "[!загрузчик]");
    }
    /// <summary>
    /// Deserializes Fabric JSON manifest
    /// </summary>
    /// <param name="entry"></param>
    /// <param name="loaderName"></param>
    /// <returns></returns>
    private (string Loader, string Version, string McVersion) ParseFabricBasedMetadata(ZipArchiveEntry entry, string loaderName)
    {
        using (Stream stream = entry.Open())
        using (StreamReader reader = new(stream))
        {
            string jsonContent = reader.ReadToEnd();
            Fabric document = JsonSerializer.Deserialize<Fabric>(jsonContent)!;
            
            if (document.Depends.TryGetValue("fabricloader", out string? depend))
            {
                if (document.Depends.TryGetValue("minecraft", out string? mcver))
                    return (loaderName, depend, mcver);

                return (loaderName, depend, "[не найдено]");
            }

            if (document.Depends.TryGetValue("fabric", out string? documentDepend))
            {
                if (document.Depends.TryGetValue("minecraft", out string? mcver))
                    return (loaderName, documentDepend, mcver);

                return (loaderName, documentDepend, "[не найдено]");
            }
        }
        return (loaderName, "[не найдено]", "[не найдено]");
    }

    private void AddRow(DataTable table, FileInfo fileInfo, string loader, string version, string mcVersion)
    {
        DataRow row = table.NewRow();
        row["FileName"] = fileInfo.Name;
        row["Size"] = fileInfo.Length / 8192;
        row["Loader"] = loader;
        row["LoaderVersion"] = version;
        row["MinecraftVersion"] = mcVersion;
        
        table.Rows.Add(row);
    }
}