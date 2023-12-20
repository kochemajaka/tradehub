using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the ;container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<tradehub.Infrastructure.Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Database")));

builder.Services.AddCors(policy => {

    policy.AddPolicy("Policy_Name", builder =>
    builder.WithOrigins("https://localhost:7119/")
    .SetIsOriginAllowedToAllowWildcardSubdomains()
    .AllowAnyOrigin()
    .WithMethods("GET", "POST", "PUT", "DELETE")
    .AllowAnyHeader()
    );
});



var app = builder.Build();
app.UseCors("Policy_Name");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
