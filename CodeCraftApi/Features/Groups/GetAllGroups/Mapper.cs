namespace CodeCraftApi.Features.Groups.GetAllGroups;

using Domain.Entities;
/// <summary>
/// Mapper class for converting a list of groups to a response object.
/// </summary>
internal sealed class Mapper : ResponseMapper<GetAllGroupsResponse, List<Group>>
{
    /// <summary>
    /// Maps a GetAllGroupsRequest to a list of Group entities.
    /// </summary>
    /// <param name="groups"> The list of Group entities to convert.</param>
    /// <returns>> The converted GetAllGroupsResponse.</returns>
    public override GetAllGroupsResponse FromEntity(List<Group> groups)
    {
        GetAllGroupsResponse res = new()
        {
            Groups = groups
        };
        return res;
    }
}