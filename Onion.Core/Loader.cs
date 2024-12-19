namespace Onion.Core;

/// <summary>
/// Basic definitions of detectable
/// modification's loaders
/// </summary>
public enum Loader
{
    Undefined = 1 << 1,
    Fabric = 1 << 2,
    Quilt = 1 << 3,
    Forge = 1 << 4,
    NeoForge = 1 << 5
}