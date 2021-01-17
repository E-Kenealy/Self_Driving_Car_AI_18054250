using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeGenerator : MonoBehaviour
{
    public int numOfNodes = 10; //User an change how many nodes are in path for car

    //Limits of the square the car can drive in
    private int maxBounds = 240; 
    private int minBounds = -240;

    //Node coordinates
    private Vector3 Node;
    private float randomX;
    private float setY = 0f;
    private float randomZ;

    public bool canSpawn;

    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }

    private void SpawnNodes()
    {
        if(canSpawn)
        {
           
        }
    }
}
