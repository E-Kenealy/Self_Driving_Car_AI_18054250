﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Power : MonoBehaviour
{
    //Node path to be drawn
    public Transform path;
    
    //Colliders of wheels on car
    [Header("Wheel Colliders")]
    public WheelCollider frontLeftWheel;
    public WheelCollider frontRightWheel;
    public WheelCollider backLeftWheel;
    public WheelCollider backRightWheel;

    //Car acceleration and turning
    [Header("Acceleration and Turning")]
    public float maxTurningAngle = 45f;
    public float maxTorque = 200f;
    public float speed;
    public float maxSpeed = 50f;
    public Vector3 cOM;

    //Car braking
    [Header("Braking")]
    public bool brake = false;
    public Texture2D notBraking;
    public Texture2D isBraking;
    public Renderer car;
    public float maxBrakingTorque = 150f;

    [Header("Sensors")]
    public float sensorRange = 3.5f;
    public Vector3 frontSensorOrigin = new Vector3 (0.0f, 0.2f, 0.5f);
    public float frontOffsetSensorOrigin = 0.45f;
    public float frontAngledSensorTheta = 30f;

    private List<Transform> nodeList;
    private int currentNode = 0;


    // Start is called before the first frame update
    private void Start()
    {
        GetComponent<Rigidbody>().centerOfMass = cOM;

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
        SensorArray();
        Turn();
        Drive();
        NodeNear();
        Braking();
    }

    private void SensorArray()
    {
        RaycastHit hit;
        Vector3 sensorOrigin = transform.position + frontSensorOrigin;

        //Front Sensor
        if (Physics.Raycast(sensorOrigin, transform.forward, out hit, sensorRange))
        {
            Debug.DrawLine(sensorOrigin, hit.point); //Will draw a line in the direction of the sensor if the raycast detects a hit
        }
        
        //Front Right Sensor
        sensorOrigin.x += frontOffsetSensorOrigin;
        if (Physics.Raycast(sensorOrigin, transform.forward, out hit, sensorRange))
        {
            Debug.DrawLine(sensorOrigin, hit.point);
        }

        //Front Right Angled Sensor
        if (Physics.Raycast(sensorOrigin, Quaternion.AngleAxis(frontAngledSensorTheta, transform.up) * transform.forward, out hit, sensorRange)) //Creates a sensor to look what is at an angle out in front of the car
        {
            Debug.DrawLine(sensorOrigin, hit.point);
        }

        //Front Left Sensor
        sensorOrigin.x -= frontOffsetSensorOrigin * 2; //Mulitiplied by 2 to get back to origin, then negative x values
        if (Physics.Raycast(sensorOrigin, transform.forward, out hit, sensorRange))
        {
            Debug.DrawLine(sensorOrigin, hit.point);
        }
     
        //Front Left Angled Sensor
        if (Physics.Raycast(sensorOrigin, Quaternion.AngleAxis(-frontAngledSensorTheta, transform.up) * transform.forward, out hit, sensorRange)) //Creates a sensor to look what is at an angle out in front of the car
        {
            Debug.DrawLine(sensorOrigin, hit.point);
        }
        
    }



    private void Turn()
    {
        Vector3 relativeVector = transform.InverseTransformPoint(nodeList[currentNode].position);
        float newSteerAngle = (relativeVector.x / relativeVector.magnitude) * maxTurningAngle;
        frontLeftWheel.steerAngle = newSteerAngle;
        frontRightWheel.steerAngle = newSteerAngle;
    }

    private void Drive()
    {
        //Sets the speed of the vehicle relative to the wheels
        speed = 2 * Mathf.PI * frontLeftWheel.radius * frontLeftWheel.rpm * 60 / 1000;
        //Accelerate
        if (speed < maxSpeed && !brake)
        {
            frontLeftWheel.motorTorque = maxTorque;
            frontRightWheel.motorTorque = maxTorque;
        }
        //No power
        else
        {
            frontLeftWheel.motorTorque = 0;
            frontRightWheel.motorTorque = 0;
        }
    }
    //Checks if close to the node, ready to select the next one
    private void NodeNear()
    {
        if (Vector3.Distance(transform.position, nodeList[currentNode].position) <= 4.0f) //Adjust this to change how near to node
        {
            if (currentNode == nodeList.Count - 1) //Checks for last node
            {
                currentNode = 0; //Changes to node index 0 if reached end
            }
            else currentNode++; //Iterates to next node
        }
    }

    private void Braking()
    {
        if (brake == true) 
        {
            car.material.mainTexture = isBraking; //Change to texture with bright brake lights
            backLeftWheel.brakeTorque = maxBrakingTorque; //Brakes at set value of torque
            backRightWheel.brakeTorque = maxBrakingTorque;
        }
        else 
        {
            car.material.mainTexture = notBraking; //Standard car texture
            backLeftWheel.brakeTorque = 0; //Stops the car  braking
            backRightWheel.brakeTorque = 0;
        }
    }

}
