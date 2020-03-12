using Data;
using Microsoft.EntityFrameworkCore;

namespace ClaimsManagementTests
{
    public class TestUtilities
    {
        public static DbContextOptions<ClaimsDbContext> GetOptions(string databaseName)
        {
            return new DbContextOptionsBuilder<ClaimsDbContext>()
                .UseInMemoryDatabase(databaseName)
                .Options;
        }
    }
}
