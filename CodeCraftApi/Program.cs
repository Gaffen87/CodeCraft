using FastEndpoints;
using FastEndpoints.Swagger;
using Scalar.AspNetCore;

namespace CodeCraftApi
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			builder.Services.AddFastEndpoints()
				.SwaggerDocument();

			var app = builder.Build();

			app.UseFastEndpoints();

			if (app.Environment.IsDevelopment())
			{
				//scalar by default looks for the swagger json file here: 
				app.UseOpenApi(c => c.Path = "/openapi/{documentName}.json");
				app.MapScalarApiReference();
			}

			app.Run();
		}
	}
}
