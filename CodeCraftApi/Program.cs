global using FastEndpoints;
global using FastEndpoints.Swagger;
using CodeCraftApi.Database;
using CodeCraftApi.Domain.Entities;
using FastEndpoints.Security;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

namespace CodeCraftApi
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			builder.Services
				.AddAuthenticationJwtBearer(s => s.SigningKey = "EgliLoHLLxgdxLBuUH9thsIhkKjA4ieaUm0THxZA4a2yySKW2pYKNbl9mGpHkm1u7KE1UtHmmmtQQdwgPWBRjw==")
				.AddAuthorization()
				.AddFastEndpoints()
				.SwaggerDocument(o => o.ShortSchemaNames = true);

			builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"),
				o =>
				{
					o.MapEnum<ExerciseDifficulty>();
					o.MapEnum<Role>();
					o.MapEnum<Status>("status");
					o.MapEnum<SessionStatus>();
					o.MapEnum<GroupSize>();
				}));

			var app = builder.Build();

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

			app.Run();
		}
	}
}
