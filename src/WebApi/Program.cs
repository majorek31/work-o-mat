using System.Reflection.Metadata;
using Infrastructure;
using Microsoft.OpenApi;
using WorkOMat.Application;
using WorkOMat.WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);



builder.Services
    .AddInfrastructure(builder.Configuration)
    .AddApplication();


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = "Bearer";
    options.DefaultChallengeScheme = "Bearer";
}).AddJwtBearer("Bearer", options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"] ?? throw new InvalidOperationException("JWT Issuer is not configured."),
        ValidAudience = builder.Configuration["Jwt:Audience"] ?? throw new InvalidOperationException("JWT Audience is not configured."),
        IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(
            System.Text.Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"] ?? throw new InvalidOperationException("JWT Secret Key is not configured.")))
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Bearer", policy =>
    {
        policy.AuthenticationSchemes.Add("Bearer");
        policy.RequireAuthenticatedUser();
    });
});

builder.Services.AddOpenApi(o =>
{
    o.AddDocumentTransformer((doc, ctx, cancellationToken) =>
    {
        doc.Components ??= new OpenApiComponents();

        doc.Components.SecuritySchemes ??= new Dictionary<string, IOpenApiSecurityScheme>();

        doc.Components.SecuritySchemes["Bearer"] = new OpenApiSecurityScheme
        {
            Type = SecuritySchemeType.Http,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            Description = "Enter your JWT token in format: Bearer {YOUR_TOKEN}"
        };

        doc.Security ??= new List<OpenApiSecurityRequirement>();

        doc.Security.Add(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecuritySchemeReference("Bearer", doc),
                new List<string>()
            } 
        });
        return Task.CompletedTask;
    });
})
.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwagger();
    app.UseSwaggerUI();

    
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapEndpoints();

app.Run();
