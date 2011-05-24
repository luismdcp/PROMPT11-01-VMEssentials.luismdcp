using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Session4.Binding
{
    public class Binder
    {
        private static bool ValidateType(Type type)
        {
            return type.IsPrimitive || type == typeof (String);
        }

        public T BindTo<T>(IEnumerable<KeyValuePair<string, string>> pairs)
        {
            var temp = Activator.CreateInstance(typeof (T));

            foreach (var keyValuePair in pairs)
            {
                var prop = temp.GetType().GetProperty(keyValuePair.Key);

                if (prop != null)
                {
                    if (!ValidateType(prop.PropertyType))
                    {
                        throw new InvalidMemberTypeException(prop);
                    }

                    var propCustomAttributes = prop.GetCustomAttributes(typeof(BindableAttribute), false);

                    if (propCustomAttributes.Length > 0 && ((BindableAttribute) propCustomAttributes[0]).Required)
                    {
                        prop.SetValue(temp, Convert.ChangeType(keyValuePair.Value, prop.PropertyType), null);   
                    }
                }
                else
                {
                    var field = temp.GetType().GetField(keyValuePair.Key);

                    if (field != null)
                    {
                        if (!ValidateType(field.FieldType))
                        {
                            throw new InvalidMemberTypeException(field);
                        }

                        var fieldCustomAttributes = field.GetCustomAttributes(typeof(BindableAttribute), true);

                        if (fieldCustomAttributes.Length > 0 && ((BindableAttribute) fieldCustomAttributes[0]).Required)
                        {
                            field.SetValue(temp, Convert.ChangeType(keyValuePair.Value, field.FieldType));   
                        }
                    }
                }
            }

            return (T) temp;
        }
    }
}