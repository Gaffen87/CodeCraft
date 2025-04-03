using System.ComponentModel.DataAnnotations.Schema;

namespace CodeCraftApi.Domain.Entities;

public class Test
{
	public Guid Id { get; set; }

	[Column(TypeName = "jsonb")]
	public string Content { get; set; }

	public DateTimeOffset CreatedAt { get; set; }

	public DateTimeOffset UpdatedAt { get; set; }
}
