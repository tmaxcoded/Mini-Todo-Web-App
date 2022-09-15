


using Microsoft.AspNetCore.Identity;

namespace TodoApp.Persistence
{
    public static class PersistenceDependencyInjection
    {
        public static IServiceCollection  PersistentLayerDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {

           

            var connString = "Data Source=LocalBuilder.db";
                var con = new SqliteConnection(connString);
       

           services.AddDbContext<TodoAppDbContext>(option => option.UseSqlite(con));

           services.AddTransient<ITodoAppRepository,TodoAppRepository>();

           return services;
        }
    }
}