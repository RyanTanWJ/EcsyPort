using System;
using System.Linq;
using System.Collections.Generic;

namespace EcsyPort
{
    public class EntityManager
    {
        private Dictionary<Type, EntityPool> _entities;

        public EntityManager()
        {
            _entities = new Dictionary<Type, EntityPool>();
        }

        public T requestEntity<T>() where T : Entity, new()
        {
            Type entityType = typeof(T);
            if (!_entities.ContainsKey(entityType))
            {
                _entities.Add(entityType, new EntityPool());
            }
            return _entities[entityType].newEntity<T>();
        }

        public void unregisterEntity<T>(T entity) where T : Entity
        {
            Type entityType = typeof(T);
            if (_entities.ContainsKey(entityType))
            {
                _entities[entityType].removeEntity(entity);
            }
        }

        public void unregisterEntity<T>(int entityId) where T : Entity
        {
            Type entityType = typeof(T);
            if (_entities.ContainsKey(entityType))
            {
                _entities[entityType].removeEntity<T>(entityId);
            }
        }

        public T getEntity<T>(int entityId) where T : Entity
        {
            bool inPool = _entities.ContainsKey(typeof(T));
            if (!inPool)
            {
                return null;
            }
            return _entities[typeof(T)].getEntity<T>(entityId);
        }

        public Dictionary<Type, EntityPool> getEntities()
        {
            return _entities;
        }

        public string stats()
        {
            string entitiesStr = "";
            foreach(var type in _entities.Keys){
                entitiesStr += string.Format("{0}: {1}\n", type.ToString(), _entities[type].getActiveEntities().Count);
            }
            
            return string.Format("EntityManager:\nNum Entity Types: {0}\nEntity Type Count: {1}", _entities.Count, entitiesStr);
        }
    }

    public class EntityPool
    {
        private int entityCount;

        private Dictionary<int, Entity> entitiesInUse;
        private Dictionary<int, Entity> reserved;

        public EntityPool()
        {
            entityCount = 0;
            entitiesInUse = new Dictionary<int, Entity>();
            reserved = new Dictionary<int, Entity>();
        }

        public T newEntity<T>() where T : Entity, new()
        {
            bool inReserve = reserved.Count > 0;
            if (inReserve)
            {
                int key = reserved.Keys.First();
                T e = (T)(reserved[key]);
                entitiesInUse.Add(key, e);
                return e;
            }
            var newEntity = new T();
            newEntity.id = entityCount;
            entitiesInUse.Add(newEntity.id, newEntity);
            entityCount++;
            return newEntity;
        }

        public T getEntity<T>(int entityId) where T : Entity
        {
            return (T)(entitiesInUse[entityId]);
        }

        public void removeEntity(Entity entity)
        {
            if (!entitiesInUse.ContainsKey(entity.id))
            {
                return;
            }
            entitiesInUse.Remove(entity.id);
            reserved.Add(entity.id, entity);
        }

        public void removeEntity<T>(int entityId) where T : Entity
        {
            if (!entitiesInUse.ContainsKey(entityId))
            {
                return;
            }
            T entity = (T)(entitiesInUse[entityId]);
            entitiesInUse.Remove(entityId);
            reserved.Add(entity.id, entity);
        }

        public List<Entity> getActiveEntities()
        {
            return entitiesInUse.Values.ToList<Entity>();
        }

        public List<Entity> getAllEntities()
        {
            var inUse = entitiesInUse.Values.ToList<Entity>();
            var res = reserved.Values.ToList<Entity>();
            inUse.AddRange(res);
            return inUse;
        }
    }
}