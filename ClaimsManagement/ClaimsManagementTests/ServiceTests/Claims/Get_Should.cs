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
    public class Get_Should
    {
        [TestMethod]
        public void ReturnCorrectClaimInstanceByID()
        {
            // Arrange
            var options = TestUtilities.GetOptions(nameof(ReturnCorrectClaimInstanceByID));

            // Act, Assert
            using (var assertContext = new ClaimsDbContext(options))
            {
                var myProfile = new ClaimProfile();
                var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
                IMapper mapper = new Mapper(configuration);
                IFormFile file = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("This is a dummy file")), 0, 0, "Data", "dummy.txt");
                var claimDto = new ClaimDto();
                claimDto.BPImage = file;
                var sut = new ClaimServices(assertContext, mapper);
                sut.CreateAsync(claimDto).GetAwaiter().GetResult();
                var testResult = sut.GetAsync(1).GetAwaiter().GetResult();

                Assert.IsTrue(assertContext.Claims.Count() == 1 && testResult.Id == 1);
            }
        }
    }
}
