using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ResourceType
{
    metalOre,
    stoneOre
}
public class Node : MonoBehaviour
{
    public GameObject resourcePrefab;
    public Transform[] nodes;
    public ResourceType currentResource;

    private void Start()
    {
        spawnNodes();
    }
    private void spawnNodes()
    {
        NodeManager nodeManager = new NodeManager(nodes);

        currentResource = nodeManager.returnRandomResourceNode;

        for (int i = 0; i < nodes.Length; i++)
        {
            Vector3 nodePos = nodes[nodeManager.randomNodeSelection].transform.position;

            if (currentResource == ResourceType.metalOre || currentResource == ResourceType.stoneOre)
            {
                if (!nodeManager.doesResourceExist(nodePos))
                {
                    GameObject nodeSpawned = Instantiate(resourcePrefab,
                    nodePos,
                    Quaternion.identity);

                    nodeManager.nodeDuplicateCheck.Add(nodeSpawned.transform.position, nodePos);

                    nodeSpawned.transform.SetParent(this.transform);
                }
            }
        }
    }
}

public class NodeManager
{
    private Transform[] nodes;
    public Hashtable nodeDuplicateCheck = new Hashtable();

    public NodeManager(Transform[] node)
    {
        this.nodes = node;
    }

    public int randomNodeSelection
    {
        get
        {
            return Random.Range(0, nodes.Length);
        }
    }

    public int randomResourceSelection
    {
        get
        {
            return Random.Range(0, 100) % 50;
        }
    }

    public bool doesResourceExist(Vector3 pos)
    {
        return nodeDuplicateCheck.ContainsKey(pos);
    }

    public ResourceType returnRandomResourceNode
    {
        get
        {
            if (randomResourceSelection >= 20)
            {
                return ResourceType.metalOre;
            }
            else
            {
                return ResourceType.stoneOre;
            }
        }
    }
}
