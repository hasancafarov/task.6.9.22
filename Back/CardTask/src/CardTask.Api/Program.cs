using CardTask.Application.Helpers;
using CardTask.Application.Authorization;
using CardTask.Application.Services;
using CardTask.DataAccess.Persistence;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using CardTask.Application.MapppingProfiles;
using System.Text.Json.Serialization;
using Microsoft.IdentityModel.Logging;

var builder = WebApplication.CreateBuilder(args);

// add services to DI container
{
    var services = builder.Services;
    var env = builder.Environment;
    // use sql server db in production and sqlite db in development
    //if (env.IsProduction())
    services.AddDbContext<CardDbContext>(optionsBuilder => optionsBuilder.UseNpgsql("server=localhost;database=CardDb;User ID=postgres;password=Aze***055;"));
    //else
    //  services.AddDbContext<DataContext, SqliteDataContext>();

    services.AddCors();
    services.AddControllers().AddJsonOptions(x =>
    {
        // serialize enums as strings in api responses (e.g. Role)
        x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });
    

    services.AddAutoMapper(typeof(AutoMapperProfile));
    services.AddSwaggerGen();

    AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

    // configure strongly typed settings object
    services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

    // configure DI for application services
    services.AddScoped<IJwtUtils, JwtUtils>();
    services.AddScoped<IUserService, UserService>();
    services.AddScoped<IAccountService, AccountService>();
    services.AddScoped<ICardService, CardService>();
    services.AddScoped<IEmailService, EmailService>();

}

var app = builder.Build();

// migrate any database changes on startup (includes initial db creation)
/*using (var scope = app.Services.CreateScope())
{
    var dataContext = scope.ServiceProvider.GetRequiredService<DataContext>();
    dataContext.Database.Migrate();
}*/

// configure HTTP request pipeline
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(x => x.SwaggerEndpoint("/swagger/v1/swagger.json", "Card System API"));
        IdentityModelEventSource.ShowPII = true;
    }

    // global cors policy
    app.UseCors(x => x
        .SetIsOriginAllowed(origin => true)
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials());

    // global error handler
    app.UseMiddleware<ErrorHandlerMiddleware>();

    // custom jwt auth middleware
    app.UseMiddleware<JwtMiddleware>();

    app.MapControllers();
 

    //app.UseHttpsRedirection();

    //app.UseAuthorization();

}
app.Run("http://localhost:4000");