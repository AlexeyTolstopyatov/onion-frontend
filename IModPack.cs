using System.IO;

namespace onion.desktop.Middleware;

public interface IModPack
{
    void Get(ref FileInfo modlist);
    void Set(ref List<FileInfo> modlist);
}