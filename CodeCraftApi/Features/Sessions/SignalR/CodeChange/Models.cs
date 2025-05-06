namespace CodeCraftApi.Features.Sessions.SignalR.CodeChange;

internal sealed class CodeChangePayload
{
	public string GroupName { get; set; }
	public ICollection<Changes> Changes { get; set; }
}

internal sealed class Changes
{
	public bool? ForceMoveMarkers { get; set; }
	public Range? Range { get; set; }
	//public int RangeLength { get; set; }
	//public int RangeOffset { get; set; }
	public string? Text { get; set; }
}

internal sealed class Range
{
	public int EndColumn { get; set; }
	public int StartColumn { get; set; }
	public int EndLineNumber { get; set; }
	public int StartLineNumber { get; set; }
}
