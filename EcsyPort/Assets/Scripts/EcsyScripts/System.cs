using System;
using System.Collections.Generic;

namespace EcsyPort
{
    public abstract class System
    {
        /// <summary>
        /// Whether the system will execute during the world tick.
        /// </summary>
        public bool enabled = true;

        /// <summary>
        /// Execution priority (i.e: order) of the system.
        /// </summary>
        public int priority;

        /// <summary>
        /// The results of the queries. Should be used inside of execute.
        /// </summary>
        public SystemQueries queries;

        public static bool isSystem = true;
        protected abstract bool componentCheck(Entity entity);

        /// <summary>
        /// This function is called for each run of world. All of the queries defined on the class are available here.
        /// </summary>
        /// <param name="deltaTime">-</param>
        /// <param name="entity"></param>
        public abstract void execute(float deltaTime, Entity entity);

        /// <summary>
        /// Called when the system is added to the world.
        /// </summary>
        public abstract void init();

        /// <summary>
        /// Resume execution of this system.
        /// </summary>
        public void play()
        {
            enabled = true;
        }

        /// <summary>
        /// Stop execution of this system.
        /// </summary>
        public void stop()
        {
            enabled = false;
        }
    }

    public class SystemQueries
    {
        public string queryName;
        /// <summary>
        /// Components that should be in an entity for the entity to be added to the query
        /// </summary>
        public List<Type> components;
        /// <summary>
        /// Components that should not be in an entity for the entity to be added to the query
        /// </summary>
        public List<Type> notComponents;
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

        public SystemQueries(List<Type> components, List<Type> notComponents, string name = "")
        {
            queryName = name;
            this.components = components;
            this.notComponents = notComponents;
            added = new List<Entity>();
            removed = new List<Entity>();
            changed = new List<Entity>();
            results = new List<Entity>();
        }

        public void AddComponent<C>() where C: Component{
            components.Add(typeof(C));
        }

        public void RemoveComponent<C>() where C:Component{
            components.Remove(typeof(C));
        }

        public void AddNotComponent<C>() where C: Component{
            notComponents.Add(typeof(C));
        }

        public void RemoveNotComponent<C>() where C: Component{
            notComponents.Remove(typeof(C));
        }
        
        // public void OnEntityAdded(Entity entity){
        //     if (entity.hasComponents(components)){

        //     }
        // }
    }
}
