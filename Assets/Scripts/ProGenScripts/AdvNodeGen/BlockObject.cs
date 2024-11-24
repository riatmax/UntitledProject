using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockObject : MonoBehaviour
{
    private readonly float nodeOffset = -1f;

    public List<Color> colors = new List<Color>();
    public List<NodeData> nodeData = new List<NodeData>();

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        for (int i = 0; i < GetCenterPoints(nodeOffset).Count; i++)
        {
            Gizmos.DrawSphere(GetCenterPoints(nodeOffset)[i].NodePosition, 0.2f);
        }
    }

    private void Awake()
    {
        nodeData = GetCenterPoints(nodeOffset);
    }

    private List<NodeData> GetCenterPoints(float offset)
    {
        List<NodeData> points = new List<NodeData>();

        //Right
        points.Add(
            new NodeData(
                transform.right * (transform.localScale.x / 2) * offset + transform.position)
            );

        //Left
        points.Add(
            new NodeData(
                -transform.right * (transform.localScale.x / 2) * offset + transform.position)
            );

        //Forwards
        points.Add(
            new NodeData(
                transform.forward * (transform.localScale.z / 2) * offset + transform.position)
            );

        //Backwards
        points.Add(
            new NodeData(
                -transform.forward * (transform.localScale.z / 2) * offset + transform.position)
            );

        return points;
    }
}
