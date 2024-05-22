using DISERTATIE.FLACARA.CAPTURII.CONFIGURATION;
using DISERTATIE.FLACARA.CAPTURII.HUBS;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSignalR();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ValidationConfiguration();
builder.Services.AddRepositoryConfiguration(builder.Configuration);
builder.Services.AddMapperConfiguration();
builder.Services.AddServiceConfiguration(builder.Configuration);
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder =>
    {
        builder.WithOrigins("http://localhost:4200", "https://effortless-longma-c36c0b.netlify.app/")
               .AllowAnyMethod()
               .AllowAnyHeader()
               .AllowCredentials()
               .SetIsOriginAllowed((host) => true);
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(x => x
   .WithOrigins(builder.Configuration.GetConnectionString("CorsLocation"), "https://effortless-longma-c36c0b.netlify.app/", "http://localhost:4201")
   .AllowAnyMethod()
   .AllowAnyHeader()
   .AllowCredentials()
   .SetIsOriginAllowed((host) => true));

app.UseHttpsRedirection();

app.UseAuthentication();

app.MapHub<PostHub>("/posthub");

app.UseAuthorization();

var imagesPath = Path.Combine(builder.Environment.ContentRootPath, "Images");

if (!Directory.Exists(imagesPath))
{
    Directory.CreateDirectory(imagesPath);
}

app.UseStaticFiles
    (
        new StaticFileOptions
        {
            FileProvider = new PhysicalFileProvider(Path.Combine(builder.Environment.ContentRootPath, "Images")),
            RequestPath = "/Images"
        }
    );

app.MapControllers();

app.Run();
