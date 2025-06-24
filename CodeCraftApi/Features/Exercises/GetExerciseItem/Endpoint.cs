namespace CodeCraftApi.Features.Exercises.GetExerciseItem;

using CodeCraftApi.Features.DbAbstraction;
using Domain.Entities;

/// <summary>
/// Endpoint til at hente en enkelt <see cref="ExerciseItem"/> baseret på dets ID.
/// </summary>
/// <param name="context">Databasens kontekst, injiceret via dependency injection.</param>
internal sealed class GetExerciseItemEndpoint(IAppDbContext context)
	: Endpoint<GetExerciseItemRequest, GetExerciseItemResponse, Mapper>
{
	/// <summary>
	/// Konfigurerer endpointet med HTTP-metode, URL-rute, gruppe og beskrivelse.
	/// </summary>
	public override void Configure()
	{
		Get("/Item/{ExerciseItemId}");
		Group<ExerciseGroup>();
		Description(x => x.WithName("Get Exercise Item"));
		Summary(new Summary());
		Roles("student", "teacher");
	}
	/// <summary>
	/// Håndterer HTTP-anmodningen og returnerer en <see cref="GetExerciseItemResponse"/> 
	/// hvis elementet findes, ellers returneres en 404 Not Found.
	/// </summary>
	/// <param name="req">Anmodningen, der indeholder ID'et for det ønskede <see cref="ExerciseItem"/>.</param>
	/// <param name="ct">Cancellation token til at annullere den asynkrone operation.</param>
	public override async Task HandleAsync(GetExerciseItemRequest req, CancellationToken ct)
	{
		var exerciseItem = await Data.GetExerciseItemsAsync(context, req.ExerciseItemId);

		if (exerciseItem == null)
		{
			await SendNotFoundAsync(ct);
		}
		else
		{
			await SendAsync(Map.FromEntity(exerciseItem), cancellation: ct);
		}
	}
}