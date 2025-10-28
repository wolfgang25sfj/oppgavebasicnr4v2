Quick Intro
This is my project for the "C# Basic Oppgave 4: MVC og LINQ utforsking" assignment. It's a basic ASP.NET Core web app that reads a CSV file full of fake people data (like names, ages, and cities), turns each row into a C# object, and lets you run some LINQ queries on it right in the browser. Think tables, filters, sorts—nothing crazy, but it shows off the MVC pattern and how LINQ makes querying easy.
I used .NET 9.0 because it's the latest on my machine, but it should work fine on 8 too. The CSV is super simple—just drop it in wwwroot/data/ and go.
How I Brainstormed This
Okay, so I started by rereading the assignment: Read CSV, map to objects, do LINQ stuff like Select and Where. I wanted something I could actually see working, so I picked a "people" dataset—easy to relate to, like filtering folks from Oslo. (I thought about using movie data, but that'd need more columns and complicate parsing.)
First sketch:

Use MVC 'cause the assignment mentions it—Model for Person class, Controller to load/run queries, Views for showing tables.
For CSV: File.ReadAllLines to grab lines, skip the header, then Split(',') to break into parts. Map to Person with try-catch for bad lines (like if age isn't a number).
LINQ: Had to have Select (pull one thing, like cities) and Where (filter by city). Then I added extras like OrderBy for sorting ages and GroupBy for averages per city—felt like good practice.
Modular bits: Made a CsvReader helper so the controller isn't messy. Loaded data once in the constructor.

Challenges: Paths to the CSV kept breaking—turns out it's relative to wwwroot. Also, Select with anonymous types didn't play nice in views, so I made a quick CityAverage class. Tested with a small CSV, added duplicates to check Distinct works. Took me a couple hours, mostly debugging views.
I kept it vanilla—no extra packages—to stick to basics.
Setup Steps

Open Visual Studio → Create a new project → Search "ASP.NET Core Web App (Model-View-Controller)" → Name it "CsvMvcExplorer" → Pick .NET 9.0 (or 8) → Create.
Copy my files over: Replace Program.cs, add Models/Person.cs and ErrorViewModel.cs, Helpers/CsvReader.cs, update HomeController.cs, and paste the .cshtml views into Views/Home and Views/Shared.
Make a data folder in wwwroot and add people.csv there.
Build it: Hit Ctrl+Shift+B (or right-click project → Build). Fix any red squiggles (like missing usings).

Running It

In VS: Hit F5 (starts with debugger) or Ctrl+F5 (no debug).
Browser pops up at something like https://localhost:7xxx/Home/Index. (Accept the HTTPS warning if it asks.)
Or from command line (in project folder): dotnet run → Check console for the port → Open that URL.
Play around: Click buttons to see LINQ in action. If no data shows, check the CSV path.

To stop: Ctrl+C in console or stop in VS.
How to Use

Home: Full list in a table.
Buttons:

Filter by Oslo: Uses Where—shows only Oslo peeps. originaly started with generic names and generic cities but replaced with people close to me
Sort by Age: OrderBy—youngest or oldest first.
City Averages: GroupBy + Average—avg age per city.
Unique Cities: Select + Distinct—list of cities without repeats.


Mess with URLs too, like /Home/FilterByCity

How It Hits the Requirements

Select(): In SelectCities—grabs just City from every Person.
Where(): FilterByCity—my custom filter for city names.
Modular: CsvReader for file stuff, Person model for rows, Controller runs queries.
IO/Parsing: File.ReadAllLines + Split to build objects.
Extra LINQ: OrderBy, GroupBy, Distinct—chained 'em for fun.