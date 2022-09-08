using System;
using System.Collections.Generic;

class Program
{
	static void Main()
	{
		Dictionary<int, string> numbers = new Dictionary<int, string>();
		numbers.Add(1, "One");
		numbers.Add(2, "Two");
		numbers.Add(3, "Three");

		//The following throws an exception: An item with the same key has already been added. Key: 3
		// numbers.Add(3, "Three"); 

		foreach (KeyValuePair<int, string> kvp in numbers)
		{
			Console.WriteLine($"Number: {kvp.Key}, Name: {kvp.Value}");
		}

		// Collection initializer syntax for creating and populating a dictionary
		var cities = new Dictionary<string, List<string>>()
		{
			{ "UK", new List<string>() { "London", "Manchester", "Birmingham" } },
			{ "USA", new List<string>() { "Chicago", "New York", "Washington" } },
			{ "Bulgaria", new List<string>() { "Sofia", "Plovdiv", "Varna", "Burgas" } }
		};

		foreach (var kvp in cities)
		{
			Console.WriteLine($"Country: {kvp.Key}, Cities: {string.Join(", ", kvp.Value)}");
		}

		List<string> ukCities = cities["UK"];
		Console.WriteLine(string.Join(",", ukCities));             // Prints the value for key: UK
		Console.WriteLine(string.Join(",", cities["USA"]));        // Prints the value for key: USA
		Console.WriteLine(string.Join(",", cities["Bulgaria"]));   // Prints the value for key: Bulgaria
		//Console.WriteLine(string.Join(",", cities["France"]));   // Run-time exception: KeyNotFoundException: The given key 'France' was not present in the dictionary.

		// Always check if a key exists!
		if (cities.ContainsKey("France"))
		{
			Console.WriteLine(string.Join(",", cities["France"]));
		}

		// Collection containing only the keys
		foreach (string key in cities.Keys)
		{
			Console.WriteLine($"Key: {key}");
		}

		// Collection containing only the values
		foreach (List<string> value in cities.Values)
		{
			Console.WriteLine($"Value: {string.Join(", ", value)}");
		}
	}
}
