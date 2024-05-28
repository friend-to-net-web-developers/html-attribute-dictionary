# HTML Attribute Dictionary
## Summary
A dictionary which extends Dictionary&lt;string,string&gt; and provides html attribute and value safety features.

## Installation
`Install-Package FriendToNetWebDevelopers.HtmlAttributeDictionary`

## Usage
### How?

Add the following to your _ViewImports.cshtml

```
@using FriendToNetWebDevelopers.HtmlAttributeDictionary
@addTagHelper *, FriendToNetWebDevelopers.HtmlAttributeDictionary
```

This is the simplest use case I could come up with.

```
@{  //                                 Please note the multiple classes being brought in
    //                                          Along with your id ↓         ↓
    //                                                      ↓      ↓         ↓
    var dictionary = HtmlAttributeDictionaryFactory.WithId("foo", "some-hero bar");
    if(needsScrim)
    {
        dictionary.AddClassToClassAttribute("hero-scrim");
    }
}
<section>
    <!-- Produces the div for the hero with or without the "hero-scrim" class, as needed. -->
    <div html-attributes="dictionary">
        <!-- ... -->
    </div>
</section>
```

-or just with classes-

```
@{
    var dictionary = HtmlAttributeDictionaryFactory.WithClasses("some-hero bar");
    if(needsScrim)
    {
        dictionary.AddClassToClassAttribute("hero-scrim");
    }
}
<section>
    <!-- Produces the div for the hero with or without the "hero-scrim" class, as needed. -->
    <!-- An alternative way to use the dictionary -->
    <div @Html.Raw(dictionary)>
        <!-- ... -->
    </div>
</section>
```
But can be initialized in the following ways as well.
```csharp
//Provides an empty dictionary
var dictionary = HtmlAttributeDictionaryFactory.Get();

//ALSO provides an empty dictionary
dictionary = new HtmlAttributeDictionary();

//Follows the safety procedures to attempt to bring in the attributes from a different type of dictionary
var someOtherDictionary = new Dictionary<string, string>();
dictionary = new HtmlAttributeDictionary(someOtherDictionary);

//Follows the safety procedures to attempt to bring in the attributes from a list of key-value-pairs
var list = new KeyValuePair<string,string>();
dictionary = new HtmlAttributeDictionary(list);
```

While I like using the factory when producing the attribute dictionary inline, that's just because of the shortcuts
  it provides.  This can be produced entirely dynamically and still work.

### Why?

This keeps things simple and safe.  It even allows for data to come in from an unsafe source and be safe to display.

It even prevents invalid attributes from being added.

# What if there's a problem

Please feel free to report any issues [here](https://github.com/friend-to-net-web-developers/html-attribute-dictionary/issues) on GitHub.

Include as many details as possible and one of us will check it out.

# Thanks

A big thank you to Joshua Hess for his initial review and contribution of the Tag Helper feature.

![Heartland Business Systems Logo](https://cdn-ilaepil.nitrocdn.com/lwEpTzOpowNrpEQtaopWrEAXNdUgLLes/assets/images/optimized/rev-64f2520/www.hbs.net/wp-content/uploads/2022/11/HBS-website-logo.png)

I would also like to extend my gratitude to my employer, [Heartland Business Systems](https://www.hbs.net), for putting me in a position to help
 others by providing this helper library to others free of charge.  They've supported me in creating this work and share it openly with .NET Web Developers everywhere.

# License Information

[HTML Attribute Dictionary](https://github.com/friend-to-net-web-developers/html-attribute-dictionary)
 by [Christopher Goehrs](https://github.com/chris-goehrs) 
 through [Friend to .NET Web Developers](https://github.com/friend-to-net-web-developers) 
 is licensed under [CC BY 4.0](https://creativecommons.org/licenses/by/4.0)

