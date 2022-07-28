<img src="https://webassets.telerikacademy.com/images/default-source/logos/telerik-academy.svg)" alt="logo" width="300px" style="margin-top: 20px;"/>

# LINQ Practice

### 1. Description
Now we will practice some of the LINQ methods provided by the System.Linq library. You've been provided with an already built project, however there are certain method implementations missing. Once you open it you'll see 2 projects, Data and Examples. Data is not important right now, it simply feeds data to the other project which we will use. If you find the ```Main``` method inside the Examples project you'll see several examples listed. You can comment/uncomment methods here in order to see what is working and what is not.
The actual place you'll write code in is inside the Examples folder. There you'll find several classes - each contains methods which are yet to be implemented.
Above each example method there is a comment describing what the code should do as well as what LINQ methods you should use in order to complete the task
.
```cs
// This sample uses Count to get the number of odd numbers in the array.
public static void Example02()
{
    int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
    //Your implementation here
    Console.WriteLine("There are {0} odd numbers in the list.", oddNumbers);
}
```

### 2. AggregationOperators class
#### Description
In this class there are 16 examples that need to be implemented. The LINQ methods that must be used are ```Count()```, ```Select()```, ```Sum()```, ```Min()```, ```Max()```, ```Average()``` and ```Aggregate()```.

### 3. ElementOperators class
#### Description
Here we have a total of 5 methods we need to complete. The methods used here are: ```First()```, ```FirstOrDefault()``` and ```ElementAt()```.

### 4. OrderingOperators class
#### Description
In this class there are 12 examples. The LINQ methods needed to complete it are: ```OrderBy()```, ```OrderByDescending()```, ```Reverse()``` and ```ThenBy()```.

### 5. PartitioningOperators class
#### Description
The LINQ methods needed to complete this task are ```Take()```, ```Skip()```, ```TakeWhile()``` and ```SkipWhile()```, 

### 6. Quantifiers class
#### Description
Here you need ```Any()``` and ```All()```. 

### 7. RestrictionOperators class
#### Description
Use ```Where()``` to solve those tasks.

### 8. SetOperators class
#### Description
Here you must use ```Distinct()```, ```Union()```, ```Intersect()``` and ```Except()```.

### 9. MiscellaneousOperators class
#### Description
Here we have only 4 examples. The LINQ methods needed are: Concat```()``` and ```SequenceEquals()```