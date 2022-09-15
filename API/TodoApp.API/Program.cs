

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using TodoApp.Persistence.DatabaseContext;
using WatchDog.src.Enums;

var builder = WebApplication.CreateBuilder(args);

var _builder =builder.Services.AddIdentityCore<User>(o =>
{
    o.Password.RequireDigit = true;
    o.Password.RequireLowercase = false;
    o.Password.RequireUppercase = false;
    o.Password.RequireNonAlphanumeric = false;
    o.Password.RequiredLength = 10;
    o.User.RequireUniqueEmail = true;
});
_builder = new IdentityBuilder(_builder.UserType, typeof(IdentityRole),
_builder.Services);
_builder.AddEntityFrameworkStores<TodoAppDbContext>();




// Add services to the container.
builder.Services.CoreLayerDependencyInjection(builder.Configuration);
builder.Services.PersistentLayerDependencyInjection(builder.Configuration);


builder.Services.AddWatchDogServices( settings =>
{
    //settings.SqlDriverOption = WatchDogSqlDriverEnum.MSSQL;
    //settings.SetExternalDbConnString = "Server=localhost;Database=testLoggingDb;User Id=sa;Password=password;";
});



builder.Services.AddWatchDogServices(settings =>
{
    settings.IsAutoClear = true;
    settings.ClearTimeSchedule = WatchDogAutoClearScheduleEnum.Weekly;
});

builder.Services.AddApiVersioning(o =>
{
    o.AssumeDefaultVersionWhenUnspecified = true;
    o.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
    o.ReportApiVersions = true;
    

});


#pragma warning disable CS0618 // Type or member is obsolete

builder.Services.AddControllers()
    .AddFluentValidation(options => {
        options.ImplicitlyValidateChildProperties = true;
        options.ImplicitlyValidateRootCollectionElements = true;
        options.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
    });
#pragma warning restore CS0618 // Type or member is obsolete
                              // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(p => p.AddDefaultPolicy( builder =>

{

    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();

}));

var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;



    var context = services.GetRequiredService<TodoAppDbContext>();
    context.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors();

app.UseWatchDog(opt =>
{
    opt.WatchPageUsername = "todo";
    opt.WatchPagePassword = "todo";
    opt.Blacklist = "/api/v1/WeatherForecast";
});

app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
