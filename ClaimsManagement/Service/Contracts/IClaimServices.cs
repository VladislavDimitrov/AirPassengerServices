using Data.DTOs;
using Data.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface IClaimServices
    {
        Task<ClaimDto> CreateAsync(ClaimDto claimDto);
        Task<ClaimDto> GetAsync(int id);
        Task<ClaimDto> UpdateAsync(ClaimDto claimDto);
        Task<List<ClaimDto>> GetClaimsByUserAsync(User user);
        Task<List<ClaimDto>> Get20LatestClaimsAsync();
        Task<List<ClaimDto>> FilterByMultipleCriteriaAsync(string airline, int flightNumber, DateTime from, DateTime to);
    }
}
