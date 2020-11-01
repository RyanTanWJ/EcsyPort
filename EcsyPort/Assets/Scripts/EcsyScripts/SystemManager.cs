using System;
using System.Collections.Generic;

namespace EcsyPort
{
    public class SystemManager
    {
        private Dictionary<Type, System> _systems;

        public SystemManager()
        {
            _systems = new Dictionary<Type, System>();
        }

        public void registerSystem(System system)
        {
            _systems.Add(system.GetType(), system);
        }

        public void unregisterSystem(System system)
        {
            _systems.Remove(system.GetType());
        }

        public void sortSystems()
        {

        }

        public System getSystem(Type systemClass)
        {
            if (!systemClass.IsSubclassOf(typeof(System)))
            {
                return null;
            }
            if (!_systems.ContainsKey(systemClass))
            {
                return null;
            }
            return _systems[systemClass];
        }

        public Dictionary<Type, System> getSystems()
        {
            return _systems;
        }

        public void execute(float deltaTime, Entity entity, Type systemType = null)
        {
            if (systemType == null)
            {
                foreach (Type key in _systems.Keys)
                {
                    _systems[key].execute(deltaTime, entity);
                }
                return;
            }
            _systems[systemType].execute(deltaTime, entity);
        }

        public string stats()
        {
            return "";
        }
    }
}