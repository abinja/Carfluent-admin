using GlobalEntityLayer.Models.Admin;
using Microsoft.AspNetCore.Http;
using SQLite;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace GlobalEntityLayer.Models
{
    public class CarDetails
    {
        [Key]
        public int CarID { get; set; } = 0;

        public string Make { get; set; } = string.Empty;


        public string Model { get; set; } = string.Empty;

        public string FuelType { get; set; } = string.Empty;

        public string TransmissionType { get; set; } = string.Empty;


        public int NumberOfSeats { get; set; }

        public string Colour { get; set; } = string.Empty;

       // [Required]
       // [Remote("IsRegNumAvailable", "CarController", ErrorMessage = "This Registration Number already exists!")]
        public string RegistrationNumber { get; set; } = string.Empty;

        public int Price { get; set; }


        public string CarType { get; set; } = string.Empty;
        public string Hub { get; set; } = string.Empty;

        public int Kilometers { get; set; }

        public string Availabilitystatus { get; set; } = string.Empty;  

        public int Status { get; set; }

        public string? CarPhoto { get; set; }
        [NotMapped]
        public IFormFile Photo { get; set; }






        public int CategoryPicklistID { get; set; }
        public CategoryPicklist CategoryPicklist { get; set; }
        



    }
    //public class CarDetailsValidator : AbstractValidator<CarDetails>
    //{
    //    private readonly CarDetailsDBcontext carDetailsDBcontext;
    //    public CarDetailsValidator(CarDetailsDBcontext carDetailsDBcontext)
    //    {
    //        this.carDetailsDBcontext = carDetailsDBcontext;
    //        RuleFor(x => x.RegistrationNumber).Must(BeUniqueRegistrationNumber).WithMessage("Registration Number already exists");
    //    }
    //    private bool BeUniqueRegistrationNumber(string RegistrationNumber)
    //    {
    //        return new Carfluent_Cars.Places.FirstOrDefault(x => x.RegistrationNumber == RegistrationNumber) == null;
    //    }
    //}
    }