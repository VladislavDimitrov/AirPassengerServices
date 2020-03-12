using AutoMapper;
using Data;
using Data.DTOs;
using Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Service;
using System.IO;
using System.Linq;
using System.Text;
using Web.AutoMapperProfiles;

namespace ClaimsManagementTests.ServiceTests.Claims
{
    [TestClass]
    public class Create_Should
    {
        [TestMethod]
        public void CreateNewInstanceOfClaimDto()
        {
            // Arrange
            var options = TestUtilities.GetOptions(nameof(CreateNewInstanceOfClaimDto));

            // Act, Assert
            using (var assertContext = new ClaimsDbContext(options))
            {
                var myProfile = new ClaimProfile();
                var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
                IMapper mapper = new Mapper(configuration);
                var claimDto = new ClaimDto();
                IFormFile file = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("This is a dummy file")), 0, 0, "Data", "dummy.txt");
                claimDto.BPImage = file;
                var sut = new ClaimServices(assertContext, mapper);
                var testResult = sut.CreateAsync(claimDto).GetAwaiter().GetResult();

                Assert.IsInstanceOfType(testResult, typeof(ClaimDto));
            }
        }

        [TestMethod]
        public void ReturnsAnInstanceWithCorrectId()
        {
            // Arrange
            var options = TestUtilities.GetOptions(nameof(ReturnsAnInstanceWithCorrectId));

            // Act, Assert
            using (var assertContext = new ClaimsDbContext(options))
            {
                var myProfile = new ClaimProfile();
                var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
                IMapper mapper = new Mapper(configuration);
                var claimDto = new ClaimDto();
                IFormFile file = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("This is a dummy file")), 0, 0, "Data", "dummy.txt");
                claimDto.BPImage = file;
                var sut = new ClaimServices(assertContext, mapper);
                var testResult = sut.CreateAsync(claimDto).GetAwaiter().GetResult();

                Assert.IsTrue(assertContext.Claims.Count() == 1 && assertContext.Claims.Any(c => c.Id == testResult.Id));
            }
        }

        [TestMethod]
        public void AddTheClaimToTheDBSetCorrectly()
        {
            // Arrange
            var options = TestUtilities.GetOptions(nameof(AddTheClaimToTheDBSetCorrectly));

            // Act, Assert
            using (var assertContext = new ClaimsDbContext(options))
            {
                var myProfile = new ClaimProfile();
                var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
                IMapper mapper = new Mapper(configuration);
                var claimDto = new ClaimDto();
                IFormFile file = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("This is a dummy file")), 0, 0, "Data", "dummy.txt");
                claimDto.BPImage = file;
                var sut = new ClaimServices(assertContext, mapper);
                var testResult = sut.CreateAsync(claimDto).GetAwaiter().GetResult();

                Assert.IsTrue(assertContext.Claims.Count() == 1 && assertContext.Claims.Any(c => c.Id == testResult.Id));
            }
        }

    }
}
