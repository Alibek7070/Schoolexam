using BLL.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using schoolexam.BLL.Mapping;
using schoolexam.BLL.Services;
using schoolexam.BLL.Services.IServices;
using schoolexam.DAL.Contexts;
using schoolexam.DAL.Entities;
using schoolexam.DAL.Enum;
using schoolexam.DAL.Repositories;
using schoolexam.DAL.Repositories.IRepositories;
using System.Text;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        // Service
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<IUserAttempService, UserAttempService>();
        builder.Services.AddScoped<IUserAnswerService, UserAnswerService>();
        builder.Services.AddScoped<IQuestionService, QuestionService>();
        builder.Services.AddScoped<IOptionService, OptionService>(); 
        builder.Services.AddScoped<ITestService, TestService>();
        // Repository
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<IOptionRepository, OptionRepository>();
        builder.Services.AddScoped<IQuestionRepository, QuestionRepository>();
        builder.Services.AddScoped<ITestRepository, TestRepository>();
        builder.Services.AddScoped<IUserAnswerRepository, UserAnswerRepository>();
        builder.Services.AddScoped<IUserAttempRepository, UserAttempRepository>();
        builder.Services.AddDbContext<AppDbContext>(option =>
        option.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
        builder.Services.AddAutoMapper(cfg =>
        cfg.AddProfile<Mapping>());


        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "SchoolExamDb", Version = "v1" });

            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
            });
        });

        async Task SeedAdminAsync(WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<AppDbContext>();

            await context.Database.MigrateAsync();

            var adminEmail = "alibekcsharp@gmail.com";
            if (!await context.Users.AnyAsync(u => u.Email == adminEmail))
            {
                var admin = new User
                {
                    FirstName = "Alibek",
                    LastName = "Abdullayev",
                    Role = Role.Admin,
                    Email = adminEmail,
                    UserName = "Ali_Bekh2707",
                    Password = BCrypt.Net.BCrypt.HashPassword("0772")
                };

                context.Users.Add(admin);
                await context.SaveChangesAsync();
            }
        }

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
                    ClockSkew = TimeSpan.Zero
                };
            });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseHttpsRedirection();

        app.UseAuthorization();

        await SeedAdminAsync(app);
        app.MapControllers();

        app.Run();
    }
}