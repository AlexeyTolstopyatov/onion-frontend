using System;
using Tomlyn.Model;
using System.Data;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text.Json;
using System.Xml.Linq;
using Onion.Window.Metadata;
using Tomlyn;

namespace Onion.Window.Services;

public class ModInfoLoader
{
    private FileInfo? _file;

    public string Name { get; set; } = string.Empty;
    public DataTable LoadOnionScheme()
    {
        // deserialize model
        if (_file == null)
            return null!;

        DataTable table = new();
        table.Columns.Add("Key", typeof(string));
        table.Columns.Add("Value", typeof(string));
        try
        {
            XDocument document = XDocument.Load(_file.FullName);
            DataRow naming = table.NewRow();
            naming["Key"] = "Название";
            naming["Value"] = document
                .Descendants()
                .Elements("name")
                .First()
                .Value;
            Name = naming["Value"].ToString();
            DataRow loader = table.NewRow();
            loader["Key"] = "Тип загрузчика";
            loader["Value"] = document
                .Descendants()
                .Elements("loader")
                .First()
                .Value;
            DataRow loaderVersion = table.NewRow();
            loaderVersion["Key"] = "Версия загрузчика";
            loaderVersion["Value"] = document
                .Descendants()
                .Elements("loaderVersion")
                .First()
                .Value;
            DataRow minecraftVersion = table.NewRow();
            minecraftVersion["Key"] = "Minecraft";
            minecraftVersion["Value"] = document
                .Descendants()
                .Elements("minecraftVersion")
                .First()
                .Value;
            
            table.Rows.Add(naming);
            table.Rows.Add(loader);
            table.Rows.Add(loaderVersion);
            table.Rows.Add(minecraftVersion);
            
            return table;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new DataTable();
        }
    }
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
                if (string.Equals(new FileInfo(filePath).Extension, ".oxt", StringComparison.OrdinalIgnoreCase))
                {
                    // what if table.oxt exists?
                    _file = fileInfo;
                    (string loader, string _, string _) = ParseOnionXaml(filePath);
                    AddRow(table, fileInfo, loader, string.Empty, string.Empty);
                }

                continue;
            }
        }

        return table;
    }
    /// <summary>
    /// Deserializes TOML ForgeDependency manifest
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
                // fixme: Minecraft ForgeDependency mods [[dependencies.mod]]
                // if (model.TryGetValue("modId", out object modid))
                // {
                //     ForgeDependency[] deps = Toml.ToModel<ForgeDependency[]>(tomlContent);
                //     
                // }

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

    private (string Loader, string, string) ParseOnionXaml(string path)
    {
        return ("Onion", string.Empty, string.Empty);
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