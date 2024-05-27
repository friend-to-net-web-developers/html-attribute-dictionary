# HTML Attribute Dictionary
## Summary
A dictionary which extends Dictionary&lt;string,string&gt; and provides html attribute and value safety features.

## Installation
(Soon)

## Usage
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
    <div @Html.Raw(dictionary)>
        <!-- ... -->
    </div>
</section>
```
This keeps things simple and safe.  It even allows for data to come in from an unsafe source and be safe to display.

It even prevents invalid attributes from being added.

# License Information
<p xmlns:cc="http://creativecommons.org/ns#" xmlns:dct="http://purl.org/dc/terms/"><a property="dct:title" rel="cc:attributionURL" href="https://github.com/friend-to-net-web-developers/html-attribute-dictionary">HTML Attribute Dictionary</a> by <a rel="cc:attributionURL dct:creator" property="cc:attributionName" href="https://github.com/chris-goehrs">Christopher Goehrs</a> through <a rel="cc:attributionURL dct:creator" property="cc:attributionName" href="https://github.com/friend-to-net-web-developers">Friends of .NET Web Developers</a> is licensed under <a href="https://creativecommons.org/licenses/by/4.0/?ref=chooser-v1" target="_blank" rel="license noopener noreferrer" style="display:inline-block;">CC BY 4.0<img style="height:22px!important;margin-left:3px;vertical-align:text-bottom;" src="https://mirrors.creativecommons.org/presskit/icons/cc.svg?ref=chooser-v1" alt=""><img style="height:22px!important;margin-left:3px;vertical-align:text-bottom;" src="https://mirrors.creativecommons.org/presskit/icons/by.svg?ref=chooser-v1" alt=""></a></p>
