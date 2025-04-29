namespace CodeCraftApi.Features.Groups.GetAllGroups;

using Domain.Entities;

internal sealed class Mapper : ResponseMapper<GetAllGroupsResponse, List<Group>>
{
    public override GetAllGroupsResponse FromEntity(List<Group> groups)
    {
        GetAllGroupsResponse res = new()
        {
            Groups = groups
        };
        return res;
    }
}