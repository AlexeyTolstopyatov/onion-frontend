using Tomlyn.Model;
using System.Data;
using System.IO;
using System.IO.Compression;
using System.Text.Json;
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

        foreach (string filePath in Directory.GetFiles(directoryPath))
        {
            FileInfo fileInfo = new(filePath);
            
            try
            {
                using ZipArchive archive = ZipFile.OpenRead(filePath);
                (string loader, string version) = ParseForgeBasedMetadata(archive);

                AddRow(table, fileInfo, loader, version);
            }
            catch (InvalidDataException)
            {
                // skip entry
                continue;
            }
        }

        return table;
    }

    private (string Loader, string Version) ParseForgeBasedMetadata(ZipArchive archive)
    {
        ZipArchiveEntry? tomlEntry = archive.GetEntry("META-INF/mods.toml");
        if (tomlEntry != null)
        {
            using Stream stream = tomlEntry.Open();
            using StreamReader reader = new StreamReader(stream);
            
            string tomlContent = reader.ReadToEnd();
            TomlTable model = Toml.Parse(tomlContent).ToModel();
                
            if (model.TryGetValue("version", out var versionObj))
            {
                return ("Forge", versionObj.ToString()!);
            }
        }
        
        ZipArchiveEntry? fabricJsonEntry = archive.GetEntry("mod.fabric.json");
        if (fabricJsonEntry != null)
        {
            return ParseFabricBasedMetadata(fabricJsonEntry, "Fabric");
        }
        
        ZipArchiveEntry? quiltJsonEntry = archive.GetEntry("mod.quilt.json");
        if (quiltJsonEntry != null)
        {
            return ParseFabricBasedMetadata(quiltJsonEntry, "Quilt");
        }

        return (null!, null!);
    }

    private (string Loader, string Version) ParseFabricBasedMetadata(ZipArchiveEntry entry, string loaderName)
    {
        using (Stream stream = entry.Open())
        using (StreamReader reader = new StreamReader(stream))
        {
            string jsonContent = reader.ReadToEnd();
            JsonDocument doc = JsonDocument.Parse(jsonContent);

            if (doc.RootElement.TryGetProperty("fabricloader", out var versionProp))
            {
                return (loaderName, versionProp.GetInt32().ToString());
            }
        }
        return (loaderName, "Unknown");
    }

    private void AddRow(DataTable table, FileInfo fileInfo, string loader, string version)
    {
        DataRow row = table.NewRow();
        row["FileName"] = fileInfo.Name;
        row["Size"] = fileInfo.Length;
        row["Loader"] = loader;
        row["LoaderVersion"] = version;
        table.Rows.Add(row);
    }
}