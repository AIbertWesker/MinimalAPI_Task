using MinimalAPI_Task.Converters;
using System.Text.Json.Serialization;

namespace MinimalAPI_Task.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [JsonConverter(typeof(DateOnlyJsonConverter))]
        public DateOnly BirthDate { get; set; }
        public string Adress { get; set; } 
    }
}
