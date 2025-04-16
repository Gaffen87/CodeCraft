
namespace CodeCraftApi.SignalR;

public class AppHub(IEnumerable<IHubMethodHandler> methodHandlers) : ReprHub(methodHandlers)
{
}
