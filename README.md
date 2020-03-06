# OrderedDictionary

[![NuGet version (SoftCircuits.OrderedDictionary)](https://img.shields.io/nuget/v/SoftCircuits.OrderedDictionary.svg?style=flat-square)](https://www.nuget.org/packages/SoftCircuits.OrderedDictionary/)

```
Install-Package SoftCircuits.OrderedDictionary
```

## Introduction

OrderedDictionary is a .NET library that implements an ordered dictionary. It provides all the functionality of `Dictionary<TKey, TValue>` but also maintains the items in an ordered list. Items can be added, removed and accessed by key or index. The class implements IDictionary.

## Example

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

Like a dictionary, items can by accessed by key. You can also use the `ByIndex` property to access an item by index.

```cs
Debug.Assert(dictionary[127] == "Gary Wilson");
Debug.Assert(dictionary.ByIndex[3] == "Add Carpenter");
```


