using AutoMapper;
using Data;
using Data.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Service;
using System;
using System.IO;
using System.Linq;
using System.Text;
using Web.AutoMapperProfiles;

namespace ClaimsManagementTests.ServiceTests.Claims
{
    [TestClass]
    public class FilterByMultipleCriteria_Should
    {
        [TestMethod]
        public void ReturnCollectionOfClaimsFilteredByAirline()
        {
            // Arrange
            var options = TestUtilities.GetOptions(nameof(ReturnCollectionOfClaimsFilteredByAirline));

            // Act, Assert
            using (var assertContext = new ClaimsDbContext(options))
            {
                var myProfile = new ClaimProfile();
                var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
                IMapper mapper = new Mapper(configuration);
                var claimDto = new ClaimDto();
                var claimDto2 = new ClaimDto();
                var claimDto3 = new ClaimDto();
                IFormFile file = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("This is a dummy file")), 0, 0, "Data", "dummy.txt");
                claimDto.BPImage = file;
                claimDto2.BPImage = file;
                claimDto3.BPImage = file;
                claimDto.Airline = "TestAir";
                claimDto2.Airline = "TestAir";
                claimDto3.Airline = "AirTest";
                var sut = new ClaimServices(assertContext, mapper);
                sut.CreateAsync(claimDto).GetAwaiter().GetResult();
                sut.CreateAsync(claimDto2).GetAwaiter().GetResult();
                sut.CreateAsync(claimDto3).GetAwaiter().GetResult();
                var results = sut.FilterByMultipleCriteriaAsync("TestAir", default, default, default).GetAwaiter().GetResult();

                Assert.IsTrue(results.Count() == 2 && results[0].Airline == "TestAir" && results[1].Airline == "TestAir");
            }
        }

        [TestMethod]
        public void ReturnCollectionOfClaimsFilteredByFlightNumber()
        {
            // Arrange
            var options = TestUtilities.GetOptions(nameof(ReturnCollectionOfClaimsFilteredByFlightNumber));

            // Act, Assert
            using (var assertContext = new ClaimsDbContext(options))
            {
                var myProfile = new ClaimProfile();
                var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
                IMapper mapper = new Mapper(configuration);
                var claimDto = new ClaimDto();
                var claimDto2 = new ClaimDto();
                var claimDto3 = new ClaimDto();
                IFormFile file = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("This is a dummy file")), 0, 0, "Data", "dummy.txt");
                claimDto.BPImage = file;
                claimDto2.BPImage = file;
                claimDto3.BPImage = file;
                claimDto.FlightNumber = 1111;
                claimDto2.FlightNumber = 1111;
                claimDto3.FlightNumber = 123;
                var sut = new ClaimServices(assertContext, mapper);
                sut.CreateAsync(claimDto).GetAwaiter().GetResult();
                sut.CreateAsync(claimDto2).GetAwaiter().GetResult();
                sut.CreateAsync(claimDto3).GetAwaiter().GetResult();
                var results = sut.FilterByMultipleCriteriaAsync(null, 1111, default, default).GetAwaiter().GetResult();

                Assert.IsTrue(results.Count() == 2 && results[0].FlightNumber == 1111 && results[1].FlightNumber == 1111);
            }
        }

        [TestMethod]
        public void ReturnCollectionOfClaimsFilteredByCreationStartDate()
        {
            // Arrange
            var options = TestUtilities.GetOptions(nameof(ReturnCollectionOfClaimsFilteredByCreationStartDate));

            // Act, Assert
            using (var assertContext = new ClaimsDbContext(options))
            {
                var myProfile = new ClaimProfile();
                var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
                IMapper mapper = new Mapper(configuration);
                var claimDto = new ClaimDto();
                var claimDto2 = new ClaimDto();
                IFormFile file = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("This is a dummy file")), 0, 0, "Data", "dummy.txt");
                claimDto.BPImage = file;
                claimDto2.BPImage = file;
                var sut = new ClaimServices(assertContext, mapper);
                sut.CreateAsync(claimDto).GetAwaiter().GetResult();
                sut.CreateAsync(claimDto2).GetAwaiter().GetResult();
                DateTime from = default;
                from = from.AddYears(2019);
                var results = sut.FilterByMultipleCriteriaAsync(null, default, from, default).GetAwaiter().GetResult();

                Assert.IsTrue(results.Count() == 2 && results[0].CreatedAt > from && results[1].CreatedAt > from);
            }
        }

        [TestMethod]
        public void ReturnCollectionOfClaimsFilteredByCreationEndDate()
        {
            // Arrange
            var options = TestUtilities.GetOptions(nameof(ReturnCollectionOfClaimsFilteredByCreationEndDate));

            // Act, Assert
            using (var assertContext = new ClaimsDbContext(options))
            {
                var myProfile = new ClaimProfile();
                var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
                IMapper mapper = new Mapper(configuration);
                var claimDto = new ClaimDto();
                var claimDto2 = new ClaimDto();
                IFormFile file = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("This is a dummy file")), 0, 0, "Data", "dummy.txt");
                claimDto.BPImage = file;
                claimDto2.BPImage = file;
                var sut = new ClaimServices(assertContext, mapper);
                sut.CreateAsync(claimDto).GetAwaiter().GetResult();
                sut.CreateAsync(claimDto2).GetAwaiter().GetResult();
                DateTime to = default;
                to = DateTime.UtcNow.AddMonths(4);
                var results = sut.FilterByMultipleCriteriaAsync(null, default, default, to).GetAwaiter().GetResult();

                Assert.IsTrue(results.Count() == 2 && results[0].CreatedAt < to && results[1].CreatedAt < to);
            }
        }

        [TestMethod]
        public void ReturnCollectionOfClaimsFilteredByAirlineAndFlightNumber()
        {
            // Arrange
            var options = TestUtilities.GetOptions(nameof(ReturnCollectionOfClaimsFilteredByAirlineAndFlightNumber));

            // Act, Assert
            using (var assertContext = new ClaimsDbContext(options))
            {
                var myProfile = new ClaimProfile();
                var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
                IMapper mapper = new Mapper(configuration);
                var claimDto = new ClaimDto();
                var claimDto2 = new ClaimDto();
                var claimDto3 = new ClaimDto();
                IFormFile file = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("This is a dummy file")), 0, 0, "Data", "dummy.txt");
                claimDto.BPImage = file;
                claimDto2.BPImage = file;
                claimDto3.BPImage = file;
                claimDto.FlightNumber = 1111;
                claimDto2.FlightNumber = 1111;
                claimDto3.FlightNumber = 123;
                claimDto.Airline = "TestAir";
                claimDto2.Airline = "TestAir";
                claimDto3.Airline = "AirTest";
                var sut = new ClaimServices(assertContext, mapper);
                sut.CreateAsync(claimDto).GetAwaiter().GetResult();
                sut.CreateAsync(claimDto2).GetAwaiter().GetResult();
                sut.CreateAsync(claimDto3).GetAwaiter().GetResult();
                var results = sut.FilterByMultipleCriteriaAsync("TestAir", 1111, default, default).GetAwaiter().GetResult();

                Assert.IsTrue(results.Count() == 2 && results[0].FlightNumber == 1111 && results[1].FlightNumber == 1111
                    && results[0].Airline == "TestAir" && results[1].Airline == "TestAir");
            }
        }

        [TestMethod]
        public void ReturnCollectionOfClaimsFilteredByAirlineAndCreationStartDate()
        {
            // Arrange
            var options = TestUtilities.GetOptions(nameof(ReturnCollectionOfClaimsFilteredByAirlineAndCreationStartDate));

            // Act, Assert
            using (var assertContext = new ClaimsDbContext(options))
            {
                var myProfile = new ClaimProfile();
                var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
                IMapper mapper = new Mapper(configuration);
                var claimDto = new ClaimDto();
                var claimDto2 = new ClaimDto();
                var claimDto3 = new ClaimDto();
                IFormFile file = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("This is a dummy file")), 0, 0, "Data", "dummy.txt");
                claimDto.BPImage = file;
                claimDto2.BPImage = file;
                claimDto3.BPImage = file;
                claimDto.Airline = "TestAir";
                claimDto2.Airline = "TestAir";
                claimDto3.Airline = "AirTest";
                var sut = new ClaimServices(assertContext, mapper);
                sut.CreateAsync(claimDto).GetAwaiter().GetResult();
                sut.CreateAsync(claimDto2).GetAwaiter().GetResult();
                sut.CreateAsync(claimDto3).GetAwaiter().GetResult();
                DateTime from = default;
                from = from.AddYears(2019);
                var results = sut.FilterByMultipleCriteriaAsync("TestAir", default, from, default).GetAwaiter().GetResult();

                Assert.IsTrue(results.Count() == 2 && results[0].CreatedAt > from && results[1].CreatedAt > from
                    && results[0].Airline == "TestAir" && results[1].Airline == "TestAir");
            }
        }


        [TestMethod]
        public void ReturnCollectionOfClaimsFilteredByAirlineAndCreationEndDate()
        {
            // Arrange
            var options = TestUtilities.GetOptions(nameof(ReturnCollectionOfClaimsFilteredByAirlineAndCreationEndDate));

            // Act, Assert
            using (var assertContext = new ClaimsDbContext(options))
            {
                var myProfile = new ClaimProfile();
                var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
                IMapper mapper = new Mapper(configuration);
                var claimDto = new ClaimDto();
                var claimDto2 = new ClaimDto();
                var claimDto3 = new ClaimDto();
                IFormFile file = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("This is a dummy file")), 0, 0, "Data", "dummy.txt");
                claimDto.BPImage = file;
                claimDto2.BPImage = file;
                claimDto3.BPImage = file;
                claimDto.Airline = "TestAir";
                claimDto2.Airline = "TestAir";
                claimDto3.Airline = "AirTest";
                var sut = new ClaimServices(assertContext, mapper);
                sut.CreateAsync(claimDto).GetAwaiter().GetResult();
                sut.CreateAsync(claimDto2).GetAwaiter().GetResult();
                sut.CreateAsync(claimDto3).GetAwaiter().GetResult();
                DateTime to = default;
                to = DateTime.UtcNow.AddMonths(4);
                var results = sut.FilterByMultipleCriteriaAsync("TestAir", default, default, to).GetAwaiter().GetResult();

                Assert.IsTrue(results.Count() == 2 && results[0].CreatedAt < to && results[1].CreatedAt < to
                    && results[0].Airline == "TestAir" && results[1].Airline == "TestAir");
            }
        }

        [TestMethod]
        public void ReturnCollectionOfClaimsFilteredByFlightNumberAndCreationStartDate()
        {
            // Arrange
            var options = TestUtilities.GetOptions(nameof(ReturnCollectionOfClaimsFilteredByFlightNumberAndCreationStartDate));

            // Act, Assert
            using (var assertContext = new ClaimsDbContext(options))
            {
                var myProfile = new ClaimProfile();
                var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
                IMapper mapper = new Mapper(configuration);
                var claimDto = new ClaimDto();
                var claimDto2 = new ClaimDto();
                var claimDto3 = new ClaimDto();
                IFormFile file = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("This is a dummy file")), 0, 0, "Data", "dummy.txt");
                claimDto.BPImage = file;
                claimDto2.BPImage = file;
                claimDto3.BPImage = file;
                claimDto.FlightNumber = 1111;
                claimDto2.FlightNumber = 1111;
                claimDto3.FlightNumber = 123;
                var sut = new ClaimServices(assertContext, mapper);
                sut.CreateAsync(claimDto).GetAwaiter().GetResult();
                sut.CreateAsync(claimDto2).GetAwaiter().GetResult();
                sut.CreateAsync(claimDto3).GetAwaiter().GetResult();
                DateTime from = default;
                from = from.AddYears(2019);
                var results = sut.FilterByMultipleCriteriaAsync(null, 1111, from, default).GetAwaiter().GetResult();

                Assert.IsTrue(results.Count() == 2 && results[0].CreatedAt > from && results[1].CreatedAt > from
                    && results[0].FlightNumber == 1111 && results[1].FlightNumber == 1111);
            }
        }

        [TestMethod]
        public void ReturnCollectionOfClaimsFilteredByFlightNumberAndCreationEndDate()
        {
            // Arrange
            var options = TestUtilities.GetOptions(nameof(ReturnCollectionOfClaimsFilteredByFlightNumberAndCreationEndDate));

            // Act, Assert
            using (var assertContext = new ClaimsDbContext(options))
            {
                var myProfile = new ClaimProfile();
                var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
                IMapper mapper = new Mapper(configuration);
                var claimDto = new ClaimDto();
                var claimDto2 = new ClaimDto();
                var claimDto3 = new ClaimDto();
                IFormFile file = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("This is a dummy file")), 0, 0, "Data", "dummy.txt");
                claimDto.BPImage = file;
                claimDto2.BPImage = file;
                claimDto3.BPImage = file;
                claimDto.FlightNumber = 1111;
                claimDto2.FlightNumber = 1111;
                claimDto3.FlightNumber = 123;
                var sut = new ClaimServices(assertContext, mapper);
                sut.CreateAsync(claimDto).GetAwaiter().GetResult();
                sut.CreateAsync(claimDto2).GetAwaiter().GetResult();
                sut.CreateAsync(claimDto3).GetAwaiter().GetResult();
                DateTime to = default;
                to = DateTime.UtcNow.AddMonths(4);
                var results = sut.FilterByMultipleCriteriaAsync(null, 1111, default, to).GetAwaiter().GetResult();

                Assert.IsTrue(results.Count() == 2 && results[0].CreatedAt < to && results[1].CreatedAt < to
                    && results[0].FlightNumber == 1111 && results[1].FlightNumber == 1111);
            }
        }

        [TestMethod]
        public void ReturnCollectionOfClaimsFilteredByCreationStartAndEndDate()
        {
            // Arrange
            var options = TestUtilities.GetOptions(nameof(ReturnCollectionOfClaimsFilteredByCreationStartAndEndDate));

            // Act, Assert
            using (var assertContext = new ClaimsDbContext(options))
            {
                var myProfile = new ClaimProfile();
                var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
                IMapper mapper = new Mapper(configuration);
                var claimDto = new ClaimDto();
                var claimDto2 = new ClaimDto();
                IFormFile file = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("This is a dummy file")), 0, 0, "Data", "dummy.txt");
                claimDto.BPImage = file;
                claimDto2.BPImage = file;
                var sut = new ClaimServices(assertContext, mapper);
                sut.CreateAsync(claimDto).GetAwaiter().GetResult();
                sut.CreateAsync(claimDto2).GetAwaiter().GetResult();
                DateTime to = default;
                to = DateTime.UtcNow.AddMonths(4);
                DateTime from = default;
                from = from.AddYears(2019);
                var results = sut.FilterByMultipleCriteriaAsync(null, default, from, to).GetAwaiter().GetResult();

                Assert.IsTrue(results.Count() == 2 && results[0].CreatedAt < to && results[1].CreatedAt < to
                    && results[0].CreatedAt > from && results[1].CreatedAt > from);
            }
        }
    }
}
