using AutoMapper;
using Data;
using Data.DTOs;
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
    public class Get20LatestClaims_Should
    {
        [TestMethod]
        public void ReturnCollectionOfUpTo20NewestClaims()
        {
            // Arrange
            var options = TestUtilities.GetOptions(nameof(ReturnCollectionOfUpTo20NewestClaims));

            // Act, Assert
            using (var assertContext = new ClaimsDbContext(options))
            {
                var myProfile = new ClaimProfile();
                var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
                IMapper mapper = new Mapper(configuration);
                IFormFile file = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("This is a dummy file")), 0, 0, "Data", "dummy.txt");
                var claimDto = new ClaimDto();
                claimDto.BPImage = file;
                var claimDto2 = new ClaimDto();
                claimDto2.BPImage = file;
                var sut = new ClaimServices(assertContext, mapper);
                sut.CreateAsync(claimDto).GetAwaiter().GetResult();
                sut.CreateAsync(claimDto2).GetAwaiter().GetResult();
                var testResult = sut.Get20LatestClaimsAsync().GetAwaiter().GetResult();

                Assert.IsTrue(testResult.Count() == 2 && testResult[0].CreatedAt > testResult[1].CreatedAt);
            }
        }
    }
}
