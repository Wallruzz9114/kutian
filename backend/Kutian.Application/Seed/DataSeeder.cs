using System.Linq;
using Kutian.Domain.Entities;
using Kutian.Utilities.Abstractions;

namespace Kutian.Application.Seed
{
    public static class DataSeeder
    {
        public static void Seed(IDatabaseContext databaseContext)
        {
            CardConfiguration.Seed(databaseContext);
            UserConfiguration.Seed(databaseContext);
            DashboardConfiguration.Seed(databaseContext);
        }

        internal class CardConfiguration
        {
            public static void Seed(IDatabaseContext databaseContext)
            {
                var card = databaseContext.Set<Card>().FirstOrDefault(x => x.Name == "Leads");
                card ??= new Card("Leads");
                databaseContext.Store(card);
                databaseContext.SaveChangesAsync(default).GetAwaiter().GetResult();
            }
        }

        internal class UserConfiguration
        {
            public static void Seed(IDatabaseContext databaseContext)
            {
                var user = databaseContext.Set<User>().FirstOrDefault(x => x.Username == "testuser@gmail.com");
                user ??= new User("testuser@gmail.com", "Pa$$w0rd");
                databaseContext.Store(user);
                databaseContext.SaveChangesAsync(default).GetAwaiter().GetResult();
            }
        }

        internal class DashboardConfiguration
        {
            public static void Seed(IDatabaseContext databaseContext)
            {
                var user = databaseContext.Set<User>().FirstOrDefault(x => x.Username == "testuser@gmail.com");
                var dashboard = databaseContext.
                    Set<Dashboard>()
                    .FirstOrDefault(x => x.Name == "Default" && x.UserId == user.UserId);

                dashboard ??= new Dashboard("Default", user.UserId);
                databaseContext.Store(dashboard);
                databaseContext.SaveChangesAsync(default).GetAwaiter().GetResult();
            }
        }
    }
}