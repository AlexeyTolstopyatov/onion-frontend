using System.ComponentModel.DataAnnotations;

namespace Onion.Core.Models;


public class TomlElement
{
    [Required]
    [StringLength(50, MinimumLength = 1, ErrorMessage = "There is no key name")]
    public string Key { get; set; }

    public string Value { get; set; }

    public TomlElement(string key, string value)
    {
        Key = key;
        Value = value;
    }
}