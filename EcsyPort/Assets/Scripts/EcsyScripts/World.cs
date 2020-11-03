using System;
using System.Collections.Generic;
using System.Linq;

namespace EcsyPort
{
    public class World
    {
        /// <summary>
        /// Whether the world tick should execute.
        /// </summary>
        public bool enabled = true;
        private ComponentManager componentManager;
        private EntityManager entityManager;
        private SystemManager systemManager;
        private QueryManager queryManager;
        // private EventDispatcher eventDispatcher;

        #region API Reference
        /// <summary>
        /// Create a new World.
        /// </summary>
        public World()
        {
            componentManager = new ComponentManager();
            entityManager = new EntityManager();
            systemManager = new SystemManager();
        }

        /// <summary>
        /// Create a new entity.
        /// </summary>
        /// <typeparam name="T">The type of entity to create.</typeparam>
        /// <returns>An entity of type, T.</returns>
        public T createEntity<T>(string name) where T : Entity, new()
        {
            T newEnt = entityManager.requestEntity<T>();
            newEnt.name = name;
            return newEnt;
        }

        /// <summary>
        /// Update the systems per frame.
        /// </summary>
        /// <param name="deltaTime"></param>
        public void execute(float deltaTime)
        {
            if (!enabled)
            {
                return;
            }
            Dictionary<Type, System> systems = systemManager.getSystems();
            Dictionary<Type, EntityPool> entities = entityManager.getEntities();
            foreach (Type systemKey in systemManager.OrderedSystemKeys)
            {
                foreach (Type entityKey in entities.Keys)
                {
                    foreach (Entity entity in entities[entityKey].getActiveEntities())
                    {
                        systems[systemKey].execute(deltaTime, entity);
                    }
                }
            }
        }

        /// <summary>
        /// Get a system registered in this world.
        /// </summary>
        /// <param name="systemType">Type of system to get.</param>
        /// <returns></returns>
        public System getSystem(Type systemType)
        {
            return systemManager.getSystem(systemType);
        }

        /// <summary>
        /// Get a list of systems registered in this world.
        /// </summary>
        /// <returns></returns>
        public List<System> getSystems()
        {
            return systemManager.getSystems().Values.ToList<System>();
        }

        /// <summary>
        /// Evluate whether a component has been registered to this world or not.
        /// </summary>
        /// <param name="component">Type of component to to evaluate.</param>
        /// <returns>Whether the type of component has been registered to this world or not.</returns>
        public bool hasRegisteredComponent(Component component)
        {
            return componentManager.hasComponent(component);
        }

        /// <summary>
        /// Resume execution of this world.
        /// </summary>
        public void play()
        {
            enabled = true;
        }

        /// <summary>
        /// Register a component.
        /// </summary>
        /// <param name="component">Type of component to register.</param>
        public void registerComponent(Component component)
        {
            componentManager.registerComponent(component);
        }

        /// <summary>
        /// Register a system.
        /// </summary>
        /// <param name="system">Type of system to register.</param>
        public void registerSystem(System system)
        {
            systemManager.registerSystem(system);
        }

        /// <summary>
        /// Stop execution of this world.
        /// </summary>
        public void stop()
        {
            enabled = false;
        }

        /// <summary>
        /// Unregister a system.
        /// </summary>
        /// <param name="system">Type of system to unregister.</param>
        public void unregisterSystem(System system)
        {
            systemManager.unregisterSystem(system);
        }
        #endregion

        public Entity getEntity<T>(int id) where T : Entity
        {
            return entityManager.getEntity<T>(id);
        }

        public string stats()
        {
            return string.Format("Stats:\n{0}\n{1}\n", entityManager.stats(), systemManager.stats());
        }
    }
}
