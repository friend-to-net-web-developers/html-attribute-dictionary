namespace FriendsOfNetWebDevelopers.HtmlAttributeDictionary;

public static class HtmlAttributeDictionaryFactory
{
    /// <summary>
    /// Convenience method for new HtmlAttributeDictionary
    /// </summary>
    /// <returns>new, empty HtmlAttributeDictionary</returns>
    public static HtmlAttributeDictionary Get()
        => new HtmlAttributeDictionary();
    
    /// <summary>
    /// Convenience method for new HtmlAttributeDictionary with the preexisting dictionary of attributes safely added 
    /// </summary>
    /// <param name="dictionary">Dictionary of attributes</param>
    /// <returns>new HtmlAttributeDictionary</returns>
    public static HtmlAttributeDictionary Get(IDictionary<string, string> dictionary)
        => new HtmlAttributeDictionary(dictionary);
    
    /// <summary>
    /// Convenience method for new HtmlAttributeDictionary with the preexisting kvp collection of attributes safely added 
    /// </summary>
    /// <param name="collection">Collection of attributes</param>
    /// <returns>new HtmlAttributeDictionary</returns>
    public static HtmlAttributeDictionary Get(IEnumerable<KeyValuePair<string, string>> collection)
        => new HtmlAttributeDictionary(collection);
    
    /// <summary>
    /// Create a new HtmlAttributeDictionary with the id and classes attributes safely set
    /// </summary>
    /// <param name="id"></param>
    /// <param name="classes"></param>
    /// <returns>new HtmlAttributeDictionary</returns>
    public static HtmlAttributeDictionary WithId(string id, string? classes = null)
    {
        var dictionary = Get();
        dictionary.SetId(id);
        dictionary.AddClasses(classes);
        return dictionary;
    }

    /// <summary>
    /// Create a new HtmlAttributeDictionary with the classes attribute safely set
    /// </summary>
    /// <param name="classes">string containing multiple html classes</param>
    /// <returns>new HtmlAttributeDictionary</returns>
    public static HtmlAttributeDictionary WithClasses(string classes)
    {
        var dictionary = Get();
        dictionary.AddClasses(classes);
        return dictionary;
    }

    /// <summary>
    /// Create a new HtmlAttributeDictionary with the classes attribute safely set
    /// </summary>
    /// <param name="classes">Collection of individual classes</param>
    /// <returns>new HtmlAttributeDictionary</returns>
    public static HtmlAttributeDictionary WithClasses(ICollection<string> classes)
    {
        var dictionary = Get();
        dictionary.AddClasses(classes);
        return dictionary;
    }

}