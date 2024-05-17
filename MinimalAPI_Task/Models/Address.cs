namespace MinimalAPI_Task.Models
{
    public class Address
    {
        public string City { get; set; }
        public string? Street { get; set; }
        public int BuildingNumber { get; set; }
        public int? ApartmentNumber { get; set; }
        public string ZipCode { get; set; }
    }
}
