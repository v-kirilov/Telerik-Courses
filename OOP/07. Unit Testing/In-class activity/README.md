<img src="https://webassets.telerikacademy.com/images/default-source/logos/telerik-academy.svg)" alt="logo" width="300px" style="margin-top: 20px;"/>

# BoardR - Task Organizing System

_Part 6_

### 1. Description

**BoardR** is a task-management system which will evolve in the next several weeks. During the course of the project, we will follow the best practices of `Object-Oriented Programming` and `Design`.

### 2. Goals

Our application seems to be working now and has all the functionalities we need. Your task now is write unit tests so we can be sure the current implementations work as intended and will continue to do so even if we add additional features.
You need to follow the **3A pattern\***, **write Isolated tests** and follow the **naming conventions** we've covered.

### 3. Create a Test Project

#### Description

Before writing any tests we need to create a test project. You can create one by right clicking on the Solution 'Boarder' row in the Solution Explorer, then Add > New Project which should open the Add a new project window. Now either search for MSTest or find it manually in the list. You need the **MSTest Test Project (.NET Core) with C#**. Either double click it or click on next, then you'll have to name your new project - a good name would be **Boarder.Tests**. Click on create and there should be a second project marked with the test icon in your Solution Explorer now.

### 4. Unit test the BoardItem constructor

#### Description

Let's start with our models. There are a lot of methods and properties inside the `BoardItem` class which we should test. However, the class is abstract and we can't actually make a new instance inside our tests to test. How can we test it then?

What we can do however is test the `Issue` and `Task` classes as they are derived classes of BoardItem. Let's start with the `Task` class.

Create the needed folder and files where your tests will be. Then lets proceed to testing the constructor.

We need to test whether or not the constructor of `Task` assigns the correct values passed to it. Those are the title, the assignee, the dueDate and the status(which should always starts as Todo). Sticking to the 3A pattern our test should look like this:

```cs
        [TestMethod]
        public void AssignCorrectValues()
        {
            //Arrange
            var title = "This is a test title";
            var assignee = "TestUser";
            var dueDate = Convert.ToDateTime("01-01-2030");

            //Act
            var sut = new Task(title, assignee, dueDate);

            //Assert
            Assert.AreEqual(title, sut.Title);
        }
```

> **Note** You can leave comments for the **3A pattern** to guide you through the structure of each test. Think of what we need to **Arrange** prior to performing the **Act** and what exactly we'll have to **Assert** at the end. In this case the **Arrange** part is creating the correct type which we will then pass to the `GetLogger` method. The invocation of the method itself is the **Act**. What we will **Assert** for is whether or not the returned object is the correct type. Let's put this all into code.

You can go ahead now and write a few more, for each param set by the constructor.

Once we're done with this let's think about how we can check the constructor actually called the `AddEventLog()`. The easiest way to find out is to create a new instance of a task followed by calling its `ViewInfo()` method. We then have to check if the Created Task substring is returned. Sounds easy? There is one issue with all this. By doing so we will rely on the fact `ViewInfo()` itself is implemented correctly and working properly. Why would you consider this to be a problem?

### 5. Unit test the Task Assignee property and EnsureValidAssignee() method

#### Description

`EnsureValidAssignee()` throws 2 different exceptions based on a validation of a string. We should test this. But how when the method itself is private(thus not being able to call it from the test file)? What we can do is test it via the property itself, as this is where it's used. You can think of private methods as an add-on to the public method(in this case property). We can't test it directly but can via the provided public interface.

Let's write assignee tests for the following:

- Assignee property, via `EnsureValidAssignee` throws `ArgumentException` when null is passed as a value.
  > **Hint** You can either Assert against an exception on a given action and assign the Assert to a variable - this will hold the exception itself and you can assert towards its message as well.

```cs
    var exception = Assert.ThrowsException<ArgumentException>(() => task.Assignee = null);
```

> or you can add an attribute above the method itself stating what type of exception is expected as well as specify the message it should carry. If you chose this way you don't have to write Assert in the end of the test.

```cs
    [ExpectedException(typeof(ArgumentException), "Please provide a non-null or empty value")]
```

- Assignee property, via `EnsureValidAssignee` throws `ArgumentException` when value's length is less than expected.
- Assignee property, via `EnsureValidAssignee` throws `ArgumentException` when value's length is more than expected.

### 6. Unit test the AdvanceStatus() method inside the Issue class.

#### Description

`AdvanceStatus()` does what it says. However, we need to make sure it works exactly as we would like - if the current Status is open calling AdvanceStatus should set it to Verified. If it is already Verified it should not change it any further. What we need to do is to create our class(system under test) and call the AdvanceStatus method. Once we've done this we need to assert the result is what we would expect it to be. Sounds easy, right?

The above tests should all look very similar and should not take much time once you get the hang of it. Practice is key.

### 7. Measure your code coverage

Install the `Fine Code Coverage` extension from the Visual Studio Market Place and measure your code coverage. What part of the code needs more tests? You can continue writing unit tests in order to achieve higher code coverage (aim for 90%).
