using CodeCraftApi.Domain.DomainEvents;
using CodeCraftApi.Features.DbAbstraction;
using Compiler;

namespace CodeCraftApi.Features.Submissions.SendCodeSubmission;
/// <summary>
/// Endpoint for sending code submissions.
/// </summary>
/// <param name="dbContext"> The application database context.</param>
internal sealed class Endpoint(IAppDbContext dbContext) : Endpoint<CodeSubmissionRequest, CodeSubmissionResponse, Mapper>
{
	/// <summary>
	/// Configures the endpoint.
	/// </summary>
	public override void Configure()
	{
		Post("/code/submissions");
		Description(x => x.WithName("Send code submission"));
		AllowAnonymous();
		Summary(new Summary());
	}
	/// <summary>
	/// Handles the request to send a code submission.
	/// </summary>
	/// <param name="r"> The request containing the code submission data.</param>
	/// <param name="c"> The cancellation token.</param>
	public override async Task HandleAsync(CodeSubmissionRequest r, CancellationToken c)
	{
		var compilerResult = await ProcessCodeSubmission(r);

		var submission = Map.ToEntity(r);
		submission.IsSuccess = compilerResult.Keys.First();
		submission.Result = compilerResult.Values.First();

		var group = await Data.GetGroup(dbContext, r.SubmittedBy);
		var step = await Data.GetExerciseStep(dbContext, r.ExerciseStep);
		var dbResult = await Data.SaveCodeSubmission(dbContext, submission, group, step);

		await new CodeSubmittedEvent
		(
			group.Id,
			group.Name,
			step.Title,
			submission.Result,
			compilerResult.Keys.First(),
			dbResult.SubmitDate
		).PublishAsync(cancellation: c);

		await SendAsync(new CodeSubmissionResponse { Result = compilerResult.Values.First(), isSuccess = compilerResult.Keys.First() });
	}
	
	/// <summary>
	/// Processes the code submission by compiling and running the code.
	/// </summary>
	/// <param name="r"> The code submission request containing the code files.</param>
	/// <returns> A dictionary containing the compilation result and the output of the code.</returns>
	private static async Task<Dictionary<bool, string>> ProcessCodeSubmission(CodeSubmissionRequest r)
	{
		Dictionary<string, string> codeFiles = [];
		foreach (var file in r.Files)
		{
			codeFiles.Add(file.FileName, file.Content);
		}
		return await SimpleCompiler.CodeRunner(codeFiles);
	}
}