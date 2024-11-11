using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProGen : MonoBehaviour
{
    [Header("Generation Bounds")]
    public int worldSizeX;
    public int worldSizeZ;
    public float gridOffset;
    public int noiseHeight;

    [Header("Generation Prefabs")]
    public GameObject blockGameObj;
    public GameObject objToSpawn;

    [Header("Information")]
    public List<Vector3> blockPositions = new List<Vector3>();

    private void Start()
    {
        for (int x = 0; x < worldSizeX; x++)
        {
            for (int z = 0; z < worldSizeZ; z++)
            {
                Vector3 pos = new Vector3(x * gridOffset, 
                    generateNoise(x, z, 8f) * noiseHeight, 
                    z * gridOffset);

                GameObject block = Instantiate(blockGameObj, pos, Quaternion.identity);

                blockPositions.Add(block.transform.position);

                block.transform.SetParent(this.transform);
            }
        }
        SpawnObject();
    }

    private void SpawnObject()
    {
        for (int c = 0; c < 20; c++)
        {
            GameObject toPlaceObj = Instantiate(objToSpawn,
                ObjectSpawnLocation(),
                Quaternion.identity);
        }
    }
    private Vector3 ObjectSpawnLocation()
    {
        int randIndex = Random.Range(0, blockPositions.Count);

        Vector3 newPos = new Vector3(
            blockPositions[randIndex].x,
            blockPositions[randIndex].y + .5f,
            blockPositions[randIndex].z);

        blockPositions.RemoveAt(randIndex);
        return newPos;
    }

    private float generateNoise(int x, int z, float detailScale)
    {
        float xNoise = (x + this.transform.position.x) / detailScale;
        float zNoise = (z + this.transform.position.y) / detailScale;

        return Mathf.PerlinNoise(xNoise, zNoise);
    }
}
