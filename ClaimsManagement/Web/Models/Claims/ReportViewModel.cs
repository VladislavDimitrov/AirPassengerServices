using Data.DTOs;
using System;
using System.Collections.Generic;

namespace Web.Models.Claims
{
    public class ReportViewModel
    {
        public ReportViewModel()
        {
        }

        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public string Airline { get; set; }
        public int FlightNumber { get; set; }
        public List<ClaimDto> Claims { get; set; } = new List<ClaimDto>(64);
    }
}