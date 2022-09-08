<img src="https://webassets.telerikacademy.com/images/default-source/logos/telerik-academy.svg" alt="logo" width="300px" style="margin-top: 20px;"/>

## DSA - 04. Set

### Definition

The `HashSet<T>` class in .NET is an implementation of the non-linear abstract data type **Set**. It is part of the _System.Collections.Generic_ namespace.

The **Set** abstract data structure has two important characteristics:
  1. Elements in the set are unique​.
  2. Order is not important. ​

> Why are hash tables a good choice when implementing a set?

### Operations

The operations that one may use with a set are:​

- `Add(item)`  →  adds given element to the set  →  **​O(1)**
  - The element is added somewhere, in no order or place
  - If the such element already exists in the set, nothing happens - elements are unique.
- `Remove(item)` →  removes element from the set  →  **​O(1)**
- `Contains(item)`  →  determines whether the element is in the set  →  ​**​O(1)**
- `Count`  →  returns the number of elements that are contained in the set.  →  **​O(n)**


> Why do you think these operations have such complexities?

### HashSet\<T>

#### Constructors
The `HashSet<T>` class can be instantiated in few different ways.

- You can create an empty set.
  ```cs
    // Creates a new empty set
    var set = new HashSet<int>();
    ```

- Or you can create a set from an existing collection.
    ```cs
    // Creates a new set by copying the elements from the given collection
    var list = new List<int>() { 1, 2, 3, 4, 5 };
    var set = new HashSet<int>(list);
    ```

#### Properties
The `Count` property returns the number of elements contained in the set.

```cs
Console.WriteLine($"Elements in the set: {set.Count}");
```

#### Methods
- Add an element
  ```cs
  set.Add(1);
  set.Add(2);
  set.Add(3);
  ```

  > What happens if we add an element that already exists?

  ```cs
  var set = new HashSet<int>();
  set.Add(1);
  set.Add(2);
  set.Add(3);

  Console.WriteLine($"Count: {set.Count}"); // 3

  set.Add(1);
  set.Add(2);
  set.Add(3);

  Console.WriteLine($"Count: {set.Count}"); // ?
  ```
  > The `Add()` method returns `true` if the element is added to the set; `false` if the element already exists in the set.

- Remove an element.
  ```cs
  var set = new HashSet<int>();
  set.Add(1);
  set.Add(2);
  set.Add(3);

  Console.WriteLine($"Remove 2: {set.Remove(2)}"); // true
  Console.WriteLine($"Remove 2: {set.Remove(2)}"); // false
  ```

  > The `Remove()` method returns `true` if the element is successfully found and removed; otherwise, `false`.

- Contains an element

    ```cs
    var set = new HashSet<int>();
    set.Add(1);
    set.Add(2);
    set.Add(3);

    Console.WriteLine($"Contains 3: {set.Contains(3)}"); // true
    Console.WriteLine($"Contains 5: {set.Contains(5)}"); // false
    ```

    > The `Contains()` method returns `true` if the set contains the specified element; otherwise, `false`.

#### Iteration
Sets are iterable objects and can be enumerated in a loop.
  ```cs
  var set = new HashSet<int>();
  set.Add(1);
  set.Add(2);
  set.Add(3);

  foreach (var item in set)
  {
      Console.WriteLine(item);
  }
  ```
  > Do you remember how we made our lists iterable/enumerable?

---

### Tasks

1. Check if all elements are unique
   
   We can use a set to implement a function that checks whether the elements in a collection are unique. There are two possible approaches we can take.
   
   1. Create a new set from the existing collection and check if the number of elements in the set is equal to the number of elements in the existing collection. If they are the same, then the existing collection contains unique elements only.

      ```cs
      static bool AreAllElementsUnique<T>(IEnumerable<T> collection)
      {
          var set = new HashSet<T>(collection);

          return set.Count == collection.Count(); // requires System.Linq
      }
      ```

     1. Another approach is to iterate over the elements of the collection and try to add each one of them in an empty set. 
      
        > The `Add(item)` method of the `HashSet<T>` returns `true` or `false` depending on whether the item has been added to set or not.
      
        ```cs
        static bool AreAllElementsUnique<T>(IEnumerable<T> collection)
        {
            var set = new HashSet<T>();

            foreach (var item in collection)
            {
                if (!set.Add(item))
                {
                    return false;
                }
            }

            return true;
        }
        ```  

      ```cs
      var array = new int[] { 1, 2, 3, 4 };
      Console.WriteLine(AreAllElementsUnique(array)); // true

      var list = new List<int>() { 1, 2, 3, 3 };
      Console.WriteLine(AreAllElementsUnique(list)); // false

      Console.WriteLine(AreAllElementsUnique("telerik")); // false

      Console.WriteLine(AreAllElementsUnique("tElerik")); // true
      ```

2. Removing duplicate elements
    
    To remove duplicate elements from a collection we can use the same approach as before. We will create a new set from an existing collection and return the set as a list.

    ```cs
    static IEnumerable<T> Distinct<T>(IEnumerable<T> collection)
    {
        var set = new HashSet<T>(collection);
        return set;
    }
    ```

    ```cs
    var uniqueCharacters = Distinct("abbbc");
    Console.WriteLine(string.Join(',', uniqueCharacters)); // a,b,c

    var uniqueNumbers = Distinct(new int[] { 1, 2, 3, 3, 4, 1 });
    Console.WriteLine(string.Join(',', uniqueNumbers)); // 1,2,3,4
    ```

3. Union of two sets
    
    Union of two sets is a set containing unique elements that appear in _either_ of the initial sets.

    To solve this task we will need two sets, one for each collection. The we will iterate `set2` and add all of its elements to `set1`.

      > In what order will elements be added? Does it matter which set is the first and which is the second?

    ```cs
    static IEnumerable<T> Union<T>(IEnumerable<T> collection1, IEnumerable<T> collection2)
    {
        var set1 = new HashSet<T>(collection1);
        var set2 = new HashSet<T>(collection2);

        foreach (var item in set2)
        {
            set1.Add(item);
        }

        return set1;
    }
    ```

    ```cs
    var union1 = Union("abc", "bce");
    Console.WriteLine(string.Join(',', union1)); // a,b,c,e

    var union2 = Union("abc", "def");
    Console.WriteLine(string.Join(',', union2)); // a,b,c,d,e,f

    var collection1 = new int[] { 1, 5, 9 };
    var collection2 = new int[] { 3, 5, 8 };
    var union3 = Union(collection1, collection2);
    Console.WriteLine(string.Join(',', union3)); // 1,5,9,3,8
    ```
   
      > As a matter of fact the .NET `HashSet<T>` has a method called `UnionWith()` that does exactly that. It modifies a set to contain all elements that are present in itself, the other set, or both.

4. Intersection of two set
    
    Intersection of two sets is a set containing unique elements that exist in _both_ of the initial sets.
   
    The idea here is to iterate over the elements of one of the sets and keep only those elements that exist in the other as well.

    ```cs
    static IEnumerable<T> Intersect<T>(IEnumerable<T> collection1, IEnumerable<T> collection2)
    {
        var set1 = new HashSet<T>(collection1);
        var set2 = new HashSet<T>(collection2);
        var intersection = new HashSet<T>();

        foreach (var item in set2)
        {
            if(set1.Contains(item))
            {
                intersection.Add(item);
            }
        }

        return intersection;
    }
    ```

    ```cs
    var intersection1 = Intersect("abc", "bce");
    Console.WriteLine(string.Join(',', intersection1)); // b,c

    var intersection2 = Intersect("abc", "def");
    Console.WriteLine(string.Join(',', intersection2)); // 

    var collection1 = new int[] { 1, 5, 9 };
    var collection2 = new int[] { 3, 5, 8 };
    var intersection3 = Intersect(collection1, collection2);
    Console.WriteLine(string.Join(',', intersection3)); // 5
    ```
    
5. Difference between two sets
    
    The difference between two sets is a set containing elements that are in the first set, but not in the second. 

    ```cs
    static IEnumerable<T> Difference<T>(IEnumerable<T> collection1, IEnumerable<T> collection2)
    {
        var set1 = new HashSet<T>(collection1);
        var set2 = new HashSet<T>(collection2);
        var difference = new HashSet<T>();

        foreach (var item in set1)
        {
            if (!set2.Contains(item))
            {
                difference.Add(item);
            }
        }

        return difference;
    }
    ```
   
    Let's find the difference between `[1, 3, 5, 7, 9]` and `[2, 3, 4, 6]`.

    ```cs
    var collection1 = new int[] { 1, 3, 5, 7, 9 };
    var collection2 = new int[] { 2, 3, 4, 6 };
    var difference = Difference(collection1, collection2);
    Console.WriteLine(string.Join(',', difference)); // 1,5,7,9
    ```
> Research on your own what **symmetric difference** between sets is and try to implement it.
