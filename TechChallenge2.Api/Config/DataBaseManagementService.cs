using Microsoft.EntityFrameworkCore;
using TechChallenge2.Data.Context;
using TechChallenge2.Identity.Data;

namespace TechChallenge2.Api.Config
{
    public static class DataBaseManagementService
    {
        //Fazendo com que ao rodar o projeto ele execute o migrate para mim
        public static void MigrationInitialisation(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var serviceData = serviceScope.ServiceProvider.GetService<DataContext>();
                var serviceIdentity = serviceScope.ServiceProvider.GetService<IdentityDataContext>();

                serviceData.Database.Migrate();
                serviceIdentity.Database.Migrate();
            }
        }
    }
}
