namespace CodeCraftApi.Features.Groups.GetAllGroups;
/// <summary>
/// Response class for the GetAllGroups endpoint.
/// </summary>
internal sealed class GetAllGroupsResponse
{
    
    public List<Domain.Entities.Group> Groups { get; set; }
   
}