using System;
using System.Collections.Generic;

namespace EcsyPort
{
    public class ComponentManager
    {
        private Dictionary<Type, List<Component>> _components;

        public ComponentManager()
        {
            _components = new Dictionary<Type, List<Component>>();
        }

        public void registerComponent(Component component)
        {
            Type componentType = component.GetType();
            if (!_components.ContainsKey(componentType))
            {
                _components.Add(componentType, new List<Component>());
            }

            if (!_components[componentType].Contains(component))
            {
                _components[componentType].Add(component);
            }
        }

        public bool hasComponent(Component component)
        {
            Type componentType = component.GetType();
            if (!_components.ContainsKey(componentType))
            {
                return false;
            }
            return _components[componentType].Count <= 0;
        }

        public string stats()
        {
            return "";
        }
    }
}