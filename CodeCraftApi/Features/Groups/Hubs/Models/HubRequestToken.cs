using System.Text.Json.Serialization;

namespace CodeCraftApi.Features.Groups.Hubs.Models;

internal sealed class HubRequestToken
{
	[JsonPropertyName("sub")]
	public string Sub { get; set; }
	[JsonPropertyName("user_name")]
	public string UserName { get; set; }
	[JsonPropertyName("email")]
	public string Email { get; set; }
	[JsonPropertyName("role")]
	public string Role { get; set; }
}
