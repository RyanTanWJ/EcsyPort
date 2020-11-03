using System;
using System.Collections.Generic;
using System.Linq;

namespace EcsyPort
{
    /// <summary>
    /// An entity in the world.
    /// </summary>
    public class Entity
    {
        /// <summary>
        /// Whether or not the entity is alive or removed.
        /// </summary>
        public bool alive;

        /// <summary>
        /// A unique ID for this entity.
        /// </summary>
        public int id;

        public string name;

        protected Dictionary<Type, Component> components;
        protected Dictionary<Type, Component> componentsToRemove;

        /// <summary>
        /// Add a component to the entity.
        /// </summary>
        /// <typeparam name="C"></typeparam>
        public void addComponent<C>() where C : Component, new()
        {
            components.Add(typeof(C), new C());
        }

        public virtual E clone<E>() where E : Entity, new()
        {
            throw new NotImplementedException();
        }

        public virtual E copy<E>(E source) where E : Entity, new()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get a component on this entity.
        /// </summary>
        /// <returns>The component on this entity if it exists, else null.</returns>
        public C getComponent<C>() where C : Component
        {
            if (components.ContainsKey(typeof(C)))
            {
                return (C)components[typeof(C)];
            }
            return null;
        }

        /// <summary>
        /// Get a list of component types that have been added to this entity.
        /// </summary>
        /// <returns>A list of component types added to this entity.</returns>
        public List<Type> getComponentTypes()
        {
            return components.Keys.ToList<Type>();
        }

        /// <summary>
        /// Get a dictionary containing all the components on this entity, where the keys are the component types.
        /// </summary>
        /// <returns>A dictionary containing all the components on this entity.</returns>
        public Dictionary<Type, Component> getComponents()
        {
            return components;
        }

        public Dictionary<Type, Component> getComponentsToRemove()
        {
            return componentsToRemove;
        }

        public Component getRemovedComponent<C>() where C : Type
        {
            if (componentsToRemove.ContainsKey(typeof(C)))
            {
                return componentsToRemove[typeof(C)];
            }
            return null;
        }

        public bool hasAllComponents(List<Type> componentTypes)
        {
            foreach (Type componentType in componentTypes)
            {
                if (!components.ContainsKey(componentType))
                {
                    return false;
                }
            }
            return true;
        }

        public bool hasAnyComponents(List<Type> componentTypes)
        {
            foreach (Type componentType in componentTypes)
            {
                if (components.ContainsKey(componentType))
                {
                    return true;
                }
            }
            return false;
        }

        public bool hasComponent<C>(bool includeRemoved = false) where C : Component
        {
            return components.ContainsKey(typeof(C));
        }

        public bool hasRemovedComponent<C>() where C : Component
        {
            return componentsToRemove.ContainsKey(typeof(C));
        }

        /// <summary>
        /// Remove this entity from the world.
        /// </summary>
        /// <param name="forceImmediate">Whether this entity should be removed immediately</param>
        public void remove<C>(bool forceImmediate = false) where C : Component, new()
        {
            Type key = typeof(C);
            if (components.ContainsKey(key))
            {
                componentsToRemove.Add(key, components[key]);
            }
        }

        /// <summary>
        /// Remove all components on this entity.
        /// </summary>
        /// <param name="forceImmediate">Whether all components should be removed immediately</param>
        public void removeAllComponents(bool forceImmediate = false)
        {
            foreach (var key in components.Keys)
            {
                if (components.ContainsKey(key))
                {
                    componentsToRemove.Add(key, components[key]);
                }
            }
        }

        public virtual void reset()
        {
            throw new NotImplementedException();
        }
    }
}