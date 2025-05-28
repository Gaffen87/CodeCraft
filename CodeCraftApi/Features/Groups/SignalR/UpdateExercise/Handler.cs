using CodeCraftApi.Database;
using CodeCraftApi.SignalR;
using Microsoft.AspNetCore.SignalR;
using SignalR.PepR;

namespace CodeCraftApi.Features.Groups.SignalR.UpdateExercise;

internal sealed class UpdateExerciseHandler(IHubContext<AppHub> hub, AppDbContext dbContext) : HubMethodHandler<UpdateExercisePayload>
{
	protected override async Task HandleAsync(HubCallerContext context, UpdateExercisePayload payload)
	{
		var exerciseTitle = await Data.GetExerciseTitle(dbContext, payload.ExerciseStepId);
		var exerciseItemNumber = await Data.GetExerciseItemNumber(dbContext, payload.ExerciseStepId);
		var stepIndex = await Data.GetStepIndex(dbContext, payload.ExerciseStepId);

		if (exerciseTitle != null && exerciseItemNumber != null && stepIndex != null)
		{
			await hub.Clients.All.SendCoreAsync("ReceiveGroupExerciseMessage", [payload.GroupId, exerciseTitle, exerciseItemNumber, stepIndex]);
		}
	}
}
