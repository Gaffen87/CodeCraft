namespace CodeCraftApi.Features.Sessions.SignalR.CodeChange;
/// <summary>
/// Payload class for sending code changes to all clients in a group except the sender.
/// </summary>
internal sealed class CodeChangePayload
{
	public string GroupName { get; set; }
	public ICollection<Changes> Changes { get; set; }
}
/// <summary>
/// Class representing the changes made to the code.
/// </summary>
internal sealed class Changes
{
	public bool? ForceMoveMarkers { get; set; }
	public Range? Range { get; set; }
	public string? Text { get; set; }
}
/// <summary>
/// Class representing the range of lines and columns in the code.
/// </summary>
internal sealed class Range
{
	public int EndColumn { get; set; }
	public int StartColumn { get; set; }
	public int EndLineNumber { get; set; }
	public int StartLineNumber { get; set; }
}
