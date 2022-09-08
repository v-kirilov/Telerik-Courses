# Cafe Order Checker

#### My cake shop is so popular, I'm adding some tables and hiring wait staff so folks can have a cute sit-down cake-eating experience.

I have two registers: one for take-out orders, and the other for the other folks eating inside the cafe. All the customer orders get combined into one list for the kitchen, where they should be handled first-come, first-served.

Recently, some customers have been complaining that people who placed orders after them are getting their food first. Yikes - that's not good for business!

There are two registers: one for take-out orders, and the other for the other folks eating inside the cafe. All the customer orders get combined into one list for the kitchen, where they should be handled first-come, first-served.

Recently, some customers have been complaining that people who placed orders after them in the same register are getting their food first. Please keep in mind that people that order in the take-out do not see orders and serving of the people inside.

You have three arrays, containing the orders numbers in the order they were accepted.

- takeOutOrders: The take-out orders as they were entered into the system and given to the kitchen.
- dineInOrders: The dine-in orders as they were entered into the system and given to the kitchen.
- servedOrders: Each customer order (from either register) as it was finished by the kitchen.

Given all three arrays, write a method to check that the service really is first-come, first-served. All food should come out in the same order customers from each register requested it.

We'll represent each customer order as a unique integer.

As an example,

```
Take Out Orders: [1, 3, 5]
Dine In Orders: [2, 4, 6]
Served Orders: [1, 2, 4, 6, 5, 3]
```

would ***not*** be first-come, first-served, since order **3** was requested before order **5** but order **5** was served first.

But,

```
Take Out Orders: [1, 3, 5]
Dine In Orders: [2, 4, 6]
Served Orders: [1, 2, 3, 5, 4, 6]
```

would be first-come, first-served.

***

<details>
<summary>Give me a hint!</summary>

How can we re-phrase this problem in terms of smaller subproblems?

***

<details>
<summary>Give me a hint!</summary>

Breaking the problem into smaller subproblems will clearly involve reducing the size of at least one of our lists of customer order numbers. So to start, let's try taking the first customer order out of `servedOrders`.

What should be true of this customer order number if my service is first-come, first-served?

***

<details>
<summary>Give me a hint!</summary>

If my cake cafe is first-come, first-served, then the first customer order finished (first item in `servedOrders`) has to either be the first take-out order entered into the system (`takeOutOrders[0]`) or the first dine-in order entered into the system (`dineInOrders[0]`).

Once we can check the first customer order, how can we verify the remaining ones?

***

<details>
<summary>Give me a hint!</summary>

Let's "throw out" the first customer order from `servedOrders` as well as the customer order it matched with from the beginning of `takeOutOrders` or `dineInOrders`. That customer order is now "accounted for."

Now we're left with a smaller version of the original problem, which we can solve using the same approach! So we keep doing this over and over until we exhaust `servedOrders`. If we get to the end and every customer order "checks out," we return true.

How do we implement this in code?

***

<details>
<summary>Give me a hint!</summary>

Now that we have a problem that's the same as the original problem except smaller, our first thought might be to use **recursion**. All we need is a *base case*.

What's our base case?

***

<details>
<summary>Give me a hint!</summary>

We stop when we run out of customer orders in our servedOrders. So that's our base case: when we've checked all customer orders in servedOrders, we return true because we know all of the customer orders have been "accounted for."

```cs
private static boolean CheckOrders(int[] takeOut, int[] dineIn, int[] served) 
{
    if (served.Length == 0)
    {
        return true;
    }

    if (takeOut.Length > 0 && takeOut[0] == served[0])
    {
        return CheckOrders(RemoveFirstFromArray(takeOut), dineIn, RemoveFirstFromArray(served));
    }

    if (dineIn.Length > 0 && dineIn[0] == served[0])
    {
        return CheckOrders(takeOut, RemoveFirstFromArray(dineIn), RemoveFirstFromArray(served));
    }

    return false;
}

private static int[] RemoveFirstFromArray(int[] array)
{
    int[] newArray = new int[array.Length - 1];
    Array.Copy(array, 1, newArray, 0, array.Length - 1);
    return newArray;
}
```

This solution will work, but can we do better?

***

<details>
<summary>Give me a hint!</summary>

Before we talk about optimization, note that our inputs are probably pretty small. This method will take hardly any time or space, even if it could be more efficient. In industry, especially at small startups that want to move quickly, optimizing this might be considered a "premature optimization." Great engineers have both the skill to see how to optimize their code and the wisdom to know when those optimizations aren't worth it. At this point in the interview you can say - "I think we can optimize this a bit further, although given the nature of the input this probably won't be very resource-intensive anyway... should we talk about optimizations?"

Suppose we do want to optimize further. What are the time and space costs to beat? This method will take $`O(n^2)`$ time and $`O(n^2)`$ additional space.

***

<details>
<summary>Give me a hint!</summary>

Take a look at the above snippet again:

It costs $`O(m)`$ time and space, where ***m*** is the size of the resulting array. That's going to determine our overall time and space cost here—the rest of the work we're doing is constant space and time.

In our recursing we'll build up ***n*** frames on the call stack.

Each of those frames will hold a different slice of our original `servedOrders` (and `takeOutOrders` and `dineInOrders`, though we only slice one of them in each recursive call).

So, what's the total time and space cost of all our slices?

If `servedOrders` has `n` items to start, taking our first slice takes `n − 1` time and space (plus half that, since we're also slicing one of our halves—but that's just a constant multiplier so we can ignore it). In our second recursive call, slicing takes `n − 2` time and space. Etc.

So our total time and space cost for slicing comes to:

$`( n − 1 ) + ( n − 2 ) + . . . + 2 + 1`$

This is a series that turns out to be $`O(n^2)`$.

***

<details>
<summary>Give me a hint!</summary>

We can do better than this $`O(n^2)`$ time and space cost. One way we could to that is to avoid slicing and instead keep track of indices in the array:

```cs
private static bool CheckOrders(int[] takeOut, int[] dineIn, int[] served, int servedIndex, int takeOutIndex, int dineInIndex)
{
    if (servedIndex == served.Length)
    {
        return true;
    }

    if (takeOutIndex < takeOut.Length && takeOut[takeOutIndex] == served[servedIndex])
    {
        takeOutIndex++;
    }
    else if (dineInIndex < dineIn.Length && dineIn[dineInIndex] == served[servedIndex])
    {
        dineInIndex++;
    }
    else
    {
        return false;
    }

    servedIndex++;

    return CheckOrders(takeOut, dineIn, served, servedIndex, takeOutIndex, dineInIndex);
}
```

So now we're down to $`O(n)`$ time, but we're still taking $`O(n)`$ space in the call stack because of our recursion. We can rewrite this as an iterative method to get that memory cost down to $`O(1)`$.

What's happening in each iteration of our recursive method? Sometimes we're taking a customer order out of `takeOutOrders` and sometimes we're taking a customer order out of `dineInOrders`, but we're always taking a customer order out of `servedOrders`.

So what if instead of taking customer orders out of `servedOrders` 1-by-1, we iterated over them?

That should work. But are we missing any edge cases?

What if there are extra orders in `takeOutOrders` or `dineInOrders` that don't appear in `servedOrders`? Would our kitchen be first-come, first-served then?

Maybe.

It's possible that our data doesn't include everything from the afternoon service. Maybe we stopped recording data before every order that went into the kitchen was served. It would be reasonable to say that our kitchen is still first-come, first-served, since we don't have any evidence otherwise.

On the other hand, if our input is supposed to cover the entire service, then any orders that went into the kitchen but weren't served should be investigated. We don't want to accept people's money but not serve them!

*When in doubt, ask!* This is a great question to talk through with your interviewer and shows that you're able to think through edge cases.

Both options are reasonable. In this writeup, we'll enforce that every order that goes into the kitchen (appearing in `takeOutOrders` or `dineInOrders`) should come out of the kitchen (appearing in `servedOrders`). How can we check that?

To check that we've served every order that was placed, we'll validate that when we finish iterating through `servedOrders`, we've exhausted `takeOutOrders` and `dineInOrders`.

***

<details>
<summary>Solution</summary>

We walk through servedOrders, seeing if each customer order so far matches a customer order from one of the two registers. To check this, we:

1. Keep pointers to the current index in `takeOut`, `dineIn`, and `served`.
2. Walk through `served` from beginning to end.
3. If the current order in `served` is the same as the current customer order in `takeOut`, increment `takeOutIndex` and keep going. This can be thought of as "checking off" the current customer order in takeOut and served, reducing the problem to the remaining customer orders in the arrays.
4. Same as above with dineIn.
5. If the current order isn't the same as the customer order at the front of `takeOut` or `dineIn`, we know something's gone wrong and we're not serving food first-come, first-served.
6. If we make it all the way to the end of `served`, we'll check that we've reached the end of `takeOut` and `dineIn`. If every customer order checks out, that means we're serving food first-come, first-served.

```cs
private static bool CheckOrders(int[] takeOut, int[] dineIn, int[] served)
{
    int take = 0;
    int dine = 0;

    foreach (int order in served)
    {
        if (take < takeOut.Length && order == takeOut[take])
        {
            take++;
        }
        else if (dine < dineIn.Length && order == dineIn[dine])
        {
            dine++;
        }
        else
        {
            return false;
        }
    }

    return take == takeOut.Length && dine == dineIn.Length;
}
```

***

## Complexity

$`O(n)`$ time and $`O(1)`$ additional space.

***

## What We Learned

If you read the whole breakdown section, you might have noticed that our recursive function cost us extra space. If you missed that part, go back and take a look.

Be careful of the hidden space costs from a recursive function's call stack! If you have a solution that's recursive, see if you can save space by using an iterative algorithm instead.

***

## Bonus

1. This assumes each customer order in servedOrders is unique. How can we adapt this to handle arrays of customer orders with potential repeats?
1. Our implementation returns true when all the items in dineInOrders and takeOutOrders are first-come first-served in servedOrders and false otherwise. That said, it'd be reasonable to throw an exception if some orders that went into the kitchen were never served, or orders were served but not paid for at either register. How could we check for those cases?
1. Our solution iterates through the customer orders from front to back. Would our algorithm work if we iterated from the back towards the front? Which approach is cleaner?

</details>
</details>
</details>
</details>
</details>
</details>
</details>
</details>
</details>
</details>