using System.Runtime.Serialization;

namespace Onion.Window.Metadata;

public class ForgeDependency
{
    [DataMember(Name = "modId")] public string? ModId { get; set; }
    [DataMember(Name = "mandatory")] public string? Mandatory { get; set; }
    [DataMember(Name = "versionRange")] public string? VersionRange { get; set; }
    [DataMember(Name = "ordering")] public string? Ordering { get; set; }
    [DataMember(Name = "side")] public string? Side { get; set; }
}