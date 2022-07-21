<img src="https://webassets.telerikacademy.com/images/default-source/logos/telerik-academy.svg" alt="logo" width="300px" style="margin-top: 20px;"/>

# 👄 Exceptional Cosmetics 👄

## Description
You are given a software system for managing a cosmetics shop. The system is already implemented and works. It supports one type of products that can be grouped in categories. The user can:
- Create a category;
- Create a product;
- Add a product to a category;
- Show a category;

## Task
Your **task** is to improve the solution by adding proper **exception handling** which serves the need to validate the user input and provide informative and useful messages. The user input should meet the following requirements.

### 1. Category
- Name should be between 3 and 10 symbols.
- Name should be unique in the system.

### 2. Product
- Name should be between 3 and 10 symbols.
- Name should be unique in the system.
- Brand should be between 2 and 10 symbols.
- Price cannot be negative.
- Gender type can be **"Men"**, **"Women"** or **"Unisex"**.

### 3. Commands
- Each command expects exact number of parameters.
- Each command expects correct parameters in correct format.

## Constraints
- You can define your own exceptions.
- You can add try-catch blocks.
- You should throw proper exceptions with proper messages.

## Additional notes
- Follow the exception handling best practices.
- Throw exceptions where the program cannot continue its normal flow.
- Handle exceptions where you can address the problem in the best way.

## Step by step guide

**Hint** There are **TODOs** in the code to help you.

**1.** Define your type of exception(s) that will serve your needs.

- You can start with one custom exception and later add more if you need.

**2.** Implement validations for the fields.

**3.** Implement command arguments validations (arguments count and value).

**4.** Add exception handling to `CommandFactory` class.

- What should happen if the user enters an invalid command?
- What should happen if the enum `CommandType` has commands that don't have a command class?

**5.** Add exception handling to the `Engine` class.

## Input example

```
SomeCommand
CreateCategory S
CreateCategory Shampoos
CreateCategory Shampoos
CreateProduct MyMan 10.99 Men
CreateProduct MyMan N 10.99 Men
CreateProduct MyMan Nivea price Men
CreateProduct MyMan Nivea -5.99 Men
CreateProduct MyMan Nivea 10.99 Gender
CreateProduct MyMan Nivea 10.99 Men
CreateProduct MyMan Nivea 10.99 Men
AddProductToCategory Shampo MyMan
AddProductToCategory Shampoos MyBoy
AddProductToCategory Shampoos MyMan
CreateProduct MyWoman Nivea 17.99 Women
AddProductToCategory Shampoos MyWoman
ShowCategory Shampo
ShowCategory Shampoos
Exit
```

## Output Example

```
SomeCommand
Command SomeCommand is not supported.
CreateCategory S
Category name should be between 3 and 10 symbols.
CreateCategory Shampoos
Category with name Shampoos was created!
CreateCategory Shampoos
Category Shampoos already exist.
CreateProduct MyMan 10.99 Men
CreateProduct command expects 4 parameters.
CreateProduct MyMan N 10.99 Men
Product brand should be between 2 and 10 symbols.
CreateProduct MyMan Nivea price Men
Third parameter should be price (real number).
CreateProduct MyMan Nivea -5.99 Men
Price can't be negative.
CreateProduct MyMan Nivea 10.99 Gender
Forth parameter should be one of Men, Women or Unisex.
CreateProduct MyMan Nivea 10.99 Men
Product with name MyMan was created!
CreateProduct MyMan Nivea 10.99 Men
Product MyMan already exist.
AddProductToCategory Shampo MyMan
Category Shampo does not exist.
AddProductToCategory Shampoos MyBoy
Product MyBoy does not exist.
AddProductToCategory Shampoos MyMan
Product MyMan added to category Shampoos!
CreateProduct MyWoman Nivea 17.99 Women
Product with name MyWoman was created!
AddProductToCategory Shampoos MyWoman
Product MyWoman added to category Shampoos!
ShowCategory Shampo
Category Shampo does not exist.
ShowCategory Shampoos
#Category: Shampoos
#MyMan Nivea
 #Price: $10,99
 #Gender: Men
 ===
#MyWoman Nivea
 #Price: $17,99
 #Gender: Women
 ===
Exit
```
