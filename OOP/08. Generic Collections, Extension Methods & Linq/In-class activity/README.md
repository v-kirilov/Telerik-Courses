<img src="https://webassets.telerikacademy.com/images/default-source/logos/telerik-academy.svg)" alt="logo" width="300px" style="margin-top: 20px;"/>

# Linqify

### Description 
The provided template includes 12 tasks which are already solved using loops. Your task is to add an alternative implementation using LINQ extension methods, without using any loops. Once you load the project and open the Task folder you'll find 2 methods in each task class - an ```Execute()``` method and an ```ExecuteWithLINQ()``` method. You need the write your implementations in the latter. 

Feel free to use the ```Main()``` method to test your implementation. Just call the desired method from the static class you're currently working on and foreach the collection, print the result or do any checks you find necessary.

---

#### Task 01
You must filter all people with blue eyes and return them. You are already provided with the following implementation using a for loop:

```c#
public static List<Person> Execute(List<Person> people)
{
    var result = new List<Person>();
    for (int i = 0; i < people.Count; i++)
    {
        if (people[i].eyeColor == "blue")
        {
            result.Add(people[i]);
        }
    }
    return result;
}
```

In order to rewrite this using LINQ you can use the ```Where()``` and ```ToList()``` extension methods. 
How do we go about rewriting this using LINQ? First we need to filter only the people having blue eyes. Filtering can be done using the ```Where()``` method with a lambda expression inside it.

```c#
people.Where(person => person.eyeColor == "blue")
```
The above code filters all people with blue eyes. The only thing left is to materialize the collection using the ```ToList()``` method.

Here is the solution:

```c#
public static List<Person> ExecuteWithLINQ(List<Person> people)
{
    return people.Where(person => person.eyeColor == "blue").ToList();
}
```

That's a lot shorter and cleaner, isn't it?

#### Task 02
Now we must filter all people who are younger than 30 years and return them. Again, you are provided with a loop solution that looks like this:

```c#
public static List<Person> Execute(List<Person> people)
{
    var result = new List<Person>();

    for (int i = 0; i < people.Count; i++)
    {
        if (people[i].age < 30)
        {
            result.Add(people[i]);
        }
    }

    return result;
}
```

Now try to rewrite it using LINQ - which extension methods will you use?

> **Hint:** here is the signature you need to follow:

```c#
people
    .Where(person => /* check age */)
    .ToList()
```

#### Task 03
Now we have to filter the people who are over 30 and then sum their age. The provided solution looks like this: 

```c#
public static int Execute(List<Person> people)
{
    int totalAge = 0;

    for (int i = 0; i < people.Count; i++)
    {
        if (people[i].age > 30)
        {
            totalAge += people[i].age;
        }
    }

    return totalAge;
}
```

> **Hint:** here is the signature you need to follow:
```c#
people
    .Where(person => /* check age */)
    .Sum()
```

#### Task 04
Find if all people are 18 or older. Here is the solution using a for loop:

```c#
public static bool Execute(List<Person> people)
{
    for (int i = 0; i < people.Count; i++)
    {
        if (people[i].age <= 18)
        {
            return false;
        }
    }

    return true;
}
```

<details>
<summary><strong>Hint:</strong></summary>
<p>

```c#
people.All(person => /* check age */)
```

</p>
</details> 

#### Task 05
Now find if any person's first name is "Terry". Here is the loop solution:

```c#
public static bool Execute(List<Person> people)
{
    for (int i = 0; i < people.Count; i++)
    {
        if (people[i].firstName == "Terry")
        {
            return true;
        }
    }
    return false;
}
```

<details>
<summary><strong>Hint:</strong></summary>
<p>

```c#
people.Any(person => /* check name */)
```

</p>
</details>

#### Task 06
Find the count of the people who's first name is longer than 6 characters and their eye color is brown.


```c#
public static int Execute(List<Person> people)
{
    int count = 0;
    for (int i = 0; i < people.Count; i++)
    {
        if (people[i].firstName.Length > 6 && people[i].eyeColor == "brown")
        {
            count++;
        }
    }
    return count;
}
```

<details>
<summary><strong>Hint:</strong></summary>
<p>

```c#
people
    .Where(person => /* check first name && eye color */)
    .Count()
```

</p>
</details>

#### Task 07
Now find the first person with brown eyes and return it.

```c#
public static Person Execute(List<Person> people)
{
    for (int i = 0; i < people.Count; i++)
    {
        if (people[i].eyeColor == "brown")
        {
            return people[i];
        }
    }

    return null;
}
```

<details>
<summary><strong>Hint:</strong></summary>
<p>

```c#
people.FirstOrDefault(person => /* check eye color */)
```

</p>
</details>

#### Task 08
Return a collection of strings containing every person's full name.

```c#
public static List<string> Execute(List<Person> people)
{
    List<string> fullNames = new List<string>();

    for (int i = 0; i < people.Count; i++)
    {
        fullNames.Add($"{people[i].firstName} {people[i].lastName}");
    }

    return fullNames;
}
```

<details>
<summary><strong>Hint:</strong></summary>
<p>

```c#
people
    .Select(person => /* first name + last name */)
    .ToList()
```

</p>
</details>

#### Task 09
Filter the people who's first name ends with 'a' and find their average age.


```c#
public static double Execute(List<Person> people)
{
    var result = new List<Person>();
    var totalSum = 0.00;
    var filteredPeople = 0;

    for (int i = 0; i < people.Count; i++)
    {
        if (people[i].firstName.EndsWith('a'))
        {
            totalSum += people[i].age;
            filteredPeople++;
        }
    }
    return totalSum/filteredPeople;
}
```

<details>
<summary><strong>Hint:</strong></summary>
<p>
Use Where(), followed by Average().
</p>
</details>

#### Task 10
Return a collection of people skipping the first 5 in the list and then taking another 5


```c#
public static List<Person> Execute(List<Person> people)
{
    var result = new List<Person>();
    for (int i = 0; i < people.Count; i++)
    {
        if (i >= 5 && i <10)
        {
            result.Add(people[i]);
        }
    }
    return result;
}
```

<details>
<summary><strong>Hint:</strong></summary>
<p>

Try chaining Skip(), followed by Take(). Then materialize the collection using ToList().

</p>
</details>

#### Task 11
Return a collection ordered by first name and then ordered by last name.


```c#
public static List<Person> Execute(List<Person> people)
{
    var result = new List<Person>(people);

    result.Sort((a, b) =>
    {
        if (a.firstName.CompareTo(b.firstName) != 0)
        {
            return a.firstName.CompareTo(b.firstName);
        }
        else
        {
            return a.lastName.CompareTo(b.lastName);
        }
    });

    return result;
}
```

<details>
<summary><strong>Hint:</strong></summary>
<p>

Use OrderBy(), followed by ThenBy(). Then materialize the collection.

</p>
</details>


#### Task 12
Find the people who are under or are 55 and their eye color is green. Then order them by first name, then by last name and add them to the result list until a person's last name ends with 'z'


```c#
public static List<Person> Execute(List<Person> people)
{
    var filteredPeople = new List<Person>();

    for (int i = 0; i < people.Count; i++)
    {
        if (people[i].age <= 55 && people[i].eyeColor == "green")
        {
            filteredPeople.Add(people[i]);
        }
    }

    filteredPeople.Sort((a, b) =>
    {
        if (a.firstName.CompareTo(b.firstName) != 0)
        {
            return a.firstName.CompareTo(b.firstName);
        }
        else
        {
            return a.lastName.CompareTo(b.lastName);
        }
    });

    var result = new List<Person>();

    for (int i = 0; i < filteredPeople.Count; i++)
    {
        if (filteredPeople[i].lastName.EndsWith('z'))
        {
            break;
        }
        result.Add(filteredPeople[i]);
    }

    return result;
}
```

<details>
<summary><strong>Hint:</strong></summary>
<p>

Use Where(), followed by OrderBy(), then ThenBy(), then TakeWhile(). Finally materialize the collection.

</p>
</details>