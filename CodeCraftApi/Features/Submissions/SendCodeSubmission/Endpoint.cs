using CodeCraftApi.Database;
using CodeCraftApi.Domain.DomainEvents;
using Compiler;

namespace CodeCraftApi.Features.Submissions.SendCodeSubmission;

internal sealed class Endpoint(AppDbContext dbContext) : Endpoint<CodeSubmissionRequest, CodeSubmissionResponse, Mapper>
{
	public override void Configure()
	{
		Post("/code/submissions");
		AllowAnonymous();
		Summary(new Summary());
	}

	public override async Task HandleAsync(CodeSubmissionRequest r, CancellationToken c)
	{
		var submission = Map.ToEntity(r);

		string result = await ProcessCodeSubmission(r);
		submission.Result = result;

		var group = await Data.GetGroup(dbContext, r.SubmittedBy);
		var step = await Data.GetExerciseStep(dbContext, r.ExerciseStep);
		await Data.SaveCodeSubmission(dbContext, submission, group, step);

		await new CodeSubmittedEvent
		(
			group.Id,
			group.Name,
			step.Title,
			submission.Result
		).PublishAsync(cancellation: c);

		await SendAsync(new CodeSubmissionResponse { Result = result });
	}

	private static async Task<string> ProcessCodeSubmission(CodeSubmissionRequest r)
	{
		Dictionary<string, string> codeFiles = [];
		foreach (var file in r.Files)
		{
			codeFiles.Add(file.FileName, file.Content);
		}
		return await SimpleCompiler.CodeRunner(codeFiles);
	}
}