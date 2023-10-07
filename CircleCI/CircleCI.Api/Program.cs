using System.Text;
using System.Text.Json.Serialization;
using CircleCI.DataService.Data;
using CircleCI.DataService.DbInitializer;
using CircleCI.DataService.Repositories;
using CircleCI.DataService.Repositories.Interfaces;
using CircleCI.Services.Configuration;
using CircleCI.Services.GoogleSecrets;
using CircleCI.Services.ImageStorageService;
using CircleCI.Services.TokenService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Filters;

var connectionString = await SecretManager.GetConnectionString();
var jwtConfiguration = JsonConvert.DeserializeObject<JwtConfig>(await SecretManager.GetJwtConfiguration());
var bucketCredentials = JsonConvert.DeserializeObject<GoogleConfig>(await SecretManager.GetBucketCredentials());
var myAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<GoogleConfig>(options =>
{
    if (bucketCredentials != null)
    {
        options.GoogleBucket.GoogleCloudStorageBucket = bucketCredentials.GoogleBucket.GoogleCloudStorageBucket;
        options.GoogleBucket.GoogleCredential = bucketCredentials.GoogleBucket.GoogleCredential;
    }
});
builder.Services.Configure<JwtConfig>(options =>
{
    if (jwtConfiguration != null)
    {
        options.JwtConfiguration = jwtConfiguration.JwtConfiguration;
    }
});
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: myAllowSpecificOrigins,
        policyBuilder =>
            policyBuilder.WithOrigins("http://localhost:3000")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials()
                .WithExposedHeaders("x-total-count"));
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IUserIdentifire, UserIdentifire>();
builder.Services.AddTransient<ICloudStorage, CloudStorage>();
builder.Services.AddTransient<IDbInitializer, DbInitializer>();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = true;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateAudience = false,
        ValidateIssuer = false,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfiguration!.JwtConfiguration.AccessTokenSecret))
    };
});

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(connectionString);
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();

app.UseCors(myAllowSpecificOrigins);

app.UseCookiePolicy(new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.None,
    HttpOnly = HttpOnlyPolicy.Always,
    Secure = CookieSecurePolicy.Always
});

app.Use(async (context, next) =>
{
    var token = context.Request.Cookies["X-Access-Token"];
    
    if (!string.IsNullOrEmpty(token))
    {
        context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
        context.Response.Headers.Add("X-Xss-Protection", "1");
        context.Response.Headers.Add("X-Frame-Options", "DENY");
    }
    
    await next();
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
SeedDatabase();
await app.RunAsync();

void SeedDatabase()
{
    using var scope = app.Services.CreateScope();
    var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
    dbInitializer.Initialize();
}
