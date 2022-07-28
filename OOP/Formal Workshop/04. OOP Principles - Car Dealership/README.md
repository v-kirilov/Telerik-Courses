<img src="https://webassets.telerikacademy.com/images/default-source/logos/telerik-academy.svg)" alt="logo" width="300px" style="margin-top: 20px;"/>

## OOP Workshop - Car Dealership

There are 3 types of vehicles in the Car Dealership - **Car**, **Motorcycle** and **Truck**. Each vehicle has a **make**, **model**, **wheels count** and **price**.  

Additionally, each:
- **car** has **seats**
- **motorcycle** has a **category**
- **truck** has a **weight capacity**

The Car Dealership has users as well.  

Each user:
- Has a collection of vehicles. Every vehicle in this collection has a collection of comments.
- Can add and remove vehicles from their collection(**AddVehicle**, **RemoveVehicle**)
- Can add or remove comments to a vehicle (**AddComment**, **RemoveComment**)

A user has to **register** and **login** before using the Car Dealership system. If a user is not logged in or there is another user logged in he cannot do anything. Don't worry, this has already been implemented :) Nevertheless, do try to understand how it works.

**IMPORTANT**: The project fails to build due to incomplete class hierarchy.

### Design the Class Hierarchy

Your task is to design an object-oriented class hierarchy to model a Car Dealership, using the best practices for object-oriented design (OOD) and object-oriented programming (OOP). Avoid duplicated code through abstraction, inheritance, and polymorphism and encapsulate correctly all fields.
You are given few interfaces that you should connect in a reasonable hierarchy, implement and use as a basis of your code.

### Validation

**The error messages and all the constraints for each field that must be validated can be *found in the example output*. If you are unsure about some constraints, run the tests. You already have the error messages in the proper classes.**

- Vehicle validation
  - Make and model length.
  - Price range
  - Wheels count
  - Seats count (for car)
  - Category length (for motorcycle)
  - Weight capacity (for truck)

- Motorcycle wheels are **always 2**
- Car wheels are **always 4**
- Truck wheels are **always 8**

- Comment validation
  - Content

- User Validation
  - Username, FirstName, LastName and Password lengths

**All properties in the above interfaces are mandatory (cannot be null or empty).**

### User actions

- Adding a vehicle
  - If the user is admin he cannot add a vehicle
  - If the user is not VIP he cannot add more than 5 vehicles

- Adding a comment
  - Just add the comment to the list

- Remove a vehicle
  - Just remove the vehicle from the list

- Remove a comment
  - If the author of the comment is not the current user he cannot remove it

### Printing

- For the User class

`Username: {Username}, FullName: {FirstName} {LastName}, Role: {Role}`

- For all vehicles of the user

```none
--USER {Username}--
1. {Vehicle type}:
  Make: {Make}
  Model: {Model}
  Wheels: {Wheels}
  Price: ${Price}
  Category/Weight capacity/Seats: {Category/Weight capacity/Seats}
    --COMMENTS--
    ----------
    {Content}
      User: {Comment Username}
    ----------
    ----------
    {Content}
      User: {Comment username}
    ----------
    --COMMENTS--
2. {Vehicle type}:
  Make: {Make}
  Model: {Model}
  Wheels: {Wheels}
  Price: ${Price}
  Category/Weight capacity/Seats: {Category/Weight capacity/Seats}
    --NO COMMENTS--
```

- **The dashes separating the comments are exactly 10.**
- **Every indentation is exactly 2 spaces more than its parent.**
- **Price has `$` in front of the value and has no trailing zeros** *(e.g. `Price: $10000`)*

*Hint: You can use this method:*

```cs
public static string RemoveTrailingZerosFromDouble(double value)
{
    return value.ToString("G29", CultureInfo.InvariantCulture);
}
```

- **The weight capacity has `t` after the value** *(e.g. `Weight capacity: 40t`)*
- **Look into the example below to get better understanding of the printing format.**

#### Additional Notes

To simplify your work you are given an already built execution engine that executes a sequence of commands read from the console using the classes and interfaces in your project.

You should implement the empty classes. You can add new classes where needed and modify any of the existing code under the **Models** namespace if necessary.

Currently, the engine supports the following commands:

- **RegisterUser** **(username, firstName, lastName, password, role)** - registers user, if there is no such user already
- **Login** **(username, password)** - logs in user if there is no already logged in and there is such registered user
- **Logout** - logs out the current logged in user
- **AddVehicle** **(type, make, model, price, [category/seats/weightCapacity])** - adds a vehicle to the current user. The fourth parameter depends on the type of the vehicle
- **RemoveVehicle** **(vehicleIndex)** - remove the vehicle on that index if there is such
- **AddComment** **(content, author, vehicleIndex)** - add a comment with the content provided to the vehicle with that index and sets the author
- **RemoveComment** **(vehicleIndex, commentIndex, username)** - removes the comment from the vehicle
- **ShowVehicles** **(username)** - shows all the vehicles of the user

Commands that you should implement yourself:
- **ShowUsers** - shows all the users registered.

**All commands return appropriate success messages. In case of invalid operation or error, the engine returns appropriate error messages.**

### Step by step guide

**1.** Implement all the interfaces.

- Look at the contracts folder  and decide how to use any of the interfaces there.
- **HINT:** some interfaces are segregated (separated into several interfaces) like the Priceable interface.

**2.** Implement the classes in which there is a TODO.
**Hint:** Implement Vehicles classes first, then print methods and finally ShowUsersCommand.
**Hint:** You can check all TODOs in a project from the TODO tab on bottom left(may very for different IDE or different versions of the same IDE)

**3.** Validate all properties according to the guidelines set above.

- **HINT:** UsernamePattern = "^[A-Za-z0-9]+$";
- **HINT:** PsswordPattern = "^[A-Za-z0-9@*_-]+$";

**4.** Try running the tests. You can sort them by class in order to orient yourself more easily.

**5.** Implement printing.

- Instead of a Print() method, you need to override ToString() in order to output the classes in the console.

**6.** Implement the methods inside the User class

#### Sample Input

```none
RegisterUser p Petar Petrov 123456
RegisterUser pesh0= Petar Petrov 123456
RegisterUser pesh0 Petar Petrov 1234
RegisterUser pesh0 Petar P 123456
RegisterUser pesh0 P Petrov 123456
RegisterUser pesho Petar Petrov 123456
AddVehicle Motorcycle K Z1000 9999 Race
AddVehicle Motorcycle Kawasaki Z1000 -1000 Race
AddVehicle Motorcycle Kawasaki Z1000 9999 N
AddVehicle Car Opel Vectra 5000 -1
AddVehicle Truck Volvo FH4 11800 200
AddVehicle Motorcycle Kawasaki Z 9999 Race
AddVehicle Car Opel Vectra 5000 5
AddVehicle Car Mazda 6 10000 5
AddVehicle Motorcycle Suzuki V-Strom 7500 CityEnduro
AddVehicle Car BMW Z3 11200 2
AddVehicle Car BMW Z3 11200 2
AddVehicle Car BMW Z3 11200 2
AddComment {{U}} pesho 1
AddComment {{Amazing speed and handling!}} pesho 1
ShowUsers
RegisterUser pesho Petar Petrov 123457
Logout
RegisterUser pesho Petar Petrov 123457
RegisterUser gosho Georgi Georgiev 123457 VIP
Logout
Login pesho 123456
Login gosho 123457
Logout
Login gosho 123457
AddComment {{I like this one! It is faster than all the rest!}} pesho 1
RemoveComment 1 1 pesho
RemoveComment 2 5 pesho
AddVehicle Motorcycle Suzuki GSXR1000 8000 Racing
AddVehicle Car Skoda Fabia 2000 5
AddVehicle Car BMW 535i 7200 5
AddVehicle Motorcycle Honda Hornet600 4150 Race
AddVehicle Car Mercedes S500L 15000 5
AddVehicle Car Opel Zafira 8000 5
AddVehicle Car Opel Zafira 7450 5
AddVehicle Truck Volvo FH4 11800 40
ShowUsers
Logout
RegisterUser ivancho Ivan Ivanov admin Admin
AddVehicle Car Skoda Fabia 2000 5
ShowUsers
ShowVehicles gosho
ShowVehicles ivanch0
AddComment {{Empty comment}} pencho 1
AddComment {{Empty comment}} pesho 20
RemoveComment 1 1 pesho
ShowVehicles pesho
Logout
Login pesho 123456
AddComment {{I dream of having this one one day.}} pesho 1
Logout
Login ivancho admin
AddComment {{What is the mileage on it?}} pesho 3
Logout
Login pesho 123456
AddComment {{This one passed my by on the highway today. So pretty!}} pesho 3
ShowVehicles pesho
ShowVehicles gosho
ShowVehicles ivancho
Logout
Login gosho 123457
RemoveComment 1 2 pesho
ShowVehicles pesho
Logout
Login pesho 123456
RemoveVehicle 1
ShowVehicles pesho

```

#### Expected Output

```none
RegisterUser p Petar Petrov 123456
Username must be between 2 and 20 characters long!
####################
RegisterUser pesh0= Petar Petrov 123456
Username contains invalid symbols!
####################
RegisterUser pesh0 Petar Petrov 1234
Password must be between 5 and 30 characters long!
####################
RegisterUser pesh0 Petar P 123456
Lastname must be between 2 and 20 characters long!
####################
RegisterUser pesh0 P Petrov 123456
Firstname must be between 2 and 20 characters long!
####################
RegisterUser pesho Petar Petrov 123456
User pesho registered successfully!
####################
AddVehicle Motorcycle K Z1000 9999 Race
Make must be between 2 and 15 characters long!
####################
AddVehicle Motorcycle Kawasaki Z1000 -1000 Race
Price must be between 0.0 and 1000000.0!
####################
AddVehicle Motorcycle Kawasaki Z1000 9999 N
Category must be between 3 and 10 characters long!
####################
AddVehicle Car Opel Vectra 5000 -1
Seats must be between 1 and 10!
####################
AddVehicle Truck Volvo FH4 11800 200
Weight capacity must be between 1 and 100!
####################
AddVehicle Motorcycle Kawasaki Z 9999 Race
pesho added vehicle successfully!
####################
AddVehicle Car Opel Vectra 5000 5
pesho added vehicle successfully!
####################
AddVehicle Car Mazda 6 10000 5
pesho added vehicle successfully!
####################
AddVehicle Motorcycle Suzuki V-Strom 7500 CityEnduro
pesho added vehicle successfully!
####################
AddVehicle Car BMW Z3 11200 2
pesho added vehicle successfully!
####################
AddVehicle Car BMW Z3 11200 2
You are not VIP and cannot add more than 5 vehicles!
####################
AddVehicle Car BMW Z3 11200 2
You are not VIP and cannot add more than 5 vehicles!
####################
AddComment {{U}} pesho 1
Content must be between 3 and 200 characters long!
####################
AddComment {{Amazing speed and handling!}} pesho 1
pesho added comment successfully!
####################
ShowUsers
You are not an admin!
####################
RegisterUser pesho Petar Petrov 123457
User pesho is logged in! Please log out first!
####################
Logout
You logged out!
####################
RegisterUser pesho Petar Petrov 123457
User pesho already exist. Choose a different username!
####################
RegisterUser gosho Georgi Georgiev 123457 VIP
User gosho registered successfully!
####################
Logout
You logged out!
####################
Login pesho 123456
User pesho successfully logged in!
####################
Login gosho 123457
User pesho is logged in! Please log out first!
####################
Logout
You logged out!
####################
Login gosho 123457
User gosho successfully logged in!
####################
AddComment {{I like this one! It is faster than all the rest!}} pesho 1
gosho added comment successfully!
####################
RemoveComment 1 1 pesho
You are not the author of the comment you are trying to remove!
####################
RemoveComment 2 5 pesho
Cannot remove comment! The comment does not exist!
####################
AddVehicle Motorcycle Suzuki GSXR1000 8000 Racing
gosho added vehicle successfully!
####################
AddVehicle Car Skoda Fabia 2000 5
gosho added vehicle successfully!
####################
AddVehicle Car BMW 535i 7200 5
gosho added vehicle successfully!
####################
AddVehicle Motorcycle Honda Hornet600 4150 Race
gosho added vehicle successfully!
####################
AddVehicle Car Mercedes S500L 15000 5
gosho added vehicle successfully!
####################
AddVehicle Car Opel Zafira 8000 5
gosho added vehicle successfully!
####################
AddVehicle Car Opel Zafira 7450 5
gosho added vehicle successfully!
####################
AddVehicle Truck Volvo FH4 11800 40
gosho added vehicle successfully!
####################
ShowUsers
You are not an admin!
####################
Logout
You logged out!
####################
RegisterUser ivancho Ivan Ivanov admin Admin
User ivancho registered successfully!
####################
AddVehicle Car Skoda Fabia 2000 5
You are an admin and therefore cannot add vehicles!
####################
ShowUsers
--USERS--
1. Username: pesho, FullName: Petar Petrov, Role: Normal
2. Username: gosho, FullName: Georgi Georgiev, Role: VIP
3. Username: ivancho, FullName: Ivan Ivanov, Role: Admin
####################
ShowVehicles gosho
--USER gosho--
1. Motorcycle:
  Make: Suzuki
  Model: GSXR1000
  Wheels: 2
  Price: $8000
  Category: Racing
    --NO COMMENTS--
2. Car:
  Make: Skoda
  Model: Fabia
  Wheels: 4
  Price: $2000
  Seats: 5
    --NO COMMENTS--
3. Car:
  Make: BMW
  Model: 535i
  Wheels: 4
  Price: $7200
  Seats: 5
    --NO COMMENTS--
4. Motorcycle:
  Make: Honda
  Model: Hornet600
  Wheels: 2
  Price: $4150
  Category: Race
    --NO COMMENTS--
5. Car:
  Make: Mercedes
  Model: S500L
  Wheels: 4
  Price: $15000
  Seats: 5
    --NO COMMENTS--
6. Car:
  Make: Opel
  Model: Zafira
  Wheels: 4
  Price: $8000
  Seats: 5
    --NO COMMENTS--
7. Car:
  Make: Opel
  Model: Zafira
  Wheels: 4
  Price: $7450
  Seats: 5
    --NO COMMENTS--
8. Truck:
  Make: Volvo
  Model: FH4
  Wheels: 8
  Price: $11800
  Weight Capacity: 40t
    --NO COMMENTS--
####################
ShowVehicles ivanch0
There is no user with username ivanch0!
####################
AddComment {{Empty comment}} pencho 1
There is no user with username pencho!
####################
AddComment {{Empty comment}} pesho 20
The vehicle does not exist!
####################
RemoveComment 1 1 pesho
You are not the author of the comment you are trying to remove!
####################
ShowVehicles pesho
--USER pesho--
1. Motorcycle:
  Make: Kawasaki
  Model: Z
  Wheels: 2
  Price: $9999
  Category: Race
    --COMMENTS--
    ----------
    Amazing speed and handling!
      User: pesho
    ----------
    ----------
    I like this one! It is faster than all the rest!
      User: gosho
    ----------
    --COMMENTS--
2. Car:
  Make: Opel
  Model: Vectra
  Wheels: 4
  Price: $5000
  Seats: 5
    --NO COMMENTS--
3. Car:
  Make: Mazda
  Model: 6
  Wheels: 4
  Price: $10000
  Seats: 5
    --NO COMMENTS--
4. Motorcycle:
  Make: Suzuki
  Model: V-Strom
  Wheels: 2
  Price: $7500
  Category: CityEnduro
    --NO COMMENTS--
5. Car:
  Make: BMW
  Model: Z3
  Wheels: 4
  Price: $11200
  Seats: 2
    --NO COMMENTS--
####################
Logout
You logged out!
####################
Login pesho 123456
User pesho successfully logged in!
####################
AddComment {{I dream of having this one one day.}} pesho 1
pesho added comment successfully!
####################
Logout
You logged out!
####################
Login ivancho admin
User ivancho successfully logged in!
####################
AddComment {{What is the mileage on it?}} pesho 3
ivancho added comment successfully!
####################
Logout
You logged out!
####################
Login pesho 123456
User pesho successfully logged in!
####################
AddComment {{This one passed my by on the highway today. So pretty!}} pesho 3
pesho added comment successfully!
####################
ShowVehicles pesho
--USER pesho--
1. Motorcycle:
  Make: Kawasaki
  Model: Z
  Wheels: 2
  Price: $9999
  Category: Race
    --COMMENTS--
    ----------
    Amazing speed and handling!
      User: pesho
    ----------
    ----------
    I like this one! It is faster than all the rest!
      User: gosho
    ----------
    ----------
    I dream of having this one one day.
      User: pesho
    ----------
    --COMMENTS--
2. Car:
  Make: Opel
  Model: Vectra
  Wheels: 4
  Price: $5000
  Seats: 5
    --NO COMMENTS--
3. Car:
  Make: Mazda
  Model: 6
  Wheels: 4
  Price: $10000
  Seats: 5
    --COMMENTS--
    ----------
    What is the mileage on it?
      User: ivancho
    ----------
    ----------
    This one passed my by on the highway today. So pretty!
      User: pesho
    ----------
    --COMMENTS--
4. Motorcycle:
  Make: Suzuki
  Model: V-Strom
  Wheels: 2
  Price: $7500
  Category: CityEnduro
    --NO COMMENTS--
5. Car:
  Make: BMW
  Model: Z3
  Wheels: 4
  Price: $11200
  Seats: 2
    --NO COMMENTS--
####################
ShowVehicles gosho
--USER gosho--
1. Motorcycle:
  Make: Suzuki
  Model: GSXR1000
  Wheels: 2
  Price: $8000
  Category: Racing
    --NO COMMENTS--
2. Car:
  Make: Skoda
  Model: Fabia
  Wheels: 4
  Price: $2000
  Seats: 5
    --NO COMMENTS--
3. Car:
  Make: BMW
  Model: 535i
  Wheels: 4
  Price: $7200
  Seats: 5
    --NO COMMENTS--
4. Motorcycle:
  Make: Honda
  Model: Hornet600
  Wheels: 2
  Price: $4150
  Category: Race
    --NO COMMENTS--
5. Car:
  Make: Mercedes
  Model: S500L
  Wheels: 4
  Price: $15000
  Seats: 5
    --NO COMMENTS--
6. Car:
  Make: Opel
  Model: Zafira
  Wheels: 4
  Price: $8000
  Seats: 5
    --NO COMMENTS--
7. Car:
  Make: Opel
  Model: Zafira
  Wheels: 4
  Price: $7450
  Seats: 5
    --NO COMMENTS--
8. Truck:
  Make: Volvo
  Model: FH4
  Wheels: 8
  Price: $11800
  Weight Capacity: 40t
    --NO COMMENTS--
####################
ShowVehicles ivancho
--USER ivancho--
--NO VEHICLES--
####################
Logout
You logged out!
####################
Login gosho 123457
User gosho successfully logged in!
####################
RemoveComment 1 2 pesho
gosho removed comment successfully!
####################
ShowVehicles pesho
--USER pesho--
1. Motorcycle:
  Make: Kawasaki
  Model: Z
  Wheels: 2
  Price: $9999
  Category: Race
    --COMMENTS--
    ----------
    Amazing speed and handling!
      User: pesho
    ----------
    ----------
    I dream of having this one one day.
      User: pesho
    ----------
    --COMMENTS--
2. Car:
  Make: Opel
  Model: Vectra
  Wheels: 4
  Price: $5000
  Seats: 5
    --NO COMMENTS--
3. Car:
  Make: Mazda
  Model: 6
  Wheels: 4
  Price: $10000
  Seats: 5
    --COMMENTS--
    ----------
    What is the mileage on it?
      User: ivancho
    ----------
    ----------
    This one passed my by on the highway today. So pretty!
      User: pesho
    ----------
    --COMMENTS--
4. Motorcycle:
  Make: Suzuki
  Model: V-Strom
  Wheels: 2
  Price: $7500
  Category: CityEnduro
    --NO COMMENTS--
5. Car:
  Make: BMW
  Model: Z3
  Wheels: 4
  Price: $11200
  Seats: 2
    --NO COMMENTS--
####################
Logout
You logged out!
####################
Login pesho 123456
User pesho successfully logged in!
####################
RemoveVehicle 1
pesho removed vehicle successfully!
####################
ShowVehicles pesho
--USER pesho--
1. Car:
  Make: Opel
  Model: Vectra
  Wheels: 4
  Price: $5000
  Seats: 5
    --NO COMMENTS--
2. Car:
  Make: Mazda
  Model: 6
  Wheels: 4
  Price: $10000
  Seats: 5
    --COMMENTS--
    ----------
    What is the mileage on it?
      User: ivancho
    ----------
    ----------
    This one passed my by on the highway today. So pretty!
      User: pesho
    ----------
    --COMMENTS--
3. Motorcycle:
  Make: Suzuki
  Model: V-Strom
  Wheels: 2
  Price: $7500
  Category: CityEnduro
    --NO COMMENTS--
4. Car:
  Make: BMW
  Model: Z3
  Wheels: 4
  Price: $11200
  Seats: 2
    --NO COMMENTS--
####################
```
