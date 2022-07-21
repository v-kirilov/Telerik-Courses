<img src="https://webassets.telerikacademy.com/images/default-source/logos/telerik-academy.svg" alt="logo" width="300px" style="margin-top: 20px;"/>

# OOP Workshop - Travel Agency

## Preface

Before you start coding, read this document from top to bottom. It has some valuable information that will make your work way easier.

## Description

Implement a journey and ticket tracking system for a famous travel agency called `Sunny Travel`. The application already accepts commands and outputs text for each submitted command, what you need to do is build the OO classes hierarchy. The user can create different models (`Bus, Airplane, Train, Ticket, Journey`), as well as list them. Make sure to follow the Object Orientated Programming best practices and conventions that we have talked about during the lectures.

## Architecture

By now you should be acquainted with how the Engine works. It's the same as all previous workshops. The commands use the `Repository` class to create and store all the application information. After the command executes, it returns a result message to the `Engine` that prints it to the console.

Your focus should be on the `Models` and `Commands` namespaces, where you need to place the classes you create, using the provided interfaces in the `Contracts` namespace or implement the commands which are not ready yet.

## Part 1 - Models

> Note: All validation intervals are inclusive (closed).

### Fields and behavior

#### **Train**

- `Passenger Capacity` - a number between `30` and `150`.
  - Exception message: `A train cannot have less than 30 passengers or more than 150 passengers.`
- `Price Per Kilometer` - a number between `0.1` and `2.5` representing currency.
- `Carts` - a number between `1` and `15`.
  - Exception message: `A train cannot have less than 1 cart or more than 15 carts.`
- Should be convertible to **String** in the format:

```none
Train ----
Passenger capacity: VALUE
Price per kilometer: VALUE
Carts amount: VALUE
```

#### **Airplane**

- `Passenger Capacity` - a number.
- `Price Per Kilometer` - a number.
- `Is low-cost` - boolean.
- Should be convertible to **String** in the format:

```none
Airplane ----
Passenger capacity: VALUE
Price per kilometer: VALUE
Is low-cost: VALUE
```

#### **Bus**

- `Passenger Capacity` - a number between `10` and `50`.
  - Exception message: `A bus cannot have less than 10 passengers or more than 50 passengers.`
- `Price Per Kilometer` - a number.
- `Has Free TV` - boolean.
- Should be convertible to **String** in the format:

```none
Bus ----
Passenger capacity: VALUE
Price per kilometer: VALUE
Has free TV: VALUE
```

#### **Journey**

- `Start Location` - a string with length between `5` and `25`.
  - Exception message: `The StartingLocation's length cannot be less than 5 or more than 25 symbols long.`
- `Destination` - a string with length between `5` and `25`.
  - Exception message: `The Destination's length cannot be less than 5 or more than 25 symbols long.`
- `Distance` - a number between `5` and `5000`.
  - Exception message: `The Distance cannot be less than 5 or more than 5000 kilometers.`
- `Vehicle` - the vehicle that will be used in the journey.
- `CalculateTravelCosts()` - calculated by multiplying the `Distance` by the `Vehicle`'s `price per kilometer`.
- Should be convertible to **String** in the format:

```none
Journey ----
Start location: VALUE
Destination: VALUE
Distance: VALUE
Travel costs: VALUE
```

#### **Ticket**

- `Journey` - the journey the ticket is sold for.
- `Administrative Costs` - a number.
- `CalculatePrice()` - calculated by multiplying the `Administrative Costs` by the `Journey`'s `travel costs`.
- Should be convertible to **String** in the format:

```none
Ticket ----
Destination: VALUE
Price: VALUE
```

#### **Additional validations**

The laws of physics and finances dictate that:

- A vehicle with **less than 1** or **more than 800 passengers** cannot exist!
  - Exception message: `A vehicle with less than 1 passengers or more than 800 passengers cannot exist!`
- A vehicle with a price per kilometer **lower than $0.10** or **higher than $2.50** cannot exist!
  - Exception message: `A vehicle with a price per kilometer lower than $0.10 or higher than $2.50 cannot exist!`

In your case, there is no such vehicle, but think about these rules more generally. This system could be extended in the future to accommodate more vehicles.

### Repository

The `Repository` class is used to store and retrieve all information that the application needs. It is also in charge of creating the objects.

The class has a private `nextId` field that is used to store the next `ID` to be assigned. Whenever something with an ID is created, the `nextId` field should be incremented.

Your task is to implement all methods that throw `NotImplementedException`.

### Commands

**All commands are case insensitive, except their parameters!** Each command is represented in the code base as a separate class, that is invoked by the `CommandFactory` class.

You are given a set of commands. The following are already implemented:

- **CreateBus** `[passengerCapacity] [pricePerKilometer]` - Creates a new `Bus`.
- **CreateTrain** `[passengerCapacity] [pricePerKilometer] [carts]` - Creates a new `Train`.
- **CreateJourney** `[startLocation] [destination] [distance] [vehicleID]` - Creates a new `Journey`.
- **ListJourneys** - Lists all stored journeys.
- **ListTickets** - Lists all stored tickets.

And these are the commands you need to implement yourself:

- **CreateAirplane** `[passengerCapacity] [pricePerKilometer] [isLowCost]` - Creates a new `Airplane`.
- **CreateTicket** `[journeyID] [administrativeCosts]` - Creates a new `Ticket`.
- **ListVehicles** - Lists all stored vehicles.

### Constraints

- You are allowed to create new and modify existing **classes, interfaces, enumerations and namespaces** in the `Models` namespace.
- You are allowed to modify the **Repository**.
- You are allowed to create and modify existing classes in the `Commands` namespace.
- ***You are NOT allowed to modify any other existing interfaces!***
- ***You are NOT allowed to modify any other existing classes, enumerations and namespaces!***

### Unit Tests

- You have been given unit tests to keep track off your progress. 
- Should you get stuck, check out the tests' names to guide you what you should do.
  
> Note: Be careful not to change anything in the `Tests` project.

### Step by step guide

1. If you try to build the Template it won't compile. Do not worry.
2. Navigate to the `Models` namespace and think about what classes would we need, what interfaces do they need to implement and is there an opportunity to introduce an abstract class? Take a look at the existing interfaces and consider if they can be connected in a hierachy.
3. After implementing and encapsulating the needed models head over to the `Repository` class. All methods that throw `NotImplementedException` need to be implemented.
4. It is now time to implement all commands that are not implemented.

### Sample Input

```
createbus 10 0.7 False
createairplane 230 1 True
createtrain 80 0.44 3
listvehicles
createjourney Sofia Turnovo 300 1
createjourney Sofia Turnovo 33 2
listjourneys
createticket 4 30.2
createticket 4 -1.5
createticket 5 100
listtickets
createticket pesho 100
createairplane 250 1 True
createticket 2 pipi
createairplane alabala 23 16
createjourney Sofia Turnovo 3000 1
createjourney SsdddddddddsdsssssssssSsdddddddddsdsssssssss Turnovo 3000 1
createjourney Sofia SsdddddddddsdsssssssssSsdddddddddsdsssssssss 3000 1
createtrain 80 0.08 3
createtrain 80 2.7 3
listtickets
createtrain 28 0.4 3
createtrain 152 0.4 3
listvehicles
exit

```

### Expected Output

```
createbus 10 0.7 False
Vehicle with ID 1 was created.
createairplane 230 1 True
Vehicle with ID 2 was created.
createtrain 80 0.44 3
Vehicle with ID 3 was created.
listvehicles
Bus ----
Passenger capacity: 10
Price per kilometer: 0.70
Has free TV: False
####################
Airplane ----
Passenger capacity: 230
Price per kilometer: 1.00
Is low-cost: True
####################
Train ----
Passenger capacity: 80
Price per kilometer: 0.44
Carts amount: 3
####################
createjourney Sofia Turnovo 300 1
Journey with ID 4 was created.
createjourney Sofia Turnovo 33 2
Journey with ID 5 was created.
listjourneys
Journey ----
Start location: Sofia
Destination: Turnovo
Distance: 300
Travel costs: 210.00
####################
Journey ----
Start location: Sofia
Destination: Turnovo
Distance: 33
Travel costs: 33.00
####################
createticket 4 30.2
Ticket with ID 6 was created.
createticket 4 -1.5
Value of 'costs' must be a positive number. Actual value: -1.50.
createticket 5 100
Ticket with ID 8 was created.
listtickets
Ticket ----
Destination: Turnovo
Price: 6342.00
####################
Ticket ----
Destination: Turnovo
Price: 3300.00
####################
createticket pesho 100
Invalid value for journeyId. Should be an integer number.
createairplane 250 1 True
Vehicle with ID 9 was created.
createticket 2 pipi
Invalid value for administrativeCosts. Should be a real number.
createairplane alabala 23 16
Invalid value for passengerCapacity. Should be an integer number.
createjourney Sofia Turnovo 3000 1
Journey with ID 10 was created.
createjourney SsdddddddddsdsssssssssSsdddddddddsdsssssssss Turnovo 3000 1
The length of StartLocation must be between 5 and 25 symbols.
createjourney Sofia SsdddddddddsdsssssssssSsdddddddddsdsssssssss 3000 1
The length of Destination must be between 5 and 25 symbols.
createtrain 80 0.08 3
A vehicle with a price per kilometer lower than $0.10 or higher than $2.50 cannot exist!
createtrain 80 2.7 3
A vehicle with a price per kilometer lower than $0.10 or higher than $2.50 cannot exist!
listtickets
Ticket ----
Destination: Turnovo
Price: 6342.00
####################
Ticket ----
Destination: Turnovo
Price: 3300.00
####################
createtrain 28 0.4 3
A train cannot have less than 30 passengers or more than 150 passengers.
createtrain 152 0.4 3
A train cannot have less than 30 passengers or more than 150 passengers.
listvehicles
Bus ----
Passenger capacity: 10
Price per kilometer: 0.70
Has free TV: False
####################
Airplane ----
Passenger capacity: 230
Price per kilometer: 1.00
Is low-cost: True
####################
Train ----
Passenger capacity: 80
Price per kilometer: 0.44
Carts amount: 3
####################
Airplane ----
Passenger capacity: 250
Price per kilometer: 1.00
Is low-cost: True
####################
exit

```
