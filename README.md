# OrderedDictionary

[![NuGet version (SoftCircuits.OrderedDictionary)](https://img.shields.io/nuget/v/SoftCircuits.OrderedDictionary.svg?style=flat-square)](https://www.nuget.org/packages/SoftCircuits.OrderedDictionary/)

```
Install-Package SoftCircuits.OrderedDictionary
```

## Introduction

OrderedDictionary is a .NET library that implements an ordered dictionary. It provides all the functionality of `Dictionary<TKey, TValue>` but also maintains the items in an ordered list. Items can be added, removed and accessed by key or index. The class implements IDictionary.

## Examples

OrderedDictionary can be initialized used like any other dictionary.

```cs
OrderedDictionary<int, string> dictionary = new OrderedDictionary<int, string>
{
    [101] = "Bob Smith",
    [127] = "Gary Wilson",
    [134] = "Ann Carpenter",
    [187] = "Bill Jackson",
    [214] = "Cheryl Hansen",
};
```

Like a dictionary, items can by accessed by key. They can also be accessed using a 0-based index. Because it's possible for the key to be of type `int`, you use the `ByIndex` property to access an item using an index.

```cs
Assert.AreEqual("Gary Wilson", dictionary[127]);
Assert.AreEqual("Bill Jackson", dictionary.ByIndex[3]);
```

You can add items using the `Add()` method, and you can also insert them at a particular location.

```cs
OrderedDictionary<int, string> dictionary = new OrderedDictionary<int, string>();

dictionary.Add(101, "Bob Smith");
dictionary.Add(127, "Gary Wilson");
dictionary.Add(187, "Bill Jackson");
dictionary.Add(214, "Cheryl Hansen");

dictionary.Insert(2, 134, "Add Carpenter");

Assert.AreEqual("Bob Smith", dictionary[101]);
Assert.AreEqual("Bob Smith", dictionary.ByIndex[0]);
Assert.AreEqual("Gary Wilson", dictionary[127]);
Assert.AreEqual("Gary Wilson", dictionary.ByIndex[1]);
Assert.AreEqual("Add Carpenter", dictionary[134]);
Assert.AreEqual("Add Carpenter", dictionary.ByIndex[2]);
Assert.AreEqual("Bill Jackson", dictionary[187]);
Assert.AreEqual("Bill Jackson", dictionary.ByIndex[3]);
Assert.AreEqual("Cheryl Hansen", dictionary[214]);
Assert.AreEqual("Cheryl Hansen", dictionary.ByIndex[4]);
```

Items can also be removed by either key or index.

```cs
OrderedDictionary<int, string> dictionary = new OrderedDictionary<int, string>
{
    [101] = "Bob Smith",
    [127] = "Gary Wilson",
    [134] = "Ann Carpenter",
    [187] = "Bill Jackson",
    [214] = "Cheryl Hansen",
};

dictionary.Remove(134);
dictionary.RemoveAt(2); // Removes 187 - Bill Jackson

Assert.AreEqual(5 - 2, dictionary.Count);
Assert.IsTrue(dictionary.ContainsKey(101));
Assert.IsTrue(dictionary.ContainsKey(127));
Assert.IsFalse(dictionary.ContainsKey(134));
Assert.IsFalse(dictionary.ContainsKey(187));
Assert.IsTrue(dictionary.ContainsKey(214));
```
