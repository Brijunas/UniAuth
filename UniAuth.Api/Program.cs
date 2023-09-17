using Api.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using UniAuth.Api.Settings;
using UniAuth.Domain;
using UniAuth.Infra;
using UniAuth.Infra.Auth;
using UniAuth.Infra.Database;

var builder = WebApplication.CreateBuilder(args);

// Reading configuration
builder.Services.Configure<MongoContextSettings>(
    builder.Configuration.GetSection(nameof(MongoContextSettings)));

var jwtSettings = builder.Configuration.GetSection(nameof(JwtSettings));
builder.Services.Configure<JwtSettings>(jwtSettings);

var urls = builder.Configuration.GetSection(nameof(Urls));
builder.Services.Configure<Urls>(urls);

// Add services to the container.
builder.Services.AddInfraServices();
builder.Services.AddDomainServices();
builder.Services.AddControllers(options =>
{
    // Catch all endpoints.
    options.UseNamespaceRouteToken();

    // Add kebab-case support for namespaces.
    options.Conventions.Add(new NamespaceRoutingConvention());

    // Add kebab-case support for rest.
    options.Conventions.Add(new RouteTokenTransformerConvention(new KebabCaseParameterTransformer()));
});

builder.Services.AddCors(options =>
{
    var frontEndUrl = urls.GetValue<string>(nameof(Urls.Frontend)) ?? throw new ArgumentNullException(nameof(Urls.Frontend));

    options.AddDefaultPolicy(builderPolicy =>
        {
            builderPolicy.WithOrigins(frontEndUrl)
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
        });

});

// JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var key = jwtSettings.GetValue<string>(nameof(JwtSettings.Key)) ?? throw new ArgumentNullException(nameof(JwtSettings.Key));

        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
