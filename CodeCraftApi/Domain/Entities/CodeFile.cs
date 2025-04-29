namespace CodeCraftApi.Domain.Entities;

public class CodeFile
{
	public Guid Id { get; set; }
	public string FileName { get; set; }
	public string Content { get; set; }
}