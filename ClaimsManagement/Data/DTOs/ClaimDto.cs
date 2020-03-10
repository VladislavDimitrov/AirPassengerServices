using Data.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace Data.DTOs
{
    public class ClaimDto
    {
        public int Id { get; set; }

        [Required, MinLength(3), MaxLength(30, ErrorMessage = "The title must contain between 3 and 30 charecters.")]
        public string Title { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required, Range(100, 9999)]
        public int FlightNumber { get; set; }

        [Required, MinLength(3), MaxLength(30, ErrorMessage = "The airline name must contain between 3 and 30 charecters.")]
        public string Airline { get; set; }

        [Required] 
        public string Category { get; set; }

        [Required, MinLength(3), MaxLength(3, ErrorMessage = "Please enter the three digit abbreviation of the departure airport shown on your boarding pass.")]
        public string DepartureAirport { get; set; }

        [Required, MinLength(3), MaxLength(3, ErrorMessage = "Please enter the three digit abbreviation of the arrival airport shown on your boarding pass.")]
        public string ArrivalAirport { get; set; }

        [Required, DataType(DataType.Date)]
        public DateTime FlightDate { get; set; }

        [Required]
        public IFormFile BPImage { get; set; }

        public byte[] BoardingPassImage { get; set; }

        [Required, MinLength(2), MaxLength(2, ErrorMessage = "Please enter the two-letter region code of your Country.")]
        public string CountryCode { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required, MinLength(20), MaxLength(500)]
        public string Description { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
