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

		await Data.SaveCodeSubmission(dbContext, submission, r.SubmittedBy, r.ExerciseStep);

		await new CodeSubmittedEvent
		(
			submission.Id,
			submission.SubmittedBy.Id,
			submission.SubmittedBy.Name,
			submission.ExerciseStep.Id,
			result
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