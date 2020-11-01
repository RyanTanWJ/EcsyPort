using System;
using System.Collections.Generic;

namespace EcsyPort
{
    public class SystemManager
    {
        private SortedDictionary<int, Queue<Type>> _systemsKeyPress;
        private Dictionary<Type, System> _systems;

        public SystemManager()
        {
            _systemsKeyPress = new SortedDictionary<int, Queue<Type>>();
            _systems = new Dictionary<Type, System>();
        }

        public void registerSystem(System system)
        {
            Type systemType = system.GetType();
            if (_systems.ContainsKey(systemType))
            {
                return;
            }

            if (!_systemsKeyPress.ContainsKey(system.priority))
            {
                _systemsKeyPress.Add(system.priority, new Queue<Type>());
            }

            _systemsKeyPress[system.priority].Enqueue(systemType);
            _systems.Add(systemType, system);
        }

        public void unregisterSystem(System system)
        {
            Type systemType = system.GetType();
            if (!_systems.ContainsKey(systemType))
            {
                return;
            }

            Queue<Type> priorityQueue = _systemsKeyPress[system.priority];
            int iterations = priorityQueue.Count;
            for (int i = 0; i < iterations; i++)
            {
                Type sysType = priorityQueue.Dequeue();
                if (sysType != systemType)
                {
                    priorityQueue.Enqueue(sysType);
                }
            }

            _systems.Remove(systemType);
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
            string systemString = "";
            foreach (var key in OrderedSystemKeys){
                systemString += "\n" + key;
            }
            return string.Format("SystemManager:\nNum System Types: {0}\nSystems:{1}", _systems.Keys.Count, systemString);
        }

        public List<Type> OrderedSystemKeys
        {
            get
            {
                List<Type> orderedKeys = new List<Type>();
                foreach (var keyset in _systemsKeyPress.Values)
                {
                    foreach (var key in keyset)
                    {
                        orderedKeys.Add(key);
                    }
                }
                return orderedKeys;
            }
        }
    }
}