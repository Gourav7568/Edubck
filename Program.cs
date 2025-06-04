using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SampleProject.Data;
using SampleProject.Models;


var builder = WebApplication.CreateBuilder(args);

// Define CORS policy
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins("https://calm-meadow-0ad74d900.6.azurestaticapps.net") // React app URL
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();

builder.Services.AddDbContext<EduSyncContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "EduSync API", Version = "v1" });
    

});
builder.Services.AddApplicationInsightsTelemetry();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// ✅ Use CORS BEFORE authentication & authorization
app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.UseStaticFiles();

app.MapControllers();

app.Run();
