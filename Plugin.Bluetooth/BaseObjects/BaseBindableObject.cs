using System.Collections.Concurrent;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using Microsoft.Extensions.Logging;

namespace Plugin.Bluetooth.Shared;

/// <summary>
///     Provides a base class for bindable objects with property change notification, value storage.
/// </summary>
public abstract class BaseBindableObject : INotifyPropertyChanged
{
    private readonly ConcurrentDictionary<string, object?> _values = new ConcurrentDictionary<string, object?>();

    /// <summary>
    ///     Returns the name of the object's type.
    /// </summary>
    /// <returns>The name of the object's type.</returns>
    public override string ToString()
    {
        return GetType().Name;
    }

    /// <summary>
    ///     Occurs when a property value changes.
    /// </summary>
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    ///     Determines whether a value has been set for the specified property.
    /// </summary>
    /// <param name="propertyName">The name of the property. If not provided, the caller's member name is used.</param>
    /// <returns>True if a value has been set for the property; otherwise, false.</returns>
    /// <exception cref="ArgumentException">If the property name is null or whitespace.</exception>
    protected bool HasValue([CallerMemberName] string? propertyName = null)
    {
        if (string.IsNullOrWhiteSpace(propertyName))
        {
            throw new ArgumentException("Invalid property name", propertyName);
        }

        return _values.ContainsKey(propertyName);
    }

    /// <summary>
    ///     Gets the value of the specified property, or sets and returns the default value if not present.
    /// </summary>
    /// <typeparam name="T">The type of the property value.</typeparam>
    /// <param name="defaultValue">The default value to use if the property is not set.</param>
    /// <param name="propertyName">The name of the property. If not provided, the caller's member name is used.</param>
    /// <returns>The value of the property, or the default value if not set.</returns>
    /// <exception cref="ArgumentException">If the property name is null or whitespace.</exception>
    protected T GetValue<T>(T defaultValue, [CallerMemberName] string? propertyName = null)
    {
        if (string.IsNullOrWhiteSpace(propertyName))
        {
            throw new ArgumentException("Invalid property name", propertyName);
        }

        if (_values.TryGetValue(propertyName, out var value) && value is T tValue)
        {
            return tValue;
        }

        _values.TryAdd(propertyName, defaultValue);
        return defaultValue;
    }

    /// <summary>
    ///     Gets the value of the specified property without storing a default value if not present.
    /// </summary>
    /// <typeparam name="T">The type of the property value.</typeparam>
    /// <param name="defaultValue">The default value to return if the property is not set.</param>
    /// <param name="propertyName">The name of the property. If not provided, the caller's member name is used.</param>
    /// <returns>The value of the property, or the default value if not set.</returns>
    /// <exception cref="ArgumentException">If the property name is null or whitespace.</exception>
    protected T GetValueOrDefault<T>(T defaultValue, [CallerMemberName] string? propertyName = null)
    {
        if (string.IsNullOrWhiteSpace(propertyName))
        {
            throw new ArgumentException("Invalid property name", propertyName);
        }

        return _values.TryGetValue(propertyName, out var value) && value is T tValue ? tValue : defaultValue;
    }

    /// <summary>
    ///     Sets the value of the specified property and raises the <see cref="PropertyChanged"/> event if the value changes.
    /// </summary>
    /// <typeparam name="T">The type of the property value.</typeparam>
    /// <param name="value">The value to set.</param>
    /// <param name="propertyName">The name of the property. If not provided, the caller's member name is used.</param>
    /// <returns>True if the value was changed and the property was set; otherwise, false.</returns>
    /// <exception cref="ArgumentException">If the property name is null or whitespace.</exception>
    protected bool SetValue<T>(T value, [CallerMemberName] string? propertyName = null)
    {
        if (string.IsNullOrWhiteSpace(propertyName))
        {
            throw new ArgumentException("Invalid property name", propertyName);
        }

        if (_values.TryGetValue(propertyName, out var existingValue) && Equals(existingValue, value))
        {
            return false; // No change
        }

        _values.AddOrUpdate(propertyName, value, (key, oldValue) => value);
        OnPropertyChanged(propertyName);
        return true;
    }

    /// <summary>
    ///     Sets the value of the specified property, raises the <see cref="PropertyChanged"/> event if the value changes, and logs the change.
    /// </summary>
    /// <typeparam name="T">The type of the property value.</typeparam>
    /// <param name="value">The value to set.</param>
    /// <param name="loggingFlag">The logging flag to use for the log entry.</param>
    /// <param name="logLevel">The log level to use for the log entry.</param>
    /// <param name="propertyName">The name of the property. If not provided, the caller's member name is used.</param>
    /// <returns>True if the value was changed and the property was set; otherwise, false.</returns>
    /// <exception cref="ArgumentException">If the property name is null or whitespace.</exception>
    protected bool SetValue<T>(T value, string loggingFlag, LogLevel logLevel, [CallerMemberName] string? propertyName = null)
    {
        if (string.IsNullOrWhiteSpace(propertyName))
        {
            throw new ArgumentException("Invalid property name", propertyName);
        }

        if (_values.TryGetValue(propertyName, out var existingValue) && Equals(existingValue, value))
        {
            return false; // No change
        }

        _values.AddOrUpdate(propertyName, value, (key, oldValue) => value);
        OnPropertyChanged(propertyName);
        return true;
    }

    /// <summary>
    ///     Clears the value of the specified property and raises the <see cref="PropertyChanged"/> event.
    /// </summary>
    /// <param name="propertyName">The name of the property to clear. If not provided, the caller's member name is used.</param>
    /// <returns>True if the property was cleared; otherwise, false.</returns>
    /// <exception cref="ArgumentException">If the property name is null or whitespace.</exception>
    protected bool ClearValue([CallerMemberName] string? propertyName = null)
    {
        if (string.IsNullOrWhiteSpace(propertyName))
        {
            throw new ArgumentException("Invalid property name", propertyName);
        }

        if (_values.TryRemove(propertyName, out _))
        {
            OnPropertyChanged(propertyName);
            return true;
        }

        return false;
    }

    /// <summary>
    ///     Raises the <see cref="PropertyChanged"/> event for the specified property.
    /// </summary>
    /// <param name="propertyName">The name of the property that changed. If not provided, the caller's member name is used.</param>
    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
