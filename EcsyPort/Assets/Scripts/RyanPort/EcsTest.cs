using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EcsTest : MonoBehaviour
{
    public GameObject testCubePrefab;

    public List<EcsTestCube> testCubes;
    public EcsyPort.World world;

    void Start()
    {
        world = new EcsyPort.World();
        world.registerSystem(new TestPort.MovementSystem());
        world.registerSystem(new TestPort.RotationSystem());
    }

    void Update()
    {
        CreateCube();
        world.execute(Time.deltaTime);
    }

    public void CreateCube(){
        GameObject cubeObject = Instantiate(testCubePrefab, RandomVector3, RandomQuaternion);
        TestPort.CubeEntity cubeEntity = world.createEntity<TestPort.CubeEntity>("Test Cube");
        EcsTestCube ecsCubeObject = cubeObject.GetComponent<EcsTestCube>();
        ecsCubeObject.AssignEntity(cubeEntity);
        cubeEntity.name += cubeEntity.id;
        cubeObject.name = cubeEntity.name;
        testCubes.Add(ecsCubeObject);
    }

    public Vector3 RandomVector3
    {
        get
        {
            return new Vector3(
                Random.Range(-10f, 10f),
                Random.Range(-10f, 10f),
                Random.Range(-10f, 10f)
            );
        }
    }

    public Quaternion RandomQuaternion
    {
        get
        {
            return Quaternion.Euler(
                Random.Range(-180f, 180f),
                Random.Range(-180f, 180f),
                Random.Range(-180f, 180f)
            );
        }
    }
}
