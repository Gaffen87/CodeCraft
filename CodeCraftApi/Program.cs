global using FastEndpoints;
global using FastEndpoints.Swagger;
using CodeCraftApi.Database;
using CodeCraftApi.Domain.Entities;
using CodeCraftApi.Features.DbAbstraction;
using CodeCraftApi.SignalR;
using FastEndpoints.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Scalar.AspNetCore;
using SignalR.PepR;

namespace CodeCraftApi
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			builder.Logging.AddApplicationInsights().AddAzureWebAppDiagnostics();

			builder.Services.AddMediatR(cfg =>
			{
				cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
			});

			builder.Services.AddSignalR()
				.AddNewtonsoftJsonProtocol(config => config.PayloadSerializerSettings.Converters.Add(new StringEnumConverter(new CamelCaseNamingStrategy())));

			builder.Services.AddHubMethodHandlers([typeof(Program).Assembly]);

			builder.Services.AddSingleton<IUserIdProvider, HubUserIdProvider>();

			builder.Services.AddCors(
				options =>
				options.AddDefaultPolicy(
					p => p.AllowAnyHeader()
					.AllowAnyMethod()
					.WithOrigins("http://localhost:5173", "https://code-craft-ecru.vercel.app", "https://code-craft-ecru.vercel.app/", "https://vercel.app")
					.AllowCredentials())
				);

			builder.Services
				.AddAuthenticationJwtBearer(s => s.SigningKey = "EgliLoHLLxgdxLBuUH9thsIhkKjA4ieaUm0THxZA4a2yySKW2pYKNbl9mGpHkm1u7KE1UtHmmmtQQdwgPWBRjw==",
				options => options.Events = new JwtBearerEvents
				{
					OnMessageReceived = context =>
					{
						var accessToken = context.Request.Query["access_token"];
						var path = context.HttpContext.Request.Path;

						if (!string.IsNullOrEmpty(accessToken) &&
						(path.StartsWithSegments("/hub")))
						{
							context.Token = accessToken;
						}
						return Task.CompletedTask;
					}
				})
				.AddAuthorization()
				.AddFastEndpoints()
				.SwaggerDocument(o => o.ShortSchemaNames = true);

			builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"),
				o =>
				{
					o.MapEnum<ExerciseDifficulty>();
					o.MapEnum<Role>();
					o.MapEnum<Status>();
					o.MapEnum<SessionStatus>();
				}));

			builder.Services.AddScoped<IAppDbContext>(sp => sp.GetRequiredService<AppDbContext>());

			var app = builder.Build();


			app.UseCors();

			app.UseAuthentication()
				.UseAuthorization()
				.UseFastEndpoints(c => c.Security.RoleClaimType = "user_role")
				.UseSwaggerGen();

			if (app.Environment.IsDevelopment())
			{
				//scalar by default looks for the swagger json file here: 
				app.UseOpenApi(c => c.Path = "/openapi/{documentName}.json");
				app.MapScalarApiReference(x =>
				{
					x.Title = "CodeCraft";
					x.Layout = ScalarLayout.Modern;
					x.Theme = ScalarTheme.DeepSpace;
				});
			}

			app.MapHub<AppHub>("/hub");

			app.Run();
		}
	}
}
