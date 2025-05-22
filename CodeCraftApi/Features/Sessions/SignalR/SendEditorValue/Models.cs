namespace CodeCraftApi.Features.Sessions.SignalR.SendEditorValue;
/// <summary>
/// Payload class for sending editor values to a specific user.
/// </summary>
internal sealed class SendEditorValuePayload
{
	public Guid UserId { get; set; }
	public string EditorValue { get; set; }
}
