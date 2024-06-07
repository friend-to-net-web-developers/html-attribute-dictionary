﻿namespace FriendToNetWebDevelopers.HtmlAttributeDictionary.Test;

[TestFixture]
public class ClassTests
{
    [Test]
    public void InitializeWithClasses()
    {
        var dictionary = HtmlAttributeDictionaryFactory.WithClasses(["foo-bar"]);
        Assert.That(dictionary, Is.Not.Empty);
        Assert.That(dictionary["class"], Is.EqualTo("foo-bar"));
    }
    
    [Test]
    public void AddClasses2Valid()
    {
        var dictionary = HtmlAttributeDictionaryFactory.Get();
        Assert.That(dictionary, Is.Empty);
        var okay = dictionary.AddClasses("foo bar");
        Assert.Multiple(() =>
        {
            Assert.That(okay, Is.EqualTo(2));
            Assert.That(dictionary, Is.Not.Empty);
        });
        var classes = dictionary.GetClasses();
        Assert.That(classes, Is.Not.Empty);
        Assert.Multiple(() =>
        {
            Assert.That(classes.Contains("foo"), Is.True);
            Assert.That(classes.Contains("bar"), Is.True);
        });
    }

    [Test]
    public void AddClasses1Valid1Invalid()
    {
        var dictionary = HtmlAttributeDictionaryFactory.Get();
        Assert.That(dictionary, Is.Empty);
        var okay = dictionary.AddClasses("foo b@r");
        Assert.Multiple(() =>
        {
            Assert.That(okay, Is.EqualTo(1));
            Assert.That(dictionary, Is.Not.Empty);
        });
        var classes = dictionary.GetClasses();
        Assert.Multiple(() =>
        {
            Assert.That(classes, Is.Not.Empty);
            Assert.That(classes.Contains("foo"), Is.True);
            Assert.That(classes.Contains("b@r"), Is.False);
        });
    }

    [Test]
    public void AddClasses2Invalid()
    {
        var dictionary = HtmlAttributeDictionaryFactory.Get();
        Assert.That(dictionary, Is.Empty);
        var okay = dictionary.AddClasses("f0o! b@r");
        Assert.Multiple(() =>
        {
            Assert.That(okay, Is.Zero);
            Assert.That(dictionary, Is.Empty);
        });
        var classes = dictionary.GetClasses();
        Assert.That(classes, Is.Empty);
    }

    [Test]
    public void AddClassesNullOnly()
    {
        var dictionary = HtmlAttributeDictionaryFactory.Get();
        Assert.That(dictionary, Is.Empty);
        string? nullToAdd = null;
        var okay = dictionary.AddClasses(nullToAdd);
        Assert.Multiple(() =>
        {
            Assert.That(okay, Is.Zero);
            Assert.That(dictionary, Is.Empty);
        });
        var classes = dictionary.GetClasses();
        Assert.That(classes, Is.Empty);
    }

    [Test]
    public void AddClassesNullAlso()
    {
        var dictionary = HtmlAttributeDictionaryFactory.Get();
        Assert.That(dictionary, Is.Empty);
        var okay = dictionary.AddClasses("foo bar");
        Assert.Multiple(() =>
        {
            Assert.That(okay, Is.EqualTo(2));
            Assert.That(dictionary, Is.Not.Empty);
        });
        var classes = dictionary.GetClasses();
        Assert.Multiple(() =>
        {
            Assert.That(classes, Is.Not.Empty);
            Assert.That(classes, Has.Count.EqualTo(2));
            Assert.That(classes.Contains("foo"), Is.True);
            Assert.That(classes.Contains("bar"), Is.True);
        });
        string? nullToAdd = null;
        okay = dictionary.AddClasses(nullToAdd);
        Assert.That(okay, Is.Zero);
        classes = dictionary.GetClasses();
        Assert.That(classes, Is.Not.Empty);
        Assert.That(classes, Has.Count.EqualTo(2));
    }

    [Test]
    public void AddClasses2ValidWithAddSingle1Valid()
    {
        var dictionary = HtmlAttributeDictionaryFactory.Get();
        Assert.That(dictionary, Is.Empty);
        var okay = dictionary.AddClasses("foo bar");
        Assert.Multiple(() =>
        {
            Assert.That(okay, Is.EqualTo(2));
            Assert.That(dictionary, Is.Not.Empty);
        });
        var classes = dictionary.GetClasses();
        Assert.That(classes, Is.Not.Empty);
        Assert.That(classes, Has.Count.EqualTo(2));
        Assert.Multiple(() =>
        {
            Assert.That(classes.Contains("foo"), Is.True);
            Assert.That(classes.Contains("bar"), Is.True);
        });
        var singleOkay = dictionary.AddClassToClassAttribute("baz");
        Assert.That(singleOkay);
        classes = dictionary.GetClasses();
        Assert.That(classes, Has.Count.EqualTo(3));
        Assert.That(classes.Contains("baz"), Is.True);
    }

    [Test]
    public void RemoveClassesFromClassAttribute_RemoveValid1Missing1Invalid1_From3()
    {
        var dictionary = HtmlAttributeDictionaryFactory.WithClasses("foo bar baz");
        var classes = dictionary.GetClasses();
        Assert.Multiple(() =>
        {
            Assert.That(classes.Contains("foo"));
            Assert.That(classes.Contains("bar"));
            Assert.That(classes.Contains("baz"));
        });
        var removed = dictionary.RemoveClassesFromClassAttribute("baz bob d@l3");
        Assert.That(removed, Is.EqualTo(1));
        classes = dictionary.GetClasses();
        Assert.That(classes, Has.Count.EqualTo(2));
        Assert.Multiple(() =>
        {
            Assert.That(classes.Contains("foo"));
            Assert.That(classes.Contains("bar"));
            Assert.That(classes.Contains("baz"), Is.False);
        });
    }

    [Test]
    public void RemoveClassAttribute()
    {
        var dictionary = HtmlAttributeDictionaryFactory.Get();
        Assert.That(dictionary, Is.Empty);
        var okay = dictionary.AddClasses("foo bar");
        Assert.Multiple(() =>
        {
            Assert.That(okay, Is.EqualTo(2));
            Assert.That(dictionary, Is.Not.Empty);
        });
        var classes = dictionary.GetClasses();
        Assert.Multiple(() =>
        {
            Assert.That(classes, Is.Not.Empty);
            Assert.That(classes.Contains("foo"), Is.True);
            Assert.That(classes.Contains("bar"), Is.True);
        });
        var removedOkay = dictionary.RemoveClassAttribute();
        Assert.Multiple(() =>
        {
            Assert.That(removedOkay);
            Assert.That(dictionary, Is.Empty);
        });
    }

    [Test]
    public void AddClasses_MultipleOf2()
    {
        var dictionary = HtmlAttributeDictionaryFactory.Get();
        Assert.That(dictionary, Is.Empty);
        var okay = dictionary.AddClasses("foo foo foo bar bar bar");
        Assert.Multiple(() =>
        {
            Assert.That(okay, Is.EqualTo(6));
            Assert.That(dictionary["class"], Is.EqualTo("foo foo foo bar bar bar"));
        });
        var classes = dictionary.GetClasses();
        Assert.That(classes, Has.Count.EqualTo(2));
    }

    [Test]
    public void RemoveClassesFromClassAttribute_AddMultipleOf2_RemoveMultipleOf1()
    {
        var dictionary = HtmlAttributeDictionaryFactory.Get();
        Assert.That(dictionary, Is.Empty);
        var okay = dictionary.AddClasses("foo bar foo bar foo bar");
        Assert.Multiple(() =>
        {
            Assert.That(okay, Is.EqualTo(6));
            Assert.That(dictionary["class"], Is.EqualTo("foo bar foo bar foo bar"));
        });
        var classes = dictionary.GetClasses();
        Assert.That(classes, Has.Count.EqualTo(2));
        okay = dictionary.RemoveClassesFromClassAttribute("foo");
        Assert.That(okay, Is.EqualTo(3));
        classes = dictionary.GetClasses();
        Assert.Multiple(() =>
        {
            Assert.That(classes, Has.Count.EqualTo(1));
            Assert.That(dictionary["class"], Is.EqualTo("bar bar bar"));
        });
    }

    [Test]
    public void RemoveClassesFromClassAttribute_AddMultipleOf2_Remove1WhichHasMultiple()
    {
        var dictionary = HtmlAttributeDictionaryFactory.Get();
        Assert.That(dictionary, Is.Empty);
        var okay = dictionary.AddClasses("foo bar foo bar foo bar");
        Assert.Multiple(() =>
        {
            Assert.That(okay, Is.EqualTo(6));
            Assert.That(dictionary["class"], Is.EqualTo("foo bar foo bar foo bar"));
        });
        var classes = dictionary.GetClasses();
        Assert.That(classes, Has.Count.EqualTo(2));
        okay = dictionary.RemoveClassesFromClassAttribute("foo", 1);
        Assert.That(okay, Is.EqualTo(1));
        classes = dictionary.GetClasses();
        Assert.Multiple(() =>
        {
            Assert.That(classes, Has.Count.EqualTo(2));
            Assert.That(dictionary["class"], Is.EqualTo("foo bar foo bar bar"));
        });
    }

    [Test]
    public void RemoveClassesFromClassAttribute_AddMultipleOf2_Remove2WhichHasMultiple()
    {
        var dictionary = HtmlAttributeDictionaryFactory.Get();
        Assert.That(dictionary, Is.Empty);
        var okay = dictionary.AddClasses("foo bar foo bar foo bar");
        Assert.Multiple(() =>
        {
            Assert.That(okay, Is.EqualTo(6));
            Assert.That(dictionary["class"], Is.EqualTo("foo bar foo bar foo bar"));
        });
        var classes = dictionary.GetClasses();
        Assert.That(classes, Has.Count.EqualTo(2));
        okay = dictionary.RemoveClassesFromClassAttribute("foo", 2);
        Assert.That(okay, Is.EqualTo(2));
        classes = dictionary.GetClasses();
        Assert.Multiple(() =>
        {
            Assert.That(classes, Has.Count.EqualTo(2));
            Assert.That(dictionary["class"], Is.EqualTo("foo bar bar bar"));
        });
    }
}