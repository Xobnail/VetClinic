namespace VetClinic.Models
{
    public class Vaccination
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateTime { get; set; }
        public int AnimalId { get; set; }
        public Animal Animal { get; set; }
        public string Description { get; set; }
    }
}
