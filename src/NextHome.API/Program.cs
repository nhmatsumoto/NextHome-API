using NextHome.Infrastructure.IoC;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);


// Adiciona suporte a localiza��o
builder.Services.AddLocalization(options => options.ResourcesPath = "../NextHome.API/Resources");

// Configura os idiomas suportados
var supportedCultures = new[]
{
    new CultureInfo("en"), // Ingl�s
    new CultureInfo("pt-BR"), // Portugu�s
    new CultureInfo("ja") // Japon�s
};

// Cross Cutting IoC
builder.Services.AddInfrastructure(builder.Configuration);

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

app.UseCors("NextHomeCorsPolicy");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
