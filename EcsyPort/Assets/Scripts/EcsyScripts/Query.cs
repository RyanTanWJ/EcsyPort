using System;
using System.Collections.Generic;

namespace EcsyPort
{
    public class QueryKey
    {
        /// <summary>
        /// Components that should be in an entity for the entity to be added to the query
        /// </summary>
        public HashSet<Type> components;
        /// <summary>
        /// Components that should not be in an entity for the entity to be added to the query
        /// </summary>
        public HashSet<Type> notComponents;

        public QueryKey()
        {
            components = new HashSet<Type>();
            notComponents = new HashSet<Type>();
        }

        public void AddComponent<C>() where C : Component{
            components.Add(typeof(C));
        }

        public void RemoveComponent(Type systemType){
            components.Remove(systemType);
        }

        public void AddNotComponent<C>() where C : Component{
            notComponents.Add(typeof(C));
        }

        public void RemoveNotComponent(Type systemType){
            notComponents.Remove(systemType);
        }

        public bool HasComponent(Type componentType){
            return components.Contains(componentType);
        }

        public bool HasNotComponent(Type componentType){
            return notComponents.Contains(componentType);
        }

        // override object.Equals
        public override bool Equals(object obj)
        {
            //
            // See the full list of guidelines at
            //   http://go.microsoft.com/fwlink/?LinkID=85237
            // and also the guidance for operator== at
            //   http://go.microsoft.com/fwlink/?LinkId=85238
            //

            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            QueryKey qkObj = (QueryKey)obj;
            foreach (var component in qkObj.components)
            {
                if (!components.Contains(component))
                {
                    return false;
                }
            }
            foreach (var notComponent in qkObj.notComponents)
            {
                if (!notComponents.Contains(notComponent))
                {
                    return false;
                }
            }
            return true;
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            // TODO: verify correctness
            int componentsHashCode = 0;
            foreach (var component in components)
            {
                componentsHashCode = componentsHashCode ^ component.GetHashCode();

            }
            int notComponentsHashCode = 0;
            foreach (var notComponent in notComponents)
            {
                notComponentsHashCode = notComponentsHashCode ^ notComponent.GetHashCode();
            }

            // factored sum
            int hash = 27;
            hash = (13 * hash) + componentsHashCode;
            hash = (13 * hash) + notComponentsHashCode;
            return hash;
        }
    }

    public class QueryResults
    {
        public string queryName;
        /// <summary>
        /// Entities that have been added to the query.
        /// </summary>
        public List<Entity> added;
        /// <summary>
        /// Entities that are slated to be removed.
        /// </summary>
        public List<Entity> removed;
        /// <summary>
        /// Entities that have been modified.
        /// </summary>
        public List<Entity> changed;
        public List<Entity> results;

        public QueryResults(string name = "")
        {
            queryName = name;
            added = new List<Entity>();
            removed = new List<Entity>();
            changed = new List<Entity>();
            results = new List<Entity>();
        }

        public void AddEntity(Entity entity){
            added.Add(entity);
        }

        public void RemoveEntity(Entity entity){
            if (added.Remove(entity)){
                removed.Add(entity);
            }
        }

        public bool HasEntity(Entity entity){
            return added.Contains(entity);
        }

        public void ClearRemovedEntities(Entity entity){
            removed.Clear();
        }
    }

}