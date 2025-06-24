using CodeCraftApi.Domain.DomainEvents;
using CodeCraftApi.Features.DbAbstraction;
using Microsoft.EntityFrameworkCore;
using SignalR.PepR;

namespace CodeCraftApi.SignalR;
/// <summary>
/// SignalR hub for the application.
/// </summary>
/// <param name="methodHandlers"> The collection of hub method handlers.</param>
/// <param name="dbContext"> The application database context.</param>
public class AppHub(IEnumerable<IHubMethodHandler> methodHandlers, IAppDbContext dbContext) : PepRHub(methodHandlers)
{
	public async override Task OnDisconnectedAsync(Exception? exception)
	{
		var userGroup = dbContext.Groups.Include(x => x.Members).SingleOrDefault(x => x.Members.Any(x => x.Id == Guid.Parse(Context.UserIdentifier!)));
		var groups = dbContext.Groups.ToList();

		if (userGroup != null)
		{
			userGroup.Members.RemoveAt(userGroup.Members.FindIndex(x => x.Id == Guid.Parse(Context.UserIdentifier!)));
			await dbContext.SaveChangesAsync();
			await Groups.RemoveFromGroupAsync(Context.ConnectionId, userGroup.Name);
		}

		foreach (var group in groups)
		{
			await new UserLeftGroupEvent(group.Id, Guid.Parse(Context.UserIdentifier!)).PublishAsync();
		}
	}

	public async override Task OnConnectedAsync()
	{
		var userGroup = dbContext.Groups.Include(x => x.Members).FirstOrDefault(x => x.Members.Any(x => x.Id == Guid.Parse(Context.UserIdentifier!)));
		var groups = dbContext.Groups.ToList();

		foreach (var group in groups)
		{
			await new UserLeftGroupEvent(group.Id, Guid.Parse(Context.UserIdentifier!)).PublishAsync();
		}

		var user = dbContext.Users.Find(Guid.Parse(Context.UserIdentifier!));

		if (userGroup != null)
		{
			if (userGroup.Members == null)
			{
				userGroup.Members = [];
			}
			userGroup.Members.Add(user!);

			await Groups.AddToGroupAsync(Context.ConnectionId, userGroup.Name);

			await new UserJoinedGroupEvent(userGroup.Id, userGroup.Name, [user]).PublishAsync();
		}
	}
}
