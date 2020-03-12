using Data.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace Data.DTOs
{
    public class ClaimDto
    {
        public int Id { get; set; }

        [MinLength(3), MaxLength(30, ErrorMessage = "The title must contain between 3 and 30 charecters.")]
        [Required(ErrorMessage ="The Title field is required.")]
        public string Title { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Range(100, 9999, ErrorMessage = "The value is invalid.")]
        [Required(ErrorMessage = "The Flight Number field is required.")]
        public int FlightNumber { get; set; }

        [MinLength(3), MaxLength(30, ErrorMessage = "The airline name must contain between 3 and 30 charecters.")]
        [Required(ErrorMessage = "The Airline field is required.")]
        public string Airline { get; set; }

        [Required] 
        public string Category { get; set; }

        [MinLength(3), MaxLength(3, ErrorMessage = "Please enter the three digit abbreviation of the departure airport shown on your boarding pass.")]
        [Required(ErrorMessage = "The Departure Airport field is required.")]
        public string DepartureAirport { get; set; }

        [MinLength(3), MaxLength(3, ErrorMessage = "Please enter the three digit abbreviation of the arrival airport shown on your boarding pass.")]
        [Required(ErrorMessage = "The Arrival Airport field is required.")]
        public string ArrivalAirport { get; set; }

        [DataType(DataType.Date, ErrorMessage = "Please enter a valid date.")]
        [Required(ErrorMessage = "The Flight Date field is required.")]
        public DateTime FlightDate { get; set; }

        [Required(ErrorMessage = "The Boarding Pass Image field is required.")]
        public IFormFile BPImage { get; set; }

        public byte[] BoardingPassImage { get; set; }

        [MinLength(2), MaxLength(2, ErrorMessage = "Please enter the two-letter region code of your Country.")]
        [Required(ErrorMessage = "The Country Code field is required.")]
        public string CountryCode { get; set; }

        [Required(ErrorMessage = "The Phone Number field is required.")]
        public string PhoneNumber { get; set; }

        [ MinLength(20), MaxLength(500)]
        [Required(ErrorMessage = "The Description field is required.")]
        public string Description { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
