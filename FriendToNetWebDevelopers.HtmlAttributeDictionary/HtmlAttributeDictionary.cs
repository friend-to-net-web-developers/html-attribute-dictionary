using System.Text.RegularExpressions;
using System.Web;
using FriendToNetWebDevelopers.HtmlAttributeDictionary.Models;

#pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable BuiltInTypeReferenceStyle

namespace FriendToNetWebDevelopers.HtmlAttributeDictionary;

public partial class HtmlAttributeDictionary : Dictionary<string, string>, ICloneable
{
    private const string IdAttribute = "id";
    private const string ClassAttribute = "class";
    private const string DisabledAttribute = "disabled";
    private const string RoleAttribute = "role";
    private const string StyleAttribute = "style";

    private const string DataAttributePrefix = "data-";
    private const string AriaAttributePrefix = "aria-";

    #region Constructors

    public HtmlAttributeDictionary()
    {
    }

    public HtmlAttributeDictionary(IDictionary<string, string> dictionary)
    {
        foreach (var (attribute, value) in dictionary)
        {
            this[attribute] = value;
        }
    }

    public HtmlAttributeDictionary(IEnumerable<KeyValuePair<string, string>> collection)
    {
        foreach (var (attribute, value) in collection)
        {
            this[attribute] = value;
        }
    }

    public HtmlAttributeDictionary(HtmlAttributeDictionary dictionary)
    {
        foreach (var (attribute, value) in dictionary)
        {
            //This is faster and should still be safe
            ForceSet(attribute, value);
        }
    }

    #endregion

    #region Standard Setters & Getters

    public new string this[string attribute]
    {
        get => base[attribute]; //This can stay default
        set
        {
            switch (attribute)
            {
                case IdAttribute:
                    SetId(value);
                    break;
                case ClassAttribute:
                    ForceSetClasses(value);
                    break;
                case DisabledAttribute:
                    SetDisabled();
                    break;
                case RoleAttribute:
                    SetRole(value);
                    break;
                case StyleAttribute:
                    SetStyleAttribute(value);
                    break;
                default:
                    SetAttributeDiscardNullOrEmpty(attribute, value);
                    break;
            }
        }
    }

    public new void Add(string attribute, string value)
    {
        if (IsValidHtmlAttribute(attribute))
        {
            base.Add(attribute, value);
        }
    }

    public new bool TryAdd(string attribute, string value)
    {
        return IsValidHtmlAttribute(attribute) && base.TryAdd(attribute, value);
    }

    #endregion

    #region Specialized Setters

    /// <summary>
    /// If value is null, the key is removed<br/>
    /// Otherwise, it attempts to set the value.
    /// </summary>
    /// <param name="attribute">The attribute to be set</param>
    /// <param name="value">The nullable value to set</param>
    /// <returns>Success of setting an attribute - false on null or empty</returns>
    public bool SetAttributeDiscardNull(string attribute, string? value)
    {
        if (string.IsNullOrEmpty(attribute)) return false;
        if (value == null)
        {
            Remove(attribute);
            return false;
        }

        if (ContainsKey(attribute)) Remove(attribute);
        var addOkay = TryAdd(attribute, value);
        if (!addOkay) Remove(attribute);
        return addOkay;
    }

    /// <summary>
    /// If value is null or empty, the key is removed<br/>
    /// Otherwise, it attempts to set the value.
    /// </summary>
    /// <param name="attribute">The attribute to be set</param>
    /// <param name="value">The nullable value to set</param>
    /// <returns>Success of setting an attribute - false on null or empty</returns>
    public bool SetAttributeDiscardNullOrEmpty(string attribute, string? value)
    {
        if (string.IsNullOrEmpty(attribute)) return false;
        //Removes the key if value is empty and does nothing else
        if (string.IsNullOrEmpty(value))
        {
            Remove(attribute);
            return false; //failed to set a value - key removed
        }

        //Pre-clears the key
        if (ContainsKey(attribute)) Remove(attribute);
        //Try adding in the new value
        var addOkay = TryAdd(attribute, value);
        //If it failed to add, clear the key
        if (!addOkay) Remove(attribute);
        //return the success of the add
        return addOkay;
    }

    /// <summary>
    /// Add the attribute without a value
    /// </summary>
    /// <param name="attribute"></param>
    /// <returns></returns>
    public bool SetAttributeOnly(string attribute)
    {
        if (ContainsKey(attribute)) Remove(attribute);
        return TryAdd(attribute, string.Empty);
    }

    /// <summary>
    /// WARNING: Should only be used in cases where all values are already known to be safe such as cloning
    /// </summary>
    /// <param name="attribute">Attribute</param>
    /// <param name="value">Value</param>
    private void ForceSet(string attribute, string value)
        => base[attribute] = value;

    #endregion

    #region Id Attribute

    public bool SetId(string? id)
    {
        if (!string.IsNullOrEmpty(id))
        {
            return IdValueRegex.IsMatch(id) && SetAttributeDiscardNullOrEmpty(IdAttribute, id);
        }

        return SetAttributeDiscardNullOrEmpty(IdAttribute, id);
    }

    public void RemoveId() => Remove(IdAttribute);

    #endregion

    #region Class Attribute

    /// <summary>
    /// Sets the classes field, overriding any previously existing contents.  Ignores any individual class value cleanup.
    /// </summary>
    /// <param name="classes"></param>
    /// <returns></returns>
    internal bool ForceSetClasses(string? classes) => SetAttributeDiscardNullOrEmpty(ClassAttribute, classes);


    /// <summary>
    /// Add classes (space delimited) to the existing list of classes, validating them one at a time.
    /// </summary>
    /// <param name="multipleClasses"></param>
    /// <returns>The number of classes added</returns>
    public uint AddClasses(string? multipleClasses)
        => string.IsNullOrWhiteSpace(multipleClasses) ? 0 : AddClasses(multipleClasses.Split(' '));

    /// <summary>
    /// Add classes to the existing list of classes, validating them one at a time.
    /// </summary>
    /// <param name="classes"></param>
    /// <returns>The number of classes added</returns>
    public uint AddClasses(ICollection<string> classes)
        => classes.Count == 0 ? 0 : (uint)classes.Count(AddClassToClassAttribute);

    /// <summary>
    /// Safely add a single class to the classes attribute
    /// </summary>
    /// <param name="singleClass"></param>
    /// <returns></returns>
    public bool AddClassToClassAttribute(string singleClass)
    {
        if (!SingleClassValueRegex.IsMatch(singleClass)) return false;
        if (!TryGetValue(ClassAttribute, out var classesRaw)) return ForceSetClasses(singleClass);
        if (string.IsNullOrEmpty(classesRaw)) return ForceSetClasses(singleClass);
        var allClasses = classesRaw.Split(' ').ToList();
        allClasses.AddRange(singleClass.Split(' '));
        return ForceSetClasses(string.Join(' ', allClasses));
    }

    /// <summary>
    /// Retrieves a set of class strings as stored in the class attribute
    /// </summary>
    /// <returns>ISet of class strings</returns>
    public ISet<string> GetClasses()
    {
        return !TryGetValue(ClassAttribute, out var classesRaw)
            ? []
            : new SortedSet<string>(classesRaw.Split(' '));
    }

    /// <summary>
    /// Remove multiple classes (space-delimited) from the existing list of classes
    /// </summary>
    /// <param name="classes"></param>
    /// <param name="limit">0 = No Limit | greater than 0 = Limit to a certain number </param>
    /// <returns>The number actually removed</returns>
    public uint RemoveClassesFromClassAttribute(string? classes, uint limit = 0)
        => string.IsNullOrWhiteSpace(classes) ? 0 : RemoveClassesFromClassAttribute(classes.Split(' '), limit);

    /// <summary>
    /// Remove multiple classes from the existing list of classes
    /// </summary>
    /// <param name="classesToRemove"></param>
    /// <param name="limit">0 = No Limit | greater than 0 = Limit to a certain number </param>
    /// <returns>The number actually removed</returns>
    public uint RemoveClassesFromClassAttribute(ICollection<string> classesToRemove, uint limit = 0)
    {
        if (classesToRemove.Count == 0) return 0;
        if (!TryGetValue(ClassAttribute, out var classesRaw)) return 0;
        var allClasses = classesRaw.Split(' ').ToList();
        uint classesRemoved = 0;
        for (var i = allClasses.Count - 1; i >= 0; i--)
        {
            if (limit > 0 && classesRemoved >= limit) break;
            if (!classesToRemove.Contains(allClasses[i])) continue;
            allClasses.RemoveAt(i);
            classesRemoved++;
        }

        ForceSetClasses(string.Join(' ', allClasses));
        return classesRemoved;
    }

    /// <summary>
    /// Removes the "class" attribute entirely
    /// </summary>
    public bool RemoveClassAttribute() => Remove(ClassAttribute);

    #endregion

    #region Disabled Attribute

    public void SetDisabled() => SetAttributeOnly(DisabledAttribute);

    public void RemoveDisabled() => Remove(DisabledAttribute);

    #endregion

    #region Role Attribute

    public bool SetRole(string? role) => SetAttributeDiscardNullOrEmpty(RoleAttribute, role);

    public void RemoveRole() => Remove(RoleAttribute);

    #endregion

    #region Style Attribute

    /// <summary>
    /// Retrieves a style attribute dictionary which represents the contents of the style attribute
    /// </summary>
    /// <returns></returns>
    public StyleAttributeDictionary GetStyleAttribute()
        => TryGetValue(StyleAttribute, out var rawStyleAttribute)
            ? new StyleAttributeDictionary(rawStyleAttribute)
            : new StyleAttributeDictionary();

    /// <summary>
    /// Replaces the style attribute with a new set of declarations
    /// </summary>
    /// <param name="declarations"></param>
    /// <returns>Tuple:
    ///     <br/>Item1 = was the html attribute set okay?
    ///     <br/>Item2 = how many declarations exist in the style declarations
    /// </returns>
    public Tuple<bool, int> SetStyleAttribute(StyleAttributeDictionary declarations)
        => new(SetAttributeDiscardNullOrEmpty(StyleAttribute, declarations.ToString()),
            declarations.Count);


    /// <summary>
    /// Replaces the style attribute with a new set of declarations
    /// </summary>
    /// <param name="declarations">A raw string with css declarations</param>
    /// <returns>Tuple:
    ///     <br/>Item1 = was the html attribute set okay?
    ///     <br/>Item2 = how many declarations exist in the style declarations?
    /// </returns>
    public Tuple<bool, int> SetStyleAttribute(string? declarations)
        => SetStyleAttribute(new StyleAttributeDictionary(declarations));

    /// <summary>
    /// Convenince method to add "display:none" to the style attribute
    /// </summary>
    /// <returns>Tuple:
    ///     <br/>Item1 = was the html attribute set okay?
    ///     <br/>Item2 = how many declarations were added or replaced?
    ///     <br/>Item3 = how many declarations exist in the style declarations?
    /// </returns>
    public Tuple<bool, uint, int> AddDisplayNoneToStyle()
        => AddToStyleAttribute("display:none;");

    /// <summary>
    /// Adds or replaces the specified declarations to the existing style tag.
    /// </summary>
    /// <param name="declarations">A raw string with css declarations</param>
    /// <returns>Tuple:
    ///     <br/>Item1 = was the html attribute set okay?
    ///     <br/>Item2 = how many declarations were added or replaced?
    ///     <br/>Item3 = how many declarations exist in the style declarations?
    /// </returns>
    public Tuple<bool, uint, int> AddToStyleAttribute(string? declarations)
    {
        var atr = GetStyleAttribute();
        var addedOkay = atr.SetDeclarations(declarations);
        var setResults = SetStyleAttribute(atr);
        return new Tuple<bool, uint, int>(setResults.Item1, addedOkay, setResults.Item2);
    }

    /// <summary>
    /// Removes the specified declarations names in the existing style tag.
    /// </summary>
    /// <param name="declarationNames">A set of declaration names</param>
    /// <returns>Tuple:
    ///     <br/>Item1 = was the html attribute set okay?
    ///     <br/>Item2 = how many declarations were removed?
    ///     <br/>Item3 = how many declarations exist in the style declarations?
    /// </returns>
    public Tuple<bool, uint, int> RemoveDeclarationsFromStyleAttribute(ISet<string> declarationNames)
    {
        var atr = GetStyleAttribute();
        var removedOkay = atr.RemoveDeclarations(declarationNames);
        var setResults = SetStyleAttribute(atr);
        return new Tuple<bool, uint, int>(setResults.Item1, removedOkay, setResults.Item2);
    }

    /// <summary>
    /// Removes the style attribute
    /// </summary>
    /// <returns>Was the style attribute removed okay?</returns>
    public bool RemoveStyleAttribute()
        => Remove(StyleAttribute);

    #endregion

    #region Data Attributes Convenience Methods

    public bool SetData(string attributeSuffix, string? value)
        => SetAttributeDiscardNullOrEmpty(DataAttributePrefix + attributeSuffix, value);

    public bool RemoveData(string attributeSuffix)
        => Remove(DataAttributePrefix + attributeSuffix);

    #endregion

    #region Aria Attributes Convenience Methods

    public bool SetAria(string attributeSuffix, string? value)
        => SetAttributeDiscardNullOrEmpty(AriaAttributePrefix + attributeSuffix, value);

    public bool RemoveAria(string attributeSuffix)
        => Remove(AriaAttributePrefix + attributeSuffix);

    #endregion

    #region Type Conversion Methods

    public override string ToString()
    {
        if (Count == 0) return string.Empty;
        var list = new List<string>();
        foreach (var (attribute, value) in this)
        {
            //If the value is empty, it can be added with no value
            if (string.IsNullOrWhiteSpace(value))
            {
                list.Add(attribute);
                continue;
            }

            //Escape and add the key value pair
            var valEscaped = HttpUtility.HtmlEncode(value);
            list.Add(string.IsNullOrEmpty(valEscaped) ? attribute : $"{attribute}=\"{valEscaped}\"");
        }

        return list.Count == 0 ? string.Empty : string.Join(' ', list);
    }

    public object Clone() => new HtmlAttributeDictionary(this);

    #endregion


    private static List<string> _validHtmlAttributes = [];
    private static readonly Regex AttributeRegex = HtmlAttributeInternalRegex();
    private static readonly Regex IdValueRegex = HtmlIdValueInternalRegex();
    private static readonly Regex SingleClassValueRegex = HtmlSingleClassValueInternalRegex();

    private static bool IsValidHtmlAttribute(string? attribute)
    {
        if (string.IsNullOrWhiteSpace(attribute)) return false;
        if (!AttributeRegex.IsMatch(attribute)) return false;
        if (attribute.StartsWith(DataAttributePrefix) || attribute.StartsWith(AriaAttributePrefix)) return true;
        if (_validHtmlAttributes.Count == 0)
        {
            _validHtmlAttributes =
            [
                "accept",
                "accept-charset",
                "accesskey",
                "action",
                "align",
                "allow",
                "allowfullscreen",
                "allowpaymentrequest",
                "alt",
                "async",
                "autocomplete",
                "autofocus",
                "autoplay",
                "bgcolor",
                "border",
                "charset",
                "checked",
                "cite",
                ClassAttribute,
                "color",
                "cols",
                "colspan",
                "content",
                "contenteditable",
                "controls",
                "coords",
                "crossorigin",
                "data",
                "datetime",
                "default",
                "defer",
                "dir",
                "dirname",
                DisabledAttribute,
                "download",
                "draggable",
                "enctype",
                "for",
                "form",
                "formaction",
                "formenctype",
                "formmethod",
                "formnovalidate",
                "formtarget",
                "headers",
                "height",
                "hidden",
                "high",
                "href",
                "hreflang",
                "http-equiv",
                IdAttribute,
                "integrity",
                "ismap",
                "kind",
                "loading",
                "label",
                "lang",
                "list",
                "longdesc",
                "loop",
                "low",
                "max",
                "maxlength",
                "media",
                "method",
                "min",
                "minlength",
                "multiple",
                "muted",
                "name",
                "novalidate",
                "onabort",
                "onafterprint",
                "onbeforeprint",
                "onbeforeunload",
                "onblur",
                "oncanplay",
                "oncanplaythrough",
                "onchange",
                "onclick",
                "oncontextmenu",
                "oncopy",
                "oncuechange",
                "oncut",
                "ondblclick",
                "ondrag",
                "ondragend",
                "ondragenter",
                "ondragleave",
                "ondragover",
                "ondragstart",
                "ondrop",
                "ondurationchange",
                "onemptied",
                "onended",
                "onerror",
                "onfocus",
                "onhashchange",
                "oninput",
                "oninvalid",
                "onkeydown",
                "onkeypress",
                "onkeyup",
                "onload",
                "onloadeddata",
                "onloadedmetadata",
                "onloadstart",
                "onmousedown",
                "onmousemove",
                "onmouseout",
                "onmouseover",
                "onmouseup",
                "onmousewheel",
                "onoffline",
                "ononline",
                "onpagehide",
                "onpageshow",
                "onpaste",
                "onpause",
                "onplay",
                "onplaying",
                "onpopstate",
                "onprogress",
                "onratechange",
                "onreset",
                "onresize",
                "onscroll",
                "onsearch",
                "onseeked",
                "onseeking",
                "onselect",
                "onstalled",
                "onstorage",
                "onsubmit",
                "onsuspend",
                "ontimeupdate",
                "ontoggle",
                "onunload",
                "onvolumechange",
                "onwaiting",
                "onwheel",
                "open",
                "optimum",
                "pattern",
                "placeholder",
                "popovertarget",
                "popovertargetaction",
                "poster",
                "preload",
                "readonly",
                "referrerpolicy",
                "rel",
                "required",
                "reversed",
                RoleAttribute,
                "rows",
                "rowspan",
                "sandbox",
                "scope",
                "selected",
                "shape",
                "size",
                "sizes",
                "span",
                "spellcheck",
                "src",
                "srcdoc",
                "srclang",
                "srcset",
                "start",
                "step",
                "style",
                "tabindex",
                "target",
                "title",
                "translate",
                "type",
                "usemap",
                "value",
                "width",
                "wrap"
            ];
        }

        return _validHtmlAttributes.Contains(attribute);
    }

    [GeneratedRegex("^[a-zA-Z][a-zA-Z0-9\\-]*[a-z-A-Z0-9]$")]
    private static partial Regex HtmlAttributeInternalRegex();

    [GeneratedRegex("^[a-zA-Z][a-zA-Z0-9_\\-]*[a-zA-Z0-9]$")]
    private static partial Regex HtmlIdValueInternalRegex();

    [GeneratedRegex("^[a-zA-Z0-9][a-zA-Z0-9_\\-]*[a-zA-Z0-9]$")]
    private static partial Regex HtmlSingleClassValueInternalRegex();
}