namespace SignalR.PepR;

[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
public sealed class MethodNameAttribute : Attribute
{
	public string Name { get; }

	public MethodNameAttribute(string name) => Name = name;
}
