using CodeCraftApi.Features.DbAbstraction;
using SignalR.PepR;

namespace CodeCraftApi.SignalR;

public class AppHub(IEnumerable<IHubMethodHandler> methodHandlers, IAppDbContext dbContext) : PepRHub(methodHandlers)
{
	//public override async Task OnConnectedAsync()
	//{
	//	var userId = Guid.Parse(Context.UserIdentifier!);
	//	var user = await dbContext.Users.AsNoTracking().SingleOrDefaultAsync(u => u.Id == userId);

	//	var groups = dbContext.Groups.AsNoTracking().Include(m => m.Members).Where(g => g.Members.Any(u => u.Id == user!.Id)).ToList();

	//	foreach (var group in groups)
	//	{
	//		await Groups.AddToGroupAsync(Context.ConnectionId, group.Name);
	//		var response = new HubResponse<dynamic>()
	//		{
	//			Type = HubResponseType.Group,
	//			Content = new
	//			{
	//				GroupName = group.Name,
	//				Members = group.Members,
	//			}
	//		};
	//		await Clients.All.SendCoreAsync("ReceiveMessage", [response]);
	//	}
	//}
}
