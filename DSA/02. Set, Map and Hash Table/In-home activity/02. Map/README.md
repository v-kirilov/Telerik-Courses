<img src="https://webassets.telerikacademy.com/images/default-source/logos/telerik-academy.svg" alt="logo" width="300px" style="margin-top: 20px;"/>

## DSA - 05. Map

### Definition

The `Dictionary<TKey, TValue>` class in .NET is an implementation of the non-linear abstract data type **Associative array**. It is part of the _System.Collections.Generic_ namespace.

The **associative array** or more commonly refered to as **map**, **symbol table**, or **dictionary** is an abstract data type composed of a collection of (key, value) pairs, such that each possible key appears at most once in the collection.

### Operations

Operations associated with this data type allow:

the modification of an existing pair
the lookup of a value associated with a particular key

- `Add(key, value)`  →  adds a pair to the collection  →  **​O(1)**
  - The key must be unique
- `Remove(key)` →  removes a pair from the collection by using a key  →  **​O(1)**
- `this[key] = value`  →  updates the value associated with the specified key  →  ​**​O(1)**
- `this[key]`  →  returns the value associated with the specified key​  →  **​O(1)**


> Think why these operations have these complexities?

### Dictionary\<TKey, TValue>

#### Constructors
The `Dictionary<TKey, TValue>` class can be instantiated in few different ways.

- You can create an empty dictionary.
  ```cs
    // Creates a new empty dictionary
    var dictionary = new Dictionary<string, int>();
    ```

- Or you can create a dictionary from an existing `KeyValuePair` collection.
    ```cs
    // A dictionary containing Country - Capital pairs
    var list = new List<KeyValuePair<string, string>>()
    {
        new KeyValuePair<string,string>("Argentina", "Buenos Aires"),
        new KeyValuePair<string,string>("Bulgaria", "Sofia"),
        new KeyValuePair<string,string>("Colombia", "Bogota"),
        new KeyValuePair<string,string>("Denmark", "Copenhagen"),
        new KeyValuePair<string,string>("Egypt", "Cairo"),
        new KeyValuePair<string,string>("France", "Paris")
    };

    var dictionary = new Dictionary<string, string>(list);
    ```

    The above can be simplified further:
    ```cs
    // A dictionary containing Country - Capital pairs
    var dictionary = new Dictionary<string, string>()
    {
        { "Argentina", "Buenos Aires" },
        { "Bulgaria", "Sofia" },
        { "Colombia", "Bogota" },
        { "Denmark", "Copenhagen" },
        { "Egypt", "Cairo" },
        { "France", "Paris" }
    };
    ```

#### Properties
- `Count` returns the number of elements contained in the dictionary.

  ```cs
  Console.WriteLine($"Elements in the dictionary: {dictionary.Count}");
  ```
  ```
  6
  ```

- `Keys` is a collection containing the keys in the dictionary. 

  ```cs
  Console.WriteLine(string.Join(", ", dictionary.Keys));
  ```
  ```
  Argentina, Bulgaria, Colombia, Denmark, Egypt, France
  ```

- `Values` is a collection containing the values in the dictionary.

  ```cs
  Console.WriteLine(string.Join(", ", dictionary.Values));
  ```
  ```
  Buenos Aires, Sofia, Bogota, Copenhagen, Cairo, Paris
  ```

#### Methods
- To add an element use the `Add(key,value)` method which adds the specified key and value to the dictionary.

  ```cs
  // A dictionary containing Country - Capital pairs
  var dictionary = new Dictionary<string, string>();

  dictionary.Add("Argentina", "Buenos Aires");
  dictionary.Add("Bulgaria", "Sofia");
  dictionary.Add("Colombia", "Bogota");
  ```

  > What happens if we add an element that already exists?

  ```cs
  dictionary.Add("Bulgaria", "Svoge");
  ```

  If an element with the same key already exists in the dictionary an `ArgumentException` is thrown. 

- The `Remove(key)` method removes the value with the specified key from the dictionary. This method returns `true` if the element is successfully found and removed; otherwise, `false`. This method _also_ returns `false` if the key is not found in the dictionary.

  ```cs
  dictionary.Remove("Bulgaria"); // true
  dictionary.Remove("Bulgaria"); // false
  ```

- `ContainsKey(key)` determines whether the dictionary contains the specified key. The method returns `true` if the dictionary contains the specified key; otherwise, `false`.

    ```cs
    // A dictionary containing Country - Capital pairs
    var dictionary = new Dictionary<string, string>()
    {
        { "Argentina", "Buenos Aires" },
        { "Bulgaria", "Sofia"   },
        { "Colombia", "Bogota"  }
    };

    dictionary.ContainsKey("Bulgaria"); // true

    dictionary.Remove("Bulgaria");      // true

    dictionary.ContainsKey("Bulgaria"); // false
    ```

    > What is the complexity of `ContainsKey(key)` and why?

- `ContainsValue(value)` determines whether the dictionary contains a specific value. The method returns `true` if the dictionary contains the specified value; otherwise, `false`.

    ```cs
    // A dictionary containing Country - Capital pairs
    var dictionary = new Dictionary<string, string>()
    {
        { "Argentina", "Buenos Aires" },
        { "Bulgaria", "Sofia"   },
        { "Colombia", "Bogota"  }
    };

    dictionary.ContainsValue("Sofia"); // true
    dictionary.ContainsValue("New York"); // false
    ```

    > What is the complexity of `ContainsValue(value)` and why?

- To get a value by key you can use one of two ways.
  - Using the property indexer.
    ```cs
    // A dictionary containing Country - Capital pairs
    var dictionary = new Dictionary<string, string>()
    {
        { "Argentina", "Buenos Aires" },
        { "Bulgaria", "Sofia" },
        { "Colombia", "Bogota" },
        { "Denmark", "Copenhagen" },
        { "Egypt", "Cairo" },
        { "France", "Paris" }
    };

    string country = "Bulgaria";
    // using property indexer to get a value by key
    string capitol = dictionary[country]; 

    Console.WriteLine($"The capitol of {country} is {capitol}.");
    ```

    > Be careful with the property indexer because if you try to get a value using a _key that does NOT exist_ in the dictionary you will receive a `KeyNotFoundException`. 

    ```cs
    string country = "USA";
    string capitol = dictionary[country]; // KeyNotFoundException will be thrown
    ```

  - If you suspect that the key might not exist, then either wrap the property indexer in a `try/catch` block or use the `TryGetValue(key, out value)` method.

    ```cs
    // A dictionary containing Country - Capital pairs
    var dictionary = new Dictionary<string, string>()
    {
        { "Argentina", "Buenos Aires" },
        { "Bulgaria", "Sofia" },
        { "Colombia", "Bogota" },
        { "Denmark", "Copenhagen" },
        { "Egypt", "Cairo" },
        { "France", "Paris" }
    };

    string country = "USA";

    dictionary.TryGetValue(country, out string capitol);

    if (string.IsNullOrEmpty(capitol))
    {
          Console.WriteLine($"The capitol of {country} cannot be found!");
    }
    else
    {
          Console.WriteLine($"The capitol of {country} is {capitol}.");
    }
    ```

#### Iteration
The `Dictionary<TKey, TValue>` class supports enumeration and can be used in a loop.

```cs
// A dictionary containing Country - Capital pairs
var dictionary = new Dictionary<string, string>()
{
    { "Argentina", "Buenos Aires" },
    { "Bulgaria", "Sofia" },
    { "Colombia", "Bogota" },
    { "Denmark", "Copenhagen" },
    { "Egypt", "Cairo" },
    { "France", "Paris" }
};

foreach (var pair in dictionary)
{
    Console.WriteLine($"The capitol of {pair.Key} is {pair.Value}.");
}
```

```
The capitol of Argentina is Buenos Aires.
The capitol of Bulgaria is Sofia.
The capitol of Colombia is Bogota.
The capitol of Denmark is Copenhagen.
The capitol of Egypt is Cairo.
The capitol of France is Paris.
```

---

### Tasks

1. Counting the occurrences of a specific element in a collection
   We can use map to implement function that counts how many times each element is presented in a collection. The idea here is to use each element as key and its occurences as values. We will get use of the fact that set is updating the element and it's doing it fast.

    ```cs
    static Dictionary<string, int> CountOccurrences(string[] array)
    {
        var dictionary = new Dictionary<string, int>();
        foreach (string item in array)
        {
            if (!dictionary.ContainsKey(item))
            {
                dictionary.Add(item, 0);
            }

            dictionary[item]++;
        }

        return dictionary;
    }
    ```

    ```cs
    var result = CountOccurrences(new[] { "js", "c#", "js", "c#", "c++" });
    foreach (var item in result)
    {
        Console.WriteLine($"{item.Key}: {item.Value}");
    }
    ```

    ```
    js: 2
    c#: 2
    c++: 1
    ```

2. Grouping by key.  
Using similar technique we can group a collection of objects by its key property.
   1. Let's consider this array:
        ```cs
        var data = new[]
        {
            new KeyValuePair<string,string>("US", "New York"),
            new KeyValuePair<string,string>("BG", "Sofia"),
            new KeyValuePair<string,string>("UK", "London"),
            new KeyValuePair<string,string>("BG", "Plovdiv"),
            new KeyValuePair<string,string>("UK", "Manchester"),
            new KeyValuePair<string,string>("US", "Chicago")
        };
        ```

   2. We would like to know how many cities we have per each country. This means we want to group by the `Key` property of each pair. Additionally, all values for a certain key will be added to a list.
      ```cs
      static Dictionary<string, List<string>> Group(KeyValuePair<string, string>[] data)
      {
          var dictionary = new Dictionary<string, List<string>>();

          foreach (var kvp in data)
          {
              if (!dictionary.ContainsKey(kvp.Key))
              {
                  dictionary.Add(kvp.Key, new List<string>());
              }

              dictionary[kvp.Key].Add(kvp.Value);
          }

          return dictionary;
      }
      ``` 

   3. Grouping the data should result in something like this.
      ```cs
      var groupedData = Group(data);

      foreach (var item in groupedData)
      {
          Console.WriteLine($"{item.Key}: {string.Join(", ", item.Value)}");
      }
      ```
          
      ```
      US: New York, Chicago
      BG: Sofia, Plovdiv
      UK: London, Manchester
      ```
