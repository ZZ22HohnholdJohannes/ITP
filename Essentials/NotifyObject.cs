using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Reserve_iT.Essentials
{
  /// <summary>
  /// Base class for all classes that want to notify about property changes.
  /// </summary>
  public abstract class NotifyObject : INotifyPropertyChanged
  {
    private readonly Dictionary<string, object> propertyValues;

    protected NotifyObject()
    {
      propertyValues = new Dictionary<string, object>();
    }
    /// <summary>
    /// Sets the value of a property and raises the PropertyChanged event if the value has changed.
    /// </summary>
    /// <typeparam name="T">The type of the property.</typeparam>
    /// <param name="value">The new value of the property.</param>
    /// <param name="propertyName">The name of the property. This is optional and will be automatically set by the compiler.</param>
    protected virtual void Set<T>(T value, [CallerMemberName] string propertyName = null)
    {
      if (propertyValues.ContainsKey(propertyName))
      {
        // Only set the value if it is different or null
        if (propertyValues[propertyName] == null || !propertyValues[propertyName].Equals(value))
        {
          propertyValues[propertyName] = value;
        }
      }
      else
      {
        propertyValues.Add(propertyName, value);
      }
      OnPropertyChanged(propertyName);
    }
    /// <summary>
    /// Gets the value of a property.
    /// </summary>
    /// <typeparam name="T">The type of the property.</typeparam>
    /// <param name="propertyName">The name of the property. This is optional and will be automatically set by the compiler.</param>
    /// <returns>The value of the property.</returns>
    protected T Get<T>([CallerMemberName] string propertyName = null)
    {
      if (propertyValues.TryGetValue(propertyName, out object? value))
      {
        return (T)value;
      }
      return default;
    }

    public event PropertyChangedEventHandler PropertyChanged;
    /// <summary>
    /// Raises the PropertyChanged event.
    /// </summary>
    /// <param name="propertyName">The name of the property that changed. This is optional and will be automatically set by the compiler.</param>
    public virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
  }
}
