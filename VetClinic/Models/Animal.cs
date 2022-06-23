namespace VetClinic.Models
{
    public class Animal
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NickName { get; set; }
        public DateTime EnterDate { get; set; }
        public string DiseaseDescription { get; set; }
        public int OwnerId { get; set; }
        public Owner Owner { get; set; }
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }
        public ICollection<Vaccination> Vaccinations { get; set; }
    }
}
