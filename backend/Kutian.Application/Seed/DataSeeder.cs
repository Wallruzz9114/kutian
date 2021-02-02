using System.Linq;
using Kutian.Domain.Entities;
using Kutian.Utilities.Abstractions;

namespace Kutian.Application.Seed
{
    public static class DataSeeder
    {
        public static void Seed(IDatabaseContext databaseContext)
        {
            var user = databaseContext.Set<User>().FirstOrDefault(x => x.Username == "testuser@gmail.com");
            user ??= new User("testuser@gmail.com", "Pa$$w0rd");
            databaseContext.Store(user);
            databaseContext.SaveChangesAsync(default).GetAwaiter().GetResult();
        }
    }
}