using Ordo.Core;
using System.Linq;

Console.WriteLine("Ordo - The MODERN Todo Manager");
var manager = new TodoManager();
// Seed with five sample todos
manager.Add("Call Alice", DateTime.Today.AddDays(1));
manager.Add("Finish report", DateTime.Today.AddDays(3));
manager.Add("Pay bills", DateTime.Today.AddDays(7));
manager.Add("Buy groceries", DateTime.Today);
manager.Add("Plan trip", DateTime.Today.AddDays(14));
Console.WriteLine("(seeded 5 sample todos)");

while (true)
{
	Console.WriteLine();
	Console.WriteLine("1) Add Todo");
	Console.WriteLine("2) Remove Todo");
	Console.WriteLine("3) List Todos");
	Console.WriteLine("4) Sort By Name");
	Console.WriteLine("5) Sort By Date");
	Console.WriteLine("0) Exit");
	Console.Write("Select an option: ");
	var choice = Console.ReadLine()?.Trim();
	if (string.IsNullOrEmpty(choice)) continue;

	switch (choice)
	{
		case "1":
			Console.Write("Name: ");
			var name = Console.ReadLine() ?? string.Empty;
			Console.Write("Due date (yyyy-MM-dd or leave blank for today): ");
			var dateStr = Console.ReadLine();
			DateTime due;
			if (string.IsNullOrWhiteSpace(dateStr))
			{
				due = DateTime.Today;
			}
			else if (!DateTime.TryParse(dateStr, out due))
			{
				Console.WriteLine("Invalid date.");
				break;
			}
			var added = manager.Add(name, due);
			Console.WriteLine($"Added: {added.Id} | {added.Name} | {added.DueDate:yyyy-MM-dd}");
			break;

		case "2":
			PrintTodos(manager.List());
			Console.Write("Enter Id (GUID) or index to remove: ");
			var input = Console.ReadLine();
			if (string.IsNullOrWhiteSpace(input)) break;
			if (int.TryParse(input, out var idx))
			{
				var todos = manager.List().ToList();
				if (idx >= 0 && idx < todos.Count)
				{
					var id = todos[idx].Id;
					var removed = manager.Remove(id);
					Console.WriteLine(removed ? "Removed." : "Not found.");
				}
				else Console.WriteLine("Index out of range.");
			}
			else if (Guid.TryParse(input, out var gid))
			{
				var removed = manager.Remove(gid);
				Console.WriteLine(removed ? "Removed." : "Not found.");
			}
			else Console.WriteLine("Invalid input.");
			break;

		case "3":
			PrintTodos(manager.List());
			break;

		case "4":
			PrintTodos(manager.SortByName());
			break;

		case "5":
			PrintTodos(manager.SortByDate());
			break;

		case "0":
			return;

		default:
			Console.WriteLine("Unknown option.");
			break;
	}
}

static void PrintTodos(IEnumerable<Todo> todos)
{
	var list = todos.ToList();
	if (!list.Any())
	{
		Console.WriteLine("(no todos)");
		return;
	}

	for (int i = 0; i < list.Count; i++)
	{
		var t = list[i];
		Console.WriteLine($"{i}: {t.Id} | {t.Name} | {t.DueDate:yyyy-MM-dd}");
	}
}
