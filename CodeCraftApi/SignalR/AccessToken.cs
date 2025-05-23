﻿using System.Text.Json.Serialization;

namespace CodeCraftApi.SignalR;
/// <summary>
/// Represents an access token used for authentication.
/// </summary>
public class AccessToken
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
