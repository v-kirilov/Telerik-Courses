<img src="https://i.imgur.com/yqIN5FX.png" width="300px" />

# Cosmetics shop - workshop (OOP - Classes exercise)

### 1. Description
The shop already has a working Engine. You do not have to touch anything in it. Just use it.
Each product has **name, brand, price and gender** (men, women, unisex).
There are **categories** of products. Each **category** has **name** and products can be **added or removed**. The same product can be added to a category more than once. There is also a **shopping cart**. Products can be **added or removed** from it. The same product can be added to the shopping cart more than once. The shopping cart can calculate the **total price** of all products in it.
- Your **task** is to **finish the implementation** of the classes to model the cosmetics shop.
- The **NotImplementedExceptions** should give you an idea where to write code.

### 2. Category class
#### Description
- Minimum category name’s length is 2 symbols and maximum is 15 symbols.
- Products in category should be sorted by brand in ascending order and then by price in descending order.  
- When removing product from category, if the product is not found you should throw an exception.
- Category’s Print() should return text in the following format:

```
#Category: {category name}
 #{Name} {Brand}
 #Price: {price}
 #Gender: {genderType}
 ===
 #{Name} {Brand}
 #Price: {price}
 #Gender: {genderType}
 ===
```

```
#Category: {category name}
 #No products in this category
```

### 3. Products
#### Description
- Minimum product name’s length is 3 symbols and maximum is 10 symbols.
- Minimum brand name’s length is 2 symbols and maximum is 10 symbols.
- Price cannot be negative.
- Gender type can be **"Men"**, **"Women"** or **"Unisex"**.
- Print returns text in the following format: _(you might consider reusing this in the category print.)_
```
#{Name} {Brand}
#Price: {Price}
#Gender: {GenderType}
```

### 4. Shopping cart
#### Description
- Adding the same product more than once is allowed.
- Do not check if the product exists, when removing it from the shopping cart.

> **Constraint 1** - If a null value is passed to some mandatory property, your program should throw a proper exception.  
> **Constraint 2** - There is no need to touch any classes outside the Models folder.

> **Notes** - To simplify your work you are given an already built Engine (for executing some basic operations) and ApplicationData (contains all products and categories).

### Input example

```
CreateProduct MyMan Nivea 10.99 Men
CreateCategory Shampoos
AddToCategory Shampoos MyMan
AddToShoppingCart MyMan
ShowCategory Shampoos 
TotalPrice
RemoveFromCategory Shampoos MyMan
ShowCategory Shampoos
RemoveFromShoppingCart MyMan
TotalPrice
```

### Output Example

```
Product with name MyMan was created!
Category with name Shampoos was created!
Product MyMan added to category Shampoos!
Product MyMan was added to the shopping cart!
#Category: Shampoos
 #MyMan Nivea
 #Price: $10.99
 #Gender: Men
 ===
$10.99 total price currently in the shopping cart!
Product MyMan removed from category Shampoos!
#Category: Shampoos
 #No products in this category
Product MyMan was removed from the shopping cart!
No products in shopping cart!
```

> **Hint**: You don't need to take care of the Engine class, the Repository class and the Main method but of course you could try to understand how they work.

>You are given a template of the Cosmetics shop. Please take a look at it carefully before you try to do anything. Try to understand all the classes and how they are supposed to interact with each other. (You should not touch the Engine and Repository classes at all).
