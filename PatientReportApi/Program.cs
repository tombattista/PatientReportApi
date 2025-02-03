using PatientReportApi.Repositories;
using PatientReportApi.Repositories.Interfaces;
using PatientReportApi.Services;
using PatientReportApi.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);
var isDevelopment = builder.Environment.IsDevelopment();

// Register services and repositories
builder.Services.AddScoped<IAlertService, AlertService>();
builder.Services.AddScoped<IReportService, ReportService>();

builder.Services.AddScoped<IAlertRepository, AlertRepository>();
builder.Services.AddScoped<IInMemoryReportRepository, InMemoryReportRepository>();

if (isDevelopment)
{
    // Handle CORS for local development
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowLocalhost4200",
            builder => builder.WithOrigins("http://localhost:4200")
                              .AllowAnyHeader()
                              .AllowAnyMethod());
    });
}

// Add controllers
builder.Services.AddControllers();

// Add in-memory cache
builder.Services.AddMemoryCache();
builder.Services.AddScoped<ICacheService, CacheService>();

// Configure logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();
builder.Logging.AddEventSourceLogger();

// Add Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Build app
var app = builder.Build();
app.UseCors("AllowLocalhost4200");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
