using EmploYee.Infrastructure;
using MediatR;
using Startup.Handlers;
using Startup.Providers;
using Startup.Services;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ISecurityProvider, SecurityProvider>();
builder.Services.AddScoped<ISessionService, SessionService>();
builder.Services.AddScoped<AuthHandler>();
builder.Services.AddAuthentication(AuthHandler.Scheme)
    .AddCookie(AuthHandler.Scheme, options =>
    {
        options.Cookie = new CookieBuilder
        {
            HttpOnly = false,
            Name = "SID"
        };
        options.SlidingExpiration = true;
        options.EventsType = typeof(AuthHandler);
    });


builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddMediatR(typeof(Program).Assembly);
builder.Services.AddInfrastructure(builder.Configuration.GetConnectionString("DefaultConnection"));

var app = builder.Build();

app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true)
    .AllowCredentials());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();