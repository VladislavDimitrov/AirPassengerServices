using Data.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface IClaimServices
    {
        Task<ClaimDto> CreateAsync(ClaimDto claimDto);
        Task<ClaimDto> GetAsync(int id);
        Task UpdateAsync(ClaimDto claimDto);
        Task<List<ClaimDto>> Get20LatestClaimsAsync();
        Task<List<ClaimDto>> FilterByMultipleCriteriaAsync(string airline, int flightNumber, DateTime from, DateTime to);
    }
}
