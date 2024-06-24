using System.Text.RegularExpressions;

namespace FriendToNetWebDevelopers.HtmlAttributeDictionary.Models;

/// <summary>
/// A helper class for dealing with the "style" html attribute within the HtmlAttributeDictionary
/// <br/>It is fairly simplistic and doesn't try and maintain a valid list of css declarations
/// </summary>
public partial class StyleAttributeDictionary : Dictionary<string, string>
{
    public StyleAttributeDictionary() { }
    public StyleAttributeDictionary(IDictionary<string, string> dictionary) : base(dictionary)
    {
        foreach (var (name, value) in dictionary)
        {
            this[name] = value;
        }
    }
    public StyleAttributeDictionary(string? existingStyles)
    {
        SetDeclarations(existingStyles);
    }

    public uint SetDeclarations(string? declarations)
    {
        if (string.IsNullOrEmpty(declarations)) return 0;
        uint declarationsSet = 0;
        foreach (var declaration in declarations.Trim().Split(';'))
        {
            if (string.IsNullOrWhiteSpace(declaration)) continue;
            if (SetDeclaration(declaration)) declarationsSet++;
        }
        return declarationsSet;
    }
    
    public bool SetDeclaration(string declaration)
    {
        var kvp = declaration.Trim().Split(':');
        if (kvp.Length < 2) return false;
        this[kvp[0].Trim()] = kvp[1].Trim();
        return true;
    }
    
    public new string this[string propertyName]
    {
        get => base[propertyName]; //This can stay default
        set
        {
            if (!PropertyNameRegex.IsMatch(propertyName)) 
                return;
            base[propertyName] = value;
        }
    }

    public override string ToString()
    {
        using var sw = new StringWriter();
        foreach (var (propertyName, propertyValue) in this)
        {
            sw.Write($"{propertyName}:{propertyValue};");
        }
        return sw.ToString();
    }

    public uint RemoveDeclarations(ISet<string> declarationNames)
    {
        if (declarationNames.Count == 0) return 0;
        uint removed = 0;
        foreach (var declarationName in declarationNames)
            if (Remove(declarationName)) removed++;
        return removed;
    }
    
    private static readonly Regex PropertyNameRegex = CssDeclarationPropertyNameRegex();
    
    [GeneratedRegex(@"^[a-zA-Z\\-]+$")]
    private static partial Regex CssDeclarationPropertyNameRegex();
}