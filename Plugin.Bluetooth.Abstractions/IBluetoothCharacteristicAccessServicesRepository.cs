using System.Reflection;

namespace Plugin.Bluetooth.Abstractions;

/// <summary>
/// Interface representing a repository for managing Bluetooth characteristic access services, providing methods for adding and retrieving service definitions.
/// </summary>
public interface IBluetoothCharacteristicAccessServicesRepository
{
    /// <summary>
    /// Adds all service definitions found in the current assembly.
    /// </summary>
    void AddAllServiceDefinitionsInCurrentAssembly();

    /// <summary>
    /// Asynchronously adds all service definitions found in the current assembly.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task AddAllServiceDefinitionsInCurrentAssemblyAsync();

    /// <summary>
    /// Adds all service definitions found in the specified assembly by name.
    /// </summary>
    /// <param name="assemblyName">The name of the assembly.</param>
    void AddAllServiceDefinitionsInAssembly(string assemblyName);

    /// <summary>
    /// Asynchronously adds all service definitions found in the specified assembly by name.
    /// </summary>
    /// <param name="assemblyName">The name of the assembly.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task AddAllServiceDefinitionsInAssemblyAsync(string assemblyName);

    /// <summary>
    /// Adds all service definitions found in the specified assembly.
    /// </summary>
    /// <param name="assembly">The assembly.</param>
    void AddAllServiceDefinitionsInAssembly(Assembly assembly);

    /// <summary>
    /// Asynchronously adds all service definitions found in the specified assembly.
    /// </summary>
    /// <param name="assembly">The assembly.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task AddAllServiceDefinitionsInAssemblyAsync(Assembly assembly);

    /// <summary>
    /// Adds a known service name to the repository.
    /// </summary>
    /// <param name="serviceId">The unique identifier of the service.</param>
    /// <param name="serviceName">The name of the service.</param>
    void AddKnownServiceName(Guid serviceId, string serviceName);

    /// <summary>
    /// Adds a known characteristic access service to the repository.
    /// </summary>
    /// <param name="characteristicAccessService">The characteristic access service.</param>
    void AddKnownCharacteristicAccessService(IBluetoothCharacteristicAccessService characteristicAccessService);

    /// <summary>
    /// Gets the name of the service associated with the specified service ID.
    /// </summary>
    /// <param name="serviceId">The unique identifier of the service.</param>
    /// <returns>The name of the service.</returns>
    string GetServiceName(Guid serviceId);

    /// <summary>
    /// Gets the characteristic access service associated with the specified characteristic ID.
    /// </summary>
    /// <param name="characteristicId">The unique identifier of the characteristic.</param>
    /// <returns>The characteristic access service.</returns>
    IBluetoothCharacteristicAccessService GetCharacteristicAccessService(Guid characteristicId);

    /// <summary>
    /// Tries to get the characteristic access service associated with the specified characteristic ID.
    /// </summary>
    /// <param name="characteristicId">The unique identifier of the characteristic.</param>
    /// <returns>The characteristic access service, or null if not found.</returns>
    IBluetoothCharacteristicAccessService? TryGetCharacteristicAccessService(Guid characteristicId);
}
