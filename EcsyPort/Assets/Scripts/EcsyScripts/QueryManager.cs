using System;
using System.Linq;
using System.Collections.Generic;

namespace EcsyPort
{
    public class QueryManager
    {
        public Dictionary<QueryKey, QueryResults> queries;

        public QueryManager()
        {
            queries = new Dictionary<QueryKey, QueryResults>();
        }

        public void addQuery(QueryKey queryKey)
        {
            if (queryKey.components.Count() <= 0)
            {
                return;
            }
            if (queries.ContainsKey(queryKey))
            {
                return;
            }
            queries.Add(queryKey, new QueryResults());
        }

        /// <summary>
        /// Get a query for the specified components
        /// </summary>
        /// <param name="queryKey">QueryKey containing components that the query should have</param>
        /// <returns></returns>
        public QueryResults getQuery(QueryKey queryKey)
        {
            if (queries.ContainsKey(queryKey))
            {
                return queries[queryKey];
            }
            return null;
        }

        public void onEntityAdded(Entity entity)
        {
            foreach (QueryKey queryKey in queries.Keys)
            {
                bool hasComponents = entity.hasAllComponents(queryKey.components.ToList<Type>());
                bool noNotComponents = !entity.hasAnyComponents(queryKey.components.ToList<Type>());
                if (hasComponents && noNotComponents)
                {
                    queries[queryKey].AddEntity(entity);
                }
            }
        }

        public void onEntityRemoved(Entity entity)
        {
            foreach (QueryKey queryKey in queries.Keys)
            {
                queries[queryKey].RemoveEntity(entity);
            }
        }

        /// <summary>
        /// Callback when a component is added to an entity
        /// </summary>
        /// <param name="entity">Entity that just got the new component</param>
        /// <param name="component">Component added to the entity</param>
        public void onEntityComponentAdded(Entity entity, Component component)
        {
            // Check each indexed query to see if we need to add this entity to the list
            foreach (QueryKey queryKey in queries.Keys)
            {
                // Add the entity only if:
                // Component is in the query
                // and Entity has ALL the components of the query
                // and Entity is not already in the query
                bool hasComponents = entity.hasAllComponents(queryKey.components.ToList<Type>());
                bool noNotComponents = !entity.hasAnyComponents(queryKey.components.ToList<Type>());
                if (noNotComponents)
                {
                    if (hasComponents && !queries[queryKey].HasEntity(entity))
                    {
                        queries[queryKey].AddEntity(entity);
                    }
                }
                else if (queries[queryKey].HasEntity(entity))
                {
                    queries[queryKey].RemoveEntity(entity);
                }
            }
        }

        /// <summary>
        /// Callback when a component is removed to an entity
        /// </summary>
        /// <param name="entity">Entity to remove the component from</param>
        /// <param name="component">Component to remove from the entity</param>
        public void onEntityComponentRemoved(Entity entity, Component component)
        {
            // Check each indexed query to see if we need to add this entity to the list
            foreach (QueryKey queryKey in queries.Keys)
            {
                bool hasComponents = entity.hasAllComponents(queryKey.components.ToList<Type>());
                bool noNotComponents = !entity.hasAnyComponents(queryKey.components.ToList<Type>());
                if (noNotComponents)
                {
                    if (hasComponents && !queries[queryKey].HasEntity(entity))
                    {
                        queries[queryKey].AddEntity(entity);
                    }
                }
                else if (queries[queryKey].HasEntity(entity))
                {
                    queries[queryKey].RemoveEntity(entity);
                }
            }
        }

        public string stats()
        {
            throw new NotImplementedException();
        }
    }

}