using AutoMapper;
using Data;
using Data.DTOs;
using Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Service;
using System.IO;
using System.Linq;
using System.Text;
using Web.AutoMapperProfiles;

namespace ClaimsManagementTests.ServiceTests.Claims
{
    [TestClass]
    public class GetClaimsByUser_Should
    {
        [TestMethod]
        public void ReturnCollectionOfClaimsSubmittedByTheSpecifiedUser()
        {
            // Arrange
            var options = TestUtilities.GetOptions(nameof(ReturnCollectionOfClaimsSubmittedByTheSpecifiedUser));

            // Act, Assert
            using (var assertContext = new ClaimsDbContext(options))
            {
                var myProfile = new ClaimProfile();
                var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
                IMapper mapper = new Mapper(configuration);
                IFormFile file = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("This is a dummy file")), 0, 0, "Data", "dummy.txt");
                var user = new User
                {
                    Id = "1"

                };
                var claimDto = new ClaimDto();
                claimDto.BPImage = file;
                claimDto.User = user;
                var claimDto2 = new ClaimDto();
                claimDto2.BPImage = file;
                claimDto2.User = user;
                var claimDto3 = new ClaimDto();
                claimDto3.BPImage = file;
                var sut = new ClaimServices(assertContext, mapper);
                sut.CreateAsync(claimDto).GetAwaiter().GetResult();
                sut.CreateAsync(claimDto2).GetAwaiter().GetResult();
                sut.CreateAsync(claimDto3).GetAwaiter().GetResult();
                var testResult = sut.GetClaimsByUserAsync(user).GetAwaiter().GetResult();

                Assert.IsTrue(testResult.Count() == 2 && testResult[0].User == user && testResult[1].User == user);
            }
        }
    }
}
