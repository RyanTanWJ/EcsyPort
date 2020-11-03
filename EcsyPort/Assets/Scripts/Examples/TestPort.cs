using System;
using System.Collections.Generic;
using EcsyPort;

namespace TestPort
{
    public class MovementSystem : EcsyPort.System
    {
        public MovementSystem()
        {
            init();
        }

        public override void execute(float deltaTime, Entity entity)
        {
            PositionComponent position = entity.getComponent<PositionComponent>();
            position.x += deltaTime;
        }

        public override void init()
        {
            priority = 2;
            queryKey = new QueryKey();
            queryKey.AddComponent<PositionComponent>();
        }
    }
    public class RotationSystem : EcsyPort.System
    {
        public RotationSystem()
        {
            init();
        }

        public override void execute(float deltaTime, Entity entity)
        {
            RotationComponent rotation = entity.getComponent<RotationComponent>();
            rotation.x += 10 * deltaTime;
            rotation.y += 5 * deltaTime;
        }

        public override void init()
        {
            priority = 1;
            queryKey = new QueryKey();
            queryKey.AddComponent<RotationComponent>();
        }
    }

    public class CubeEntity : EcsyPort.Entity
    {
        public CubeEntity()
        {
            components = new Dictionary<Type, Component>();
            addComponent<PositionComponent>();
            addComponent<RotationComponent>();
        }

        public PositionComponent Position
        {
            get { return (PositionComponent)components[typeof(PositionComponent)]; }
        }

        public RotationComponent Rotation
        {
            get { return (RotationComponent)components[typeof(RotationComponent)]; }
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
