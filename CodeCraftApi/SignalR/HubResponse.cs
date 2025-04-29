namespace CodeCraftApi.SignalR;

public class HubResponse<T>
{
	public HubResponseType Type { get; set; }
	public T Content { get; set; }
}

public enum HubResponseType
{
	Group,
	User,
	Chat,
	Code
}
