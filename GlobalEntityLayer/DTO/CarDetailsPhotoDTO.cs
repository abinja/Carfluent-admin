using GlobalEntityLayer.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalEntityLayer.DTO
{
    public class CarDetailsPhotoDTO
    {
        [Key]
        public string Make { get; set; } = string.Empty;

        public string Model { get; set; } = string.Empty;

        public string FuelType { get; set; } = string.Empty;

        public string TransmissionType { get; set; } = string.Empty;

        public int NumberOfSeats { get; set; }

        public string Colour { get; set; } = string.Empty;

        public string RegistrationNumber { get; set; } = string.Empty;

        public int Price { get; set; }
        public string CarType { get; set; } = string.Empty;
        public string Hub { get; set; } = string.Empty;

        public int Kilometers { get; set; }

        public string Availabilitystatus { get; set; } = string.Empty;

        public int Status { get; set; }

        public int CategoryPicklistID { get; set; }
    }
}
