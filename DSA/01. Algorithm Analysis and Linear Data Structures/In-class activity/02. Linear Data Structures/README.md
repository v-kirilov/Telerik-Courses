<img src="https://webassets.telerikacademy.com/images/default-source/logos/telerik-academy.svg)" alt="logo" width="300px" style="margin-top: 20px;"/>

# Linear Data Structures

### 1. Description 

In this exercise, we are going to practice classic problems involving linear data structures. Try your best by following the guides, debugging your implementation, and if all else fails, consulting with the solutions. These tasks are common interview questions and you are **very likely to encounter them again**.

### 2. Test if two lists are equal

Given two singly linked lists, return if they are equal. Equality is defined as having the same elements in the same order and quantity.

```cs
public static bool AreListsEqual<T>(SinglyLinkedList<T> list1, SinglyLinkedList<T> list2)
{
    // Add your implementation here.
}
```

**Examples**
```
Input
 list1: 1 -> 2 -> 3
 list2: 1 -> 2 -> 3

Output
 true
```

```
Input
 list1: 1 -> 2 -> 3
 list2: 1 -> 2 -> 3 -> 4

Output
 false
```

```
Input
 list1: 1 -> 2 -> 3
 list2: 3 -> 2 -> 1

Output
 false
```
### 3. Find the middle node in a list

Given a singly linked list, return a reference to its middle node.

```cs
public static Node<T> FindMiddleNode<T>(SinglyLinkedList<T> list)
{
    // Add your implementation here.
}
```

**Examples:**
```
Input
 list: 1 -> 2 -> 3

Output
 2
```
```
Input
 list: 1 -> 2 -> 3 -> 4

Output
 3
```

If this was an array, it would be easy, but lists have no indices. One possible solution is to traverse the whole list and `count` the number of nodes. Then traverse the list again till `count/2` and return the node at `count/2`. Another possible solution is to traverse the list using two pointers. Move the first (`slow`) pointer by one and the second (`fast`) one by two. When the `fast` pointer reaches the end of the list the `slow` pointer will be at the middle.

### 4. Merge Sorted Linked Lists 

The method _MergeLists_ takes two _sorted_ linked lists and merges them before returning a new sorted linked list.

```cs
public static SinglyLinkedList<T> MergeLists<T>(SinglyLinkedList<T> list1, SinglyLinkedList<T> list2)
{
    // Add your implementation here.
}
```

**Example:**
```
Input
 list1: 1 -> 2 -> 3
 list2: 1 -> 4

Output
 merged: 1 -> 1 -> 2 -> 3 -> 4
```

Try to think about the problem for a while, but if you get lost, here are some basic steps:
1. Get a reference to the heads of each list.
2. Iterate over both lists using their heads and on every iteration compare the values of `head1` and `head2`.
3. Add the lower value to the resulting merged list.
4. Advance the lower of the two nodes to the next node.
5. Repeat until both h1 and h2 are null.
6. Return the merged list.

### 5. Reverse Linked List

The method _ReverseList_ takes a singly linked list as an input parameter and returns a new linked list in reversed order.

```cs
public static SinglyLinkedList<T> ReverseList<T>(SinglyLinkedList<T> list)
{
    // Add your implementation here.
}
```

**Example:**
```
Input
 list: 1 -> 2 -> 3 -> 4

Output
 reversed: 4 -> 3 -> 2 -> 1
```

#### Approach I
You can push all nodes to a stack and then construct the reversed list by popping them.  
This will consume additional O(n) memory.

#### Approach II (Advanced)
You can solve it without additional memory (O(1) complexity) by using one additional reference and constructing the reversed list on the fly. (Each `next` node is actually `prev` for the `current` one in the reversed list.)

### 6. Valid Parentheses

The method _ValidateParentheses_ takes a string _expression_ and determines if every closing bracket has a corresponding opening bracket.

```cs
public static bool AreValidParentheses(string expression)
{
    // Add your implementation here.
}
```

**Examples:**
```
Input
 expression: 1 + (2 * 3)

Output
 true
```

```
Input
 expression: 2 + (1 + (2 * 3)

Output
 false
```

Try solving it by using a stack. 
1. Think about **when** and **what** to push and when to pop.
1. What happens if its time to pop but the stack is empty?
1. If the expression is valid, how many items do you expect to find in the stack?

### 7. Backspace charater deletion

If you open any text editor, let's say Notepad, and press the following keys on the keyboard: `a`, `b`, `c`, **`backspace`**, `d` the output on the screen should be is `abd` because you have deleted `c` with the _backspace_ key.

The method _RemoveBackspaces_ takes a sequence of keystrokes in the form of a string, a single character that represents the _backspace_ and calculates the output for the screen.

```cs
public static string RemoveBackspaces(string sequence, char backspaceChar)
{
    // Add your implementation here.
}
```

**Examples:**
```
Input
 sequence: "abc#d"
 backspaceChar: '#'

Output
 "abd"
```

```
Input
 sequence: "abcd##e##"
 backspaceChar: '#'

Output
 "a"
```

You can once again use a stack.
1. Most of the keystrokes will be pushed, but sometimes (think when) you need to pop.
2. At the end, you must convert the remaining stack elements to a string.