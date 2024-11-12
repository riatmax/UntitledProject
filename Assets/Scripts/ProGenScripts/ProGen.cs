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
    public GameObject player;

    [Header("Information")]
    public List<Vector3> blockPositions = new List<Vector3>();

    private Vector3 startPos;
    private Hashtable blockContainer = new Hashtable();

    private void Start()
    {
        //startPos = player.transform.position;

        for (int x = -worldSizeX; x < worldSizeX; x++)
        {
            for (int z = -worldSizeZ; z < worldSizeZ; z++)
            {
                Vector3 pos = new Vector3(x * 1 + startPos.x, 
                    generateNoise(x, z, 8f) * noiseHeight, 
                    z * 1 + startPos.z);

                GameObject block = Instantiate(blockGameObj, pos, Quaternion.identity);

                blockContainer.Add(pos, block);
                blockPositions.Add(block.transform.position);
                block.transform.SetParent(this.transform);
            }
        }
        //SpawnObject();
    }
    private void Update()
    {
        if (Mathf.Abs(xPlayerMove) >= 1 || Mathf.Abs(zPlayerMove) >= 1)
        {
            for (int x = -worldSizeX; x < worldSizeX; x++)
            {
                for (int z = -worldSizeZ; z < worldSizeZ; z++)
                {
                    Vector3 pos = new Vector3(x * 1 + xPlayerLocation,
                        generateNoise(x + xPlayerLocation, z + zPlayerLocation, 8f) * noiseHeight,
                        z * 1 + zPlayerLocation);

                    if (!blockContainer.ContainsKey(pos))
                    {
                        GameObject block = Instantiate(blockGameObj, pos, Quaternion.identity);

                        blockContainer.Add(pos, block);
                        blockPositions.Add(block.transform.position);
                        block.transform.SetParent(this.transform);
                    }
                }
            }
        }
    }
    private int xPlayerMove
    {
        get
        {
            return (int)(player.transform.position.x - startPos.x);
        }
    }
    private int zPlayerMove
    {
        get
        {
            return (int)(player.transform.position.z - startPos.z);
        }
    }
    private int xPlayerLocation
    {
        get
        {
            return (int)Mathf.Floor(player.transform.position.x);
        }
    }
    private int zPlayerLocation
    {
        get
        {
            return (int)Mathf.Floor(player.transform.position.z);
        }
    }
   /* private void SpawnObject()
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
    }*/

    private float generateNoise(int x, int z, float detailScale)
    {
        float xNoise = (x + this.transform.position.x) / detailScale;
        float zNoise = (z + this.transform.position.y) / detailScale;

        return Mathf.PerlinNoise(xNoise, zNoise);
    }
}
