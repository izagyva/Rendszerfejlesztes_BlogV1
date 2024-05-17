using Blog.Core.Profiles;
using Blog.Core.Services;
using Blog.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Security.Cryptography;
using System.Text.Json;

namespace Blog.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add IHttpContextAccessor to the services collection
            builder.Services.AddHttpContextAccessor();

            // Add services to the container.
            builder.Services.AddScoped<ITopicService, TopicService>();
            builder.Services.AddScoped<ICommentService, CommentService>();
            builder.Services.AddScoped<IAuthService, AuthService>();

            // Add autoMapper
            builder.Services.AddAutoMapper(typeof(CommentMapperConfig), typeof(TopicMapperConfig), typeof(UserMapperConfig));

            // Configure MongoDB
            //var mongoDbSettings = builder.Configuration.GetSection("MongoDBSettings");
            //builder.Services.AddSingleton<BlogMongoDbContext>(serviceProvider =>
            //{
            //    return new BlogMongoDbContext(
            //        mongoDbSettings["Connection"],
            //        mongoDbSettings["DatabaseName"]);
            //});
            ConfigureMongoDB(builder.Configuration);
            builder.Services.AddSingleton<BlogMongoDbContext>(serviceProvider =>
            {
                return ConfigureMongoDB(builder.Configuration);
            });

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API", Version = "v1" });
            });

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(builder.Configuration.GetSection("Token").Value)),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true
                    };
                });


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API v1"));
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
        public static BlogMongoDbContext ConfigureMongoDB(IConfiguration configuration)
        {
            //const string connectionUri = "mongodb+srv://<test>:<test>@cluster0.vwobijk.mongodb.net/?retryWrites=true&w=majority&appName=Cluster0";

            //var settings = MongoClientSettings.FromConnectionString(connectionUri);
            //settings.ServerApi = new ServerApi(ServerApiVersion.V1);

            //var client = new MongoClient(settings);

            //try
            //{
            //    var result = client.GetDatabase("admin").RunCommand<BsonDocument>(new BsonDocument("ping", 1));
            //    Console.WriteLine("Pinged your deployment. You successfully connected to MongoDB!");
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine($"An error occurred while connecting to MongoDB: {ex.Message}");
            //}
            string username = "admin";
            string password = "admin123"; // Replace with your actual password
            string encodedPassword = Uri.EscapeDataString(password);
            string connectionUri = $"mongodb+srv://{username}:{encodedPassword}@cluster0.vwobijk.mongodb.net/Blog?retryWrites=true&w=majority&appName=Cluster0";
            var client = new MongoClient(connectionUri);
            return new BlogMongoDbContext(connectionUri);
        }
    }
}