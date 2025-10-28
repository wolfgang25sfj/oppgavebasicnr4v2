using System.ComponentModel.DataAnnotations;

namespace CsvMvcExplorer.Models
{
    public class Person
    {
        [Display(Name = "Full Name")]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Age")]
        public int Age { get; set; }

        [Display(Name = "City")]
        public string City { get; set; } = string.Empty;
    }
}