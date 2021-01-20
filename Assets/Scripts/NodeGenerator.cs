using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeGenerator : MonoBehaviour
{
    [Header("Number of Entities")]
    public int numOfNodes = 10; //User an change how many nodes are in path for car
    public int numOfBarrier = 20;
    public int numOfBarrierDouble = 15;
    public int numOfBarrierQuad = 10;

    //Limits of the square the car can drive in
    private int maxBounds = 200;
    private int minBounds = -200;

    //Node coordinates

    private Vector3 nodeCoords;
    private Quaternion theta = Quaternion.identity;

    //Barrier coordinates
    private Vector3 barrierCoords;
    private Quaternion sigma;

    [Header("Inputs")]
    public Object node;
    public Transform path;

    public Object barrier;
    public Object barrierDouble;
    public Object barrierQuad;
    public Transform barriers;


    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < numOfNodes; i++)
        {
            nodeCoords = new Vector3(Random.Range(minBounds, maxBounds), 0f, Random.Range(minBounds, maxBounds));
            Instantiate(node, nodeCoords, theta, path);
        }

        for (int i = 0; i < numOfBarrier; i++)
        {
            barrierCoords = new Vector3(Random.Range(minBounds, maxBounds), 0f, Random.Range(minBounds, maxBounds));
            sigma = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);
            Instantiate(barrier, barrierCoords, sigma, barriers);
        }

        for (int i = 0; i < numOfBarrierDouble; i++)
        {
            barrierCoords = new Vector3(Random.Range(minBounds, maxBounds), 0f, Random.Range(minBounds, maxBounds));
            sigma = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);
            Instantiate(barrierDouble, barrierCoords, sigma, barriers);
        }

        for (int i = 0; i < numOfBarrierQuad; i++)
        {
            barrierCoords = new Vector3(Random.Range(minBounds, maxBounds), 0f, Random.Range(minBounds, maxBounds));
            sigma = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);
            Instantiate(barrierQuad, barrierCoords, sigma, barriers);
        }
    }

}
