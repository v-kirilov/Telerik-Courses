# Linked List Cycles

#### You have a singly-linked list and want to check if it contains a cycle.

A singly-linked list is built with nodes, where each node has:

- node.Next - the next node in the list.
- node.Value - the data held in the node.

A _Node_ might look like this:

```cs
public class Node
{
    public int Value
    {
        get;
        set;
    }
    public Node Next
    {
        get;
        set;
    }
}
```

A **cycle** occurs when a node’s next points back to a previous node in the list. The linked list is no longer linear with a beginning and end - instead, it cycles through a loop of nodes.

![Singly-linked List with Cycle](https://i.ibb.co/FwKqSdR/1473367882-8b197c6f91-Linked-List-Cycle.png "Singly-linked List with Cycle")

Write a method `DetectCycle()` that takes the first node in a singly-linked list and returns a boolean indicating whether the list contains a cycle or not.

***

<details>
<summary>Give me a hint!</summary>

Because a cycle could result from the last node linking to the first node, we might need to look at every node before we even see where the cycle begins. So it seems like we can’t do better than $`O(n)`$ runtime.

How can we track the nodes we’ve already seen?

***

<details>
<summary>Give me a hint!</summary>


Using a **set**, we could store all the nodes we’ve seen so far. The algorithm is simple:

1. If the current node is already in our set, we have a cycle. Return *true*.
2. If the current node is null we've hit the end of the list. Return *false*.
3. Else throw the current node in our set and keep going.

```cs
public static bool DetectCycleWithSet(Node head)
{
    var set = new HashSet<Node>();
    var current = head;

    while (current != null)
    {
        if (set.Contains(current))
        {
            return true;
        }

        set.Add(current);

        current = current.Next;
    }

    return false;
}
```

What are the time and space costs of this approach? Can we do better?

Our runtime is $`O(n)`$, the best we can do. But our space cost is also $`O(n)`$. Can we get our space cost down to $`O(1)`$ by storing a constant number of nodes?

Think about a *looping list* and a *linear list*. What happens when you traverse one versus the other?

A *linear list* has an end - a node that doesn’t have a next node. But a *looped list* will run forever. We know we don’t have a loop if we ever reach an end node, but how can we tell if we’ve run into a loop?

***

<details>
<summary>Give me a hint!</summary>

We can’t just run our method for a really long time, because we’d never really know with certainty if we were in a loop or just a really long list.

Imagine that you're running on a long, mountainous running trail that happens to be a loop. What are some ways you can tell you're running in a loop?

One way is to look for **landmarks**. You could remember one specific point, and if you pass that point again, you know you’re running in a loop. Can we use that principle here?

***

<details>
<summary>Give me a hint!</summary>


Well, our cycle can occur after a non-cyclical "head" section in the beginning of our linked list. So we'd need to make sure we chose a "landmark" node that is in the cyclical "tail" and not in the non-cyclical "head." That seems impossible unless we already know whether or not there's a cycle...

Think back to the running trail. Besides landmarks, what are some other ways you could tell you’re running in a loop? What if you had another runner? (Remember, it’s a singly-linked list, so no running backwards!)

A tempting approach could be to have the other runner stop and act as a "landmark," and see if you pass her again. But we still have the problem of making sure our "landmark" is in the loop and not in the non-looping beginning of the trail.

What if our "landmark" runner moves continuously but slowly?

***

<details>
<summary>Give me a hint!</summary>


If we sprint *quickly* down the trail and the landmark runner jogs *slowly*, we will eventually "lap" (catch up to) the landmark runner!

But what if there isn't a loop?

Then we (the faster runner) will simply hit the end of the trail (or linked list).

***

<details>
<summary>Give me a hint!</summary>


So let's make two variables, `slowRunner` and `fastRunner`. We’ll start both on the first node, and every time `slowRunner` advances one node, we’ll have `fastRunner` advance two nodes.

If `fastRunner` catches up with `slowRunner`, we know we have a loop. If not, eventually `fastRunner` will hit the end of the linked list and we'll know we don't have a loop.

This is a complete solution! Can you code it up?

Make sure the method eventually terminates in all cases!

***

<details>
<summary>Solution</summary>

We keep two pointers to nodes (we'll call these “runners”), both starting at the first node. Every time `slowRunner` moves one node ahead, `fastRunner` moves two nodes ahead.

If the linked list has a cycle, `fastRunner` will "lap" (catch up with) `slowRunner`, and they will momentarily equal each other.

If the list does not have a cycle, `fastRunner` will reach the end.

```cs
public static bool DetectCycleFastAndSlow(Node head)
{
    Node slow = head;
    Node fast = head;

    while (fast != null && fast.Next != null)
    {

        slow = slow.Next;
        fast = fast.Next.Next;

        if (fast == slow)
        {
            return true;
        }
    }

    return false;
}
```

***

!["Two Runners Approach"](https://upload.wikimedia.org/wikipedia/commons/thumb/5/5f/Tortoise_and_hare_algorithm.svg/1024px-Tortoise_and_hare_algorithm.svg.png "Two Runners Approach")

## Complexity

$`O(n)`$ time and $`O(1)`$ space.

The runtime analysis is a little tricky. The worst case is when we do have a cycle, so we don't return until `fastRunner` equals `slowRunner`. But how long will that take?

First, we notice that when both runners are circling around the cycle `fastRunner` can never skip over `slowRunner`. Why is this true?

Suppose `fastRunner` had just skipped over `slowRunner`. `fastRunner` would only be 1 node ahead of `slowRunner`, since their speeds differ by only 1. So we would have something like this:

```
[ ] -> [s] -> [f]
```

What would the step right before this "skipping step" look like? `fastRunner` would be 2 nodes back, and `slowRunner` would be 1 node back. But wait, that means they would be at the same node! So `fastRunner` didn't skip over `slowRunner`! (This is a proof by contradiction.)

Since `fastRunner` can't skip over `slowRunner`, at most `slowRunner` will run around the cycle once and `fastRunner` will run around twice. This gives us a runtime of $`O(n)`$.

For space, we store two variables no matter how long the linked list is, which gives us a space cost of $`O(1)`$

[Linked List Cycle Article in Leetcode](https://leetcode.com/articles/linked-list-cycle/)

## Bonus

1. How would you detect the first node in the cycle? Define the first node of the cycle as the one closest to the head of the list.
2. Would the program always work if the fast runner moves three steps every time the slow runner moves one step?

## What We Learned

Some people have trouble coming up with the "two runners" approach. That's expected - it's somewhat of a blind insight. Even great candidates might need a few hints to get all the way there. And that's fine.

*Remember that the coding interview is a dialogue, and sometimes your interviewer expects he'll have to offer some hints along the way.*

One of the most impressive things you can do as a candidate is listen to a hint, fully understand it, and take it to its next logical step. Interview Cake gives you lots of opportunities to practice this. Don't be shy about showing lots of hints on our exercises - that's what they're there for!

</details>
</details>
</details>
</details>
</details>
</details>
</details>