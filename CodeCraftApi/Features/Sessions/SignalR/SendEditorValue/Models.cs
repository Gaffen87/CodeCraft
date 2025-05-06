namespace CodeCraftApi.Features.Sessions.SignalR.SendEditorValue;

internal sealed class SendEditorValuePayload
{
	public Guid UserId { get; set; }
	public string EditorValue { get; set; }
}
