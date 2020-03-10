using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities
{
    public class Claim
    {
        public int Id { get; set; }
        [MinLength(3), MaxLength(30)]
        public string Title { get; set; }
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [Range(100, 9999)]
        public int FlightNumber { get; set; }
        [MinLength(3), MaxLength(30)]
        public string Airline { get; set; }
        public string Category { get; set; }
        [MinLength(3), MaxLength(3)]
        public string DepartureAirport { get; set; }
        [MinLength(3), MaxLength(3)]
        public string ArrivalAirport { get; set; }
        [DataType(DataType.Date)]
        public DateTime FlightDate { get; set; }
        public byte[] BoardingPassImage { get; set; }
        public string CountryCode { get; set; }
        public string PhoneNumber { get; set; }
        [MinLength(20), MaxLength(500)]
        public string Description { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
