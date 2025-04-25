using CodeCraftApi.Domain.Entities;

namespace CodeCraftApi.Features.Submissions.SendCodeSubmission;

internal sealed class Mapper : Mapper<CodeSubmissionRequest, CodeSubmissionResponse, CodeSubmission>
{
	public override CodeSubmission ToEntity(CodeSubmissionRequest r)
	{
		return new CodeSubmission
		{
			Id = Guid.NewGuid(),
			SubmitDate = DateTimeOffset.UtcNow,
			IsSuccess = true,
			Files = FilesToEntity(r.Files),
		};
	}

	private static List<CodeFile> FilesToEntity(List<CodeFileRequest> files)
	{
		List<CodeFile> codeFiles = [];
		foreach (var file in files)
		{
			codeFiles.Add(new()
			{
				Id = Guid.NewGuid(),
				FileName = file.FileName,
				Content = file.Content,
			});
		}
		return codeFiles;
	}
}