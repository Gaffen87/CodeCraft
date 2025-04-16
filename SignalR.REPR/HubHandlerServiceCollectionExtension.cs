using CodeCraftApi.SignalR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CodeCraft.SignalR.Handlers;
public static class HubHandlerServiceCollectionExtensions
{
	public static IServiceCollection AddHubMethodHandlers(this IServiceCollection services, Assembly[] assemblies)
	{
		var handlerTypes = assemblies
			.SelectMany(a => a.GetTypes())
			.Where(t => !t.IsAbstract && typeof(IHubMethodHandler).IsAssignableFrom(t));

		foreach (var type in handlerTypes)
			services.AddScoped(typeof(IHubMethodHandler), type);

		return services;
	}
}
