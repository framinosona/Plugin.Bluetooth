
namespace Plugin.Bluetooth.BaseObjects;

/// <summary>
/// Base class for platform-specific permission handling.
/// </summary>
public abstract class BasePermissionHandler : Permissions.BasePlatformPermission
{
    /// <summary>
    /// Requests the permission if needed asynchronously.
    /// </summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    public abstract Task RequestIfNeededAsync();
}
