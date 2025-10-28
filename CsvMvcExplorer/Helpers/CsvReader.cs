using CsvMvcExplorer.Models;
using System.Globalization;

namespace CsvMvcExplorer.Helpers
{
    public static class CsvReader
    {
        public static List<Person> ReadCsv(string filePath)
        {
            var people = new List<Person>();

            if (!File.Exists(filePath))
            {
                // In a real app, you'd log this or throw
                return people;
            }

            try
            {
                var lines = File.ReadAllLines(filePath);
                if (lines.Length < 2) return people; // Header + data needed

                // Skip header
                foreach (var line in lines.Skip(1))
                {
                    if (string.IsNullOrWhiteSpace(line)) continue;

                    var parts = line.Split(',');
                    if (parts.Length < 3) continue; // Skip bad rows

                    var person = new Person
                    {
                        Name = parts[0].Trim().Trim('"'), // Handle potential quotes
                        Age = int.Parse(parts[1].Trim(), CultureInfo.InvariantCulture),
                        City = parts[2].Trim().Trim('"')
                    };
                    people.Add(person);
                }
            }
            catch (Exception ex)
            {
                // Quick console log for dev—remove later
                Console.WriteLine($"CSV read error: {ex.Message}");
            }

            return people;
        }
    }
}