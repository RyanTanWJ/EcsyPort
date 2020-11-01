using System;

namespace EcsyPort
{
    public abstract class Component
    {
        public int id;
        public static bool isComponent = true;

        public virtual C clone<C>() where C : Component, new()
        {
            throw new NotImplementedException();
        }

        public virtual C copy<C>(C source) where C : Component, new()
        {
            throw new NotImplementedException();
        }

        public virtual void dispose()
        {
            throw new NotImplementedException();
        }

        public virtual Component reset()
        {
            throw new NotImplementedException();
        }
    }
}