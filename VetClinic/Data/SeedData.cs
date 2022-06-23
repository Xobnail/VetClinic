using Microsoft.EntityFrameworkCore;
using VetClinic.Models;

namespace VetClinic.Data
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new VetClinicContext(serviceProvider.GetRequiredService<DbContextOptions<VetClinicContext>>()))
            {
                if (context.Animals.Any())
                {
                    return;
                }

                Doctor doctor = new Doctor
                {
                    Name = "Nick Peterson",
                    Position = "Marine veterinarian",
                    PhoneNumber = "+71234567890",
                    Email = "NickSon@gmail.com"
                };

                Owner owner = new Owner
                {
                    Name = "Jim Carry",
                    PhoneNumber = "+75675677890",
                    Email = "JimCarry@gmail.com"
                };

                Animal animal = new Animal
                {
                    Name = "Turtle",
                    NickName = "Joe",
                    EnterDate = DateTime.UtcNow,
                    Owner = owner,
                    Doctor = doctor,
                    DiseaseDescription = "Salmonella infection"
                };

                Service service = new Service
                {
                    Name = "Vaccination",
                    Price = 100,
                    Duration = "1 hour",
                    Description = "We offer all types of vaccinations"
                };

                Vaccination vaccination = new Vaccination
                {
                    Name = "Adenoviruses",
                    DateTime = DateTime.UtcNow,
                    Animal = animal,
                    Description = "Contains live adenovirus Type 4 and Type 7"
                };
                
                context.Animals.Add(animal);                
                context.Doctors.Add(doctor);
                context.Owners.Add(owner);
                context.Services.Add(service);
                context.Vaccinations.Add(vaccination);
                
                context.SaveChanges();
            }
        }
    }
}
