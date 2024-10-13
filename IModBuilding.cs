namespace onion.desktop.Middleware;

public interface IModBuilding : IModPack
{
    void Get();
    void Set();
    void Compare();
}