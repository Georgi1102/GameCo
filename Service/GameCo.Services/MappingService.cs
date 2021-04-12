using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace GameCo.Services
{
    public class MappingService : IMappingService
    {
        public T MapOject<T>(object obj)
        {
            T instance = (T)Activator.CreateInstance(typeof(T));
            Type instanceType = instance.GetType();

            foreach (var property in obj.GetType().GetProperties())
            {
                PropertyInfo destination = instanceType.GetProperty(property.Name);
                destination?.SetValue(instance, property.GetValue(obj));
            }

            return instance;
        }
    }
}
