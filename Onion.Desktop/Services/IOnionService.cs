namespace Onion.Desktop.Services;

/// <summary>
/// Represents functions that must see outside from file.
/// </summary>
public interface IOnionService
{
    /// <summary>
    /// entry point in Standard Onion Service
    /// </summary>
    void OnionMain();
}