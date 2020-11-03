using System;
using System.Collections.Generic;

namespace EcsyPort
{
    public class QueryKey
    {
        /// <summary>
        /// Components that should be in an entity for the entity to be added to the query
        /// </summary>
        public List<Type> components;
        /// <summary>
        /// Components that should not be in an entity for the entity to be added to the query
        /// </summary>
        public List<Type> notComponents;

        public QueryKey()
        {
            components = new List<Type>();
            notComponents = new List<Type>();
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

            // TODO: write your implementation of Equals() here
            throw new NotImplementedException();
            return base.Equals(obj);
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            // TODO: write your implementation of GetHashCode() here
            throw new NotImplementedException();
            return base.GetHashCode();
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
    }

}