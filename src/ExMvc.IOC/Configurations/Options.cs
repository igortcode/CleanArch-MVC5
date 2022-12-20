using ExMvc.Domain.Interfaces.Options;
using System;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;

namespace ExMvc.IOC.Configurations
{
    public class Options<T> : IOptions<T>
    {
        readonly String _keyPrefix;
        readonly T _model;
        public Options(string keyPrefix, NameValueCollection collection)
        {
            _keyPrefix = keyPrefix;
            _model = Activator.CreateInstance<T>();

            var propertyNames = collection.AllKeys.Where(k => k.StartsWith(_keyPrefix))
                .ToDictionary(k => k, v => v.Split('.').Last());

            var properties = _model.GetType().GetProperties();

            foreach (var propertyName in propertyNames)
            {
                var property = properties.FirstOrDefault(p => p.Name.Equals(propertyName.Value, StringComparison.InvariantCultureIgnoreCase));
                if (property != null)
                {
                    var value = Convert.ChangeType(collection.Get(propertyName.Key), property.PropertyType);
                    var backingFieldInfo = _model.GetType().GetField($"<{property.Name}>k__BackingField", BindingFlags.Instance | BindingFlags.NonPublic);
                    if (backingFieldInfo != null)
                    {
                        backingFieldInfo.SetValue(_model, value);
                    }
                }
            }
        }

        public T Value
        {
            get
            {
                return _model;
            }
        }
    }
}