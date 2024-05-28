# HTML Attribute Dictionary
## Summary
A dictionary which extends Dictionary&lt;string,string&gt; and provides html attribute and value safety features.

## Installation
`Install-Package FriendToNetWebDevelopers.HtmlAttributeDictionary`

## Usage
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
This keeps things simple and safe.  It even allows for data to come in from an unsafe source and be safe to display.

It even prevents invalid attributes from being added.

# License Information

[HTML Attribute Dictionary](https://github.com/friend-to-net-web-developers/html-attribute-dictionary)
 by [Christopher Goehrs](https://github.com/chris-goehrs) 
 through [Friend to .NET Web Developers](https://github.com/friend-to-net-web-developers) 
 is licensed under [CC BY 4.0](https://creativecommons.org/licenses/by/4.0)
