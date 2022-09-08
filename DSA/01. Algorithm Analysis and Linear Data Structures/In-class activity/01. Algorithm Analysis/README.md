<img src="https://webassets.telerikacademy.com/images/default-source/logos/telerik-academy.svg" alt="logo" width="300px" style="margin-top: 20px;"/>

# Algorithm Complexity Tasks

Define the complexity of each program/algorithm.

## 1. Product

```cs
// Returns the product of two numbers.
int Product(int a, int b)
{
    int sum = 0;

    for (int i = 0; i < b; i++)
    {
        sum += a;
    }

    return sum;
}
```

---
## 2. Power

```cs
// Returns a to the power of b
int Power(int a, int b)
{
    if (b < 0)
    {
        return 0;
    }

    if (b == 0)
    {
        return 1;
    }

    int power = a;

    while (b > 1)
    {
        power *= a;
        b--;
    }

    return power;
}
```

---
## 3. Mod
```cs
// Returns the remainder after dividing a by b
int Mod(int a, int b)
{
    if (b < 0)
    {
        return -1;
    }

    int div = a / b;
    return a - div * b;
}
```

---
## 4. Sum3

```cs
int Sum3(int n)
{
    int sum = 0;
    for (int a = 0; a < n; a++)
    {
        for (int b = 0; b < n; b++)
        {
            for (int c = 0; c < n; c++)
            {
                sum += (a * b * c);
            }
        }
    }
    return sum;
}
```

---
## 5. SumNM

```cs
int SumNM(int n, int m)
{
    int sum = 0;
    for (int a = 0; a < n; a++)
    {
        for (int b = 0; b < m; b++)
        {
            sum += (a * b);
        }
    }
    return sum;
}
```

---
## 6. IsEven

```cs
// Determines if a number is even.
bool IsEven(int number)
{
    return number % 2 == 0;
}
```

---
## 7. GetElementByIndex

```cs
// Returns the element at the given index.
int GetElementByIndex(int[] array, int index)
{
    int element = array[index];
    return element;
}
```

---
## 8. FindMaxElement

```cs
// Finds the maximum value from an unsorted array.
int FindMaxElement(int[] array)
{
    int max = int.MinValue;

    for (int i = 0; i < array.Length; i++)
    {
        if (array[i] > max)
        {
            max = array[i];
        }
    }

    return max;
}
```

---
## 9. Contains

```cs
// Determines whether an element exists in an unsorted array.
bool Contains(int[] array, int element)
{
    for (int i = 0; i < array.Length; i++)
    {
        if (array[i] == element)
        {
            return true;
        }
    }

    return false;
}
```

---
## 10. GetElementIndex

```cs
// Finds the index of an element in a sorted array.
int GetElementIndex(int[] array, int elementToFind)
{
    // Perform binary search
    int leftIndex = 0;
    int rightIndex = array.Length - 1;

    while (leftIndex <= rightIndex)
    {
        int middleIndex = leftIndex + (rightIndex - leftIndex) / 2;
        int middleElement = array[middleIndex];

        // Check if the element is at the middle
        if (middleElement == elementToFind)
        {
            return middleIndex;
        }

        // If the element is greater than the element in the middle, ignore the left half 
        if (elementToFind > middleElement)
        {
            leftIndex = middleIndex + 1;
        }
        // If the element is smaller than the element in the middle, ignore the right half 
        else
        {
            rightIndex = middleIndex - 1;
        }
    }

    // The element has not been found, return -1
    return -1;
}
```

---
## 11. HasDuplicates

```cs
// Checks if an array has duplicate values.
bool HasDuplicates(int[] array)
{
    for (int outerIndex = 0; outerIndex < array.Length; outerIndex++)
    {
        for (int innerIndex = 0; innerIndex < array.Length; innerIndex++)
        {
            if (outerIndex == innerIndex)
            {
                continue;
            }

            if (array[outerIndex] == array[innerIndex])
            {
                return true;
            }
        }
    }

    return false;
}
```

---
## 12. Iterative Fibonacci

```cs
// Returns the Fibonacci number on the n-th position using an iterative approach.
int GetFibonacciIterative(int n)
{
    if (n < 0)
    {
        return 0;
    }

    if (n < 2)
    {
        return n;
    }

    int prev = 0;
    int result = 1;

    for (int i = 1; i < n; i++)
    {
        int temp = result;
        result += prev;
        prev = temp;
    }

    return result;
}
```

---
## 13. Recursive Fibonacci

```cs
// Returns the Fibonacci number on the n-th position using a recursive approach.
int GetFibonacciRecursive(int n)
{
    if (n <= 1)
        return 1;

    return GetFibonacciRecursive(n - 1) + GetFibonacciRecursive(n - 2);
}
```

---