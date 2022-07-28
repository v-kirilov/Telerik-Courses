<img src="https://i.imgur.com/yqIN5FX.png" width="300px" />

# Cosmetics shop - workshop (OOP - Principles exercise)

### 1. Description
The provided project has two types of products - shampoos and toothpastes. Your **task** is to **design an object-oriented class hierarchy** to model the cosmetics shop, **using the best practices for object-oriented design** (OOD) and **object-oriented programming** (OOP). Encapsulate correctly all fields and use validation whenever necessary. Use the unit tests to help you along the way! At the end of this document we've provided you with an example of an input and output. All the interfaces you would need to complete your task are already present in the project. In order to achieve the best OOP design **you have to use them all!**

Some general notes and constraints:
- You don't need to take care of the Engine class and the Main method but of course you could try to understand how they work.
- To simplify your work you are given an already built execution engine that executes a sequence of commands read from the console using the classes and interfaces in your project (see the Cosmetics-Template folder)
- The **NotImplementedExceptions** should give you an idea where to write code (Models/`Toothpaste`, `Shampoo`; Core/`Repository`, `CommandFactory`; Commands/`CreateShampooCommand`, `CreateToothpasteCommand`)

### 2. Shampoo class
#### Description
Below you'll find a list of tasks and constraints you need to follow in order to implement the Shampoo product correctly:
- Implements IShampoo
- It has **name, brand, price, gender, millilitres, usage**
- Minimum shampoo name’s length is 3 symbols and maximum is 10 symbols.
- Minimum shampoo brand name’s length is 2 symbols and maximum is 10 symbols.
- Price cannot be negative.
- Gender type can be **"Men"**, **"Women"** or **"Unisex"**.
- Milliliters are not negative number
- Usage type can be **"EveryDay"** or **"Medical"**
- If a null value is passed to some mandatory property, your program should throw a proper exception.
> Note - All number type fields should be printed “as is”, without any formatting or rounding.

> **Hint**: Use the Unit tests whenever you finish a task.


### 3. Toothpaste class
#### Description
Below you'll find a list of tasks and constraints you need to follow in order to implement the Toothpaste product correctly:
- Implements IToothpaste
- It has **name, brand, price, gender, ingredients**
- Minimum toothpaste name’s length is 3 symbols and maximum is 10 symbols.
- Minimum toothpaste brand name’s length is 2 symbols and maximum is 10 symbols.
- Price cannot be negative.
- Gender type can be **"Men"**, **"Women"** or **"Unisex"**.
- Ingredients are comma separated values
- If a null value is passed to some mandatory property, your program should throw a proper exception.
> Note - All number type fields should be printed “as is”, without any formatting or rounding.

> **Hint**: Use the Unit tests whenever you finish a task.


### 4. Repository class
#### Description
Repository stores all the application's data. It is responsible for creating all the objects(products, categories, shopping cart). It also provides methods for finding objects. Some of the methods are already implemented, other throw `NotImplementedException` and you should implement them.

### Interfaces

- All the needed interfaces are already there. **You must use them all** in order to achieve the best OOP design.

### Unit Tests

You are given unit tests to keep track of your progress.

## Step by step guide

> *Hint*: You don't need to modify the Engine class and the Main method but of course you could try to understand how they work.

> *Hint*: Run the Unit tests whenever you finish a task.

> *Hint*: See what methods are there in `ValidationHelper` class and use them whenever possible!

1. Implement the classes for shampoo and toothpaste

   - Implement the necessary interfaces.
   - Look at the **Models.Contracts** folder and think about how to build a correct hierarchy.
   - Create the necessary fields and initialize them in the constructor.
   - Validate all the fields according to the constraints above.
   - Implement all the necessary methods. Leave the print method for later.
   - Did you notice the repeating code in the Shampoo and Toothpaste classes (the common fields/methods)? What could you do in order to avoid the repetition?

1. Implement the unfinished methods in `Repository`.

   - This is where the creation of the object should be.
   - Add the newly created object to the list.

1. Implement `CreateShampooCommand` and `CreateToothpasteCommand`.

   - In the `Execute` method validate that the input parameters' count match the expected count. Next, they should be extracted and parsed, and after that used to create the Shampoo/Toothpaste.

     > *Hint*: Look at `CreateCategoryCommand` if you get stuck.

1. Implement methods in `CommandFactory`.

   - This is where we connect the incoming command from the console with the class that implements that certain command.

1. Implement the `Print()` methods


### Input example

```
CreateShampoo MyMan Nivea 10.99 Men 1000 EveryDay
CreateToothpaste White Colgate 10.99 Men calcium,fluorid
CreateCategory Shampoos
CreateCategory Toothpastes
AddToCategory Shampoos MyMan
AddToCategory Toothpastes White
AddToShoppingCart MyMan
AddToShoppingCart White
ShowCategory Shampoos
ShowCategory Toothpastes
TotalPrice
RemoveFromCategory Shampoos MyMan
ShowCategory Shampoos
RemoveFromShoppingCart MyMan
TotalPrice
```

### Output Example

```
Shampoo with name MyMan was created!
Toothpaste with name White was created!
Category with name Shampoos was created!
Category with name Toothpastes was created!
Product MyMan added to category Shampoos!
Product White added to category Toothpastes!
Product MyMan was added to the shopping cart!
Product White was added to the shopping cart!
#Category: Shampoos
#MyMan Nivea
 #Price: $10.99
 #Gender: Men
 #Milliliters: 1000
 #Usage: EveryDay
 ===
#Category: Toothpastes
#White Colgate
 #Price: $10.99
 #Gender: Men
 #Ingredients: calcium, fluorid
 ===
$21.98 total price currently in the shopping cart!
Product MyMan removed from category Shampoos!
#Category: Shampoos
 #No product in this category
Product MyMan was removed from the shopping cart!
$10.99 total price currently in the shopping cart!
```
> **Note** - Don't worry if your program prints with a ','(comma) as a decimal separator instead a '.'(dot). That's a Windows setting and not an issue with your implementation.

### 5. OPTIONAL ADVANCED TASK - Cream class
#### Description
- Implement new product and its creation in the engine class. 
- Cream (name, brand, price, gender, scent)
    - name minimum 3 symbols and maximum 15
    - brand minimum 3 symbols and maximum 15
    - price greater than zero
    - gender (men, women, unisex)
    - scent (lavender, vanilla, rose)
- Implement product creation in the Factory and the Engine
    - Just look at the other products
- Test it if it works correctly

