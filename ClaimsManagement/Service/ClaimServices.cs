using AutoMapper;
using Data;
using Data.DTOs;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Service.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Service
{
    public class ClaimServices : IClaimServices
    {
        private readonly ClaimsDbContext context;
        private readonly IMapper mapper;

        public ClaimServices(ClaimsDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<ClaimDto> CreateAsync(ClaimDto claimDto)
        {
            var claim = mapper.Map<Claim>(claimDto);
            claim.CreatedAt = DateTime.UtcNow;
            using (var ms = new MemoryStream())
            {
                claimDto.BPImage.CopyTo(ms);
                var fileBytes = ms.ToArray();
                claim.BoardingPassImage = fileBytes;
            }
            context.Claims.Add(claim);
            await context.SaveChangesAsync();
            claimDto.Id = claim.Id;

            return claimDto;
        }

        public async Task<ClaimDto> GetAsync(int id)
        {
            var claim = await context.Claims.Include(c => c.User).FirstOrDefaultAsync(c => c.Id == id);
            var claimDto = mapper.Map<ClaimDto>(claim);

            return claimDto;
        }

        public async Task UpdateAsync(ClaimDto claimDto)
        {
            var claim = mapper.Map<Claim>(claimDto);
            if (claimDto.BPImage != null)
            {
                using (var ms = new MemoryStream())
                {
                    claimDto.BPImage.CopyTo(ms);
                    var fileBytes = ms.ToArray();
                    claim.BoardingPassImage = fileBytes;
                }
            }

            context.Claims.Update(claim);
            await context.SaveChangesAsync();
        }

        public async Task<List<ClaimDto>> Get20LatestClaimsAsync()
        {
            var claims = await context.Claims
                .OrderByDescending(c => c.CreatedAt)
                .Take(20).Include(c => c.User)
                .ToListAsync();
            var claimDtos = mapper.Map<List<ClaimDto>>(claims);

            return claimDtos;
        }

        public async Task<List<ClaimDto>> FilterByMultipleCriteriaAsync(string airline, int flightNumber, DateTime from, DateTime to)
        {
            var claims = await context.Claims
                .Include(c => c.User)
                .Where(c => (airline == null || c.Airline.Contains(airline)
                && (flightNumber == default || c.FlightNumber == flightNumber)
                && (from == default || c.CreatedAt >= from)
                && (to == default || c.CreatedAt <= to)))
                .ToListAsync();

            var claimDtos = mapper.Map<List<ClaimDto>>(claims);

            return claimDtos;
        }
    }
}
