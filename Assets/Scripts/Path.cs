using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{

    private List<Transform> nodeList = new List<Transform>();

    void OnDrawGizmosSelected()
    {
        if (!Application.isPlaying) return;

        Gizmos.color = Color.magenta;

        Transform[] pathTransforms = GetComponentsInChildren<Transform>();
        nodeList = new List<Transform>();

        for (int x = 0; x < pathTransforms.Length; x++)
        {
            if (pathTransforms[x] != transform)
            {
                nodeList.Add(pathTransforms[x]); 
            }
        }

        for (int y = 0; y < nodeList.Count; y++)
        {
            Vector3 currentNode = nodeList[y].position;
            Vector3 prevNode = Vector3.zero;

            if (y > 0)
            {
                prevNode = nodeList[y - 1].position;
            }
            else if (y == 0 && nodeList.Count > 1)
            {
                prevNode = nodeList[nodeList.Count - 1].position; //Prevents the counter going to -1 and goes back to end node
            }

            Gizmos.DrawLine(prevNode, currentNode);
            Gizmos.DrawWireSphere(currentNode, 0.2f);
        }

    }
}
