using Microsoft.EntityFrameworkCore;

namespace DiscountGrpc.Data
{
    //Its not a good approach to add the Database via tha package manager console..
    //so via this we'll create our database..it is a very strong powerful feature(migrateasync)
    public static class Extension
    {
        public static IApplicationBuilder UseMigration(this IApplicationBuilder app)
        { 
            using var scope = app.ApplicationServices.CreateScope();
            using var dbContext = scope.ServiceProvider.GetRequiredService<DiscountContext>();
            dbContext.Database.MigrateAsync();

            return app;
        }

    }
}
