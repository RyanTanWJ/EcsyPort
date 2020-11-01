using System;
using System.Collections;
using System.Collections.Generic;
using EcsyPort;

namespace TestPort
{
    public class MovementSystem : EcsyPort.System
    {
        public MovementSystem(){
            init();
        }

        protected override bool componentCheck(Entity entity)
        {
            return entity.hasComponent<PositionComponent>();
        }

        public override void execute(float deltaTime, Entity entity)
        {
            if (componentCheck(entity))
            {
                PositionComponent position = entity.getComponent<PositionComponent>();
                position.x += deltaTime;
            }
        }

        public override void init()
        {
            // TODO: Create System Query
        }
    }
    public class RotationSystem : EcsyPort.System
    {
        public RotationSystem(){
            init();
        }

        protected override bool componentCheck(Entity entity)
        {
            return entity.hasComponent<RotationComponent>();
        }

        public override void execute(float deltaTime, Entity entity)
        {
            if (componentCheck(entity))
            {
                RotationComponent rotation = entity.getComponent<RotationComponent>();
                rotation.x += 10 * deltaTime;
                rotation.y += 5 * deltaTime;
            }
        }

        public override void init()
        {
            // TODO: Create System Query
        }
    }

    public class CubeEntity : EcsyPort.Entity
    {
        public CubeEntity(){
            components = new Dictionary<Type, Component>();
            addComponent<PositionComponent>();
            addComponent<RotationComponent>();
        }

        public PositionComponent Position{
            get{return (PositionComponent) components[typeof(PositionComponent)];}
        }

        public RotationComponent Rotation{
            get{return (RotationComponent) components[typeof(RotationComponent)];}
        }
    }

    public class PositionComponent : EcsyPort.Component
    {
        public float x;
        public float y;
        public float z;
    }

    public class RotationComponent : EcsyPort.Component
    {
        public float x;
        public float y;
        public float z;
    }
}
