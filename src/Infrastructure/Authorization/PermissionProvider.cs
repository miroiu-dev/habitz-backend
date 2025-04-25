namespace Infrastructure.Authorization;

internal sealed class PermissionProvider
{
    public Task<HashSet<string>> GetForUserIdAsync(int userId)
    {
        HashSet<string> permissionsSet = [];

        return Task.FromResult(permissionsSet);
    }
}
