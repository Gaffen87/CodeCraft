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