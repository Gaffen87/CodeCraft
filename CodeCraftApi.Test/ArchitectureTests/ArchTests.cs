namespace CodeCraftApi.Test.ArchitectureTests;

using FastEndpoints;
using NetArchTest.Rules;
public class ArchTests
{
	[Fact]
	public void DomainClassShouldBePublic()
	{

		var result = Types.InAssembly(typeof(Program).Assembly)
			.That()
			.ResideInNamespace("CodeCraftApi.Domain")
			.Should()
			.BePublic()
			.GetResult();

		Assert.True(result.IsSuccessful);
	}

	[Fact]
	public void ClassesShouldBeSealed()
	{
		var result = Types.InAssembly(typeof(Program).Assembly)
			.That()
			.AreClasses()
			.And()
			.ResideInNamespace("CodeCraftApi.Features")
			.Should()
			.BeSealed()
			.GetResult();

		Assert.True(result.IsSuccessful);
	}

	[Fact]
	public void EventHandlersShouldBePublic()
	{
		var result = Types.InAssembly(typeof(Program).Assembly)
			.That()
			.ResideInNamespace("CodeCraftApi.SignalR.EventHandlers")
			.Should()
			.BePublic()
			.GetResult();
		Assert.True(result.IsSuccessful);
	}

	[Fact]
	public void ClassesInSignalRShouldBePublic()
	{
		var result = Types.InAssembly(typeof(Program).Assembly)
			.That()
			.ResideInNamespace("CodeCraftApi.SignalR")
			.Should()
			.BePublic()
			.GetResult();

		Assert.True(result.IsSuccessful);
	}

	[Fact]
	public void DomainShouldNotKnowSignalR()
	{
		var result = Types.InAssembly(typeof(Program).Assembly)
			.That()
			.ResideInNamespace("CodeCraftApi.Domain")
			.ShouldNot()
			.HaveDependencyOn("CodeCraftApi.SignalR")
			.GetResult();

		Assert.True(result.IsSuccessful);
	}

	[Fact]
	public void DomainShouldNotKnowFeatures()
	{
		var result = Types.InAssembly(typeof(Program).Assembly)
			.That()
			.ResideInNamespace("CodeCraftApi.Domain")
			.ShouldNot()
			.HaveDependencyOn("CodeCraftApi.Features")
			.GetResult();

		Assert.True(result.IsSuccessful);
	}

	[Fact]
	public void DomainShouldNotKnowDatabase()
	{
		var result = Types.InAssembly(typeof(Program).Assembly)
			.That()
			.ResideInNamespace("CodeCraftApi.Domain")
			.ShouldNot()
			.HaveDependencyOn("CodeCraftApi.Database")
			.GetResult();

		Assert.True(result.IsSuccessful);
	}

	[Fact]
	public void FeatureShouldNotKnowDatabase()
	{
		var result = Types.InAssembly(typeof(Program).Assembly)
			.That()
			.ResideInNamespace("CodeCraftApi.Features")
			.ShouldNot()
			.HaveDependencyOn("CodeCraftApi.Database")
			.GetResult();

		Assert.True(result.IsSuccessful);
	}

	[Fact]
	public void DomainEventsShouldImplementIEvent()
	{
		var result = Types.InAssembly(typeof(Program).Assembly)
			.That()
			.ResideInNamespace("CodeCraftApi.Domain.DomainEvents")
			.Should()
			.ImplementInterface(typeof(IEvent))
			.GetResult();

		Assert.True(result.IsSuccessful);
	}

	[Fact]
	public void EventHandlersShouldImplementIEventHandler()
	{
		var result = Types.InAssembly(typeof(Program).Assembly)
			.That()
			.ResideInNamespace("CodeCraftApi.SignalR.EventHandlers")
			.Should()
			.ImplementInterface(typeof(IEventHandler))
			.GetResult();

		Assert.True(result.IsSuccessful);
	}

	[Fact]
	public void DataBaseLayerShouldNotKnowFeatures()
	{
		var result = Types.InAssembly(typeof(Program).Assembly)
			.That()
			.ResideInNamespace("CodeCraftApi.Database")
			.ShouldNot()
			.HaveDependencyOn("Features")
			.GetResult();

		Assert.True(result.IsSuccessful);
	}
}