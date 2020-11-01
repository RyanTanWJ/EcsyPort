using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EcsTestCube : MonoBehaviour
{
    private TestPort.CubeEntity cubeEntity;

    void Update()
    {
        transform.position = new Vector3(cubeEntity.Position.x, cubeEntity.Position.y, cubeEntity.Position.z);
        transform.rotation = Quaternion.Euler(cubeEntity.Rotation.x, cubeEntity.Rotation.y, cubeEntity.Rotation.z);
    }
    
    public void AssignEntity(TestPort.CubeEntity entity){
        cubeEntity = entity;

        // Assign position
        TestPort.PositionComponent pc = entity.Position;
        Vector3 pos = transform.position;
        pc.x = pos.x;
        pc.y = pos.y;
        pc.z = pos.z;

        // Assign Rotation
        TestPort.RotationComponent rc = entity.Rotation;
        Vector3 rot = transform.rotation.eulerAngles;
        pc.x = pos.x;
        pc.y = pos.y;
        pc.z = pos.z;
    }
}
