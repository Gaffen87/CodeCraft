using CodeCraftApi.Features.DbAbstraction;
using SignalR.PepR;

namespace CodeCraftApi.SignalR;
/// <summary>
/// SignalR hub for the application.
/// </summary>
/// <param name="methodHandlers"> The collection of hub method handlers.</param>
/// <param name="dbContext"> The application database context.</param>
public class AppHub(IEnumerable<IHubMethodHandler> methodHandlers, IAppDbContext dbContext) : PepRHub(methodHandlers)
{

}
