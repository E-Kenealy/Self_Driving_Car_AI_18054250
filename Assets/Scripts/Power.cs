using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Power : MonoBehaviour
{
    public Transform path;
    public float maxSteeringAngle = 45f;
    public WheelCollider frontLeftWheel;
    public WheelCollider frontRightWheel;

    private List<Transform> nodeList;
    private int currentNode = 0;


    // Start is called before the first frame update
    private void Start()
    {
        Transform[] pathTransforms = path.GetComponentsInChildren<Transform>();
        nodeList = new List<Transform>();

        for (int x = 0; x < pathTransforms.Length; x++)
        {
            if (pathTransforms[x] != path.transform)
            {
                nodeList.Add(pathTransforms[x]);
            }
        }
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        Steering();
        Drive();
    }
    private void Steering()
    {
        Vector3 relativeVector = transform.InverseTransformPoint(nodeList[currentNode].position);
        float newSteerAngle = (relativeVector.x / relativeVector.magnitude) * maxSteeringAngle;
        frontLeftWheel.steerAngle = newSteerAngle;
        frontRightWheel.steerAngle = newSteerAngle;
    }

    private void Drive()
    {
        frontLeftWheel.motorTorque = 3000f;
        frontRightWheel.motorTorque = 3000f;
    }
}
