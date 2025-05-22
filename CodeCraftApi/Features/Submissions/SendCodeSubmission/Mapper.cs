using CodeCraftApi.Domain.Entities;

namespace CodeCraftApi.Features.Submissions.SendCodeSubmission;
/// <summary>
/// Mapper class for converting a CodeSubmissionRequest to a CodeSubmission entity.
/// </summary>
internal sealed class Mapper : Mapper<CodeSubmissionRequest, CodeSubmissionResponse, CodeSubmission>
{
	/// <summary>
	/// Converts a CodeSubmissionRequest to a CodeSubmission entity.
	/// </summary>
	/// <param name="r"> The CodeSubmissionRequest to convert.</param>
	/// <returns>> The converted CodeSubmission entity.</returns>
	public override CodeSubmission ToEntity(CodeSubmissionRequest r)
	{
		return new CodeSubmission
		{
			Id = Guid.NewGuid(),
			SubmitDate = DateTimeOffset.UtcNow,
			Files = FilesToEntity(r.Files),
		};
	}
	/// <summary>
	/// Converts a list of CodeFileRequest to a list of CodeFile entities.
	/// </summary>
	/// <param name="files"> The list of CodeFileRequest to convert.</param>
	/// <returns>> The converted list of CodeFile entities.</returns>
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