using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    public BlockObject[] BlockObjects;
    public int roomCount;

    private BlockObject starterObject;
    private readonly Hashtable _occupiedPositions = new();

    private void Start()
    {
        SpawnObjectOnNode();
    }

    private void SpawnObjectOnNode()
    {
        CreateStarterPosition(Vector3.zero);

        int retryCount = 0;
        for (int i = 1; i < roomCount + 1; i++)
        {
            Vector3 offset = GenerateOffsetValue();

            starterObject = Instantiate(BlockObjects[0], Vector3.zero, Quaternion.identity);
            starterObject.transform.position = offset;

            ChangeObjectColor(starterObject);

            _occupiedPositions.Add(offset, starterObject.gameObject);
        }
    }

    private void CreateStarterPosition(Vector3 pos)
    {
        int rndIndex = Random.Range(0, BlockObjects.Length);
        starterObject = Instantiate(BlockObjects[rndIndex], pos, Quaternion.identity);
        _occupiedPositions.Add(starterObject.transform.position, starterObject.gameObject);
    }

    private Vector3 GenerateOffsetValue()
    {
        int rndIndex = Random.Range(0, starterObject.nodeData.Count - 1);
        NodeData selectedNode = starterObject.nodeData[rndIndex];

        if (_occupiedPositions.ContainsKey(starterObject.transform.position + selectedNode.NodePosition * 2f))
        {
            return GenerateOffsetValue();
        }
        return starterObject.transform.position + selectedNode.NodePosition * 2f;
    }

    private void ChangeObjectColor(BlockObject objectToSpawn)
    {
        int rndIndex = Random.Range(0, objectToSpawn.colors.Count);
        objectToSpawn
            .GetComponent<Renderer>()
            .material.color = objectToSpawn.colors[rndIndex];
    }
}
