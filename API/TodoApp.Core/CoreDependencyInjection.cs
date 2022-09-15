


using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace TodoApp.Core
{
   public static  class CoreDependencyInjection
    {
        public static IServiceCollection CoreLayerDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddScoped<ITodoAppService,TodoAppService>();
            services.AddScoped<IAuthenticationManagerService,AuthenticationManagerService>();

            var jwtSettings = configuration.GetSection("JwtSettings");
            var ValidAud = jwtSettings.GetSection("validAudience").Value;
            var validIssuer = jwtSettings.GetSection("validIssuer").Value;
            var secret = jwtSettings.GetSection("secret").Value;
            var symetickKey = new
                SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.GetSection("secret").Value));
           
            services.AddAuthentication(opt => {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;



            })
             .AddJwtBearer(options =>
             {
                 var key = Encoding.UTF8.GetBytes(secret);
                 options.SaveToken = true;
                 options.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateIssuer = true,
                      ValidateAudience = true,
                     ValidateLifetime = true,
                     ValidateIssuerSigningKey = true,
                     ValidIssuer = validIssuer,
                     ValidAudience = ValidAud,
                     IssuerSigningKey = new SymmetricSecurityKey(key),
                     
                    
                     RequireExpirationTime = false,
                     
                     
                    
                     
                 };
             });



            return services;
        }
    }
}
